using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Parquet
{
    /// <summary>
    /// A simple representation of two coordinate integers, tailored for Parquet's needs.
    /// </summary>
    // TODO [API] Perhaps this should be renamed Point2D, to follow the standard used by System.Drawing and MonoGame (but not clash with them as they lack the 2D).
    public readonly struct Vector2D : IEquatable<Vector2D>, ITypeConverter
    {
        #region Class Defaults
        /// <summary>The zero vector.</summary>
        public static readonly Vector2D Zero = new Vector2D(0, 0);

        /// <summary>The unit vector.</summary>
        public static readonly Vector2D Unit = new Vector2D(1, 1);

        /// <summary>The vector offset to the North.</summary>
        public static readonly Vector2D North = new Vector2D(0, -1);

        /// <summary>The vector offset to the South.</summary>
        public static readonly Vector2D South = new Vector2D(0, 1);

        /// <summary>The vector offset to the East.</summary>
        public static readonly Vector2D East = new Vector2D(1, 0);

        /// <summary>The vector offset to the West.</summary>
        public static readonly Vector2D West = new Vector2D(-1, 0);
        #endregion

        #region Characteristics
        /// <summary>Offset from origin in x.</summary>
        public int X { get; }

        /// <summary>Offset from origin in y.</summary>
        public int Y { get; }

        /// <summary>Provides the magnitude of the vector as an integer, rounded-down.</summary>
        public int Magnitude { get; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="inX">The X coordinate.</param>
        /// <param name="inY">The Y coordinate.</param>
        public Vector2D(int inX, int inY)
        {
            X = inX;
            Y = inY;
            Magnitude = Convert.ToInt32(Math.Floor(Math.Sqrt((X * X) + (Y * Y))));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="inCoordinates">The coordinates.</param>
        public Vector2D((int X, int Y) inCoordinates)
            : this(inCoordinates.X, inCoordinates.Y)
        { }
        #endregion

        #region Vector Math
        /// <summary>
        /// Sums the given vectors.
        /// </summary>
        /// <param name="inVector1">First operand.</param>
        /// <param name="inVector2">Second operand.</param>
        /// <returns>A vector representing the sum of the given vectors.</returns>
        public static Vector2D operator +(Vector2D inVector1, Vector2D inVector2)
            => new Vector2D(inVector1.X + inVector2.X, inVector1.Y + inVector2.Y);

        /// <summary>
        /// Finds the difference between the given vectors.
        /// </summary>
        /// <param name="inVector1">First operand.</param>
        /// <param name="inVector2">Second operand.</param>
        /// <returns>A vector representing the difference of the given vectors.</returns>
        public static Vector2D operator -(Vector2D inVector1, Vector2D inVector2)
            => new Vector2D(inVector1.X - inVector2.X, inVector1.Y - inVector2.Y);

        /// <summary>
        /// Scales a vector.
        /// </summary>
        /// <param name="inScalar">The scalar.</param>
        /// <param name="inVector">The vector.</param>
        /// <returns>A scaled vector.</returns>
        public static Vector2D operator *(int inScalar, Vector2D inVector)
            => new Vector2D(inScalar * inVector.X, inScalar * inVector.Y);
        #endregion

        #region IEquatable Implementation
        /// <summary>
        /// Serves as a hash function for a <see cref="Vector2D"/> struct.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures.</returns>
        public override int GetHashCode()
            => (X, Y).GetHashCode();

        /// <summary>
        /// Determines whether the specified <see cref="Vector2D"/> is equal to the current <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="inVector">The <see cref="Vector2D"/> to compare with the current.</param>
        /// <returns><c>true</c> if the <see cref="Vector2D"/>s are equal.</returns>
        public bool Equals(Vector2D inVector)
            => X == inVector.X
            && Y == inVector.Y;

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Vector2D"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="Vector2D"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
            => obj is Vector2D vector
            && Equals(vector);

        /// <summary>
        /// Determines whether a specified instance of <see cref="Vector2D"/> is equal to
        /// another specified instance of <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="inVector1">The first <see cref="Vector2D"/> to compare.</param>
        /// <param name="inVector2">The second <see cref="Vector2D"/> to compare.</param>
        /// <returns><c>true</c> if the two operands are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Vector2D inVector1, Vector2D inVector2)
            => inVector1.Equals(inVector2);

        /// <summary>
        /// Determines whether a specified instance of <see cref="Vector2D"/> is not equal
        /// to another specified instance of <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="inVector1">The first <see cref="Vector2D"/> to compare.</param>
        /// <param name="inVector2">The second <see cref="Vector2D"/> to compare.</param>
        /// <returns><c>true</c> if the two operands are NOT equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Vector2D inVector1, Vector2D inVector2)
            => !inVector1.Equals(inVector2);
        #endregion

        #region ITypeConverter Implementation
        /// <summary>Allows the converter to construct itself statically.</summary>
        internal static Vector2D ConverterFactory { get; } = Zero;

        /// <summary>
        /// Converts the given <see cref="object"/> to a <see cref="string"/> for serialization.
        /// </summary>
        /// <param name="inValue">The instance to convert.</param>
        /// <param name="inRow">The current context and configuration.</param>
        /// <param name="inMemberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance serialized.</returns>
        public string ConvertToString(object inValue, IWriterRow inRow, MemberMapData inMemberMapData)
            => inValue is Vector2D vector
                ? $"{vector.X}{Delimiters.ElementDelimiter}" +
                  $"{vector.Y}"
                : Logger.DefaultWithConvertLog(inValue?.ToString() ?? "null", nameof(Vector2D), nameof(Zero));

        /// <summary>
        /// Converts the given <see cref="string"/> to an <see cref="object"/> as deserialization.
        /// </summary>
        /// <param name="inText">The text to convert.</param>
        /// <param name="inRow">The current context and configuration.</param>
        /// <param name="inMemberMapData">Mapping info for a member to a CSV field or property.</param>
        /// <returns>The given instance deserialized.</returns>
        public object ConvertFromString(string inText, IReaderRow inRow, MemberMapData inMemberMapData)
        {
            if (string.IsNullOrEmpty(inText)
                || string.Compare(nameof(Zero), inText, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return Zero;
            }

            var parameterText = inText.Split(Delimiters.ElementDelimiter);

            var x = int.TryParse(parameterText[0], All.SerializedNumberStyle, CultureInfo.InvariantCulture, out var temp1)
                ? temp1
                : Logger.DefaultWithParseLog(parameterText[0], nameof(X), Zero.X);
            var y = int.TryParse(parameterText[1], All.SerializedNumberStyle, CultureInfo.InvariantCulture, out var temp2)
                ? temp2
                : Logger.DefaultWithParseLog(parameterText[1], nameof(Y), Zero.Y);

            return new Vector2D(x, y);
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Deconstructs the current <see cref="Vector2D"/> into its constituent coordinates.
        /// </summary>
        /// <param name="outX">The X coordinate.</param>
        /// <param name="outY">The Y coordinate.</param>
        public void Deconstruct(out int outX, out int outY)
            => (outX, outY) = (X, Y);

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="Vector2D"/>.
        /// </summary>
        /// <returns>The representation.</returns>
        public override string ToString()
            => $"({X}, {Y})";
        #endregion
    }
}
