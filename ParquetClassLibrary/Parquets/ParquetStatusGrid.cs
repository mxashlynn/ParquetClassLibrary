using System.Collections;
using System.Collections.Generic;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Parquets
{
    /// <summary>
    /// A square, two-dimensional collection of <see cref="ParquetStatus"/>es for use in <see cref="MapParent"/> and derived classes.
    /// The intent is that this class function much like a read-only array.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming",
        "CA1710:Identifiers should have correct suffix",
        Justification = "Grid is a custom suffix implying Collection.  See https://github.com/dotnet/roslyn-analyzers/issues/3072")]
    public class ParquetStatusGrid : IGrid<ParquetStatus>
    {
        /// <summary>The backing collection of <see cref="ParquetStatus"/>es.</summary>
        private ParquetStatus[,] ParquetStatuses { get; }

        /// <summary>Gets the number of elements in the Y dimension of the <see cref="ParquetStatusGrid"/>.</summary>
        public int Rows => ParquetStatuses?.GetLength(0) ?? 0;

        /// <summary>Gets the number of elements in the X dimension of the <see cref="ParquetStatusGrid"/>.</summary>
        public int Columns => ParquetStatuses?.GetLength(1) ?? 0;

        /// <summary>The total number of parquets collected.</summary>
        public int Count => Rows * Columns;

        /// <summary>
        /// Initializes a new <see cref="ParquetStatusGrid"/>.
        /// </summary>
        /// <param name="inRows">The length of the Y dimension of the collection.</param>
        /// <param name="inColumns">The length of the X dimension of the collection.</param>
        public ParquetStatusGrid(int inRows, int inColumns)
            => ParquetStatuses = new ParquetStatus[inRows, inColumns];

        /// <summary>
        /// Determines if the given position corresponds to a point within the collection.
        /// </summary>
        /// <param name="inPosition">The position to validate.</param>
        /// <returns><c>true</c>, if the position is valid, <c>false</c> otherwise.</returns>
        public bool IsValidPosition(Vector2D inPosition)
            => ParquetStatuses.IsValidPosition(inPosition);

        /// <summary>Access to any <see cref="ParquetStatus"/> in the grid.</summary>
        public ref ParquetStatus this[int y, int x]
        {
            get => ref ParquetStatuses[y, x];
        }

        /// <summary>
        /// Exposes an <see cref="IEnumerator{ParquetStatus}"/>, which supports simple iteration.
        /// </summary>
        /// <returns>An enumerator.</returns>
        IEnumerator<ParquetStatus> IEnumerable<ParquetStatus>.GetEnumerator()
            => (IEnumerator<ParquetStatus>)ParquetStatuses.GetEnumerator();

        /// <summary>
        /// Exposes an enumerator for the <see cref="ParquetStatusGrid"/>, which supports simple iteration.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator GetEnumerator()
            => ParquetStatuses.GetEnumerator();
    }
}