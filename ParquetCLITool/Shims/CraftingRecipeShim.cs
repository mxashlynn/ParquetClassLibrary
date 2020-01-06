using System.Collections.Generic;
using ParquetClassLibrary;
using ParquetClassLibrary.Crafting;
using ParquetClassLibrary.Utilities;

namespace ParquetCLITool.Shims
{
    /// <summary>
    /// Provides a default public parameterless constructor for a
    /// <see cref="CraftingRecipe"/>-like class that CSVHelper can instantiate.
    /// 
    /// Provides the ability to generate a <see cref="CraftingRecipe"/> from this class.
    /// </summary>
    public class CraftingRecipeShim : EntityShim
    {
        /// <summary>The types and amounts of <see cref="Items.Item"/>s created by following this recipe.</summary>
        public IReadOnlyList<RecipeElement> Products { get; }

        /// <summary>All materials and their quantities needed to follow this recipe once.</summary>
        public IReadOnlyList<RecipeElement> Ingredients { get; }

        /// <summary>The arrangment of panels encompassed by this recipe.</summary>
        // TODO How do we handle this?
        public StrikePanelGrid PanelPattern { get; }

        /// <summary>
        /// Converts a shim into the class it corresponds to.
        /// </summary>
        /// <typeparam name="TargetType">The type to convert this shim to.</typeparam>
        /// <returns>An instance of a child class of <see cref="Enity"/>.</returns>
        public override TargetType To<TargetType>()
        {
            Precondition.IsOfType<TargetType, CraftingRecipe>(typeof(TargetType).ToString());

            return (TargetType)(Entity)new CraftingRecipe(ID, Name, Description, Comment, Products, Ingredients, PanelPattern);
        }
    }
}
