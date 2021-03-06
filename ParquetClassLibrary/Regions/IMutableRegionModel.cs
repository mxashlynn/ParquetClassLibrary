namespace Parquet.Regions
{
    /// <summary>
    /// Facilitates editing of <see cref="RegionModel"/> characteristics from design tools while maintaining a read-only face during play.
    /// </summary>
    public interface IMutableRegionModel : IMutableModel
    {
        /// <summary>A color to display in any empty areas of the region.</summary>
        string BackgroundColor { get; set; }

        /// <summary>The <see cref="ModelID"/> of the region to the north of this one.</summary>
        ModelID RegionToTheNorthID { get; set; }

        /// <summary>The <see cref="ModelID"/> of the region to the east of this one.</summary>
        ModelID RegionToTheEastID { get; set; }

        /// <summary>The <see cref="ModelID"/> of the region to the south of this one.</summary>
        ModelID RegionToTheSouthID { get; set; }

        /// <summary>The <see cref="ModelID"/> of the region to the west of this one.</summary>
        ModelID RegionToTheWestID { get; set; }

        /// <summary>The <see cref="ModelID"/> of the region above this one.</summary>
        ModelID RegionAboveID { get; set; }

        /// <summary>The <see cref="ModelID"/> of the region below this one.</summary>
        ModelID RegionBelowID { get; set; }

        /// <summary>Instructions on how to procedurally generate this region.</summary>
        MapChunkGrid MapChunks { get; set; }
    }
}
