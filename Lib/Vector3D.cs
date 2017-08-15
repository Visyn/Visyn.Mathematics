using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Visyn.Geometry;
using Visyn.JetBrains;

namespace Visyn.Mathematics
{
    /// <summary>
    /// 3D Vector structure with X, Y and Z coordinates.
    /// </summary>
    /// <remarks><para>The structure encapsulates X, Y and Z coordinates of a 3D vector and
    /// provides some operations with it.</para></remarks>
    public struct Vector3D : IPoint3D, IReadOnlyList<double>, IEqualityComparer<Vector3D>, IEqualsToleranced<IPoint3D>
    {
        [NotNull]
        [XmlIgnore]
        private readonly double[] _value;

        /// <summary>
        /// X coordinate of the vector.
        /// </summary>
        [DataMember(Order = 1)]
        public double X { get; }

        /// <summary>
        /// Y coordinate of the vector.
        /// </summary>
        [DataMember(Order = 2)]
        public double Y { get;  }

        /// <summary>
        /// Z coordinate of the vector.
        /// </summary>
        [DataMember(Order = 3)]
        public double Z { get;  }

        /// <summary>
        /// Returns maximum value of the vector.
        /// </summary>
        ///
        /// <remarks><para>Returns maximum value of all 3 vector's coordinates.</para></remarks>
        ///
        public double Max => (X > Y) ? ((X > Z) ? X : Z) : ((Y > Z) ? Y : Z);

        /// <summary>
        /// Returns minimum value of the vector.
        /// </summary>
        ///
        /// <remarks><para>Returns minimum value of all 3 vector's coordinates.</para></remarks>
        ///
        public double Min => (X < Y) ? ((X < Z) ? X : Z) : ((Y < Z) ? Y : Z);

        /// <summary>
        /// Returns index of the coordinate with maximum value.
        /// </summary>
        ///
        /// <remarks><para>Returns index of the coordinate, which has the maximum value - 0 for X,
        /// 1 for Y or 2 for Z.</para>
        /// 
        /// <para><note>If there are multiple coordinates which have the same maximum value, the
        /// property returns smallest index.</note></para>
        /// </remarks>
        ///
        public int MaxIndex => (X >= Y) ? ((X >= Z) ? 0 : 2) : ((Y >= Z) ? 1 : 2);

        /// <summary>
        /// Returns index of the coordinate with minimum value.
        /// </summary>
        ///
        /// <remarks><para>Returns index of the coordinate, which has the minimum value - 0 for X,
        /// 1 for Y or 2 for Z.</para>
        /// 
        /// <para><note>If there are multiple coordinates which have the same minimum value, the
        /// property returns smallest index.</note></para>
        /// </remarks>
        ///
        public int MinIndex => (X <= Y) ? ((X <= Z) ? 0 : 2) : ((Y <= Z) ? 1 : 2);

        /// <summary>
        /// Returns vector's norm: Math.Sqrt(X * X + Y * Y + Z * Z)
        /// </summary>
        /// <remarks><para>Returns Euclidean norm of the vector, which is a
        /// square root of the sum: X<sup>2</sup>+Y<sup>2</sup>+Z<sup>2</sup>.</para>
        /// </remarks>
        public double Norm => Math.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary>
        /// Returns square of the vector's norm: X * X + Y * Y + Z * Z
        /// </summary>
        /// <remarks><para>Return X<sup>2</sup>+Y<sup>2</sup>+Z<sup>2</sup>, which is
        /// a square of <see cref="Norm">vector's norm</see> or a <see cref="Dot">dot product</see> of this vector
        /// with itself.</para></remarks>
        public double Square => X * X + Y * Y + Z * Z;

        public Vector3D(double[] array)
        {
            if(array == null) throw new ArgumentNullException(nameof(array),$"Vector3 array constructor must be non-null");
            if(array.Length < 3) throw new ArgumentOutOfRangeException(nameof(array),$"Vector3 array constructor must have at lease 3 elements");

            X = array[0];
            Y = array[1];
            Z = array[2];
            _value = new[] {array[0], array[1], array[2]};
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> structure.
        /// </summary>
        /// 
        /// <param name="x">X coordinate of the vector.</param>
        /// <param name="y">Y coordinate of the vector.</param>
        /// <param name="z">Z coordinate of the vector.</param>
        /// 
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            _value = new[] {x, y, z};
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> struct form a IPoint3D.
        /// </summary>
        /// <param name="point">The point.</param>
        public Vector3D(IPoint3D point) : this(point.X, point.Y, point.Z) { }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> structure.
        /// </summary>
        /// 
        /// <param name="value">Value, which is set to all 3 coordinates of the vector.</param>
        /// 
        public Vector3D(double value) : this(value, value, value) { }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// 
        /// <returns>A string representation of this object.</returns>
        /// 
        public override string ToString() => string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}, {1}, {2}", X, Y, Z);

        /// <summary>
        /// Returns array representation of the vector.
        /// </summary>
        /// 
        /// <returns>Array with 3 values containing X/Y/Z coordinates.</returns>
        /// 
        public double[] ToArray() => new double[3] { X, Y, Z };

        /// <summary>
        /// Adds corresponding coordinates of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">The vector to add to.</param>
        /// <param name="vector2">The vector to add to the first vector.</param>
        /// 
        /// <returns>Returns a vector which coordinates are equal to sum of corresponding
        /// coordinates of the two specified vectors.</returns>
        ///
        public static Vector3D operator +(Vector3D vector1, Vector3D vector2) => new Vector3D(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);

        /// <summary>
        /// Adds corresponding coordinates of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">The vector to add to.</param>
        /// <param name="vector2">The vector to add to the first vector.</param>
        /// 
        /// <returns>Returns a vector which coordinates are equal to sum of corresponding
        /// coordinates of the two specified vectors.</returns>
        ///
        public static Vector3D Add(Vector3D vector1, Vector3D vector2) => vector1 + vector2;

        /// <summary>
        /// Adds a value to all coordinates of the specified vector.
        /// </summary>
        /// 
        /// <param name="vector">Vector to add the specified value to.</param>
        /// <param name="value">Value to add to all coordinates of the vector.</param>
        /// 
        /// <returns>Returns new vector with all coordinates increased by the specified value.</returns>
        /// 
        public static Vector3D operator +(Vector3D vector, double value) => new Vector3D(vector.X + value, vector.Y + value, vector.Z + value);

        /// <summary>
        /// Adds a value to all coordinates of the specified vector.
        /// </summary>
        /// 
        /// <param name="vector">Vector to add the specified value to.</param>
        /// <param name="value">Value to add to all coordinates of the vector.</param>
        /// 
        /// <returns>Returns new vector with all coordinates increased by the specified value.</returns>
        /// 
        public static Vector3D Add(Vector3D vector, double value) => vector + value;

        /// <summary>
        /// Subtracts corresponding coordinates of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">The vector to subtract from.</param>
        /// <param name="vector2">The vector to subtract from the first vector.</param>
        /// 
        /// <returns>Returns a vector which coordinates are equal to difference of corresponding
        /// coordinates of the two specified vectors.</returns>
        ///
        public static Vector3D operator -(Vector3D vector1, Vector3D vector2) => new Vector3D(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);

        /// <summary>
        /// Subtracts corresponding coordinates of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">The vector to subtract from.</param>
        /// <param name="vector2">The vector to subtract from the first vector.</param>
        /// 
        /// <returns>Returns a vector which coordinates are equal to difference of corresponding
        /// coordinates of the two specified vectors.</returns>
        ///
        public static Vector3D Subtract(Vector3D vector1, Vector3D vector2) => vector1 - vector2;

        /// <summary>
        /// Subtracts a value from all coordinates of the specified vector.
        /// </summary>
        /// 
        /// <param name="vector">Vector to subtract the specified value from.</param>
        /// <param name="value">Value to subtract from all coordinates of the vector.</param>
        /// 
        /// <returns>Returns new vector with all coordinates decreased by the specified value.</returns>
        /// 
        public static Vector3D operator -(Vector3D vector, double value) => new Vector3D(vector.X - value, vector.Y - value, vector.Z - value);

        /// <summary>
        /// Subtracts a value from all coordinates of the specified vector.
        /// </summary>
        /// 
        /// <param name="vector">Vector to subtract the specified value from.</param>
        /// <param name="value">Value to subtract from all coordinates of the vector.</param>
        /// 
        /// <returns>Returns new vector with all coordinates decreased by the specified value.</returns>
        /// 
        public static Vector3D Subtract(Vector3D vector, double value) => vector - value;

        /// <summary>
        /// Multiplies corresponding coordinates of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">The first vector to multiply.</param>
        /// <param name="vector2">The second vector to multiply.</param>
        /// 
        /// <returns>Returns a vector which coordinates are equal to multiplication of corresponding
        /// coordinates of the two specified vectors.</returns>
        ///
        public static Vector3D operator *(Vector3D vector1, Vector3D vector2) => new Vector3D(vector1.X * vector2.X, vector1.Y * vector2.Y, vector1.Z * vector2.Z);

        /// <summary>
        /// Multiplies corresponding coordinates of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">The first vector to multiply.</param>
        /// <param name="vector2">The second vector to multiply.</param>
        /// 
        /// <returns>Returns a vector which coordinates are equal to multiplication of corresponding
        /// coordinates of the two specified vectors.</returns>
        ///
        public static Vector3D Multiply(Vector3D vector1, Vector3D vector2) => vector1 * vector2;

        /// <summary>
        /// Multiplies coordinates of the specified vector by the specified factor.
        /// </summary>
        /// 
        /// <param name="vector">Vector to multiply coordinates of.</param>
        /// <param name="factor">Factor to multiple coordinates of the specified vector by.</param>
        /// 
        /// <returns>Returns new vector with all coordinates multiplied by the specified factor.</returns>
        ///
        public static Vector3D operator *(Vector3D vector, double factor) => new Vector3D(vector.X * factor, vector.Y * factor, vector.Z * factor);

        /// <summary>
        /// Multiplies coordinates of the specified vector by the specified factor.
        /// </summary>
        /// 
        /// <param name="vector">Vector to multiply coordinates of.</param>
        /// <param name="factor">Factor to multiple coordinates of the specified vector by.</param>
        /// 
        /// <returns>Returns new vector with all coordinates multiplied by the specified factor.</returns>
        ///
        public static Vector3D Multiply(Vector3D vector, double factor) => vector * factor;

        /// <summary>
        /// Divides corresponding coordinates of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">The first vector to divide.</param>
        /// <param name="vector2">The second vector to devide.</param>
        /// 
        /// <returns>Returns a vector which coordinates are equal to coordinates of the first vector divided by
        /// corresponding coordinates of the second vector.</returns>
        ///
        public static Vector3D operator /(Vector3D vector1, Vector3D vector2) => new Vector3D(vector1.X / vector2.X, vector1.Y / vector2.Y, vector1.Z / vector2.Z);

        /// <summary>
        /// Divides corresponding coordinates of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">The first vector to divide.</param>
        /// <param name="vector2">The second vector to devide.</param>
        /// 
        /// <returns>Returns a vector which coordinates are equal to coordinates of the first vector divided by
        /// corresponding coordinates of the second vector.</returns>
        ///
        public static Vector3D Divide(Vector3D vector1, Vector3D vector2) => vector1 / vector2;

        /// <summary>
        /// Divides coordinates of the specified vector by the specified factor.
        /// </summary>
        /// 
        /// <param name="vector">Vector to divide coordinates of.</param>
        /// <param name="factor">Factor to divide coordinates of the specified vector by.</param>
        /// 
        /// <returns>Returns new vector with all coordinates divided by the specified factor.</returns>
        ///
        public static Vector3D operator /(Vector3D vector, double factor) => new Vector3D(vector.X / factor, vector.Y / factor, vector.Z / factor);

        /// <summary>
        /// Divides coordinates of the specified vector by the specified factor.
        /// </summary>
        /// 
        /// <param name="vector">Vector to divide coordinates of.</param>
        /// <param name="factor">Factor to divide coordinates of the specified vector by.</param>
        /// 
        /// <returns>Returns new vector with all coordinates divided by the specified factor.</returns>
        ///
        public static Vector3D Divide(Vector3D vector, double factor) => vector / factor;

        /// <summary>
        /// Tests whether two specified vectors are equal.
        /// </summary>
        /// 
        /// <param name="vector1">The left-hand vector.</param>
        /// <param name="vector2">The right-hand vector.</param>
        /// 
        /// <returns>Returns <see langword="true"/> if the two vectors are equal or <see langword="false"/> otherwise.</returns>
        /// 
        public static bool operator ==(Vector3D vector1, Vector3D vector2)
        {
            return ((vector1.X == vector2.X) && (vector1.Y == vector2.Y) && (vector1.Z == vector2.Z));
        }

        /// <summary>
        /// Tests whether two specified vectors are not equal.
        /// </summary>
        /// 
        /// <param name="vector1">The left-hand vector.</param>
        /// <param name="vector2">The right-hand vector.</param>
        /// 
        /// <returns>Returns <see langword="true"/> if the two vectors are not equal or <see langword="false"/> otherwise.</returns>
        /// 
        public static bool operator !=(Vector3D vector1, Vector3D vector2)
        {
            return ((vector1.X != vector2.X) || (vector1.Y != vector2.Y) || (vector1.Z != vector2.Z));
        }

        /// <summary>
        /// Normalizes the vector by dividing it’s all coordinates with the vector's norm.
        /// </summary>
        /// 
        /// <returns>Returns the value of vectors’ norm before normalization.</returns>
        ///
        public Vector3D Normalize()
        {
            double norm = Math.Sqrt(X * X + Y * Y + Z * Z);
            double invNorm = 1.0f / norm;

            return new Vector3D(X * invNorm, Y * invNorm, Z * invNorm);
        }

        /// <summary>
        /// Inverse the vector.
        /// </summary>
        /// 
        /// <returns>Returns a vector with all coordinates equal to 1.0 divided by the value of corresponding coordinate
        /// in this vector (or equal to 0.0 if this vector has corresponding coordinate also set to 0.0).</returns>
        ///
        public Vector3D Inverse()
        {
            return new Vector3D(
                (X == 0) ? 0 : 1.0f / X,
                (Y == 0) ? 0 : 1.0f / Y,
                (Z == 0) ? 0 : 1.0f / Z);
        }

        /// <summary>
        /// Calculate absolute values of the vector.
        /// </summary>
        /// 
        /// <returns>Returns a vector with all coordinates equal to absolute values of this vector's coordinates.</returns>
        /// 
        public Vector3D Abs() => new Vector3D(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));

        /// <summary>
        /// Calculates cross product of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">First vector to use for cross product calculation.</param>
        /// <param name="vector2">Second vector to use for cross product calculation.</param>
        /// 
        /// <returns>Returns cross product of the two specified vectors.</returns>
        /// 
        public static Vector3D Cross(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(
                vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                vector1.Z * vector2.X - vector1.X * vector2.Z,
                vector1.X * vector2.Y - vector1.Y * vector2.X);
        }

        /// <summary>
        /// Calculates dot product of two vectors.
        /// </summary>
        /// 
        /// <param name="vector1">First vector to use for dot product calculation.</param>
        /// <param name="vector2">Second vector to use for dot product calculation.</param>
        /// 
        /// <returns>Returns dot product of the two specified vectors.</returns>
        /// 
        public static double Dot(Vector3D vector1, Vector3D vector2) => vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;

        #region Implementation of IReadOnlyList<double>

        /// <summary>
        /// Items in Vector: 3 (constant)
        /// </summary>
        public int Count => 3;

        /// <summary>
        /// Get enumerator for double[3] array
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => _value.GetEnumerator();

        /// <summary>
        /// Get enumerator of underlying array
        /// </summary>
        /// <returns></returns>
        public IEnumerator<double> GetEnumerator() => ((IEnumerable<double>) _value).GetEnumerator();

        /// <summary>
        /// Index access of underlying array
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index] => _value[index];

        #endregion

        /// <summary>
        /// Tests whether the vector equals to the specified one.
        /// </summary>
        /// 
        /// <param name="vector">The vector to test equality with.</param>
        /// 
        /// <returns>Returns <see langword="true"/> if the two vectors are equal or <see langword="false"/> otherwise.</returns>
        /// 
        public bool Equals(Vector3D vector) => ((vector.X == X) && (vector.Y == Y) && (vector.Z == Z));

        public bool Equals(Vector3D vector, double tolerance) => Equals(this,vector,tolerance);

        /// <summary>
        /// Tests whether the vector equals to the specified object.
        /// </summary>
        /// 
        /// <param name="obj">The object to test equality with.</param>
        /// 
        /// <returns>Returns <see langword="true"/> if the vector equals to the specified object or <see langword="false"/> otherwise.</returns>
        /// 
        public override bool Equals(object obj) => obj is Vector3D && Equals((Vector3D) obj);

        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// 
        /// <returns>A 32-bit signed integer hash code.</returns>
        /// 
        public override int GetHashCode() => GetHashCode(this.X, this.Y, this.Z);

        #region Implementation of IEqualityComparer<in Vector3D>

        public bool Equals(Vector3D x, Vector3D y) => x.Equals(y);

        public int GetHashCode(Vector3D obj) => GetHashCode(obj.X,obj.Y,obj.Z);

        #endregion
        #region Implementation of IEqualsToleranced<IPoint3D>

        public bool Equals(IPoint3D other, double tolerance) => Equals(this,other,tolerance);

        #endregion

        public static bool Equals(Vector3D p1, IPoint3D p2, double tolerance)
        {
            if (p2 is Vector3D) return Equals(p1, (Vector3D) p2, tolerance);

            if (Math.Abs(p1.X - p2.X) > tolerance) return false;
            if (Math.Abs(p1.Y - p2.Y) > tolerance) return false;
            if (Math.Abs(p1.Z - p2.Z) > tolerance) return false;
            return true;
        }

        public static bool Equals(Vector3D p1, Vector3D p2, double tolerance)
        {
            if (p1 == p2) return true;
            if (Math.Abs(p1.X - p2.X) > tolerance) return false;
            if (Math.Abs(p1.Y - p2.Y) > tolerance) return false;
            if (Math.Abs(p1.Z - p2.Z) > tolerance) return false;
            return true;
        }

        public static int GetHashCode(double x,double y, double z)
        {
            unchecked
            {
                int hash = 643;
                hash = (int)(x * 10223) ^ hash;
                hash = (int)(y * 10847) ^ hash;
                hash = (int)(z * 11807) ^ hash;
                return hash;
            }
        }

    }
}

