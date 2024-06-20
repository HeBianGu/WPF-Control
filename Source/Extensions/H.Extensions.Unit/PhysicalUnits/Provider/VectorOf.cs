// Keith Barrett 2022
using System;

namespace H.Extensions.Unit
{
    /// <summary>
    /// Structure to represent vector quantities, such as Displacement, Velocity or Force.
    /// </summary>
    /// <typeparam name="T">A physical quantity, such as length.</typeparam>
    public class VectorOf<T> where T : IPhysicalQuantity, new()
    {
#if USE_GENERIC_MATHS
        // In .Net 6 onwards we can use generic maths to make this more type safe
        public T X;
        public T Y;
        public T Z;

        public Vector(T x, T y, T z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector(T magnitude, Angle inclination, Angle azimuth)
        {
            this.X = magnitude * Functions.Cos(azimuth) * Functions.Sin(inclination));
            this.Y = magnitude * Functions.Sin(azimuth) * Functions.Sin(inclination);
            this.Z = magnitude * Functions.Cos(inclination);
        }

        public T Magnitude { get { return Functions.Sqrt(X * X + Y * Y + Z * Z); } }
        public Angle Inclination { get { return Functions.Acos(Z/Magnitude); } }
        public Angle Azimuth { get { return Functions.Atan2(Y, X); } }

        public static Vector<T> operator +(Vector<T> v1, Vector<T> v2)
        {
            return new Vector<T>(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector<T> operator -(Vector<T> v1, Vector<T> v2)
        {
            return new Vector<T>(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
       
        public static Vector<T> operator *(Vector<T> v1, double v2)
        {
            return new Vector<T>(v1.X * v2, v1.Y * v2, v1.Z * v2);
        }

        public static Vector<T> operator *(double v1, Vector<T> v2)
        {
            return new Vector<T>(v1 * v2.X, v1 * v2.Y, v1 * v2.Z);
        }
#else
        // Without generic maths we have to rely on the PhysicalQuantity type:
        public PhysicalQuantity X;
        public PhysicalQuantity Y;
        public PhysicalQuantity Z;

        public VectorOf(T x, T y, T z)
        {
            X = new PhysicalQuantity(x);
            Y = new PhysicalQuantity(y);
            Z = new PhysicalQuantity(z);
        }

        private static readonly Angle RightAngle = new Angle(Math.PI / 2.0);

        public VectorOf(T magnitude, Angle direction)
            : this(magnitude, RightAngle, direction)
        {
        }

        public VectorOf(T magnitude, Angle inclination, Angle azimuth)
        {
            Check.True(magnitude.Value >= 0.0, "Magnitude must be positive");
            Check.True(inclination.Value >= 0 && inclination.Value <= Math.PI, "inclination must be between zero and 180°");
            Check.True(azimuth.Value >= 0 && azimuth.Value <= Math.PI * 2, "azimuth must be between zero and 360°");
            double x = magnitude.Value * Functions.Cos(azimuth) * Functions.Sin(inclination);
            double y = magnitude.Value * Functions.Sin(azimuth) * Functions.Sin(inclination);
            double z = magnitude.Value * Functions.Cos(inclination);
            X = new PhysicalQuantity(x, magnitude.Dimensions);
            Y = new PhysicalQuantity(y, magnitude.Dimensions);
            Z = new PhysicalQuantity(z, magnitude.Dimensions);
        }

        public VectorOf(double x, double y, double z, Dimensions dimensions)
        {
            X = new PhysicalQuantity(x, dimensions);
            Y = new PhysicalQuantity(y, dimensions);
            Z = new PhysicalQuantity(z, dimensions);
        }

        public PhysicalQuantity Magnitude { get { return Functions.Sqrt(X * X + Y * Y + Z * Z); } }
        public Angle Inclination { get { return Functions.Acos((Z / this.Magnitude).Value); } }
        public Angle Azimuth { get { return new Angle(Math.Atan2(Y.Value, X.Value)); } }

        public static VectorOf<T> operator +(VectorOf<T> v1, VectorOf<T> v2)
        {
            double x = v1.X.Value + v2.X.Value;
            double y = v1.Y.Value + v2.Y.Value;
            double z = v1.Z.Value + v2.Z.Value;
            return new VectorOf<T>(x, y, z, v1.X.Dimensions);
        }

        public static VectorOf<T> operator -(VectorOf<T> v1, VectorOf<T> v2)
        {
            double x = v1.X.Value - v2.X.Value;
            double y = v1.Y.Value - v2.Y.Value;
            double z = v1.Z.Value - v2.Z.Value;
            return new VectorOf<T>(x, y, z, v1.X.Dimensions);
        }

        public static VectorOf<T> operator *(VectorOf<T> v1, double v2)
        {
            double x = v1.X.Value * v2;
            double y = v1.Y.Value * v2;
            double z = v1.Z.Value * v2;
            return new VectorOf<T>(x, y, z, v1.X.Dimensions);
        }

        public static VectorOf<T> operator *(double v1, VectorOf<T> v2)
        {
            double x = v1 * v2.X.Value;
            double y = v1 * v2.Y.Value;
            double z = v1 * v2.Z.Value;
            return new VectorOf<T>(x, y, z, v2.X.Dimensions);
        }

#endif
        /// <summary>
        /// Direction is the same as Azimuth
        /// </summary>
        public Angle Direction { get { return this.Azimuth; } }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }

    }
}