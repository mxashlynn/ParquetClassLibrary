using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using ParquetClassLibrary.Properties;

namespace ParquetClassLibrary.Maps
{
    /// <summary>
    /// Indicates which parquets constitute this <see cref="MapChunk"/> and how they are arranged.
    /// </summary>
    /// <remarks>
    /// Every chunk is either handmade or procedurally generated.<para />
    ///<para />
    /// Chunks that are not hand made are instead composed of two layers: a base and a modifier.
    /// The base is the underlying structure of the chunk and the modifier overlays it to
    /// produce more complex arrangements than would otherwise be possible.  For example:
    /// - Forest: Base·Grassy Solid · Modifier·Scattered Trees
    /// - Seaside: Base·Watery Solid · Modifier·Eastern Sandy
    /// - Town: Handmade
    /// </remarks>
    public class ChunkDescription : IEquatable<ChunkDescription>, ITypeConverter
    {
        #region Class Defaults
        /// <summary>The null <see cref="ChunkDescription"/>, which generates an empty <see cref="MapChunk"/>.</summary>
        public static readonly ChunkDescription Empty = new ChunkDescription();
        #endregion

        #region Characteristics
        /// <summary>Indicates the basic form that the <see cref="MapChunk"/> of parquets takes.</summary>
        public ChunkTopography BaseTopography { get; }

        /// <summary>Indicates the overall type of parquets in the <see cref="MapChunk"/>.</summary>
        public ModelTag BaseComposition { get; }

        /// <summary>Indicates a modifier on the <see cref="MapChunk"/> of parquets.</summary>
        public ChunkTopography ModifierTopography { get; }

        /// <summary>Indicates the type of parquets modifying the <see cref="MapChunk"/>.</summary>
        public ModelTag ModifierComposition { get; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new default instance of the <see cref="ChunkDescription"/> class for use with serialization.
        /// </summary>
        public ChunkDescription()
        {
            BaseTopography = ChunkTopography.Empty;
            BaseComposition = ModelTag.None;
            ModifierTopography = ChunkTopography.Empty;
            ModifierComposition = ModelTag.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkDescription"/> class.
        /// </summary>
        /// <param name="inBaseTopography">The basic form that the <see cref="MapChunk"/> of parquets takes.</param>
        /// <param name="inBaseComposition">Indicates the overall type of parquets in the <see cref="MapChunk"/>.</param>
        /// <param name="inModifierTopography">Indicates a modifier on the <see cref="MapChunk"/> of parquets.</param>
        /// <param name="inModifierComposition">Indicates the type of parquets modifying the <see cref="MapChunk"/>.</param>
        public ChunkDescription(ChunkTopography inBaseTopography, ModelTag inBaseComposition,
                                ChunkTopography inModifierTopography, ModelTag inModifierComposition)
        {
            BaseTopography = inBaseTopography;
            BaseComposition = inBaseComposition ?? ModelTag.None;
            ModifierTopography = inModifierTopography;
            ModifierComposition = inModifierComposition ?? ModelTag.None;
        }
        #endregion

        #region IEquatable Implementation
        /// <summary>
        /// Serves as a hash function for a <see cref="ChunkDescription"/> class.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures.</returns>
        public override int GetHashCode()
            => (BaseTopography, BaseComposition, ModifierTopography, ModifierComposition).GetHashCode();

        /// <summary>
        /// Determines whether the specified <see cref="ChunkDescription"/> is equal to the current <see cref="ChunkDescription"/>.
        /// </summary>
        /// <param name="inChunkType">The <see cref="ChunkDescription"/> to compare with the current.</param>
        /// <returns><c>true</c> if the <see cref="ChunkDescription"/>s are equal.</returns>
        public bool Equals(ChunkDescription inChunkType)
            => BaseTopography == inChunkType?.BaseTopography
            && BaseComposition == inChunkType.BaseComposition
            && ModifierTopography == inChunkType.ModifierTopography
            && ModifierComposition == inChunkType.ModifierComposition;

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ChunkDescription"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ChunkDescription"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="ChunkDescription"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
            => obj is ChunkDescription chunkType
            && Equals(chunkType);

        /// <summary>
        /// Determines whether a specified instance of <see cref="ChunkDescription"/> is equal to
        /// another specified instance of <see cref="ChunkDescription"/>.
        /// </summary>
        /// <param name="inChunkType1">The first <see cref="ChunkDescription"/> to compare.</param>
        /// <param name="inChunkType2">The second <see cref="ChunkDescription"/> to compare.</param>
        /// <returns><c>true</c> if the two <see cref="ChunkDescription"/>s are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ChunkDescription inChunkType1, ChunkDescription inChunkType2)
            => inChunkType1?.Equals(inChunkType2) ?? inChunkType2?.Equals(inChunkType1) ?? true;

        /// <summary>
        /// Determines whether a specified instance of <see cref="ChunkDescription"/> is unequal to
        /// another specified instance of <see cref="ChunkDescription"/>.
        /// </summary>
        /// <param name="inChunkType1">The first <see cref="ChunkDescription"/> to compare.</param>
        /// <param name="inChunkType2">The second <see cref="ChunkDescription"/> to compare.</param>
        /// <returns><c>true</c> if the two <see cref="ChunkDescription"/>s are NOT equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ChunkDescription inChunkType1, ChunkDescription inChunkType2)
            => !(inChunkType1 == inChunkType2);
        #endregion

        #region ITypeConverter Implementation
        /// <summary>Allows the converter to construct itself statically.</summary>
        internal static ChunkDescription ConverterFactory { get; } = Empty;

        /// <summary>
        /// Converts the given <see cref="object"/> to a <see cref="string"/> for serialization.
        /// </summary>
        /// <param name="inValue">The instance to convert.</param>
        /// <param name="inRow">The current context and configuration.</param>
        /// <param name="inMemberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance serialized.</returns>
        public string ConvertToString(object inValue, IWriterRow inRow, MemberMapData inMemberMapData)
            => inValue is ChunkDescription chunk
            && null != chunk
                ? $"{chunk.BaseTopography}{Delimiters.InternalDelimiter}" +
                  $"{chunk.BaseComposition}{Delimiters.InternalDelimiter}" +
                  $"{chunk.ModifierTopography}{Delimiters.InternalDelimiter}" +
                  $"{chunk.ModifierComposition}"
                : throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorCannotConvert,
                                                            inValue, nameof(ChunkDescription)));

        /// <summary>
        /// Converts the given <see cref="string"/> to an <see cref="object"/> as deserialization.
        /// </summary>
        /// <param name="inText">The text to convert.</param>
        /// <param name="inRow">The current context and configuration.</param>
        /// <param name="inMemberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance deserialized.</returns>
        public object ConvertFromString(string inText, IReaderRow inRow, MemberMapData inMemberMapData)
        {
            if (string.IsNullOrEmpty(inText))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorCannotConvert,
                                                          inText, nameof(ChunkDescription)));
            }
            else
            {
                try
                {
                    var parameterText = inText.Split(Delimiters.InternalDelimiter);

                    var baseTopography = (ChunkTopography)Enum.Parse(typeof(ChunkTopography), parameterText[0]);
                    var baseComposition = (ModelTag)ModelTag.ConverterFactory.ConvertFromString(parameterText[1], inRow, inMemberMapData);
                    var modifierTopography = (ChunkTopography)Enum.Parse(typeof(ChunkTopography), parameterText[2]);
                    var modifierComposition = (ModelTag)ModelTag.ConverterFactory.ConvertFromString(parameterText[3], inRow, inMemberMapData);

                    return new ChunkDescription(baseTopography, baseComposition, modifierTopography, modifierComposition);
                }
                catch (Exception e)
                {
                    throw new FormatException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorCannotParse,
                                                            inText, nameof(ChunkDescription)), e);
                }
            }
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Creates a new instance with the same characteristics as the current instance.
        /// </summary>
        /// <returns></returns>
        public ChunkDescription Clone()
            => new ChunkDescription(BaseTopography, BaseComposition, ModifierTopography, ModifierComposition);

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Rooms.MapSpace"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => $"[{BaseTopography} {BaseComposition} above {ModifierTopography} {ModifierComposition}]";
        #endregion
    }
}
