using System.Collections.Generic;

namespace ParquetClassLibrary.Beings
{
    /// <summary>
    /// Facilitates editing of a <see cref="CharacterModel"/> from design tools while maintaining a read-only face for use during play.
    /// </summary>
    /// <remarks>
    /// By design, children of <see cref="CharacterModel"/> should never themselves use <see cref="ICharacterModelEdit"/>.
    /// ICharacterModelEdit is for use only by external types that require read/write access to model properties.
    /// </remarks>
    public interface ICharacterModelEdit : IBeingModelEdit
    {
        /// <summary>Player-facing personal name.</summary>
        public string PersonalName { get; set; }

        /// <summary>Player-facing family name.</summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// A key for the <see cref="PronounGroup"/> the <see cref="CharacterModel"/> uses,
        /// stored as "<see cref="PronounGroup.Objective"/>/<see cref="PronounGroup.Subjective"/>.
        /// </summary>
        public string Pronouns { get; set; }

        /// <summary>The story character that this <see cref="CharacterModel"/> represents.</summary>
        /// <remarks>
        /// This identifier provides a link between software character classes
        /// and the characters written of in a game's narrative that they represent.  The goal
        /// is that these identifiers be able to span any number of shipped titles, allowing a
        /// sequel title to import data from prior titles in such a way that one game's NPC
        /// can become another game's protagonist.
        /// </remarks>
        public string StoryCharacterID { get; set; }

        /// <summary>The <see cref="Scripts.InteractionModel"/>s that this <see cref="CharacterModel"/> either offers or has undertaken.</summary>
        /// <remarks>Typically, NPCs offer quests, player characters undertake them.</remarks>
        public IList<ModelID> StartingQuests { get; }

        /// <summary>Dialogue lines this <see cref="CharacterModel"/> can say.</summary>
        public IList<ModelID> StartingDialogue { get; }

        /// <summary>The set of belongings that this <see cref="CharacterModel"/> begins with.</summary>
        /// <remarks>This is not the full <see cref="Items.Inventory"/> but a list of item IDs to populate it with.</remarks>
        public IList<ModelID> StartingInventory { get; }
    }
}