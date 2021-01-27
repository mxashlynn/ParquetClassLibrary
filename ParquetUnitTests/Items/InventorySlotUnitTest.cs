using System;
using Parquet;
using Parquet.Items;
using Xunit;

namespace ParquetUnitTests.Items
{
    public class InventorySlotUnitTest
    {
        #region Test Values
        private const int None = 0;
        private const int One = 1;
        private const int Some = 10;
        #endregion

        [Fact]
        public void CannotStoreSomeOfNothingTest()
        {
            static void TestCode()
            {
                var _ = new InventorySlot(ModelID.None, Some);
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void CannotStoreNoneOfSomethingTest()
        {
            static void TestCode()
            {
                var _ = new InventorySlot(TestModels.TestItem1.ID, None);
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }

        [Fact]
        public void CannotStoreNonItemTest()
        {
            static void TestCode()
            {
                var _ = new InventorySlot(TestModels.TestBlock.ID, Some);
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }


        [Fact]
        public void CountMatchesItemsStoredTest()
        {
            var slot = new InventorySlot(TestModels.TestItem1.ID, Some);

            Assert.Equal(Some, slot.Count);
        }

        [Fact]
        public void SlotAddTest()
        {
            var slot = new InventorySlot(TestModels.TestItem1.ID, One);
            var remainder1 = 0;
            var remainder2 = One + Some;

            Assert.Equal(remainder1, slot.Give(Some));
            Assert.Equal(remainder2, slot.Give(TestModels.TestItem1.StackMax));
        }

        [Fact]
        public void SlotRemoveTest()
        {
            var slot = new InventorySlot(TestModels.TestItem1.ID, Some);
            var remainder1 = 0;
            var remainder2 = TestModels.TestItem1.StackMax - (Some - One);

            Assert.Equal(remainder1, slot.Take(One));
            Assert.Equal(remainder2, slot.Take(TestModels.TestItem1.StackMax));
        }
    }
}
