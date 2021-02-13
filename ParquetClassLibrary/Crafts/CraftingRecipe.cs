using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration.Attributes;
using Parquet.Properties;

namespace Parquet.Crafts
{
    /// <summary>
    /// Models the ingredients and process needed to produce a new item.
    /// </summary>
    public partial class CraftingRecipe : Model
    {
        #region Class Defaults
        /// <summary>Used in defining <see cref="NotCraftable"/>.</summary>
        private static IReadOnlyList<RecipeElement> EmptyCraftingElementList { get; } =
            new List<RecipeElement> { RecipeElement.None };

        /// <summary>Represents the lack of a <see cref="CraftingRecipe"/> for uncraftable <see cref="Items.ItemModel"/>s.</summary>
        public static CraftingRecipe NotCraftable { get; } = new CraftingRecipe(ModelID.None, "Not Craftable",
                                                                                "Not Craftable", "", null,
                                                                                EmptyCraftingElementList,
                                                                                EmptyCraftingElementList,
                                                                                StrikePanelGrid.Empty);
        #endregion

        #region Characteristics
        /// <summary>The types and amounts of <see cref="Items.ItemModel"/>s created by following this recipe.</summary>
        [Index(5)]
        public IReadOnlyList<RecipeElement> Products { get; }

        /// <summary>All materials and their quantities needed to follow this recipe once.</summary>
        [Index(6)]
        public IReadOnlyList<RecipeElement> Ingredients { get; }

        /// <summary>The arrangement of panels encompassed by this recipe.</summary>
        [Index(7)]
        public IReadOnlyGrid<StrikePanel> PanelPattern { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CraftingRecipe"/> class.
        /// </summary>
        /// <param name="inID">Unique identifier for the <see cref="CraftingRecipe"/>.  Cannot be null.</param>
        /// <param name="inName">Player-friendly name of the <see cref="CraftingRecipe"/>.  Cannot be null or empty.</param>
        /// <param name="inDescription">Player-friendly description of the <see cref="CraftingRecipe"/>.</param>
        /// <param name="inComment">Comment of, on, or by the <see cref="CraftingRecipe"/>.</param>
        /// <param name="inTags">Any additional functionality this <see cref="CraftingRecipe"/> has.</param>
        /// <param name="inProducts">The types and quantities of <see cref="Items.ItemModel"/>s created by following this recipe once.</param>
        /// <param name="inIngredients">All items needed to follow this <see cref="CraftingRecipe"/> once.</param>
        /// <param name="inPanelPattern">The arrangement of panels encompassed by this <see cref="CraftingRecipe"/>.</param>
        /// <remarks>
        /// <paramref name="inPanelPattern"/> must have dimensions between <c>1</c> and those given by
        /// <see cref="StrikePanelGrid.PanelsPerPatternWidth"/> and <see cref="StrikePanelGrid.PanelsPerPatternHeight"/>.
        /// </remarks>
        public CraftingRecipe(ModelID inID, string inName, string inDescription, string inComment,
                              IEnumerable<ModelTag> inTags = null, IEnumerable<RecipeElement> inProducts = null,
                              IEnumerable<RecipeElement> inIngredients = null, IReadOnlyGrid<StrikePanel> inPanelPattern = null)
            : base(All.CraftingRecipeIDs, inID, inName, inDescription, inComment, inTags)
        {
            var nonNullProducts = inProducts ?? Enumerable.Empty<RecipeElement>();
            var nonNullIngredients = inIngredients ?? Enumerable.Empty<RecipeElement>();
            var nonNullPanelPattern = inPanelPattern ?? StrikePanelGrid.Empty;

#if !DESIGN
            // DESIGN-Time Note: These two checks should not be made from within editing tools.
            Precondition.IsInRange(nonNullProducts.Count(), CraftConfiguration.ProductCount, $"{nameof(inProducts)}.Count");
            Precondition.IsInRange(nonNullIngredients.Count(), CraftConfiguration.IngredientCount, $"{nameof(inIngredients)}.Count");
#endif

            if (nonNullPanelPattern.Rows > StrikePanelGrid.PanelsPerPatternHeight
                || nonNullPanelPattern.Columns > StrikePanelGrid.PanelsPerPatternWidth)
            {
                Logger.Log(LogLevel.Warning, string.Format(CultureInfo.CurrentCulture,
                                                           Resources.ErrorUnsupportedDimension,
                                                           nameof(inPanelPattern)));
            }

            Products = nonNullProducts.ToList();
            Ingredients = nonNullIngredients.ToList();
            PanelPattern = nonNullPanelPattern;
        }
        #endregion
    }
}
