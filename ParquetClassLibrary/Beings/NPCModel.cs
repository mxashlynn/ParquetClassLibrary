using System;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using ParquetClassLibrary.Serialization;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Beings
{
    /// <summary>
    /// Models the definition for a non-player character, such as a shop keeper.
    /// </summary>
    public sealed class NPCModel : CharacterModel, ITypeConverter
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="NPCModel"/> class.
        /// </summary>
        /// <param name="inID">
        /// Unique identifier for the <see cref="NPCModel"/>.  Cannot be null.
        /// Must be a valid <see cref="All.NpcIDs"/>.
        /// </param>
        /// <param name="inPersonalName">Personal name of the <see cref="NPCModel"/>.  Cannot be null or empty.</param>
        /// <param name="inFamilyName">Family name of the <see cref="NPCModel"/>.  Cannot be null or empty.</param>
        /// <param name="inDescription">Player-friendly description of the <see cref="NPCModel"/>.</param>
        /// <param name="inComment">Comment of, on, or by the <see cref="NPCModel"/>.</param>
        /// <param name="inNativeBiome">The <see cref="EntityID"/> of the <see cref="Biome"/> in which this <see cref="NPCModel"/> is most comfortable.</param>
        /// <param name="inPrimaryBehavior">The rules that govern how this <see cref="NPCModel"/> acts.  Cannot be null.</param>
        /// <param name="inAvoids">Any parquets this <see cref="NPCModel"/> avoids.</param>
        /// <param name="inSeeks">Any parquets this <see cref="NPCModel"/> seeks.</param>
        /// <param name="inPronouns">How to refer to this <see cref="NPCModel"/>.</param>
        /// <param name="inStoryCharacterID">A means of identifying this <see cref="NPCModel"/> across multiple shipped game titles.</param>
        /// <param name="inStartingQuests">Any quests this <see cref="NPCModel"/> has to offer.</param>
        /// <param name="inDialogue">All dialogue this <see cref="NPCModel"/> may say.</param>
        /// <param name="inStartingInventory">Any items this <see cref="NPCModel"/> owns.</param>
        public NPCModel(EntityID inID, string inPersonalName, string inFamilyName,
                        string inDescription, string inComment,
                        EntityID inNativeBiome, Behavior inPrimaryBehavior,
                        IEnumerable<EntityID> inAvoids = null, IEnumerable<EntityID> inSeeks = null,
                        string inPronouns = null, string inStoryCharacterID = "",
                        IEnumerable<EntityID> inStartingQuests = null, IEnumerable<EntityID> inDialogue = null,
                        IEnumerable<EntityID> inStartingInventory = null)
            : base(All.NpcIDs, inID, inPersonalName, inFamilyName, inDescription,
                   inComment, inNativeBiome, inPrimaryBehavior, inAvoids, inSeeks,
                   inPronouns, inStoryCharacterID, inStartingQuests, inDialogue, inStartingInventory)
        {
            Precondition.IsInRange(inID, All.NpcIDs, nameof(inID));
        }
        #endregion

        #region ITypeConverter Implementation
        /// <summary>Allows the converter to construct statically.</summary>
        internal static readonly NPCModel ConverterFactory =
            new NPCModel(EntityID.None, nameof(ConverterFactory), "", "", "", EntityID.None, Behavior.Still);

        /// <summary>
        /// Converts the given <see cref="object"/> to a <see cref="string"/> for serialization.
        /// </summary>
        /// <param name="inValue">The instance to convert.</param>
        /// <param name="inRow">The current context and configuration.</param>
        /// <param name="inMemberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance serialized.</returns>
        public string ConvertToString(object inValue, IWriterRow inRow, MemberMapData inMemberMapData)
            => null != inValue
            && inValue is NPCModel model
            && model.ID != EntityID.None
                ? $"{model.ID}{Rules.Delimiters.InternalDelimiter}" +
                  $"{model.PersonalName}{Rules.Delimiters.InternalDelimiter}" +
                  $"{model.FamilyName}{Rules.Delimiters.InternalDelimiter}" +
                  $"{model.Description}{Rules.Delimiters.InternalDelimiter}" +
                  $"{model.Comment}{Rules.Delimiters.InternalDelimiter}" +
                  $"{model.NativeBiome}{Rules.Delimiters.InternalDelimiter}" +
                  $"{model.PrimaryBehavior}{Rules.Delimiters.InternalDelimiter}" +
                  $"{SeriesConverter<EntityID, List<EntityID>>.ConverterFactory.ConvertToString(model.Avoids, Rules.Delimiters.ElementDelimiter)}" +
                  $"{Rules.Delimiters.InternalDelimiter}" +
                  $"{SeriesConverter<EntityID, List<EntityID>>.ConverterFactory.ConvertToString(model.Seeks, Rules.Delimiters.ElementDelimiter)}" +
                  $"{Rules.Delimiters.InternalDelimiter}" +
                  $"{model.Pronouns}{Rules.Delimiters.InternalDelimiter}" +
                  $"{model.StoryCharacterID}{Rules.Delimiters.InternalDelimiter}" +
                  $"{SeriesConverter<EntityID, List<EntityID>>.ConverterFactory.ConvertToString(model.StartingQuests, Rules.Delimiters.ElementDelimiter)}" +
                  $"{Rules.Delimiters.InternalDelimiter}" +
                  $"{SeriesConverter<EntityID, List<EntityID>>.ConverterFactory.ConvertToString(model.Dialogue, Rules.Delimiters.ElementDelimiter)}" +
                  $"{Rules.Delimiters.InternalDelimiter}" +
                  $"{SeriesConverter<EntityID, List<EntityID>>.ConverterFactory.ConvertToString(model.StartingInventory, Rules.Delimiters.ElementDelimiter)}"
                : throw new ArgumentException($"Could not serialize {inValue} as {nameof(NPCModel)}.");

        /// <summary>
        /// Converts the given <see cref="string"/> to an <see cref="object"/> as deserialization.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <param name="row">The current context and configuration.</param>
        /// <param name="memberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance deserialized.</returns>
        public object ConvertFromString(string inText, IReaderRow inRow, MemberMapData inMemberMapData)
        {
            if (string.IsNullOrEmpty(inText)
                || string.Compare(nameof(EntityID.None), inText, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                throw new ArgumentException($"Could not convert '{inText}' to {nameof(NPCModel)}.");
            }

            try
            {
                var parameterText = inText.Split(Rules.Delimiters.InternalDelimiter);

                var id = (EntityID)EntityID.ConverterFactory.ConvertFromString(parameterText[0], inRow, inMemberMapData);
                var personalName = parameterText[1];
                var familyName = parameterText[2];
                var description = parameterText[3];
                var comment = parameterText[4];
                var biome = (EntityID)EntityID.ConverterFactory.ConvertFromString(parameterText[5], inRow, inMemberMapData);
                var behavior = (Behavior)Enum.Parse(typeof(Behavior), parameterText[6]);
                var avoids = (List<EntityID>)SeriesConverter<EntityID, List<EntityID>>
                    .ConverterFactory.ConvertFromString(parameterText[7], inRow, inMemberMapData, Rules.Delimiters.ElementDelimiter);
                var seeks = (List<EntityID>)SeriesConverter<EntityID, List<EntityID>>
                    .ConverterFactory.ConvertFromString(parameterText[8], inRow, inMemberMapData, Rules.Delimiters.ElementDelimiter);
                var pronouns = parameterText[9];
                var storyID = parameterText[10];
                var startingQuests = (List<EntityID>)SeriesConverter<EntityID, List<EntityID>>
                    .ConverterFactory.ConvertFromString(parameterText[10], inRow, inMemberMapData, Rules.Delimiters.ElementDelimiter);
                var dialogue = (List<EntityID>)SeriesConverter<EntityID, List<EntityID>>
                    .ConverterFactory.ConvertFromString(parameterText[11], inRow, inMemberMapData, Rules.Delimiters.ElementDelimiter);
                var startingInventory = (List<EntityID>)SeriesConverter<EntityID, List<EntityID>>
                    .ConverterFactory.ConvertFromString(parameterText[12], inRow, inMemberMapData, Rules.Delimiters.ElementDelimiter);

                return new NPCModel(id, personalName, familyName, description, comment, biome, behavior, avoids, seeks,
                                    pronouns, storyID, startingQuests, dialogue, startingInventory);
            }
            catch (Exception e)
            {
                throw new FormatException($"Could not parse '{inText}' as {nameof(NPCModel)}: {e}");
            }
        }
        #endregion
    }
}
