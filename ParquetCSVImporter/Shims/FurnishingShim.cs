﻿using ParquetClassLibrary.Sandbox.Parquets;

namespace ParquetCSVImporter.ClassMaps
{
    /// <summary>
    /// Provides a default public parameterless constructor for a
    /// <see cref="T:ParquetClassLibrary.Sandbox.Parquets.Furnishing"/>-like
    /// class that CSVHelper can instantiate.
    /// 
    /// Provides the ability to generate a <see cref="T:ParquetClassLibrary.Sandbox.Parquets.Furnishing"/>
    /// from this class.
    /// </summary>
    public class FurnishingShim : ParquetParentShim
    {
        /// <summary>
        /// Converts a shim into the class is corresponds to.
        /// </summary>
        /// <typeparam name="T">The type to convert this shim to.</typeparam>
        /// <returns>An instance of a child class of <see cref="T:ParquetClassLibrary.Sandbox.Parquets.ParquetParent"/>.</returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown when the current shim does not correspond to the specified type.
        /// </exception>
        public override T To<T>()
        {
            T result;

            if (typeof(T) == typeof(Furnishing))
            {
                result = (T)(ParquetParent)new Furnishing(ID, Name, AddsToBiome);
            }
            else
            {
                throw new System.ArgumentException(nameof(T));
            }

            return result;
        }
    }
}