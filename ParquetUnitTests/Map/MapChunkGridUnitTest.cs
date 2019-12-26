using System;
using ParquetClassLibrary.Map;
using ParquetClassLibrary.Utilities;
using Xunit;

namespace ParquetUnitTests.Map
{
    public class MapChunkGridUnitTest
    {
        #region Values for Tests
        private static readonly Vector2D invalidPosition = new Vector2D(-1, -1);
        private static readonly PCLColor testColor = new PCLColor(255, 128, 26, 230);
        private const string testTitle = "Test Region";
        private const int testElevation = 4;
        private static readonly Guid testID = Guid.Parse("ead51b96-21d5-4619-86e9-462a52564089");
        #endregion

        #region Initialization
        [Fact]
        public void NewDefaultMapChunkGridTest()
        {
            var defaultGrid = new MapChunkGrid();

            Assert.Equal(MapRegion.DefaultTitle, defaultGrid.Title);
            Assert.Equal(MapRegion.DefaultColor, defaultGrid.Background);
        }

        [Fact]
        public void NewNullMapChunkGridTest()
        {
            var nulledGrid = new MapChunkGrid(null);

            Assert.Equal(MapRegion.DefaultTitle, nulledGrid.Title);
            Assert.Equal(MapRegion.DefaultColor, nulledGrid.Background);
        }

        [Fact]
        public void NewCustomMapChunkGridTest()
        {
            var customRegion = new MapChunkGrid(testTitle, testColor, testElevation, testID);

            Assert.Equal(testTitle, customRegion.Title);
            Assert.Equal(testColor, customRegion.Background);
            Assert.Equal(testElevation, customRegion.GlobalElevation);
            Assert.Equal(testID, customRegion.RegionID);
        }
        #endregion

        #region Chunk Methods
        [Fact]
        public void SetChunkFailsOnInvalidPositionTest()
        {
            var grid = new MapChunkGrid();

            var wasSet = grid.SetChunk(ChunkType.Handmade, ChunkOrientation.EastWest, invalidPosition);

            Assert.False(wasSet);
        }

        [Fact]
        public void SetChunkSucceedsOnOriginPositionTest()
        {
            var grid = new MapChunkGrid();
            var chunkType = ChunkType.Handmade;
            var chunkOrientation = ChunkOrientation.EastWest;

            var wasSet = grid.SetChunk(chunkType, chunkOrientation, Vector2D.Zero);

            Assert.True(wasSet);
        }

        [Fact]
        public void GetChunkFailsOnInvalidPositionTest()
        {
            var grid = new MapChunkGrid();

            var chunkData = grid.GetChunk(invalidPosition);

            Assert.Equal(ChunkType.Empty, chunkData.type);
            Assert.Equal(ChunkOrientation.None, chunkData.orientation);
        }

        [Fact]
        public void GetChunkSucceedsOnOriginPositionTest()
        {
            var grid = new MapChunkGrid();
            var chunkType = ChunkType.Handmade;
            var chunkOrientation = ChunkOrientation.EastWest;

            var wasSet = grid.SetChunk(chunkType, chunkOrientation, Vector2D.Zero);

            var chunkData = grid.GetChunk(Vector2D.Zero);

            Assert.True(wasSet);
            Assert.Equal(chunkType, chunkData.type);
            Assert.Equal(chunkOrientation, chunkData.orientation);
        }
        #endregion
    }
}