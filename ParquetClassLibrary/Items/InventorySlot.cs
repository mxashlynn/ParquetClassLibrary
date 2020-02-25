using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Items
{
    /// <summary>
    /// Allows multiple copies of a given <see cref="ItemModel"/>
    /// to be grouped together in an <see cref="Inventory"/>.
    /// </summary>
    public class InventorySlot : ITypeConverter
    {
        #region Class Defaults
        /// <summary>
        /// Stack maximum assumed when none is defined.
        /// </summary>
        public const int DefaultStackMax = 999;
        #endregion

        #region Characteristics
        /// <summary>What <see cref="ItemModel"/>s are stored in this slot.</summary>
        public ModelID ItemID { get; }

        /// <summary>How many instances of the items are stores in this slot.</summary>
        public int Count { get; private set; }

        /// <summary>How many of the item may share this slow, cached.</summary>
        private readonly int StackMax;
        #endregion

        #region Initialization
        /// <summary>
        /// Creates a sham slot for serialization purposes.
        /// </summary>
        public InventorySlot() : this(ItemModel.ShamModel.ID) { }

        /// <summary>
        /// Creates a new slot to store the given item type.
        /// </summary>
        /// <param name="inItemToStore">
        /// The <see cref="ModelID"/> corresponding to the item being stored here.
        /// Must be in-range and not <see cref="ModelID.None"/>.
        /// </param>
        /// <param name="inHowMany">How many of the item to store initially.  Must be positive.</param>
        public InventorySlot(ModelID inItemToStore, int inHowMany = 1)
        {
            Precondition.IsNotNone(inItemToStore, nameof(inItemToStore));
            Precondition.IsInRange(inItemToStore, All.ItemIDs, nameof(inItemToStore));
            Precondition.MustBePositive(inHowMany, nameof(inHowMany));

            if (inItemToStore == ModelID.None)
            {
                throw new ArgumentException($"Tried to create {nameof(InventorySlot)} for {nameof(ModelID.None)}.");
            }

            ItemID = inItemToStore;
            Count = inHowMany;
            StackMax = All.Items?.Get<ItemModel>(inItemToStore)?.StackMax ?? DefaultStackMax;
        }
        #endregion

        #region Access
        /// <summary>
        /// Increases the number of items stored by the given amount.
        /// </summary>
        /// <param name="inHowMany">How many of the item to give.  Must be positive.</param>
        /// <returns>The number of items still needing to be stored if this stack is full.</returns>
        public int Give(int inHowMany = 1)
        {
            Precondition.MustBePositive(inHowMany, nameof(inHowMany));

            var remainder = 0;
            var total = Count + inHowMany;
            if (total > StackMax)
            {
                Count = StackMax;
                remainder = total - StackMax;
            }
            else
            {
                Count += inHowMany;
            }

            return remainder;
        }

        /// <summary>
        /// Decreases the number of items stored by the given amount.
        /// </summary>
        /// <param name="inHowMany">How many of the item to take.  Must be positive.</param>
        /// <returns>
        /// The number of items still needing to be removed from another
        /// <see cref="InventorySlot"/> once this one is emptied.
        /// </returns>
        public int Take(int inHowMany = 1)
        {
            Precondition.MustBePositive(inHowMany, nameof(inHowMany));

            var remainder = 0;
            var needed = Count - inHowMany;
            if (needed < 0)
            {
                Count = 0;
                // Return the remainder as a positive number.
                remainder = needed * -1;
            }
            else
            {
                Count -= inHowMany;
            }

            return remainder;
        }
        #endregion

        #region ITypeConverter Implementation
        /// <summary>Allows the converter to construct itself statically.</summary>
        internal static InventorySlot ConverterFactory { get; } = new InventorySlot();

        /// <summary>
        /// Converts the given <see cref="object"/> to a <see cref="string"/> for serialization.
        /// </summary>
        /// <param name="inValue">The instance to convert.</param>
        /// <param name="inRow">The current context and configuration.</param>
        /// <param name="inMemberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance serialized.</returns>
        public string ConvertToString(object inValue, IWriterRow inRow, MemberMapData inMemberMapData)
            => inValue is InventorySlot slot
            && null != slot
                ? $"{slot.ItemID}{Rules.Delimiters.InternalDelimiter}" +
                  $"{slot.Count}"
                : throw new ArgumentException($"Could not serialize '{inValue}' as {nameof(InventorySlot)}.");

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
                || string.Compare(nameof(ModelID.None), inText, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                throw new ArgumentException($"Could not convert '{inText}' to {nameof(InventorySlot)}.");
            }

            try
            {
                var numberStyle = inMemberMapData?.TypeConverterOptions?.NumberStyle ?? All.SerializedNumberStyle;
                var cultureInfo = inMemberMapData?.TypeConverterOptions?.CultureInfo ?? All.SerializedCultureInfo;
                var parameterText = inText.Split(Rules.Delimiters.InternalDelimiter);

                var id = (ModelID)ModelID.ConverterFactory.ConvertFromString(parameterText[0], inRow, inMemberMapData);
                var count = int.Parse(parameterText[1], numberStyle, cultureInfo);

                return new InventorySlot(id, count);
            }
            catch (Exception e)
            {
                throw new FormatException($"Could not parse '{inText}' as {nameof(InventorySlot)}: {e}");
            }
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Returns a <see langword="string"/> that represents the current <see cref="InventorySlot"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => $"{Count} {All.Items.Get<ItemModel>(ItemID)?.Name}";
        #endregion
    }
}