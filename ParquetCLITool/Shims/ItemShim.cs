using System.Collections.Generic;
using ParquetClassLibrary;
using ParquetClassLibrary.Items;
using ParquetClassLibrary.Utilities;

namespace ParquetCLITool.Shims
{
    /// <summary>
    /// Provides a default public parameterless constructor for a
    /// <see cref="Item"/>-like class that CSVHelper can instantiate.
    /// 
    /// Provides the ability to generate a <see cref="Item"/> from this class.
    /// </summary>
    public class ItemShim : EntityShim
    {
        /// <summary>The type of item this is.</summary>
        public ItemType Subtype { get; }

        /// <summary>In-game value of the item.  Must be non-negative.</summary>
        public int Price { get; }

        /// <summary>How relatively rare this item is.</summary>
        public int Rarity { get; }

        /// <summary>How many of the item may share a single inventory slot.</summary>
        public int StackMax { get; }

        /// <summary>An in-game effect caused by keeping the item in a character's inventory.</summary>
        public int EffectWhileHeld { get; }

        /// <summary>An in-game effect caused by using (consuming) the item.</summary>
        public int EffectWhenUsed { get; }

        /// <summary>The parquet that corresponds to this item, if any.</summary>
        public EntityID AsParquet { get; }

        /// <summary>Any additional functionality this item has, e.g. contributing to a <see cref="Biomes.Biome"/>.</summary>
        public List<EntityTag> ItemTags { get; }

        /// <summary>How this item is crafted.</summary>
        public EntityID Recipe { get; }

        /// <summary>
        /// Converts a shim into the class it corresponds to.
        /// </summary>
        /// <typeparam name="TargetType">The type to convert this shim to.</typeparam>
        /// <returns>An instance of a child class of <see cref="Enity"/>.</returns>
        public override TargetType To<TargetType>()
        {
            Precondition.IsOfType<TargetType, Item>(typeof(TargetType).ToString());

            return (TargetType)(Entity)new Item(ID, Name, Description, Comment, Subtype, Price, Rarity, StackMax,
                                                EffectWhileHeld, EffectWhenUsed, AsParquet, ItemTags, Recipe);
        }
    }
}
