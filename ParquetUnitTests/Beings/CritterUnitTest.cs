using System;
using ParquetClassLibrary;
using ParquetClassLibrary.Beings;
using Xunit;

namespace ParquetUnitTests.Beings
{
    public class CritterUnitTest
    {
        #region Test Values
        /// <summary>Identifier used when creating a new block.</summary>
        private static readonly ModelID newCritterID = TestModels.TestCritter.ID - 1;
        #endregion

        [Fact]
        public void ValidCritterIDsArePermittedTest()
        {
            var newCritter = new CritterModel(newCritterID, "will be created", "", "",
                                         All.BiomeIDs.Minimum, All.ScriptIDs.Minimum);

            Assert.NotNull(newCritter);
        }

        [Fact]
        public void InvalidCritterIDsRaiseExceptionTest()
        {
            var badCritterID = TestModels.TestBlock.ID - 1;

            void TestCode()
            {
                var _ = new CritterModel(badCritterID, "will fail", "", "",
                                    All.BiomeIDs.Minimum, All.ScriptIDs.Minimum);
            }

            Assert.Throws<ArgumentOutOfRangeException>(TestCode);
        }
    }
}
