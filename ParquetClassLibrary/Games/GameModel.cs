using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace Parquet.Games
{
    /// <summary>
    /// Collects data about a Parquet-based game generally.
    /// </summary>
    public partial class GameModel : Model
    {
        #region Characteristics
        /// <summary>If <c>true</c> this game is part of a sequence of games.</summary>
        [Index(5)]
        public bool IsEpisode { get; private set; }

        /// <summary>Subtitle, if any.  This will be used as the title of the episode if <see cref="IsEpisode"/> is <c>true</c>.</summary>
        [Index(6)]
        public string EpisodeTitle { get; private set; }

        /// <summary>Number of this episode in its sequence, if any.</summary>
        [Index(7)]
        public int EpisodeNumber { get; private set; }

        /// <summary>The <see cref="ModelID"/> of the <see cref="Beings.CharacterModel"/> that the player controls at the outset.</summary>
        [Index(8)]
        public ModelID PlayerCharacterID { get; private set; }

        /// <summary>The <see cref="ModelID"/> of the <see cref="Scripts.ScriptModel"/> to run when play begins.</summary>
        [Index(9)]
        public ModelID FirstScriptID { get; private set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Represents a game or game-episode created with the Parquet system.
        /// Primarily contains metadata and gameplay rules information.
        /// </summary>
        /// <param name="inID">Unique identifier for the <see cref="GameModel"/>.  Cannot be null.</param>
        /// <param name="inName">Player-facing title of the <see cref="GameModel"/>.  Cannot be null or empty.</param>
        /// <param name="inDescription">Player-facing description of the <see cref="GameModel"/>.</param>
        /// <param name="inComment">A private comment on the nature of the game or episode.</param>
        /// <param name="inTags">Any additional information about this <see cref="GameModel"/>.</param>
        /// <param name="inIsEpisode">If <c>true</c> this game is part of a longer sequence of games.</param>
        /// <param name="inEpisodeTitle">Title of this episode, if any.</param>
        /// <param name="inEpisodeNumber">Number of this episode in its sequence, if any.</param>
        /// <param name="inPlayerCharacterID">The <see cref="ModelID"/> of the <see cref="Beings.CharacterModel"/> that the player controls at the outset.</param>
        /// <param name="inFirstScriptID">The <see cref="ModelID"/> of the <see cref="Scripts.ScriptModel"/> to run when play begins.</param>
        public GameModel(ModelID inID, string inName, string inDescription, string inComment,
                         IEnumerable<ModelTag> inTags = null, bool inIsEpisode = false, string inEpisodeTitle = "",
                         int inEpisodeNumber = 0, ModelID? inPlayerCharacterID = null, ModelID? inFirstScriptID = null)
            : base(All.GameIDs, inID, inName, inDescription, inComment, inTags)
        {
            var nonNullPlayerCharacterID = inPlayerCharacterID ?? ModelID.None;
            var nonNullFirstScriptID = inFirstScriptID ?? ModelID.None;

            Precondition.IsInRange(nonNullPlayerCharacterID, All.CharacterIDs, nameof(inPlayerCharacterID));
            Precondition.IsInRange(nonNullFirstScriptID, All.ScriptIDs, nameof(inFirstScriptID));

            IsEpisode = inIsEpisode;
            EpisodeTitle = IsEpisode ? inEpisodeTitle : "";
            EpisodeNumber = IsEpisode ? inEpisodeNumber : 0;
            PlayerCharacterID = nonNullPlayerCharacterID;
            FirstScriptID = nonNullFirstScriptID;
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="GameModel"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => IsEpisode
            ? $"\"{Name}\" #{EpisodeNumber}: '{EpisodeTitle}' ({PlayerCharacterID} @ {FirstScriptID})"
            : $"\"{Name}\" ({PlayerCharacterID} @ {FirstScriptID})";
        #endregion
    }
}
