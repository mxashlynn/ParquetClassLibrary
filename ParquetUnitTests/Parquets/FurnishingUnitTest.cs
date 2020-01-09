using System;
using ParquetClassLibrary;
using ParquetClassLibrary.Parquets;
using Xunit;

namespace ParquetUnitTests.Parquets
{
    public class FurnishingUnitTest
    {
        #region Test Values
        /// <summary>Identifier used when creating a new furnishing.</summary>
        private static readonly EntityID newFurnishingID = TestModels.TestFurnishing.ID - 1;
        #endregion

        [Fact]
        public void ValidCollectibleIDsArePermittedTest()
        {
            var testFurnishing = new Furnishing(newFurnishingID, "will be created", "", "");

            Assert.NotNull(testFurnishing);
        }

        [Fact]
        public void InvalidCollectibleIDsRaiseExceptionTest()
        {
            var badFurnishingID = TestModels.TestBlock.ID;

            void TestCode()
            {
                var _ = new Furnishing(badFurnishingID, "will fail", "", "");
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void ValidItemIDsArePermittedTest()
        {
            EntityID goodItemID = -All.ItemIDs.Minimum;

            var testBlock = new Furnishing(newFurnishingID, "will be created", "", "", goodItemID);

            Assert.NotNull(testBlock);
        }

        [Fact]
        public void InvalidItemIDsRaiseExceptionTest()
        {
            var badItemID = TestModels.TestBlock.ID;

            void TestCode()
            {
                var _ = new Furnishing(newFurnishingID, "will fail", "", "", badItemID);
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void ValidSwapIDsArePermittedTest()
        {
            var goodSwapID = newFurnishingID - 1;

            var testBlock = new Furnishing(newFurnishingID, "will be created", "", "", inSwapID: goodSwapID);

            Assert.NotNull(testBlock);
        }

        [Fact]
        public void InvalidSwapIDsRaiseExceptionTest()
        {
            var badSwapID = TestModels.TestBlock.ID;

            void TestCode()
            {
                var _ = new Furnishing(newFurnishingID, "will fail", "", "", inSwapID: badSwapID);
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }
    }
}
