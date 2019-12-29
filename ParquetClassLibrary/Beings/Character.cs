using System.Collections.Generic;
using System.Linq;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Beings
{
    /// <summary>
    /// Models the definitions shared by in-game actors that take part in the narrative.
    /// </summary>
    public abstract class Character : Being
    {
        #region Class Defaults
        /// <summary>A pronoun to use when none is specified.</summary>
        public const string DefaultPronoun = "they";
        #endregion

        #region Characteristics
        /// <summary>Player-facing personal name.</summary>
        public string PersonalName { get; }

        /// <summary>Player-facing family name.</summary>
        public string FamilyName { get; }

        /// <summary>Player-facing full name.</summary>
        public string FullName => Name;

        /// <summary>The pronouns the <see cref="Character"/> uses.</summary>
        public string Pronoun { get; }

        /// <summary>The story character that this <see cref="Character"/> represents.</summary>
        /// <remarks>
        /// This identifier provides a link between software character <see langword="class"/>es
        /// and the characters written of in a game's narrative that they represent.  The goal
        /// is that these identifiers be able to span any number of shipped titles, allowing a
        /// sequel title to import data from prior titles in such a way that one game's <see cref="NPC"/>
        /// can become another game's <see cref="PlayerCharacter"/>.
        /// </remarks>
        public string StoryCharacterID { get; }

        /// <summary>The <see cref="Quests.Quest"/>s that this <see cref="Character"/> either offers or has undertaken.</summary>
        /// <remarks><see cref="NPC"/>s offer quests, <see cref="PlayerCharacter"/>s undertake them.</remarks>
        public IReadOnlyList<EntityID> StartingQuests { get; }

        /// <summary>Dialogue lines this <see cref="Character"/> can say.</summary>
        public IReadOnlyList<string> Dialogue { get; }

        /// <summary>The set of belongings that this <see cref="Character"/> begins with.</summary>
        /// <remarks>This is not the full <see cref="Items.Inventory"/> but a list of item IDs to populate it with.</remarks>
        public IReadOnlyList<EntityID> StartingInventory { get; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="inBounds">
        /// The bounds within which the <see cref="Character"/>'s <see cref="EntityID"/> is defined.
        /// Must be one of <see cref="All.BeingIDs"/>.
        /// </param>
        /// <param name="inID">Unique identifier for the <see cref="Character"/>.  Cannot be null.</param>
        /// <param name="inPersonalName">Personal name of the <see cref="Character"/>.  Cannot be null or empty.</param>
        /// <param name="inFamilyName">Family name of the <see cref="Character"/>.  Cannot be null or empty.</param>
        /// <param name="inDescription">Player-friendly description of the <see cref="Character"/>.</param>
        /// <param name="inComment">Comment of, on, or by the <see cref="Character"/>.</param>
        /// <param name="inNativeBiome">The <see cref="EntityID"/> for the <see cref="Biomes.Biome"/> in which this <see cref="Being"/> is most comfortable.</param>
        /// <param name="inPrimaryBehavior">The rules that govern how this <see cref="Character"/> acts.  Cannot be null.</param>
        /// <param name="inAvoids">Any parquets this <see cref="Character"/> avoids.</param>
        /// <param name="inSeeks">Any parquets this <see cref="Character"/> seeks.</param>
        /// <param name="inPronoun">How to refer to this <see cref="Character"/>.</param>
        /// <param name="inStoryCharacterID">A means of identifying this <see cref="Character"/> across multiple shipped game titles.</param>
        /// <param name="inStartingQuests">Any quests this <see cref="Character"/> has to offer or has undertaken.</param>
        /// <param name="inDialogue">All dialogue this <see cref="Character"/> may say.</param>
        /// <param name="inStartingInventory">Any items this <see cref="Character"/> possesses at the outset.</param>
        protected Character(Range<EntityID> inBounds, EntityID inID,
                            string inPersonalName, string inFamilyName,
                            string inDescription, string inComment, EntityID inNativeBiome,
                            Behavior inPrimaryBehavior, List<EntityID> inAvoids = null,
                            List<EntityID> inSeeks = null, string inPronoun = DefaultPronoun,
                            string inStoryCharacterID = "", List<EntityID> inStartingQuests = null,
                            List<string> inDialogue = null, List<EntityID> inStartingInventory = null)
            : base(inBounds, inID, $"{inPersonalName} {inFamilyName}", inDescription, inComment,
                   inNativeBiome, inPrimaryBehavior, inAvoids, inSeeks)
        {
            var nonNullPronoun = string.IsNullOrEmpty(inPronoun) ? DefaultPronoun : inPronoun;
            var nonNullQuests = inStartingQuests ?? Enumerable.Empty<EntityID>();
            var nonNullInventory = inStartingInventory ?? Enumerable.Empty<EntityID>();

            Precondition.AreInRange(nonNullQuests, All.QuestIDs, nameof(inStartingQuests));
            Precondition.AreInRange(nonNullInventory, All.ItemIDs, nameof(inStartingInventory));
            Precondition.IsNotNullOrEmpty(inPersonalName, nameof(inPersonalName));
            Precondition.IsNotNullOrEmpty(inFamilyName, nameof(inFamilyName));

            PersonalName = inPersonalName;
            FamilyName = inFamilyName;
            Pronoun = nonNullPronoun;
            StoryCharacterID = inStoryCharacterID;
            StartingQuests = nonNullQuests.ToList();
            Dialogue = (inDialogue ?? Enumerable.Empty<string>()).ToList();
            StartingInventory = nonNullInventory.ToList();
        }
        #endregion
    }
}