using ParquetClassLibrary.Beings;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Serialization.Shims
{
    /// <summary>
    /// Provides a default public parameterless constructor for a
    /// <see cref="Critter"/>-like class that CSVHelper can instantiate.
    /// 
    /// Provides the ability to generate a <see cref="Critter"/> from this class.
    /// </summary>
    public class CritterShim : BeingShim
    {
        /// <summary>
        /// Converts a shim into the class it corresponds to.
        /// </summary>
        /// <typeparam name="T">The type to convert this shim to.</typeparam>
        /// <returns>An instance of a child class of <see cref="Enity"/>.</returns>
        public override T ToEntity<T>()
        {
            Precondition.IsOfType<T, Critter>(typeof(T).ToString());

            return (T)(EntityModel)new Critter(ID, Name, Description, Comment, NativeBiome, PrimaryBehavior, Avoids, Seeks);
        }
    }
}
