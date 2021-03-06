using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Parquet;
using Xunit;

namespace ParquetUnitTests
{
    /// <summary>
    /// Unit tests <see cref="Precondition"/>.
    /// </summary>
    public class PreconditionUnitTest
    {
        #region Test Values
        /// <summary>Used when testing <see cref="Precondition.IsOfType{TToCheck, TTarget}(string)"/>.</summary>
        internal class BaseType { };

        /// <summary>Used when testing <see cref="Precondition.IsOfType{TToCheck, TTarget}(string)"/>.</summary>
        [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes",
            Justification = "The type itself is used in the test.")]
        internal class DerivedType : BaseType { };
        #endregion

        [Fact]
        internal void ModelIsInRangeTest()
        {
            var testID = (ModelID)6;
            var testRange = new Range<ModelID>(0, 10);

            var exception = Record.Exception(() => Precondition.IsInRange(testID, testRange, nameof(testID)));

            Assert.Null(exception);
        }

        [Fact]
        internal void RangeIsInRangeTest()
        {
            var innerRange = new Range<ModelID>(1, 9);
            var outterRange = new Range<ModelID>(0, 10);

            var exception = Record.Exception(() => Precondition.IsInRange(innerRange, outterRange, nameof(innerRange)));

            Assert.Null(exception);
        }

        [Fact]
        internal void ModelIsInRangeCollectionTest()
        {
            var testID = (ModelID)7;
            var testCollection = new List<Range<ModelID>> { new Range<ModelID>(0, 5), new Range<ModelID>(6, 10) };

            var exception = Record.Exception(() => Precondition.IsInRange(testID, testCollection, nameof(testID)));

            Assert.Null(exception);
        }

        [Fact]
        internal void RangeIsInRangeCollectionTest()
        {
            var innerRange = new Range<ModelID>(1, 4);
            var testCollection = new List<Range<ModelID>> { new Range<ModelID>(0, 5), new Range<ModelID>(6, 10) };

            var exception = Record.Exception(() => Precondition.IsInRange(innerRange, testCollection, nameof(innerRange)));

            Assert.Null(exception);
        }

        [Fact]
        internal void ModelIsInRangeThrowsOnOutOfRangeTest()
        {
            var testID = (ModelID)12;
            var testRange = new Range<ModelID>(0, 10);

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.IsInRange(testID, testRange, nameof(testID)));
        }

        [Fact]
        internal void RangeIsInRangeThrowsOnOutOfRangeTest()
        {
            var innerRange = new Range<ModelID>(0, 10);
            var outterRange = new Range<ModelID>(1, 9);

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.IsInRange(innerRange, outterRange, nameof(innerRange)));
        }

        [Fact]
        internal void ModelIsInRangeCollectionThrowsOnOutOfRangeTest()
        {
            var testID = (ModelID)12;
            var testCollection = new List<Range<ModelID>> { new Range<ModelID>(0, 5), new Range<ModelID>(6, 10) };

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.IsInRange(testID, testCollection, nameof(testID)));
        }

        [Fact]
        internal void RangeIsInRangeCollectionThrowsOnOutOfRangeTest()
        {
            var innerRange = new Range<ModelID>(0, 10);
            var testCollection = new List<Range<ModelID>> { new Range<ModelID>(1, 5), new Range<ModelID>(6, 9) };

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.IsInRange(innerRange, testCollection, nameof(innerRange)));
        }

        [Fact]
        internal void IsOfTypeTest()
        {
            var exception = Record.Exception(() => Precondition.IsOfType<DerivedType, BaseType>());

            Assert.Null(exception);
        }

        [Fact]
        internal void IsOfTypeThrowsOnNotSubtypeTest()
        {
            Assert.Throws<InvalidCastException>(() => Precondition.IsOfType<BaseType, DerivedType>());
        }

        [Fact]
        internal void IsOfTypeSystemTest()
        {
            var exception = Record.Exception(() => Precondition.IsOfType<string, object>());

            Assert.Null(exception);
        }

        [Fact]
        internal void IsOfTypeThrowsOnNotSubtypeSystemTest()
        {
            Assert.Throws<InvalidCastException>(() => Precondition.IsOfType<object, string>());
        }

        [Fact]
        internal void IsOfTypeThrowsOnNotSubtypeValueTypeTest()
        {
            Assert.Throws<InvalidCastException>(() => Precondition.IsOfType<float, int>());
        }

        [Fact]
        internal void AreInRangeTest()
        {
            var testIDCollection = new List<ModelID> { 0, 1, 5, 6, 9, 10 };
            var testRange = new Range<ModelID>(0, 10);

            var exception = Record.Exception(() => Precondition.AreInRange(testIDCollection, testRange, nameof(testIDCollection)));

            Assert.Null(exception);
        }

        [Fact]
        internal void AreInRangeCollectionTest()
        {
            var testIDCollection = new List<ModelID> { 0, 1, 2, 7, 8, 10 };
            var testCollection = new List<Range<ModelID>> { new Range<ModelID>(0, 5), new Range<ModelID>(6, 10) };

            var exception = Record.Exception(() => Precondition.AreInRange(testIDCollection, testCollection, nameof(testIDCollection)));

            Assert.Null(exception);
        }

        [Fact]
        internal void AreInRangeThrowsOnSingleOutOfRangeTest()
        {
            var testIDCollection = new List<ModelID> { 0, 1, 5, 9, 10, 20 };
            var testRange = new Range<ModelID>(0, 10);

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.AreInRange(testIDCollection, testRange, nameof(testIDCollection)));
        }

        [Fact]
        internal void AreInRangeThrowsOnSingleOutOfRangeCollectionTest()
        {
            var testIDCollection = new List<ModelID> { 0, 1, 3, 8, 10, 20 };
            var testCollection = new List<Range<ModelID>> { new Range<ModelID>(0, 5), new Range<ModelID>(6, 10) };

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.AreInRange(testIDCollection, testCollection, nameof(testIDCollection)));
        }

        [Fact]
        internal void IsNotNoneTest()
        {
            var testValue0 = int.MaxValue;
            var testValue1 = int.MinValue;
            var testValue2 = 1;

            var exception0 = Record.Exception(() => Precondition.IsNotNone(testValue0));
            var exception1 = Record.Exception(() => Precondition.IsNotNone(testValue1));
            var exception2 = Record.Exception(() => Precondition.IsNotNone(testValue2));

            Assert.Null(exception0);
            Assert.Null(exception1);
            Assert.Null(exception2);
        }

        [Fact]
        internal void IsNotNoneThrowsOnNoneTest()
        {
            var testValue = ModelID.None;

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.IsNotNone(testValue));
        }

        [Fact]
        internal void MustBeNonNegativeTest()
        {
            var testValue0 = 0;
            var testValue1 = 1;

            var exception0 = Record.Exception(() => Precondition.MustBeNonNegative(testValue0));
            var exception1 = Record.Exception(() => Precondition.MustBeNonNegative(testValue1));

            Assert.Null(exception0);
            Assert.Null(exception1);
        }

        [Fact]
        internal void MustBeNonNegativeThrowsOnNegativeTest()
        {
            var testValue = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.MustBeNonNegative(testValue));
        }


        [Fact]
        internal void MustBePositiveTest()
        {
            var testValue = 1;

            var exception = Record.Exception(() => Precondition.MustBePositive(testValue));

            Assert.Null(exception);
        }

        [Fact]
        internal void MustBePositiveThrowsOnZeroTest()
        {
            var testValue = 0;

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.MustBePositive(testValue));
        }

        [Fact]
        internal void MustBePositiveThrowsOnNegativeTest()
        {
            var testValue = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => Precondition.MustBePositive(testValue));
        }

        [Fact]
        internal void IsNotEmptyTest()
        {
            var testValue = new List<ModelID> { 0, 1, 2 };

            var exception = Record.Exception(() => Precondition.IsNotNullOrEmpty(testValue, nameof(testValue)));

            Assert.Null(exception);
        }


        [Fact]
        internal void IsNotEmptyStringTest()
        {
            var testValue = "will pass";

            var exception = Record.Exception(() => Precondition.IsNotNullOrEmpty(testValue, nameof(testValue)));

            Assert.Null(exception);
        }

        [Fact]
        internal void IsNotEmptyThrowsOnEmptyTest()
        {
            var testValue = new List<ModelID>();

            Assert.Throws<IndexOutOfRangeException>(() => Precondition.IsNotNullOrEmpty(testValue, nameof(testValue)));
        }

        [Fact]
        internal void IsNotEmptyStringThrowsOnEmptyStringTest()
        {
            var testValue = "";

            Assert.Throws<IndexOutOfRangeException>(() => Precondition.IsNotNullOrEmpty(testValue, nameof(testValue)));
        }

        [Fact]
        internal void IsNullTest()
        {
            var testValue = "will pass";

            var exception = Record.Exception(() => Precondition.IsNotNull(testValue));

            Assert.Null(exception);
        }

        [Fact]
        internal void IsNullThrowsOnNullTest()
        {
            object testValue = null;

            Assert.Throws<ArgumentNullException>(() => Precondition.IsNotNull(testValue));
        }
    }
}
