using System.Collections.Generic;

namespace ParquetClassLibrary.Crafts
{
    /// <summary>
    /// Facilitates editing of a <see cref="CraftingRecipe"/> from design tools while maintaining a read-only face for use during play.
    /// </summary>
    /// <remarks>
    /// By design, children of <see cref="CraftingRecipe"/> should never themselves use <see cref="ICraftingRecipeEdit"/>.
    /// ICraftingRecipeEdit is for use only by external types that require read/write access to model properties.
    /// </remarks>
    public interface ICraftingRecipeEdit : IModelEdit
    {
        /// <summary>The types and amounts of <see cref="Items.ItemModel"/>s created by following this recipe.</summary>
        public IList<RecipeElement> Products { get; }

        /// <summary>All materials and their quantities needed to follow this recipe once.</summary>
        public IList<RecipeElement> Ingredients { get; }

        /// <summary>The arrangment of panels encompassed by this recipe.</summary>
        public StrikePanelGrid PanelPattern { get; }
    }
}