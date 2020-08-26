using System.Collections.Generic;

namespace ParquetClassLibrary.Rooms
{
    /// <summary>
    /// Facilitates editing of a <see cref="RoomRecipe"/> from design tools while maintaining a read-only face for use during play.
    /// </summary>
    /// <remarks>
    /// By design, children of <see cref="RoomRecipe"/> should never themselves use <see cref="IRoomRecipeEdit"/>.
    /// IRoomRecipeEdit is for use only by external types that require read/write access to model properties.
    /// </remarks>
    public interface IRoomRecipeEdit : IModelEdit
    {
        /// <summary>Minimum number of open spaces needed for this <see cref="RoomRecipe"/> to register.</summary>
        public int MinimumWalkableSpaces { get; set; }

        /// <summary>A list of <see cref="Parquets.FurnishingModel"/> categories this <see cref="RoomRecipe"/> requires.</summary>
        public IList<RecipeElement> OptionallyRequiredFurnishings { get; }

        /// <summary>An optional list of <see cref="Parquets.FloorModel"/> categories this <see cref="RoomRecipe"/> requires.</summary>
        public IList<RecipeElement> OptionallyRequiredWalkableFloors { get; }

        /// <summary>An optional list of <see cref="Parquets.BlockModel"/> categories this <see cref="RoomRecipe"/> requires as walls.</summary>
        public IList<RecipeElement> OptionallyRequiredPerimeterBlocks { get; }
    }
}