using System;
using Newtonsoft.Json;
using ParquetClassLibrary.Crafting;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Items
{
    /// <summary>
    /// Models an item that characters may carry, use, equip, trade, and/or build with.
    /// </summary>
    public class Item : Entity
    {
        /// <summary>The type of item this is.</summary>
        [JsonProperty(PropertyName = "in_subtype")]
        public ItemType Subtype { get; }

        /// <summary>In-game value of the item.  Must be non-negative.</summary>
        [JsonProperty(PropertyName = "in_price")]
        public int Price { get; }

        /// <summary>How relatively rare this item is.</summary>
        [JsonProperty(PropertyName = "in_rarity")]
        public int Rarity { get; }

        /// <summary>How many of the item may share a single inventory slot.</summary>
        [JsonProperty(PropertyName = "in_stackMax")]
        public int StackMax { get; }

        /// <summary>An in-game effect caused by keeping the item in a character's inventory.</summary>
        [JsonProperty(PropertyName = "in_effectWhileHeld")]
        // TODO This is not actually an int; this type needs to be implemented.
        public int EffectWhileHeld { get; }

        /// <summary>An in-game effect caused by using (consuming) the item.</summary>
        [JsonProperty(PropertyName = "in_effectWhenUsed")]
        // TODO This is not actually an int; this type needs to be implemented.
        public int EffectWhenUsed { get; }

        /// <summary>The parquet that corresponds to this item, if any.</summary>
        [JsonProperty(PropertyName = "in_asParquet")]
        public EntityID AsParquet { get; }

        /// <summary>The key item this item acts as, if any.</summary>
        [JsonProperty(PropertyName = "in_asKeyItem")]
        public KeyItem AsKeyItem { get; }

        /// <summary>How this item is crafted.</summary>
        [JsonProperty(PropertyName = "in_recipe")]
        public EntityID Recipe { get; }

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="in_id">Unique identifier for the parquet.  Cannot be null.</param>
        /// <param name="in_subtype">The type of item.</param>
        /// <param name="in_name">Player-friendly name of the item.  Cannot be null or empty.</param>
        /// <param name="in_price">Item cost.</param>
        /// <param name="in_rarity">Item rarity.</param>
        // TODO Implement the Inventory class.
        /// <param name="in_stackMax">How many such items may be stacked together in the <see cref="Inventory"/>.  Must be positive.</param>
        /// <param name="in_effectWhileHeld">Item's passive effect.</param>
        /// <param name="in_effectWhenUsed">Item's active effect.</param>
        /// <param name="in_asParquet">The parquet represented, if any.</param>
        /// <param name="in_asKeyItem">The key item action, if any.</param>
        /// <param name="in_recipeID">The <see cref="EntityID"/> that expresses how to craft this item.</param>
        [JsonConstructor]
        public Item(EntityID in_id, ItemType in_subtype, string in_name, int in_price, int in_rarity, int in_stackMax,
                    int in_effectWhileHeld, int in_effectWhenUsed, EntityID in_asParquet, KeyItem in_asKeyItem,
                    EntityID? in_recipeID = null) : base(All.ItemIDs, in_id, in_name)
        {
            if (!in_asParquet.IsValidForRange(All.ParquetIDs))
            {
                throw new ArgumentOutOfRangeException(nameof(in_id));
            }
            if (in_stackMax < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(in_stackMax));
            }
            // TODO Do we need to bounds-check in_effectWhileHeld?  If so, add a unit test.
            // TODO Do we need to bounds-check in_effectWhenUsed?  If so, add a unit test.
            var nonNullCraftingRecipeID = in_recipeID ?? CraftingRecipe.NotCraftable.ID;
            var craftingRecipe = All.CraftingRecipes.Get<CraftingRecipe>(nonNullCraftingRecipeID);
            if (craftingRecipe.ItemProduced != in_id && craftingRecipe.ItemProduced != EntityID.None)
            {
                throw new ArgumentException($"The values of {in_id} and {nameof(craftingRecipe.ItemProduced)} must match.");
            }

            Subtype = in_subtype;
            Price = in_price;
            Rarity = in_rarity;
            StackMax = in_stackMax;
            EffectWhileHeld = in_effectWhileHeld;
            EffectWhenUsed = in_effectWhenUsed;
            AsParquet = in_asParquet;
            AsKeyItem = in_asKeyItem;
            Recipe = nonNullCraftingRecipeID;
        }
        #endregion
    }
}
