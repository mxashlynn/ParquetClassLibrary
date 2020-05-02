namespace ParquetClassLibrary
{
    /// <summary>
    /// Provides a unified source of serialization separators for the library.
    /// </summary>
    public static class Delimiters
    {
        /// <summary>Separator for encoding the dimensions of <see cref="IGrid{TElement}"/> implementations.</summary>
        public const string DimensionalDelimiter = "×";

        /// <summary>Separator for encoding the dimensions of <see cref="IGrid{TElement}"/> implementations.</summary>
        public const string DimensionalTerminator = "≡";

        /// <summary>Separates primitives within serialized <see cref="Vector2D"/>s and <see cref="Range{TElement}"/>s.</summary>
        public const string ElementDelimiter = "–";

        /// <summary>Separates properties within a class when in serialization.</summary>
        public const string InternalDelimiter = "·";

        /// <summary>Separates family and personal names within serialized <see cref="Beings.CharacterModel"/>s.</summary>
        public const string NameDelimiter = "§";

        /// <summary>Marks out tags that need to be replaced with pronouns from a <see cref="Beings.PronounGroup"/>s.</summary>
        public const string PronounDelimiter = "|";

        /// <summary>Separates objects within collections.</summary>
        public const string SecondaryDelimiter = "∟";
    }
}
