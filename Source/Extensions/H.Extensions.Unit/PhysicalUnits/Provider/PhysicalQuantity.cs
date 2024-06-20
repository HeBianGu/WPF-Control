// https://www.codeproject.com/Articles/5306824/Taking-Your-Brain-to-Another-Dimension-A-Csharp-li
using System;

namespace H.Extensions.Unit
{
    public interface IPhysicalQuantity
    {
        double Value { get; }
        Dimensions Dimensions { get; }

        public string ToString(int precision, params Unit[] units);

    }

    public readonly partial struct PhysicalQuantity : IPhysicalQuantity
    {
        public double Value { get; init; }
        public Dimensions Dimensions { get; init; }

        public PhysicalQuantity(double v, Dimensions d)
        {
            this.Value = v;
            this.Dimensions = d;
        }

        public PhysicalQuantity(IPhysicalQuantity q)
        {
            this.Value = q.Value;
            this.Dimensions = q.Dimensions;
        }

        public override string ToString()
        {
            return UnitsSystem.ToString(this);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        #region Comparison Operators

        public static int Compare(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            if (v1.Dimensions != v2.Dimensions)
                throw new Exception($"Dimension mismatch: {v1.Dimensions} {v2.Dimensions}");
            double d1 = v1.Value;
            double d2 = v2.Value;
            if (d1 < d2)
                return -1;
            else if (d1 > d2)
                return 1;
            else
                return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is IPhysicalQuantity)
            {
                IPhysicalQuantity pq = obj as IPhysicalQuantity;
                return PhysicalQuantity.Compare(this, pq) == 0;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return (int)this.Value;
        }

        public static bool operator ==(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            return Compare(v1, v2) == 0;
        }

        public static bool operator !=(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            return Compare(v1, v2) != 0;
        }

        public static bool operator >(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            return Compare(v1, v2) > 0;
        }

        public static bool operator >=(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        public static bool operator <=(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        public static bool operator <(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        public static PhysicalQuantity operator +(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            if (v1.Dimensions != null && v1.Dimensions != v2.Dimensions)
                throw new Exception($"Dimension mismatch: {v1.Dimensions} {v2.Dimensions}");
            return new PhysicalQuantity(v1.Value + v2.Value, v2.Dimensions);
        }

        public static PhysicalQuantity operator -(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            if (v1.Dimensions != v2.Dimensions)
                throw new Exception($"Dimension mismatch: {v1.Dimensions} {v2.Dimensions}");
            return new PhysicalQuantity(v1.Value - v2.Value, v1.Dimensions);
        }

        public static PhysicalQuantity operator *(PhysicalQuantity v1, double v2)
        {
            return new PhysicalQuantity(v1.Value * v2, v1.Dimensions);
        }

        public static PhysicalQuantity operator *(double v1, PhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1 * v2.Value, v2.Dimensions);
        }

        public static PhysicalQuantity operator *(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

        public static PhysicalQuantity operator /(PhysicalQuantity v1, double v2)
        {
            return new PhysicalQuantity(v1.Value / v2, v1.Dimensions);
        }

        public static PhysicalQuantity operator /(PhysicalQuantity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }

        public static PhysicalQuantity operator ^(PhysicalQuantity v, short p)
        {
            return new PhysicalQuantity(Math.Pow(v.Value, p), v.Dimensions ^ p);
        }


        #endregion
    }


}
