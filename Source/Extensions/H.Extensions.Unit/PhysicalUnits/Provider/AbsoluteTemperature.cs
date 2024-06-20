
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace H.Extensions.Unit
{
    public partial class Dimensions
    {
        public static readonly Dimensions AbsoluteTemperature = new Dimensions(0, 0, 0, 0, 1);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (ReferenceEquals(obj, null))
                return false;
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

    public readonly partial struct AbsoluteTemperature : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.AbsoluteTemperature; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AbsoluteTemperature(double v)
        {
            this.Value = v;
        }

        public AbsoluteTemperature(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.AbsoluteTemperature)
                throw new Exception("Invalid conversion from PhysicalQuantity to AbsoluteTemperature");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            string result = string.Format("{0:0.##} K", this.Value);
            return result;
        }

        public string ToString(UnitWithOffset unit)
        {
            Check.True(object.ReferenceEquals(unit.Dimensions, Dimensions.AbsoluteTemperature), "Dimensions must match");
            double v = unit.ConvertValueFromSI(this.Value);
            string shortName = unit.ShortName;
            string result = string.Format("{0:0.##} {1}", v, shortName);
            return result;
        }

        public string ToString(int precision, params Unit[] units)
        {
            return ToString();
        }

        public static AbsoluteTemperature Parse(string s)
        {
            string[] parts = s.Split(' ');
            if (parts.Count() != 2)
                throw new Exception($"Parsing error: invalid format: {s} ");
            string number = parts[0];
            double value = 0.0;
            if (!double.TryParse(number, out value))
                throw new Exception($"Parsing error: Not a number: {number} ");
            string unitString = parts[1];
            Unit unit = TemperatureUnits.System.FindUnit(unitString);
            if (unit == null)
                throw new Exception($"Parsing error: invalid units: {unitString} ");
            value = unit.ConvertValueToSI(value);
            AbsoluteTemperature t = new AbsoluteTemperature(value);
            return t;
        }

        #endregion

        #region Comparison Operators

        public static int Compare(AbsoluteTemperature v1, AbsoluteTemperature v2)
        {
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
                PhysicalQuantity pq = new PhysicalQuantity(this);
                return PhysicalQuantity.Compare(pq, obj as IPhysicalQuantity) == 0;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return (int)this.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator ==(AbsoluteTemperature v1, AbsoluteTemperature v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(AbsoluteTemperature v1, AbsoluteTemperature v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(AbsoluteTemperature v1, AbsoluteTemperature v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(AbsoluteTemperature v1, AbsoluteTemperature v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(AbsoluteTemperature v1, AbsoluteTemperature v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(AbsoluteTemperature v1, AbsoluteTemperature v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AbsoluteTemperature operator +(AbsoluteTemperature v1, TemperatureChange v2)
        {
            return new AbsoluteTemperature(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AbsoluteTemperature operator -(AbsoluteTemperature v1, TemperatureChange v2)
        {
            return new AbsoluteTemperature(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator -(AbsoluteTemperature v1, AbsoluteTemperature v2)
        {
            return new TemperatureChange(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AbsoluteTemperature operator *(AbsoluteTemperature v1, double v2)
        {
            return new AbsoluteTemperature(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AbsoluteTemperature operator *(double v1, AbsoluteTemperature v2)
        {
            return new AbsoluteTemperature(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(AbsoluteTemperature v1, AbsoluteTemperature v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

        #endregion

    }

    public class UnitWithOffset : Unit
    {
        public double Offset; //to convert from ISO units
        public UnitWithOffset(UnitsSystem system, string name, string shortname, string CommonCode, Dimensions dim, double conv, double offset, DisplayOption opt = DisplayOption.Explicit)
            : base(system, name, shortname, CommonCode, dim, conv, opt)
        {
            Offset = offset;
        }

        internal override double ConvertValueFromSI(double value)
        {
            return (value - Offset) / ConversionFactor;
        }

        internal override double ConvertValueToSI(double value)
        {
            return value * ConversionFactor + Offset;
        }
    }

    public partial class TemperatureUnits : UnitsSystem
    {
        public override string Name => "Temperature";

        public static UnitWithOffset Kelvin = new UnitWithOffset(System, "Kelvin", "K", "KEL", Dimensions.AbsoluteTemperature, 1.0, 0.0, Unit.DisplayOption.Standard);
        public static UnitWithOffset Celsius = new UnitWithOffset(System, "Celsius", "°C", "CEL", Dimensions.AbsoluteTemperature, 1.0, 273.15, Unit.DisplayOption.Explicit);
        public static UnitWithOffset Fahrenheit = new UnitWithOffset(System, "Fahrenheit", "°F", "FAH", Dimensions.AbsoluteTemperature, 5.0 / 9.0, 255.37, Unit.DisplayOption.Explicit);

        private static readonly Unit[] allTemperatureUnits = new Unit[]
        {
            Kelvin,
            Celsius,
            Fahrenheit,
        };

        public override Unit[] GetAllUnits()
        {
            return allTemperatureUnits;
        }

        private static readonly Unit[] allTemperatureDefaultUnits = new Unit[]
        {
            Kelvin,
        };

        protected override Unit[] GetDefaultUnits()
        {
            return allTemperatureDefaultUnits;
        }

        protected override Unit[] GetDisplayUnits()
        {
            return allTemperatureUnits;
        }

        public static TemperatureUnits System = new TemperatureUnits();

    }

    public static class TemperatureExtensions
    {
        public static AbsoluteTemperature Kelvin(this int v) { return ((double)v).Kelvin(); }
        public static AbsoluteTemperature Kelvin(this double v)
        {
            return new AbsoluteTemperature(v);
        }

        public static AbsoluteTemperature Celsius(this int v) { return ((double)v).Celsius(); }
        public static AbsoluteTemperature Celsius(this double v)
        {
            return new AbsoluteTemperature(273.15 + v);
        }

        public static AbsoluteTemperature Fahrenheit(this int v) { return ((double)v).Fahrenheit(); }
        public static AbsoluteTemperature Fahrenheit(this double v)
        {
            double kelvin = (v - 32.0) * 5.0 / 9.0 + 273.15;
            return new AbsoluteTemperature(kelvin);
        }
    }
}

