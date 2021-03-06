using System;
using System.Diagnostics.CodeAnalysis;
using CsvHelper;
using CsvHelper.Configuration;

namespace Parquet.Parquets
{
    /// <summary>
    /// Tracks the status of a <see cref="FurnishingModel"/>.
    /// Instances of this class are mutable during play.
    /// </summary>
    public sealed class FurnishingStatus : ParquetStatus<FurnishingModel>
    {
        #region Class Defaults
        /// <summary>Provides an instance of the <see cref="FurnishingStatus"/> class with default values.</summary>
        public static FurnishingStatus Default { get; } = new FurnishingStatus();
        #endregion

        #region Status
        /// <summary>When <c>true</c>, the <see cref="FurnishingModel"/> associated with this status is open.</summary>
        public bool IsOpen { get; set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new default instance of the <see cref="FurnishingStatus"/> class.
        /// </summary>
        /// <remarks>This is primarily useful for serialization.</remarks>
        public FurnishingStatus()
            : this(false)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FurnishingStatus"/> class.
        /// </summary>
        /// <param name="inIsOpen">When <c>true</c>, the <see cref="FurnishingModel"/> associated with this status is open.</param>
        public FurnishingStatus(bool inIsOpen)
            => IsOpen = inIsOpen;

        /// <summary>
        /// Initializes an instance of the <see cref="FurnishingStatus"/> class
        /// based on a given <see cref="FurnishingModel"/> identifier.
        /// </summary>
        /// <param name="inFloorID">The <see cref="ModelID"/> of the definitions being tracked.</param>
        [SuppressMessage("Usage", "CA1801:Review unused parameters",
            Justification = "This constructor is provided for consistency.  The parameter is currently ignored, but may not be in the future.")]
        [SuppressMessage("Style", "IDE0060:Remove unused parameter",
            Justification = "This constructor is provided for consistency.  The parameter is currently ignored, but may not be in the future.")]
        public FurnishingStatus(ModelID inFloorID)
            // Note: Currently, by design, no Furnishing starts open.
            : this(false)
        { }
        #endregion

        #region IEquatable Implementation
        /// <summary>
        /// Serves as a hash function for a <see cref="FurnishingStatus"/>.
        /// </summary>
        /// <returns>
        /// A hash code for this instance that is suitable for use in hashing algorithms and data structures.
        /// </returns>
        public override int GetHashCode()
            => IsOpen.GetHashCode();

        /// <summary>
        /// Determines whether the specified <see cref="FurnishingStatus"/> is equal to the current <see cref="FurnishingStatus"/>.
        /// </summary>
        /// <param name="inStatus">The <see cref="FurnishingStatus"/> to compare with the current.</param>
        /// <returns><c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public override bool Equals<T>(T inStatus)
            => inStatus is FurnishingStatus FurnishingStatus
            && IsOpen == FurnishingStatus.IsOpen;

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="FurnishingStatus"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="FurnishingStatus"/>.</param>
        /// <returns><c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
            => obj is FurnishingStatus status
            && Equals(status);

        /// <summary>
        /// Determines whether a specified instance of <see cref="FurnishingStatus"/> is equal to another specified instance of <see cref="FurnishingStatus"/>.
        /// </summary>
        /// <param name="inStatus1">The first <see cref="FurnishingStatus"/> to compare.</param>
        /// <param name="inStatus2">The second <see cref="FurnishingStatus"/> to compare.</param>
        /// <returns><c>true</c> if they are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(FurnishingStatus inStatus1, FurnishingStatus inStatus2)
            => inStatus1?.Equals(inStatus2) ?? inStatus2?.Equals(inStatus1) ?? true;

        /// <summary>
        /// Determines whether a specified instance of <see cref="FurnishingStatus"/> is not equal to another specified instance of <see cref="FurnishingStatus"/>.
        /// </summary>
        /// <param name="inStatus1">The first <see cref="FurnishingStatus"/> to compare.</param>
        /// <param name="inStatus2">The second <see cref="FurnishingStatus"/> to compare.</param>
        /// <returns><c>true</c> if they are NOT equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(FurnishingStatus inStatus1, FurnishingStatus inStatus2)
            => !(inStatus1 == inStatus2);
        #endregion

        #region ITypeConverter Implementation
        /// <summary>Allows the converter to construct itself statically.</summary>
        internal static FurnishingStatus ConverterFactory { get; } = Default;

        /// <summary>
        /// Converts the given <see cref="object"/> to a <see cref="string"/> for serialization.
        /// </summary>
        /// <param name="inValue">The instance to convert.</param>
        /// <param name="inRow">The current context and configuration.</param>
        /// <param name="inMemberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance serialized.</returns>
        public override string ConvertToString(object inValue, IWriterRow inRow, MemberMapData inMemberMapData)
            => inValue is FurnishingStatus status
                ? $"{status.IsOpen}"
                : Logger.DefaultWithConvertLog(inValue?.ToString() ?? "null", nameof(FurnishingStatus), nameof(Default));

        /// <summary>
        /// Converts the given <see cref="string"/> to an <see cref="object"/> as deserialization.
        /// </summary>
        /// <param name="inText">The text to convert.</param>
        /// <param name="inRow">The current context and configuration.</param>
        /// <param name="inMemberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance deserialized.</returns>
        public override object ConvertFromString(string inText, IReaderRow inRow, MemberMapData inMemberMapData)
        {
            if (string.IsNullOrEmpty(inText)
                || string.Compare(nameof(Default), inText, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return Default.DeepClone();
            }

            var parsedIsTrench = bool.TryParse(inText, out var temp1)
                ? temp1
                : Logger.DefaultWithParseLog(inText, nameof(IsOpen), false);

            return new FurnishingStatus(parsedIsTrench);
        }
        #endregion

        #region IDeeplyCloneable Interface
        /// <summary>
        /// Creates a new instance that is a deep copy of the current instance.
        /// </summary>
        /// <returns>A new instance with the same status as the current instance.</returns>
        public override T DeepClone<T>()
            => new FurnishingStatus(IsOpen) as T;
        #endregion

        #region Utilities
        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="FurnishingStatus"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => IsOpen
                ? "open"
                : "closed";
        #endregion
    }
}
