﻿using System.Collections.Generic;
using Xunit;
using System;
using ParquetClassLibrary;
using ParquetClassLibrary.Parquets;
using ParquetClassLibrary.Utilities;
using ParquetClassLibrary.Rooms;

namespace ParquetUnitTests.Rooms
{
    public class RoomRecipeUnitTest
    {
        #region Test Values
        private static readonly ParquetStack TestWall = new ParquetStack(TestEntities.TestFloor.ID, TestEntities.TestBlock.ID, EntityID.None, EntityID.None);

        private static readonly ParquetStack TestWalk = new ParquetStack(TestEntities.TestFloor.ID, EntityID.None, EntityID.None, EntityID.None);

        private static readonly ParquetStack TestEntry = new ParquetStack(TestEntities.TestFloor.ID, EntityID.None, TestEntities.TestFurnishing.ID, EntityID.None);

        private static readonly HashSet<MapSpace> TestPerimeter = new HashSet<MapSpace>
        {
            new MapSpace(0, 0, TestWall),
            new MapSpace(1, 0, TestWall),
            new MapSpace(2, 0, TestWall),
            new MapSpace(3, 0, TestWall),
            new MapSpace(0, 1, TestWall),
            new MapSpace(3, 1, TestWall),
            new MapSpace(0, 2, TestWall),
            new MapSpace(3, 2, TestWall),
            new MapSpace(0, 3, TestWall),
            new MapSpace(1, 3, TestWall),
            new MapSpace(2, 3, TestWall),
            new MapSpace(3, 3, TestWall),
        };

        private static readonly HashSet<MapSpace> TestWalkableArea = new HashSet<MapSpace>
        {
            new MapSpace(1, 1, TestWalk),
            new MapSpace(2, 1, TestWalk),
            new MapSpace(1, 2, TestWalk),
            new MapSpace(2, 2, TestEntry),
        };

        private static readonly List<RecipeElement> TestRequiredFurnishings = TestEntities.TestRecipeElementList;

        private static readonly RoomRecipe MinimalRecipe =
            new RoomRecipe(-All.RoomRecipeIDs.Minimum, "Minimal Room Recipe", "", "", TestRequiredFurnishings);

        private static readonly Room MinimalRoom = new Room(TestWalkableArea, TestPerimeter);
        #endregion

        [Fact]
        internal void NullRequiredFurnishingsThrowsTest()
        {
            void NullRequiredFurnishings()
            {
                var _ = new RoomRecipe(-All.RoomRecipeIDs.Minimum, "will fail", "", "", null);
            }

            Assert.Throws<ArgumentNullException>(NullRequiredFurnishings);
        }

        [Fact]
        internal void EmptyRequiredFurnishingsThrowsTest()
        {
            void EmptyRequiredFurnishings()
            {
                var _ = new RoomRecipe(-All.RoomRecipeIDs.Minimum, "will fail", "", "",
                                       new List<RecipeElement>());
            }

            Assert.Throws<IndexOutOfRangeException>(EmptyRequiredFurnishings);
        }

        [Fact]
        internal void MinimumWalkableSpacesBelowGlobalMinimumThrowsTest()
        {
            var BadMinimum = Rules.Recipes.Room.MinWalkableSpaces - 1;

            void HasBadRequiredBlocks()
            {
                var _ = new RoomRecipe(-All.RoomRecipeIDs.Minimum, "will fail", "", "",
                                       TestRequiredFurnishings, BadMinimum);
            }

            Assert.Throws<ArgumentOutOfRangeException>(HasBadRequiredBlocks);
        }

        [Fact]
        internal void MinimumWalkableSpacesAboveGlobalMaximumThrowsTest()
        {
            var BadMinimum = Rules.Recipes.Room.MaxWalkableSpaces + 1;

            void HasBadRequiredBlocks()
            {
                var _ = new RoomRecipe(-All.RoomRecipeIDs.Minimum, "will fail", "", "",
                                       TestRequiredFurnishings, BadMinimum);
            }

            Assert.Throws<ArgumentOutOfRangeException>(HasBadRequiredBlocks);
        }

        [Fact]
        internal void StricterRoomRequirementsGenerateHigherPriorityTest()
        {
            var stricterRecipe = TestEntities.TestRoomRecipe;

            Assert.True(MinimalRecipe.Priority < stricterRecipe.Priority);
        }

        [Fact]
        internal void KnownMismatchReturnsFalse()
        {
            var stricterRecipe = TestEntities.TestRoomRecipe;

            Assert.False(stricterRecipe.Matches(MinimalRoom));
        }

        [Fact]
        internal void KnownMatchReturnsTrue()
        {
            Assert.True(MinimalRecipe.Matches(MinimalRoom));
        }
    }
}