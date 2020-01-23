using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using ParquetClassLibrary.Utilities;

namespace ParquetClassLibrary.Serialization.Converters
{
    /// <summary>
    /// Type converter for any collection that implements <see cref="IGrid"/>.
    /// </summary>
    /// <typeparam name="TElement">The type collected.</typeparam>
    /// <typeparam name="TGrid">The type of the collection.</typeparam>
    public class GridConverter<TElement, TGrid> : DefaultTypeConverter
        where TGrid : IGrid<TElement>, new()
        where TElement : ITypeConverter, new()
    {
        /// <summary>
        /// Converts the given record column to a 2D collection.
        /// </summary>
        /// <param name="inText">The record column to convert to an object.</param>
        /// <param name="inRow">The <see cref="IReaderRow"/> for the current record.</param>
        /// <param name="inMemberMapData">The <see cref="MemberMapData"/> for the member being created.</param>
        /// <returns>The <see cref="IGrid{TInner}"/> created from the record column.</returns>
        public override object ConvertFromString(string inText, IReaderRow inRow, MemberMapData inMemberMapData)
        {
            Precondition.IsNotNullOrEmpty(inText);

            var grid = new TGrid();
            var elementFactory = new TElement();
            if (string.IsNullOrEmpty(inText))
            {
                return grid;
            }

            var textCollection = inText.Split(Serializer.SecondaryDelimiter);
            var textCollectionEnumerator = textCollection.GetEnumerator();
            for (var y = 0; y < grid.Rows; y++)
            {
                for (var x = 0; x < grid.Columns; x++)
                {
                    if (!textCollectionEnumerator.MoveNext())
                    {
                        return grid;
                    }

                    var currentText = (string)textCollectionEnumerator.Current;
                    grid[x, y] = (TElement)elementFactory.ConvertFromString(currentText, inRow, inMemberMapData);
                }
            }
            return grid;
        }
    }
}
