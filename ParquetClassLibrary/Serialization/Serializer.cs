using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using ParquetClassLibrary.Beings;
using ParquetClassLibrary.Biomes;
using ParquetClassLibrary.Crafts;
using ParquetClassLibrary.Interactions;
using ParquetClassLibrary.Items;
using ParquetClassLibrary.Maps;
using ParquetClassLibrary.Parquets;
using ParquetClassLibrary.Rooms;
using ParquetClassLibrary.Serialization.ClassMaps;
using ParquetClassLibrary.Serialization.Converters;
using ParquetClassLibrary.Serialization.Shims;

namespace ParquetClassLibrary.Serialization
{
    /// <summary>
    /// Provides access to the filesystem in a fashion tailed for the Parquet library.
    /// </summary>
    public static class Serializer
    {
        /// <summary>Used to separate fields.</summary>
        public const string PrimaryDelimiter = ",";

        /// <summary>Used to separate objects within collections.</summary>
        public const string SecondaryDelimiter = ";";

        /// <summary>Instructions for handling integer type conversion when reading in identifiers.</summary>
        private static TypeConverterOptions IdentifierOptions { get; } = new TypeConverterOptions
        {
            NumberStyle = NumberStyles.AllowLeadingSign &
                          NumberStyles.Integer
        };

        /// <summary>Mappings for all serializable classes.</summary>
        private static Dictionary<Type, ClassMap> ClassMapper { get; } = new Dictionary<Type, ClassMap>
        {
            { typeof(CritterModel), CritterModel.GetClassMap() },
            { typeof(NPCModel), NPCModel.GetClassMap() },
            { typeof(PlayerCharacterModel), PlayerCharacterModel.GetClassMap() },
            { typeof(BiomeModel), BiomeModel.GetClassMap() },
            { typeof(CraftingRecipe), CraftingRecipe.GetClassMap() },
            { typeof(DialogueModel), DialogueModel.GetClassMap() },
            { typeof(QuestModel), QuestModel.GetClassMap() },
            // TODO Implement this once Map serialization is decided.
            // { typeof(MapChunk), MapChunk.GetClassMap() },
            // { typeof(MapRegion), MapRegion.GetClassMap() },
            { typeof(FloorModel), FloorModel.GetClassMap() },
            { typeof(BlockModel), BlockModel.GetClassMap() },
            { typeof(FurnishingModel), FurnishingModel.GetClassMap() },
            { typeof(CollectibleModel), CollectibleModel.GetClassMap() },
            { typeof(RoomRecipe), RoomRecipe.GetClassMap() },
            { typeof(ItemModel), ItemModel.GetClassMap() },
        };

        /// <summary>Mappings for all serialization shims.</summary>
        private static Dictionary<Type, Type> ShimMapper { get; } = new Dictionary<Type, Type>
        {
            { typeof(CritterModel), CritterModel.GetShimType() },
            { typeof(NPCModel), NPCModel.GetShimType() },
            { typeof(PlayerCharacterModel), PlayerCharacterModel.GetShimType() },
            { typeof(BiomeModel), BiomeModel.GetShimType() },
            { typeof(CraftingRecipe), CraftingRecipe.GetShimType() },
            { typeof(DialogueModel), DialogueModel.GetShimType() },
            { typeof(QuestModel), QuestModel.GetShimType() },
            // TODO Implement this once Map serialization is decided.
            // { typeof(MapChunk), MapChunk.GetShimType() },
            // { typeof(MapRegion), MapRegion.GetShimType() },
            { typeof(FloorModel), FloorModel.GetShimType() },
            { typeof(BlockModel), BlockModel.GetShimType() },
            { typeof(FurnishingModel), FurnishingModel.GetShimType() },
            { typeof(CollectibleModel), CollectibleModel.GetShimType() },
            { typeof(RoomRecipe), RoomRecipe.GetShimType() },
            { typeof(ItemModel), ItemModel.GetShimType() },
        };

        /// <summary>The location of the designer CSV files.</summary>
        public static string SearchPath { get; set; }

        /// <summary>
        /// Reads all records of the given type from the appropriate file.
        /// </summary>
        /// <typeparam name="TRecord">The type to deserialize.</typeparam>
        /// <returns>The records read.</returns>
        public static IEnumerable<TRecord> GetRecordsForType<TRecord>()
            where TRecord : EntityModel
        {
            var records = new List<TRecord>();
            var filenameAndPath = Path.Combine(SearchPath, $"Designer/{typeof(TRecord).Name}s.csv");
            using (var reader = new StreamReader(filenameAndPath))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.TypeConverterCache.AddConverter(typeof(RecipeElement), new RecipeElementConverter());
                csv.Configuration.TypeConverterCache.AddConverter(typeof(EntityTag), new EntityTagConverter());
                csv.Configuration.TypeConverterCache.AddConverter(typeof(EntityID), new EntityIDConverter());
                csv.Configuration.TypeConverterCache.AddConverter(typeof(IEnumerable<EntityTag>), new EntityTagEnumerableConverter());
                csv.Configuration.TypeConverterCache.AddConverter(typeof(IEnumerable<EntityID>), new EntityIDEnumerableConverter());
                csv.Configuration.TypeConverterCache.AddConverter(typeof(IEnumerable<string>), new StringEnumerableConverter());
                csv.Configuration.TypeConverterOptionsCache.AddOptions(typeof(EntityID), IdentifierOptions);
                csv.Configuration.RegisterClassMap(ClassMapper[typeof(TRecord)]);

                IEnumerable<EntityShim> shims = csv.GetRecords(ShimMapper[typeof(TRecord)]).Cast<EntityShim>();
                foreach (var shim in shims)
                {
                    records.Add(shim.ToEntity<TRecord>());
                }
            }

            return records;
        }

        /// <summary>
        /// Reads all records of <see cref="PronounGroup"/>s from the appropriate file.
        /// </summary>
        /// <returns>The records read.</returns>
        public static HashSet<PronounGroup> GetRecordsForPronounGroup()
        {
            var records = new HashSet<PronounGroup>();
            var filenameAndPath = Path.Combine(SearchPath, $"Designer/PronounGroups.csv");
            using (var reader = new StreamReader(filenameAndPath))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.RegisterClassMap<PronounGroupClassMap>();
                var shims = csv.GetRecords<PronounGroupShim>();
                foreach (var shim in shims)
                {
                    records.Add(shim.ToPronounGroup());
                }
            }

            return records;
        }
    }
}
