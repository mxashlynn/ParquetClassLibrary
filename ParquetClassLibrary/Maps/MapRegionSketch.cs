using System;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using ParquetClassLibrary.Parquets;
using ParquetClassLibrary.Properties;

namespace ParquetClassLibrary.Maps
{
    /// <summary>
    /// A pattern and metadata to generate a <see cref="MapRegion"/>.
    /// </summary>
    /// <remarks>
    /// Before play begins, <see cref="MapRegion"/>s are stored as <see cref="MapRegionSketch"/>es, for example in an editor tool.
    /// Once loaded in-game they are composited, which often includes procedurally generating contained <see cref="MapChunk"/>s.
    /// </remarks>
    public sealed class MapRegionSketch : MapModel, IMapRegionEdit
    {
        #region Class Defaults
        /// <summary>Used to indicate a blank sketch.</summary>
        public static readonly MapRegionSketch Empty = new MapRegionSketch(ModelID.None, "Empty Ungenerated Region");

        /// <summary>The region's dimensions in parquets.</summary>
        public override Vector2D DimensionsInParquets { get; } = new Vector2D(MapRegion.ParquetsPerRegionDimension,
                                                                              MapRegion.ParquetsPerRegionDimension);

        /// <summary>The set of values that are allowed for <see cref="MapRegionSketch"/> <see cref="ModelID"/>s.</summary>
        public static Range<ModelID> Bounds
            => All.MapRegionIDs;

        /// <summary>Default name for new regions.</summary>
        internal const string DefaultTitle = "New Region";

        /// <summary>Default color for new regions.</summary>
        internal const string DefaultColor = "#FFFFFFFF";
        #endregion

        #region Characteristics
        #region Whole-Map Characteristics
        /// <summary>What the region is called in-game.</summary>
        [Ignore]
        string IMapRegionEdit.Name
        {
            get => Name;
            set
            {
                IModelEdit editableThis = this;
                editableThis.Name = value;
            }
        }

        /// <summary>A color to display in any empty areas of the region.</summary>
        [Index(5)]
        public string BackgroundColor { get; private set; }

        /// <summary>A color to display in any empty areas of the region.</summary>
        [Ignore]
        string IMapRegionEdit.BackgroundColor { get => BackgroundColor; set => BackgroundColor = value; }
        #endregion

        #region Map Contents
        #region Exit IDs
        /// <summary>The <see cref="ModelID"/> of the region to the north of this one.</summary>
        [Index(6)]
        public ModelID RegionToTheNorth { get; private set; }

        /// <summary>The <see cref="ModelID"/> of the region to the north of this one.</summary>
        [Ignore]
        ModelID IMapRegionEdit.RegionToTheNorth { get => RegionToTheNorth; set => RegionToTheNorth = value; }

        /// <summary>The <see cref="ModelID"/> of the region to the east of this one.</summary>
        [Index(7)]
        public ModelID RegionToTheEast { get; private set; }

        /// <summary>The <see cref="ModelID"/> of the region to the east of this one.</summary>
        [Ignore]
        ModelID IMapRegionEdit.RegionToTheEast { get => RegionToTheEast; set => RegionToTheEast = value; }

        /// <summary>The <see cref="ModelID"/> of the region to the south of this one.</summary>
        [Index(8)]
        public ModelID RegionToTheSouth { get; private set; }

        /// <summary>The <see cref="ModelID"/> of the region to the south of this one.</summary>
        [Ignore]
        ModelID IMapRegionEdit.RegionToTheSouth { get => RegionToTheSouth; set => RegionToTheSouth = value; }

        /// <summary>The <see cref="ModelID"/> of the region to the west of this one.</summary>
        [Index(9)]
        public ModelID RegionToTheWest { get; private set; }

        /// <summary>The <see cref="ModelID"/> of the region to the west of this one.</summary>
        [Ignore]
        ModelID IMapRegionEdit.RegionToTheWest { get => RegionToTheWest; set => RegionToTheWest = value; }

        /// <summary>The <see cref="ModelID"/> of the region above this one.</summary>
        [Index(10)]
        public ModelID RegionAbove { get; private set; }

        /// <summary>The <see cref="ModelID"/> of the region above this one.</summary>
        [Ignore]
        ModelID IMapRegionEdit.RegionAbove { get => RegionAbove; set => RegionAbove = value; }

        /// <summary>The <see cref="ModelID"/> of the <see cref="MapRegion"/> below this one.</summary>
        [Index(11)]
        public ModelID RegionBelow { get; private set; }

        /// <summary>The <see cref="ModelID"/> of the <see cref="MapRegion"/> below this one.</summary>
        [Ignore]
        ModelID IMapRegionEdit.RegionBelow { get => RegionBelow; set => RegionBelow = value; }
        #endregion

        /// <summary>Generate a <see cref="MapRegion"/> before accessing parquet statuses.</summary>
        [Ignore]
        // Index(12)
        public override ParquetStatusGrid ParquetStatuses
            => throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorUngenerated,
                                                                 nameof(ParquetStatuses), nameof(MapRegionSketch)));

        /// <summary>Generate a <see cref="MapRegion"/> before accessing parquets.</summary>
        [Ignore]
        // Index(13)
        public override ParquetStackGrid ParquetDefinitions
            => throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorUngenerated,
                                                                 nameof(ParquetDefinitions), nameof(MapRegionSketch)));

        /// <summary><see cref="ChunkType"/>s that can generate parquets to compose a <see cref="MapRegion"/>.</summary>
        [Index(14)]
        public ChunkTypeGrid Chunks { get; }
        #endregion
        #endregion

        #region Initialization
        /// <summary>
        /// Constructs a new instance of the <see cref="MapRegionSketch"/> class.
        /// </summary>
        /// <param name="inID">Unique identifier for the map.  Cannot be null.</param>
        /// <param name="inName">The player-facing name of the new region.</param>
        /// <param name="inDescription">Player-friendly description of the map.</param>
        /// <param name="inComment">Comment of, on, or by the map.</param>
        /// <param name="inRevision">An option revision count.</param>
        /// <param name="inBackgroundColor">A color to show in the new region when no parquet is present.</param>
        /// <param name="inChunks">The pattern from which a <see cref="MapRegion"/> may be generated.</param>
        public MapRegionSketch(ModelID inID, string inName = null, string inDescription = null, string inComment = null,
                               int inRevision = 0, string inBackgroundColor = DefaultColor, ChunkTypeGrid inChunks = null)
            : base(Bounds, inID, string.IsNullOrEmpty(inName) ? DefaultTitle : inName, inDescription, inComment, inRevision)
        {
            BackgroundColor = inBackgroundColor;
            Chunks = inChunks ?? new ChunkTypeGrid();
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Describes the <see cref="MapRegionSketch"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="MapRegionSketch"/>.</returns>
        public override string ToString()
            => $"Sketch {Name} ({Chunks.Columns}, {Chunks.Rows}) contains {Chunks.Columns * Chunks.Rows} chunks.";
        #endregion
    }
}
