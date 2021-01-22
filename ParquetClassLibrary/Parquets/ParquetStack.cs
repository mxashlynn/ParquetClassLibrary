using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using ParquetClassLibrary.Properties;

namespace ParquetClassLibrary.Parquets
{
    /// <summary>
    /// Simple container for one of each overlapping layer of parquets.
    /// </summary>
    // TODO: Let's consider if there is another word than Stack that we can use.  Bundle?  Sheaf?
    public class ParquetStack : IParquetStack, IEquatable<ParquetStack>, ITypeConverter
    {
        #region Class Defaults
        /// <summary>Cannonical null <see cref="ParquetStack"/>, representing an arbitrary empty stack.</summary>
        public static ParquetStack Empty
            => new ParquetStack(ModelID.None, ModelID.None, ModelID.None, ModelID.None);
        #endregion

        #region Characteristics
        /// <summary>The floor contained in this stack.</summary>
        public ModelID FloorID { get; set; }

        /// <summary>The block contained in this stack.</summary>
        public ModelID BlockID { get; set; }

        /// <summary>The furnishing contained in this stack.</summary>
        public ModelID FurnishingID { get; set; }

        /// <summary>The collectible contained in this stack.</summary>
        public ModelID CollectibleID { get; set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new default instance of the <see cref="ParquetStack"/> class.
        /// </summary>
        /// <remarks>
        /// This is primarily useful for serialization as the default values are featureless.
        /// </remarks>
        public ParquetStack() :
            this(ModelID.None, ModelID.None, ModelID.None, ModelID.None)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParquetStack"/> class.
        /// </summary>
        /// <param name="inFloor">The floor-layer parquet.</param>
        /// <param name="inBlock">The The floor-layer parquet-layer parquet.</param>
        /// <param name="inFurnishing">The furnishing-layer parquet.</param>
        /// <param name="inCollectible">The collectible-layer parquet.</param>
        public ParquetStack(ModelID inFloor, ModelID inBlock, ModelID inFurnishing, ModelID inCollectible)
        {
            Precondition.IsInRange(inFloor, All.FloorIDs, nameof(inFloor));
            Precondition.IsInRange(inBlock, All.BlockIDs, nameof(inBlock));
            Precondition.IsInRange(inFurnishing, All.FurnishingIDs, nameof(inFurnishing));
            Precondition.IsInRange(inCollectible, All.CollectibleIDs, nameof(inCollectible));

            FloorID = inFloor;
            BlockID = inBlock;
            FurnishingID = inFurnishing;
            CollectibleID = inCollectible;
        }
        #endregion

        #region Queries
        /// <summary>The number of parquets actually present in this stack.</summary>
        public int Count => ModelID.None != FloorID ? 1 : 0
                          + ModelID.None != BlockID ? 1 : 0
                          + ModelID.None != FurnishingID ? 1 : 0
                          + ModelID.None != CollectibleID ? 1 : 0;

        /// <summary>
        /// Indicates whether this <see cref="ParquetStack"/> is empty.
        /// </summary>
        /// <value><c>true</c> if the stack contains only null references; otherwise, <c>false</c>.</value>
        public bool IsEmpty => ModelID.None == FloorID
                            && ModelID.None == BlockID
                            && ModelID.None == FurnishingID
                            && ModelID.None == CollectibleID;

        /// <summary>
        /// A <see cref="ParquetStack"/> is Enclosing iff:
        /// 1, It has a <see cref="BlockID"/> that is not <see cref="BlockModel.IsLiquid"/>; or,
        /// 2, It has a <see cref="FurnishingID"/> that is <see cref="FurnishingModel.IsEnclosing"/>.
        /// </summary>
        /// <returns><c>true</c>, if this <see cref="ParquetStack"/> is Enclosing, <c>false</c> otherwise.</returns>
        public bool IsEnclosing
            => (BlockID != ModelID.None
                && !(All.Blocks.Get<BlockModel>(BlockID)?.IsLiquid ?? true))
            || (FurnishingID != ModelID.None
                && (All.Furnishings.Get<FurnishingModel>(FurnishingID)?.IsEnclosing ?? false));

        /// <summary>
        /// A <see cref="ParquetStack"/> is Entry iff:
        /// 1, It is either Walkable or Enclosing but not both; and,
        /// 2, It has a <see cref="FurnishingID"/> that is <see cref="FurnishingModel.Entry"/>.
        /// </summary>
        /// <returns><c>true</c>, if this <see cref="ParquetStack"/> is Entry, <c>false</c> otherwise.</returns>
        internal bool IsEntry
            => FurnishingID != ModelID.None
            && (All.Furnishings.Get<FurnishingModel>(FurnishingID)?.Entry ?? EntryType.None) != EntryType.None
            // Inequality standing in for missing conditional XOR here.
            && (IsWalkable != IsEnclosing);

        /// <summary>
        /// A <see cref="ParquetStack"/> is considered walkable iff:
        /// 1, It has a <see cref="FloorID"/>;
        /// 2, It does not have a <see cref="BlockID"/>;
        /// 3, It does not have a <see cref="FurnishingID"/> that <see cref="FurnishingModel.IsEnclosing"/>.
        /// </summary>
        /// <returns><c>true</c>, if this <see cref="ParquetStack"/> is Walkable, <c>false</c> otherwise.</returns>
        internal bool IsWalkable
            => FloorID != ModelID.None
            && BlockID == ModelID.None
            && (FurnishingID == ModelID.None
                || !(All.Furnishings.Get<FurnishingModel>(FurnishingID)?.IsEnclosing ?? false));
        #endregion

        #region IEquatable Implementation
        /// <summary>
        /// Serves as a hash function for a <see cref="ParquetStack"/>.
        /// </summary>
        /// <returns>
        /// A hash code for this instance that is suitable for use in hashing algorithms and data structures.
        /// </returns>
        public override int GetHashCode()
            => (FloorID, BlockID, FurnishingID, CollectibleID).GetHashCode();

        /// <summary>
        /// Determines whether the specified <see cref="ParquetStack"/> is equal to the current <see cref="ParquetStack"/>.
        /// </summary>
        /// <param name="inStack">The <see cref="ParquetStack"/> to compare with the current.</param>
        /// <returns><c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(ParquetStack inStack)
            => FloorID == inStack?.FloorID
            && BlockID == inStack.BlockID
            && FurnishingID == inStack.FurnishingID
            && CollectibleID == inStack.CollectibleID;

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="ParquetStack"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="ParquetStack"/>.</param>
        /// <returns><c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
            => obj is ParquetStack stack
            && Equals(stack);

        /// <summary>
        /// Determines whether a specified instance of <see cref="ParquetStack"/> is equal to another specified instance of <see cref="ParquetStack"/>.
        /// </summary>
        /// <param name="inStack1">The first <see cref="ParquetStack"/> to compare.</param>
        /// <param name="inStack2">The second <see cref="ParquetStack"/> to compare.</param>
        /// <returns><c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ParquetStack inStack1, ParquetStack inStack2)
            => inStack1?.Equals(inStack2) ?? inStack2?.Equals(inStack1) ?? true;

        /// <summary>
        /// Determines whether a specified instance of <see cref="ParquetStack"/> is not equal to another specified instance of <see cref="ParquetStack"/>.
        /// </summary>
        /// <param name="inStack1">The first <see cref="ParquetStack"/> to compare.</param>
        /// <param name="inStack2">The second <see cref="ParquetStack"/> to compare.</param>
        /// <returns><c>true</c> if they are NOT equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ParquetStack inStack1, ParquetStack inStack2)
            => !(inStack1 == inStack2);
        #endregion

        #region ITypeConverter
        /// <summary>Allows the converter to construct itself statically.</summary>
        internal static ParquetStack ConverterFactory { get; } = Empty;

        /// <summary>
        /// Converts the given <see cref="object"/> to a <see cref="string"/> for serialization.
        /// </summary>
        /// <param name="inValue">The instance to convert.</param>
        /// <param name="inRow">The current context and configuration.</param>
        /// <param name="inMemberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance serialized.</returns>
        public string ConvertToString(object inValue, IWriterRow inRow, MemberMapData inMemberMapData)
            => inValue is ParquetStack stack
                ? $"{stack.FloorID}{Delimiters.InternalDelimiter}" +
                  $"{stack.BlockID}{Delimiters.InternalDelimiter}" +
                  $"{stack.FurnishingID}{Delimiters.InternalDelimiter}" +
                  $"{stack.CollectibleID}"
                : throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorCannotConvert,
                                                            inValue, nameof(ParquetStack)));

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
                                                          inText, nameof(ParquetStack)));
            }

            try
            {
                var parameterText = inText.Split(Delimiters.InternalDelimiter);

                var floor = (ModelID)ModelID.ConverterFactory.ConvertFromString(parameterText[0], inRow, inMemberMapData);
                var block = (ModelID)ModelID.ConverterFactory.ConvertFromString(parameterText[1], inRow, inMemberMapData);
                var furnishing = (ModelID)ModelID.ConverterFactory.ConvertFromString(parameterText[2], inRow, inMemberMapData);
                var collectible = (ModelID)ModelID.ConverterFactory.ConvertFromString(parameterText[3], inRow, inMemberMapData);

                return new ParquetStack(floor, block, furnishing, collectible);
            }
            catch (Exception e)
            {
                throw new FormatException(string.Format(CultureInfo.CurrentCulture, Resources.ErrorCannotParse,
                                                        inText, nameof(ParquetStack)), e);
            }
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Creates a new instance with the same characteristics as the current instance.
        /// </summary>
        /// <returns></returns>
        public ParquetStack Clone()
            => new ParquetStack(FloorID, BlockID, FurnishingID, CollectibleID);

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="ParquetStack"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => $"[{FloorID} {BlockID} {FurnishingID} {CollectibleID}]";
        #endregion
    }

    /// <summary>
    /// Provides extension methods useful when dealing with 2D arrays of <see cref="ParquetStack"/>s.
    /// </summary>
    public static class ParquetStackArrayExtensions
    {
        /// <summary>
        /// Determines if the given position corresponds to a point within the current array.
        /// </summary>
        /// <param name="inSubregion">The <see cref="ParquetStack"/> array to validate against.</param>
        /// <param name="inPosition">The position to validate.</param>
        /// <returns><c>true</c>, if the position is valid, <c>false</c> otherwise.</returns>
        public static bool IsValidPosition(this ParquetStack[,] inSubregion, Vector2D inPosition)
        {
            // NOTE IDEA When we reach 1.0 we could replace this precondition with a clause in the return computation.
            Precondition.IsNotNull(inSubregion, nameof(inSubregion));

            return inPosition.X > -1
                && inPosition.Y > -1
                && inPosition.X < inSubregion.GetLength(1)
                && inPosition.Y < inSubregion.GetLength(0);
        }
    }
}
