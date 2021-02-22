using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CsvHelper.Configuration.Attributes;
using Parquet.Biomes;

namespace Parquet.Parquets
{
    /// <summary>
    /// Configurations for large sandbox parquet items, such as furniture or plants.
    /// </summary>
    [SuppressMessage("Design", "CA1033:Interface methods should be callable by subtypes",
                     Justification = "By design, subtypes of Model should never themselves use IMutableModel or derived interfaces to access their own members.  The IMutableModel family of interfaces is for external types that require read/write access.")]
    public class FurnishingModel : ParquetModel, IMutableFurnishingModel
    {
        #region Class Defaults
        /// <summary>The set of values that are allowed for Furnishing IDs.</summary>
        public static Range<ModelID> Bounds
            => All.FurnishingIDs;
        #endregion

        #region Characteristics
        /// <summary>Indicates whether this <see cref="FurnishingModel"/> may be walked on.</summary>
        [Index(8)]
        public bool IsWalkable { get; private set; }

        /// <summary>Indicates if and how this <see cref="FurnishingModel"/> serves as an entry to a <see cref="Rooms.Room"/> or <see cref="Regions.MapRegionModel"/>.</summary>
        [Index(9)]
        public EntryType Entry { get; private set; }

        /// <summary>Indicates whether this <see cref="FurnishingModel"/> serves as part of a perimeter of a <see cref="Rooms.Room"/>.</summary>
        [Index(10)]
        public bool IsEnclosing { get; private set; }

        /// <summary>Whether or not the <see cref="FurnishingModel"/> is flammable.</summary>
        [Index(11)]
        public bool IsFlammable { get; private set; }

        /// <summary>The <see cref="FurnishingModel"/> to swap with this Furnishing on an open/close action.</summary>
        [Index(12)]
        public ModelID SwapID { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="FurnishingModel"/> class.
        /// </summary>
        /// <param name="inID">Unique identifier for the <see cref="FurnishingModel"/>.  Cannot be null.</param>
        /// <param name="inName">Player-friendly name of the <see cref="FurnishingModel"/>.  Cannot be null or empty.</param>
        /// <param name="inDescription">Player-friendly description of the <see cref="FurnishingModel"/>.</param>
        /// <param name="inComment">Comment of, on, or by the <see cref="FurnishingModel"/>.</param>
        /// <param name="inTags">Any additional information about the <see cref="FurnishingModel"/>.</param>
        /// <param name="inItemID">The <see cref="ModelID"/> that represents this <see cref="FurnishingModel"/> in the <see cref="Items.Inventory"/>.</param>
        /// <param name="inAddsToBiome">Indicates which, if any, <see cref="BiomeRecipe"/> this parquet helps to generate.</param>
        /// <param name="inAddsToRoom">Describes which, if any, <see cref="Rooms.RoomRecipe"/>(s) this parquet helps form.</param>
        /// <param name="inIsWalkable">If <c>true</c> this <see cref="FurnishingModel"/> may be walked on.</param>
        /// <param name="inEntry">If <c>true</c> this <see cref="FurnishingModel"/> serves as an entry to a <see cref="Rooms.Room"/>.</param>
        /// <param name="inIsEnclosing">If <c>true</c> this <see cref="FurnishingModel"/> serves as part of a perimeter of a <see cref="Rooms.Room"/>.</param>
        /// <param name="inIsFlammable">If <c>true</c> this <see cref="FurnishingModel"/> may catch fire.</param>
        /// <param name="inSwapID">A <see cref="FurnishingModel"/> to swap with this furnishing on open/close actions.</param>
        public FurnishingModel(ModelID inID, string inName, string inDescription, string inComment,
                               IEnumerable<ModelTag> inTags = null, ModelID? inItemID = null,
                               IEnumerable<ModelTag> inAddsToBiome = null, IEnumerable<ModelTag> inAddsToRoom = null,
                               bool inIsWalkable = false, EntryType inEntry = EntryType.None, bool inIsEnclosing = false,
                               bool inIsFlammable = false, ModelID? inSwapID = null)
            : base(Bounds, inID, inName, inDescription, inComment, inTags, inItemID, inAddsToBiome, inAddsToRoom)
        {
            var nonNullSwapID = inSwapID ?? ModelID.None;

            Precondition.IsInRange(nonNullSwapID, Bounds, nameof(inSwapID));

            IsWalkable = inIsWalkable;
            Entry = inEntry;
            IsEnclosing = inIsEnclosing;
            IsFlammable = inIsFlammable;
            SwapID = nonNullSwapID;
        }
        #endregion

        #region IMutableFurnishingModel Implementation
        /// <summary>Indicates whether this <see cref="FurnishingModel"/> may be walked on.</summary>
        /// <remarks>
        /// By design, subtypes of <see cref="Model"/> should never themselves use <see cref="IMutableModel"/>.
        /// IModelEdit is for external types that require read/write access.
        /// </remarks>
        [Ignore]
        bool IMutableFurnishingModel.IsWalkable
        {
            get => IsWalkable;
            set => IsWalkable = LibraryState.IsPlayMode
                ? Logger.DefaultWithImmutableInPlayLog(nameof(IsWalkable), IsWalkable)
                : value;
        }

        /// <summary>Indicates if and how this <see cref="FurnishingModel"/> serves as an entry to a <see cref="Rooms.Room"/> or <see cref="Regions.MapRegionModel"/>.</summary>
        /// <remarks>
        /// By design, subtypes of <see cref="Model"/> should never themselves use <see cref="IMutableModel"/>.
        /// IModelEdit is for external types that require read/write access.
        /// </remarks>
        [Ignore]
        EntryType IMutableFurnishingModel.Entry
        {
            get => Entry;
            set => Entry = LibraryState.IsPlayMode
                ? Logger.DefaultWithImmutableInPlayLog(nameof(Entry), Entry)
                : value;
        }

        /// <summary>Indicates whether this <see cref="FurnishingModel"/> serves as part of a perimeter of a <see cref="Rooms.Room"/>.</summary>
        /// <remarks>
        /// By design, subtypes of <see cref="Model"/> should never themselves use <see cref="IMutableModel"/>.
        /// IModelEdit is for external types that require read/write access.
        /// </remarks>
        [Ignore]
        bool IMutableFurnishingModel.IsEnclosing
        {
            get => IsEnclosing;
            set => IsEnclosing = LibraryState.IsPlayMode
                ? Logger.DefaultWithImmutableInPlayLog(nameof(IsEnclosing), IsEnclosing)
                : value;
        }

        /// <summary>Whether or not the <see cref="FurnishingModel"/> is flammable.</summary>
        /// <remarks>
        /// By design, subtypes of <see cref="Model"/> should never themselves use <see cref="IMutableModel"/>.
        /// IModelEdit is for external types that require read/write access.
        /// </remarks>
        [Ignore]
        bool IMutableFurnishingModel.IsFlammable
        {
            get => IsFlammable;
            set => IsFlammable = LibraryState.IsPlayMode
                ? Logger.DefaultWithImmutableInPlayLog(nameof(IsFlammable), IsFlammable)
                : value;
        }

        /// <summary>The <see cref="FurnishingModel"/> to swap with this Furnishing on an open/close action.</summary>
        /// <remarks>
        /// By design, subtypes of <see cref="Model"/> should never themselves use <see cref="IMutableModel"/>.
        /// IModelEdit is for external types that require read/write access.
        /// </remarks>
        [Ignore]
        ModelID IMutableFurnishingModel.SwapID
        {
            get => SwapID;
            set => SwapID = LibraryState.IsPlayMode
                ? Logger.DefaultWithImmutableInPlayLog(nameof(SwapID), SwapID)
                : value;
        }
        #endregion
    }
}
