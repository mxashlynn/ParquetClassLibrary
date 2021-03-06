using System;
using System.Diagnostics.CodeAnalysis;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Parquet
{
    /// <summary>
    /// Identifies functional characteristics of <see cref="Model"/>s,
    /// such as their role in <see cref="Crafts.CraftingRecipe"/>s or
    /// <see cref="Biomes.BiomeRecipe"/>s.
    /// </summary>
    /// <remarks>
    /// The intent is that definitional narrative and mechanical features
    /// of each game <see cref="Model"/> be taggable.
    /// <para />
    /// This means that more than one <see cref="ModelTag"/> can coexist
    /// on a specific <see cref="Model"/> within the same model
    /// category (parquets, beings, etc.).
    /// <para />
    /// This allows for flexible definition of Models such that a loose
    /// category of models may answer a particular functional need; e.g.,
    /// "any parquet that has the Volcanic tag" or "any item that is a Key".
    /// </remarks>
    /// <seealso cref="ModelID"/>
    /// <seealso cref="All"/>
    [SuppressMessage("Design", "CA1036:Override methods on comparable types",
                     Justification = "Implementing these operators would prevent ModelTag from operating like a string.")]
    public class ModelTag : IComparable<ModelTag>, ITypeConverter
    {
        #region Class Defaults
        /// <summary>Indicates the lack of any <see cref="ModelTag"/>s.</summary>
        public static readonly ModelTag None = "";
        #endregion

        #region Characteristics
        /// <summary>Backing type for the <see cref="ModelTag"/>.</summary>
        private string tagContent = "";
        #endregion

        #region Implicit Conversion To/From Underlying Type
        /// <summary>
        /// Enables <see cref="ModelTag"/>s to be treated as their backing type.
        /// </summary>
        /// <param name="inValue">Any valid tag value.  Invalid values will be sanitized.</param>
        /// <returns>The given value as a tag.</returns>
        public static implicit operator ModelTag(string inValue)
            => new() { tagContent = inValue };

        /// <summary>
        /// Enables <see cref="ModelTag"/>s to be treated as their backing type.
        /// </summary>
        /// <param name="inTag">Any tag.</param>
        /// <returns>The tag's value.</returns>
        public static implicit operator string(ModelTag inTag)
            => inTag?.tagContent ?? "";
        #endregion

        #region IComparable Implementation
        /// <summary>
        /// Enables <see cref="ModelTag"/>s to be compared one another.
        /// </summary>
        /// <param name="inTag">Any valid <see cref="ModelTag"/>.</param>
        /// <returns>
        /// A value indicating the relative ordering of the <see cref="ModelTag"/>s being compared.
        /// The return value has these meanings:
        ///     Less than zero indicates that the current instance precedes the given <see cref="ModelTag"/> in the sort order.
        ///     Zero indicates that the current instance occurs in the same position in the sort order as the given <see cref="ModelTag"/>.
        ///     Greater than zero indicates that the current instance follows the given <see cref="ModelTag"/> in the sort order.
        /// </returns>
        public int CompareTo(ModelTag inTag)
            => string.Compare(tagContent, inTag?.tagContent ?? "", StringComparison.Ordinal);
        #endregion

        #region ITypeConverter Implementation
        /// <summary>Allows the converter to construct itself statically.</summary>
        internal static ModelTag ConverterFactory { get; } =
            None;

        /// <summary>
        /// Converts the given <see cref="string"/> to a <see cref="ModelTag"/>.
        /// </summary>
        /// <param name="inText">The <see cref="string"/> to convert to an object.</param>
        /// <param name="inRow">The <see cref="IReaderRow"/> for the current record.</param>
        /// <param name="inMemberMapData">The <see cref="MemberMapData"/> for the member being created.</param>
        /// <returns>The <see cref="ModelTag"/> created from the <see cref="string"/>.</returns>
        public object ConvertFromString(string inText, IReaderRow inRow, MemberMapData inMemberMapData)
            => string.Compare(nameof(None), inText, StringComparison.OrdinalIgnoreCase) == 0
                ? (ModelTag)""
                : (ModelTag)inText;

        /// <summary>
        /// Converts the given <see cref="ModelTag"/> to a record column.
        /// </summary>
        /// <param name="inValue">The instance to convert.</param>
        /// <param name="inRow">The <see cref="IReaderRow"/> for the current record.</param>
        /// <param name="inMemberMapData">The <see cref="MemberMapData"/> for the member being serialized.</param>
        /// <returns>The <see cref="ModelTag"/> as a CSV record.</returns>
        public string ConvertToString(object inValue, IWriterRow inRow, MemberMapData inMemberMapData)
            => inValue is ModelTag tag
                ? string.Compare("", tag, StringComparison.OrdinalIgnoreCase) == 0
                    ? nameof(None)
                    : (string)tag
                : Logger.DefaultWithConvertLog(inValue?.ToString() ?? "null", nameof(ModelTag), "");
        #endregion

        #region Utilities
        /// <summary>
        /// Determines whether the beginning of this <see cref="ModelTag"/> instance matches the given ModelTag.
        /// </summary>
        /// <param name="inPrefix">The <see cref="ModelTag"/> check against the beginning of the current ModelTag.</param>
        /// <returns><c>true</c> if this instance begins with the given prefix; otherwise, <c>false</c>.</returns>
        public bool StartsWithOrdinalIgnoreCase(ModelTag inPrefix)
            => tagContent.StartsWith(inPrefix, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="ModelTag"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => tagContent;
        #endregion
    }
}
