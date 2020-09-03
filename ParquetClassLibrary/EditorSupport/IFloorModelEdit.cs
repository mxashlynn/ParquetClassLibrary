using ParquetClassLibrary.Items;
using ParquetClassLibrary.Parquets;

namespace ParquetClassLibrary.EditorSupport
{
    /// <summary>
    /// Facilitates editing of a <see cref="FloorModel"/> from design tools while maintaining a read-only face for use during play.
    /// </summary>
    /// <remarks>
    /// By design, subtypes of <see cref="FloorModel"/> should never themselves use <see cref="IFloorModelEdit"/>.
    /// IFloorModelEdit is for use only by external types that require read/write access to model properties.
    /// </remarks>
    public interface IFloorModelEdit : IParquetModelEdit
    {
        /// <summary>The tool used to dig out or fill in the floor.</summary>
        public ModificationTool ModTool { get; set; }

        /// <summary>Player-facing name of the parquet, used when it has been dug out.</summary>
        public string TrenchName { get; set; }
    }
}
