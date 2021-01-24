using System;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using Parquet.Parquets;
using Parquet.Properties;

namespace Parquet.Maps
{
    /// <summary>
    /// Provides methods that are used by all map models.
    /// </summary>
    public abstract partial class MapModel : Model
    {
        #region Class Defaults
        /// <summary>Dimensions in parquets.  Defined by subtypes.</summary>
        [Ignore]
        public abstract Vector2D DimensionsInParquets { get; }
        #endregion

        #region Characteristics
        #region Whole-Map Characteristics
        /// <summary>Tracks how many times the data structure has been serialized.</summary>
        [Index(4)]
        public int Revision { get; protected set; }
        #endregion

        #region Map Contents
        /// <summary>
        /// Definitions for every <see cref="FloorModel"/>, <see cref="BlockModel"/>, <see cref="FurnishingModel"/>,
        /// and <see cref="CollectibleModel"/> that makes up this part of the game world.
        /// </summary>
        [Index(13)]
        // TODO Should this be IReadOnlyGrid<ParquetPack> instead?
        public abstract ParquetPackGrid ParquetDefinitions { get; }
        #endregion
        #endregion

        #region Initialization
        /// <summary>
        /// Used by subtypes of the <see cref="MapModel"/> class.
        /// </summary>
        /// <param name="inBounds">The bounds within which the derived map type's <see cref="ModelID"/> is defined.</param>
        /// <param name="inID">Unique identifier for the map.  Cannot be null.</param>
        /// <param name="inName">Player-friendly name of the map.  Cannot be null or empty.</param>
        /// <param name="inDescription">Player-friendly description of the map.</param>
        /// <param name="inComment">Comment of, on, or by the map.</param>
        /// <param name="inRevision">How many times this map has been serialized.</param>
        protected MapModel(Range<ModelID> inBounds, ModelID inID, string inName, string inDescription, string inComment, int inRevision = 0)
            // TODO Either remove Revision (YAGNI), or make it part of a custom IEquatable and IComparable implementation.
            : base(inBounds, inID, inName, inDescription, inComment)
            => Revision = inRevision;
        #endregion

        #region Utilities
        /// <summary>The total number of parquets in the entire map.</summary>
        [Ignore]
        protected int ParquetsCount
            => ParquetDefinitions?.Count ?? 0;

        /// <summary>
        /// Determines if the given position corresponds to a point in the region.
        /// </summary>
        /// <param name="inPosition">The position to validate.</param>
        /// <returns><c>true</c>, if the position is valid, <c>false</c> otherwise.</returns>
        public bool IsValidPosition(Vector2D inPosition)
            => ParquetDefinitions.IsValidPosition(inPosition);

        /// <summary>
        /// Provides all parquet definitions within the current map.
        /// </summary>
        /// <returns>The entire map as a subregion.</returns>
        public ParquetPackGrid GetSubregion()
            => GetSubregion(Vector2D.Zero, new Vector2D(DimensionsInParquets.X - 1, DimensionsInParquets.Y - 1));

        /// <summary>
        /// Provides all parquet definitions within the specified rectangular subsection of the current map.
        /// </summary>
        /// <param name="inUpperLeft">The position of the upper-leftmost corner of the subregion.</param>
        /// <param name="inLowerRight">The position of the lower-rightmost corner of the subregion.</param>
        /// <returns>A portion of the map as a subregion.</returns>
        // TODO Should this return an IReadOnlyGrid<ParquetPack>s instead?
        public ParquetPackGrid GetSubregion(Vector2D inUpperLeft, Vector2D inLowerRight)
        {
            if (!ParquetDefinitions.IsValidPosition(inUpperLeft))
            {
                throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorInvalidPosition,
                                                      nameof(inUpperLeft), nameof(ParquetDefinitions)));
            }
            else if (!ParquetDefinitions.IsValidPosition(inLowerRight))
            {
                throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorInvalidPosition,
                                                      nameof(inLowerRight), nameof(ParquetDefinitions)));
            }
            else if (inLowerRight.X < inUpperLeft.X || inLowerRight.Y < inUpperLeft.Y)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorOutOfOrderGTE,
                                                          nameof(inLowerRight), inLowerRight, inUpperLeft));
            }
            else
            {
                var subregion = new ParquetPack[inLowerRight.X - inUpperLeft.X + 1,
                                                 inLowerRight.Y - inUpperLeft.Y + 1];

                for (var x = inUpperLeft.X; x <= inLowerRight.X; x++)
                {
                    for (var y = inUpperLeft.Y; y <= inLowerRight.Y; y++)
                    {
                        subregion[y - inUpperLeft.Y, x - inUpperLeft.X] = ParquetDefinitions[y, x];
                    }
                }

                return new ParquetPackGrid(subregion);
            }
        }

        /// <summary>
        /// Describes the map through general characteristics.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current map.</returns>
        public override string ToString()
            => $"({DimensionsInParquets.X }, {DimensionsInParquets.Y}) contains {ParquetsCount} parquets.";
        #endregion
    }
}
