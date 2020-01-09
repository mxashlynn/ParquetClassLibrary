using ParquetClassLibrary.Items;
using ParquetClassLibrary.Parquets;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Serialization.Shims
{
    /// <summary>
    /// Provides a default public parameterless constructor for a <see cref="Floor"/>-like
    /// class that CSVHelper can instantiate.
    /// 
    /// Provides the ability to generate a <see cref="Floor"/> from this class.
    /// </summary>
    public class FloorShim : ParquetParentShim
    {
        /// <summary>The tool used to dig out or fill in the floor.</summary>
        public ModificationTool ModTool;

        /// <summary>Player-facing name of the parquet, used when it has been dug out.</summary>
        public string TrenchName;

        /// <summary>
        /// Converts a shim into the class it corresponds to.
        /// </summary>
        /// <typeparam name="T">The type to convert this shim to.</typeparam>
        /// <returns>An instance of a child class of <see cref="ParquetParent"/>.</returns>
        public override T ToEntity<T>()
        {
            Precondition.IsOfType<T, Floor>(typeof(T).ToString());

            return (T)(EntityModel)new Floor(ID, Name, Description, Comment, ItemID, AddsToBiome,
                                             AddsToRoom, ModTool, TrenchName);
        }
    }
}
