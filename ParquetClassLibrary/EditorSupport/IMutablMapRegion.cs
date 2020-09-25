#if DESIGN
using ParquetClassLibrary.Maps;

namespace ParquetClassLibrary.EditorSupport
{
    /// <summary>
    /// Facilitates editing of <see cref="MapRegionModel"/> characteristics from design tools while maintaining a read-only face for use during play.
    /// </summary>
    // TODO Is this being used correctly?  I don't think MapRegion should have to access this....
    // TODO Do we need to implement an IEdit interface for each subtype, or does that not matter since they are intended to be runtime editable?
    // TODO Maybe what really needs to change is a separation of MapModels from MapStatuses.....
    public interface IMutablMapRegion : IMutableModel
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
    }
}
#endif