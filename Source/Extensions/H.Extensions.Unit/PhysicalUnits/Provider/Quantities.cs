

using System;
using System.Runtime.CompilerServices;

namespace H.Extensions.Unit
{
    // Generated from Dimensions.xml
    public readonly partial struct Dimensionless : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Dimensionless; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Dimensionless(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Dimensionless(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Dimensionless)
                throw new Exception("Invalid conversion from PhysicalQuantity to Dimensionless");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Dimensionless Parse(string s)
        {
            Dimensionless q = UnitsSystem.Parse(s);
            return q;
        }

        public static Dimensionless Parse(string s, UnitsSystem system)
        {
            Dimensionless q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Dimensionless q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Dimensionless(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Dimensionless q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Dimensionless(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Dimensionless v1, Dimensionless v2)
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
        public static bool operator ==(Dimensionless v1, Dimensionless v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Dimensionless v1, Dimensionless v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Dimensionless v1, Dimensionless v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Dimensionless v1, Dimensionless v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Dimensionless v1, Dimensionless v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Dimensionless v1, Dimensionless v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator +(Dimensionless v1, Dimensionless v2)
        {
            return new Dimensionless(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator -(Dimensionless v1, Dimensionless v2)
        {
            return new Dimensionless(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator -(Dimensionless v)
        {
            return new Dimensionless(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(Dimensionless v1, int v2)
        {
            return new Dimensionless(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(Dimensionless v1, double v2)
        {
            return new Dimensionless(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(double v1, Dimensionless v2)
        {
            return new Dimensionless(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(Dimensionless v1, Dimensionless v2)
        {
            return new Dimensionless(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Dimensionless v1, int v2)
        {
            return new Dimensionless(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Dimensionless v1, double v2)
        {
            return new Dimensionless(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Dimensionless v1, Dimensionless v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }


#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Dimensionless v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Dimensionless v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Dimensionless / by Length gives ByLength 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator /(Dimensionless v1, Length v2)
        {
            return new ByLength(v1.Value / v2.Value);
        }
        // Dimensionless / by ByLength gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Dimensionless v1, ByLength v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // Dimensionless / by Area gives ByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator /(Dimensionless v1, Area v2)
        {
            return new ByArea(v1.Value / v2.Value);
        }
        // Dimensionless / by ByArea gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Dimensionless v1, ByArea v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // Dimensionless / by Time gives Frequency 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator /(Dimensionless v1, Time v2)
        {
            return new Frequency(v1.Value / v2.Value);
        }
        // Dimensionless / by Frequency gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Dimensionless v1, Frequency v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // Dimensionless / by TemperatureChange gives CoefficientOfThermalExpansion 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator /(Dimensionless v1, TemperatureChange v2)
        {
            return new CoefficientOfThermalExpansion(v1.Value / v2.Value);
        }
        // Dimensionless / by CoefficientOfThermalExpansion gives TemperatureChange 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator /(Dimensionless v1, CoefficientOfThermalExpansion v2)
        {
            return new TemperatureChange(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct Mass : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Mass; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Mass(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Mass(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Mass)
                throw new Exception("Invalid conversion from PhysicalQuantity to Mass");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Mass Parse(string s)
        {
            Mass q = UnitsSystem.Parse(s);
            return q;
        }

        public static Mass Parse(string s, UnitsSystem system)
        {
            Mass q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Mass q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Mass(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Mass q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Mass(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Mass v1, Mass v2)
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
        public static bool operator ==(Mass v1, Mass v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Mass v1, Mass v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Mass v1, Mass v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Mass v1, Mass v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Mass v1, Mass v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Mass v1, Mass v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator +(Mass v1, Mass v2)
        {
            return new Mass(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator -(Mass v1, Mass v2)
        {
            return new Mass(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator -(Mass v)
        {
            return new Mass(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(Mass v1, int v2)
        {
            return new Mass(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(Mass v1, double v2)
        {
            return new Mass(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(double v1, Mass v2)
        {
            return new Mass(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(Mass v1, Dimensionless v2)
        {
            return new Mass(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator /(Mass v1, int v2)
        {
            return new Mass(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator /(Mass v1, double v2)
        {
            return new Mass(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator /(Mass v1, Dimensionless v2)
        {
            return new Mass(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Mass v1, Mass v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Mass v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Mass v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Mass / by Volume gives Density 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator /(Mass v1, Volume v2)
        {
            return new Density(v1.Value / v2.Value);
        }
        // Mass / by Density gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator /(Mass v1, Density v2)
        {
            return new Volume(v1.Value / v2.Value);
        }
        // Mass / by AmountOfSubstance gives MolarMass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator /(Mass v1, AmountOfSubstance v2)
        {
            return new MolarMass(v1.Value / v2.Value);
        }
        // Mass / by MolarMass gives AmountOfSubstance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator /(Mass v1, MolarMass v2)
        {
            return new AmountOfSubstance(v1.Value / v2.Value);
        }
        // Mass / by Length gives MassByLength 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator /(Mass v1, Length v2)
        {
            return new MassByLength(v1.Value / v2.Value);
        }
        // Mass / by MassByLength gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Mass v1, MassByLength v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // Mass / by Area gives MassByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator /(Mass v1, Area v2)
        {
            return new MassByArea(v1.Value / v2.Value);
        }
        // Mass / by MassByArea gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Mass v1, MassByArea v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // Mass * by Velocity gives Momentum 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(Mass v1, Velocity v2)
        {
            return new Momentum(v1.Value * v2.Value);
        }
        // Mass * by Acceleration gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(Mass v1, Acceleration v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        // Mass * by Area gives MomentOfInertia 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator *(Mass v1, Area v2)
        {
            return new MomentOfInertia(v1.Value * v2.Value);
        }
        // Mass * by VelocitySquared gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(Mass v1, VelocitySquared v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // Mass / by Time gives MassFlowRate 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator /(Mass v1, Time v2)
        {
            return new MassFlowRate(v1.Value / v2.Value);
        }
        // Mass / by MassFlowRate gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Mass v1, MassFlowRate v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // Mass * by SpecificHeat gives ThermalCapacity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(Mass v1, SpecificHeat v2)
        {
            return new ThermalCapacity(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct Length : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Length; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Length(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Length(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Length)
                throw new Exception("Invalid conversion from PhysicalQuantity to Length");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Length Parse(string s)
        {
            Length q = UnitsSystem.Parse(s);
            return q;
        }

        public static Length Parse(string s, UnitsSystem system)
        {
            Length q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Length q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Length(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Length q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Length(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Length v1, Length v2)
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
        public static bool operator ==(Length v1, Length v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Length v1, Length v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Length v1, Length v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Length v1, Length v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Length v1, Length v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Length v1, Length v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator +(Length v1, Length v2)
        {
            return new Length(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator -(Length v1, Length v2)
        {
            return new Length(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator -(Length v)
        {
            return new Length(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(Length v1, int v2)
        {
            return new Length(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(Length v1, double v2)
        {
            return new Length(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(double v1, Length v2)
        {
            return new Length(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(Length v1, Dimensionless v2)
        {
            return new Length(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Length v1, int v2)
        {
            return new Length(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Length v1, double v2)
        {
            return new Length(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Length v1, Dimensionless v2)
        {
            return new Length(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Length v1, Length v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Length v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Length v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Length * by Length gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator *(Length v1, Length v2)
        {
            return new Area(v1.Value * v2.Value);
        }
        // Length * by Area gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator *(Length v1, Area v2)
        {
            return new Volume(v1.Value * v2.Value);
        }
        // Length / by Time gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator /(Length v1, Time v2)
        {
            return new Velocity(v1.Value / v2.Value);
        }
        // Length / by Velocity gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Length v1, Velocity v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // Length * by AngularVelocity gives TangentialVelocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator *(Length v1, AngularVelocity v2)
        {
            return new TangentialVelocity(v1.Value * v2.Value);
        }
        // double / by Length gives ByLength 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator /(double v1, Length v2)
        {
            return new ByLength(v1 / v2.Value);
        }
        // Length * by ByLength gives Dimensionless 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(Length v1, ByLength v2)
        {
            return new Dimensionless(v1.Value * v2.Value);
        }
        // Length / by Volume gives ByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator /(Length v1, Volume v2)
        {
            return new ByArea(v1.Value / v2.Value);
        }
        // Length / by ByArea gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator /(Length v1, ByArea v2)
        {
            return new Volume(v1.Value / v2.Value);
        }
        // Length * by MassByLength gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(Length v1, MassByLength v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        // Length * by Density gives MassByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator *(Length v1, Density v2)
        {
            return new MassByArea(v1.Value * v2.Value);
        }
        // Length * by Volume gives FourDimensionalVolume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator *(Length v1, Volume v2)
        {
            return new FourDimensionalVolume(v1.Value * v2.Value);
        }
        // Length * by MolarConcentrationGradient gives MolarConcentration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator *(Length v1, MolarConcentrationGradient v2)
        {
            return new MolarConcentration(v1.Value * v2.Value);
        }
        // Length * by Acceleration gives VelocitySquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator *(Length v1, Acceleration v2)
        {
            return new VelocitySquared(v1.Value * v2.Value);
        }
        // Length / by TimeSquared gives Acceleration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator /(Length v1, TimeSquared v2)
        {
            return new Acceleration(v1.Value / v2.Value);
        }
        // Length / by Acceleration gives TimeSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator /(Length v1, Acceleration v2)
        {
            return new TimeSquared(v1.Value / v2.Value);
        }
        // Length * by AngularVelocitySquared gives Acceleration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator *(Length v1, AngularVelocitySquared v2)
        {
            return new Acceleration(v1.Value * v2.Value);
        }
        // Length * by Force gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(Length v1, Force v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // Length * by VelocityGradient gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(Length v1, VelocityGradient v2)
        {
            return new Velocity(v1.Value * v2.Value);
        }
        // Length * by CoefficientOfViscosity gives MassFlowRate 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator *(Length v1, CoefficientOfViscosity v2)
        {
            return new MassFlowRate(v1.Value * v2.Value);
        }
        // Length * by SurfaceTension gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(Length v1, SurfaceTension v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        // Length * by Pressure gives SurfaceTension 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator *(Length v1, Pressure v2)
        {
            return new SurfaceTension(v1.Value * v2.Value);
        }
        // Length * by PowerGradient gives Power 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(Length v1, PowerGradient v2)
        {
            return new Power(v1.Value * v2.Value);
        }
        // Length * by TemperatureGradient gives TemperatureChange 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator *(Length v1, TemperatureGradient v2)
        {
            return new TemperatureChange(v1.Value * v2.Value);
        }
        // Length * by Resistance gives Resistivity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator *(Length v1, Resistance v2)
        {
            return new Resistivity(v1.Value * v2.Value);
        }
        // Length * by Resistivity gives ResistanceTimesArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator *(Length v1, Resistivity v2)
        {
            return new ResistanceTimesArea(v1.Value * v2.Value);
        }
        #endregion
        #region powers
        public Area Squared()
        {
            return new Area(this.Value * this.Value);
        }
        public Volume Cubed()
        {
            return new Volume(this.Value * this.Value * this.Value);
        }
        #endregion

    }

    public readonly partial struct Time : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Time; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Time(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Time(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Time)
                throw new Exception("Invalid conversion from PhysicalQuantity to Time");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Time Parse(string s)
        {
            Time q = UnitsSystem.Parse(s);
            return q;
        }

        public static Time Parse(string s, UnitsSystem system)
        {
            Time q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Time q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Time(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Time q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Time(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Time v1, Time v2)
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
        public static bool operator ==(Time v1, Time v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Time v1, Time v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Time v1, Time v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Time v1, Time v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Time v1, Time v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Time v1, Time v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator +(Time v1, Time v2)
        {
            return new Time(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator -(Time v1, Time v2)
        {
            return new Time(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator -(Time v)
        {
            return new Time(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator *(Time v1, int v2)
        {
            return new Time(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator *(Time v1, double v2)
        {
            return new Time(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator *(double v1, Time v2)
        {
            return new Time(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator *(Time v1, Dimensionless v2)
        {
            return new Time(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Time v1, int v2)
        {
            return new Time(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Time v1, double v2)
        {
            return new Time(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Time v1, Dimensionless v2)
        {
            return new Time(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Time v1, Time v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Time v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Time v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Time * by Velocity gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(Time v1, Velocity v2)
        {
            return new Length(v1.Value * v2.Value);
        }
        // Time * by AngularVelocity gives Angle 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator *(Time v1, AngularVelocity v2)
        {
            return new Angle(v1.Value * v2.Value);
        }
        // Time * by Time gives TimeSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator *(Time v1, Time v2)
        {
            return new TimeSquared(v1.Value * v2.Value);
        }
        // Time * by AmountOfSubstanceByTime gives AmountOfSubstance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(Time v1, AmountOfSubstanceByTime v2)
        {
            return new AmountOfSubstance(v1.Value * v2.Value);
        }
        // Time * by DiffusionFlux gives AmountOfSubstanceByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator *(Time v1, DiffusionFlux v2)
        {
            return new AmountOfSubstanceByArea(v1.Value * v2.Value);
        }
        // Time * by Acceleration gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(Time v1, Acceleration v2)
        {
            return new Velocity(v1.Value * v2.Value);
        }
        // Time * by Force gives Momentum 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(Time v1, Force v2)
        {
            return new Momentum(v1.Value * v2.Value);
        }
        // Time * by Current gives ElectricCharge 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator *(Time v1, Current v2)
        {
            return new ElectricCharge(v1.Value * v2.Value);
        }
        // Time * by Power gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(Time v1, Power v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // double / by Time gives Frequency 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator /(double v1, Time v2)
        {
            return new Frequency(v1 / v2.Value);
        }
        // Time * by Frequency gives Dimensionless 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(Time v1, Frequency v2)
        {
            return new Dimensionless(v1.Value * v2.Value);
        }
        // Time * by KinematicViscosity gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator *(Time v1, KinematicViscosity v2)
        {
            return new Area(v1.Value * v2.Value);
        }
        // Time * by MassFlowRate gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(Time v1, MassFlowRate v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        // Time * by VolumeFlowRate gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator *(Time v1, VolumeFlowRate v2)
        {
            return new Volume(v1.Value * v2.Value);
        }
        #endregion
        #region powers
        public TimeSquared Squared()
        {
            return new TimeSquared(this.Value * this.Value);
        }
        #endregion

    }

    public readonly partial struct Current : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Current; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Current(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Current(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Current)
                throw new Exception("Invalid conversion from PhysicalQuantity to Current");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Current Parse(string s)
        {
            Current q = UnitsSystem.Parse(s);
            return q;
        }

        public static Current Parse(string s, UnitsSystem system)
        {
            Current q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Current q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Current(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Current q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Current(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Current v1, Current v2)
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
        public static bool operator ==(Current v1, Current v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Current v1, Current v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Current v1, Current v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Current v1, Current v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Current v1, Current v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Current v1, Current v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator +(Current v1, Current v2)
        {
            return new Current(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator -(Current v1, Current v2)
        {
            return new Current(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator -(Current v)
        {
            return new Current(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator *(Current v1, int v2)
        {
            return new Current(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator *(Current v1, double v2)
        {
            return new Current(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator *(double v1, Current v2)
        {
            return new Current(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator *(Current v1, Dimensionless v2)
        {
            return new Current(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator /(Current v1, int v2)
        {
            return new Current(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator /(Current v1, double v2)
        {
            return new Current(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator /(Current v1, Dimensionless v2)
        {
            return new Current(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Current v1, Current v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Current v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Current v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Current * by Time gives ElectricCharge 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator *(Current v1, Time v2)
        {
            return new ElectricCharge(v1.Value * v2.Value);
        }
        // Current * by ElectricPotential gives Power 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(Current v1, ElectricPotential v2)
        {
            return new Power(v1.Value * v2.Value);
        }
        // Current * by Resistance gives ElectricPotential 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator *(Current v1, Resistance v2)
        {
            return new ElectricPotential(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct TemperatureChange : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.TemperatureChange; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public TemperatureChange(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public TemperatureChange(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.TemperatureChange)
                throw new Exception("Invalid conversion from PhysicalQuantity to TemperatureChange");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static TemperatureChange Parse(string s)
        {
            TemperatureChange q = UnitsSystem.Parse(s);
            return q;
        }

        public static TemperatureChange Parse(string s, UnitsSystem system)
        {
            TemperatureChange q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out TemperatureChange q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new TemperatureChange(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out TemperatureChange q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new TemperatureChange(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(TemperatureChange v1, TemperatureChange v2)
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
        public static bool operator ==(TemperatureChange v1, TemperatureChange v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(TemperatureChange v1, TemperatureChange v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(TemperatureChange v1, TemperatureChange v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(TemperatureChange v1, TemperatureChange v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(TemperatureChange v1, TemperatureChange v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(TemperatureChange v1, TemperatureChange v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator +(TemperatureChange v1, TemperatureChange v2)
        {
            return new TemperatureChange(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator -(TemperatureChange v1, TemperatureChange v2)
        {
            return new TemperatureChange(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator -(TemperatureChange v)
        {
            return new TemperatureChange(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator *(TemperatureChange v1, int v2)
        {
            return new TemperatureChange(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator *(TemperatureChange v1, double v2)
        {
            return new TemperatureChange(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator *(double v1, TemperatureChange v2)
        {
            return new TemperatureChange(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator *(TemperatureChange v1, Dimensionless v2)
        {
            return new TemperatureChange(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator /(TemperatureChange v1, int v2)
        {
            return new TemperatureChange(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator /(TemperatureChange v1, double v2)
        {
            return new TemperatureChange(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator /(TemperatureChange v1, Dimensionless v2)
        {
            return new TemperatureChange(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(TemperatureChange v1, TemperatureChange v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(TemperatureChange v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(TemperatureChange v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // TemperatureChange * by ThermalCapacity gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(TemperatureChange v1, ThermalCapacity v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // double / by TemperatureChange gives CoefficientOfThermalExpansion 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator /(double v1, TemperatureChange v2)
        {
            return new CoefficientOfThermalExpansion(v1 / v2.Value);
        }
        // TemperatureChange * by CoefficientOfThermalExpansion gives Dimensionless 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(TemperatureChange v1, CoefficientOfThermalExpansion v2)
        {
            return new Dimensionless(v1.Value * v2.Value);
        }
        // TemperatureChange / by Length gives TemperatureGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator /(TemperatureChange v1, Length v2)
        {
            return new TemperatureGradient(v1.Value / v2.Value);
        }
        // TemperatureChange / by TemperatureGradient gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(TemperatureChange v1, TemperatureGradient v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // TemperatureChange * by ThermalConductivity gives PowerGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator *(TemperatureChange v1, ThermalConductivity v2)
        {
            return new PowerGradient(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct AmountOfSubstance : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.AmountOfSubstance; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AmountOfSubstance(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AmountOfSubstance(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.AmountOfSubstance)
                throw new Exception("Invalid conversion from PhysicalQuantity to AmountOfSubstance");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static AmountOfSubstance Parse(string s)
        {
            AmountOfSubstance q = UnitsSystem.Parse(s);
            return q;
        }

        public static AmountOfSubstance Parse(string s, UnitsSystem system)
        {
            AmountOfSubstance q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out AmountOfSubstance q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new AmountOfSubstance(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out AmountOfSubstance q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new AmountOfSubstance(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(AmountOfSubstance v1, AmountOfSubstance v2)
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
        public static bool operator ==(AmountOfSubstance v1, AmountOfSubstance v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(AmountOfSubstance v1, AmountOfSubstance v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(AmountOfSubstance v1, AmountOfSubstance v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(AmountOfSubstance v1, AmountOfSubstance v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(AmountOfSubstance v1, AmountOfSubstance v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(AmountOfSubstance v1, AmountOfSubstance v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator +(AmountOfSubstance v1, AmountOfSubstance v2)
        {
            return new AmountOfSubstance(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator -(AmountOfSubstance v1, AmountOfSubstance v2)
        {
            return new AmountOfSubstance(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator -(AmountOfSubstance v)
        {
            return new AmountOfSubstance(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(AmountOfSubstance v1, int v2)
        {
            return new AmountOfSubstance(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(AmountOfSubstance v1, double v2)
        {
            return new AmountOfSubstance(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(double v1, AmountOfSubstance v2)
        {
            return new AmountOfSubstance(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(AmountOfSubstance v1, Dimensionless v2)
        {
            return new AmountOfSubstance(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator /(AmountOfSubstance v1, int v2)
        {
            return new AmountOfSubstance(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator /(AmountOfSubstance v1, double v2)
        {
            return new AmountOfSubstance(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator /(AmountOfSubstance v1, Dimensionless v2)
        {
            return new AmountOfSubstance(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(AmountOfSubstance v1, AmountOfSubstance v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(AmountOfSubstance v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(AmountOfSubstance v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // AmountOfSubstance * by MolarMass gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(AmountOfSubstance v1, MolarMass v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        // AmountOfSubstance / by Volume gives MolarConcentration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator /(AmountOfSubstance v1, Volume v2)
        {
            return new MolarConcentration(v1.Value / v2.Value);
        }
        // AmountOfSubstance / by MolarConcentration gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator /(AmountOfSubstance v1, MolarConcentration v2)
        {
            return new Volume(v1.Value / v2.Value);
        }
        // AmountOfSubstance / by Area gives AmountOfSubstanceByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator /(AmountOfSubstance v1, Area v2)
        {
            return new AmountOfSubstanceByArea(v1.Value / v2.Value);
        }
        // AmountOfSubstance / by AmountOfSubstanceByArea gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(AmountOfSubstance v1, AmountOfSubstanceByArea v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // AmountOfSubstance / by Time gives AmountOfSubstanceByTime 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator /(AmountOfSubstance v1, Time v2)
        {
            return new AmountOfSubstanceByTime(v1.Value / v2.Value);
        }
        // AmountOfSubstance / by AmountOfSubstanceByTime gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(AmountOfSubstance v1, AmountOfSubstanceByTime v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // AmountOfSubstance * by MolarSpecificHeat gives ThermalCapacity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(AmountOfSubstance v1, MolarSpecificHeat v2)
        {
            return new ThermalCapacity(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct LuminousIntensity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.LuminousIntensity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public LuminousIntensity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public LuminousIntensity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.LuminousIntensity)
                throw new Exception("Invalid conversion from PhysicalQuantity to LuminousIntensity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static LuminousIntensity Parse(string s)
        {
            LuminousIntensity q = UnitsSystem.Parse(s);
            return q;
        }

        public static LuminousIntensity Parse(string s, UnitsSystem system)
        {
            LuminousIntensity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out LuminousIntensity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new LuminousIntensity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out LuminousIntensity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new LuminousIntensity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(LuminousIntensity v1, LuminousIntensity v2)
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
        public static bool operator ==(LuminousIntensity v1, LuminousIntensity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(LuminousIntensity v1, LuminousIntensity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(LuminousIntensity v1, LuminousIntensity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(LuminousIntensity v1, LuminousIntensity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(LuminousIntensity v1, LuminousIntensity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(LuminousIntensity v1, LuminousIntensity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator +(LuminousIntensity v1, LuminousIntensity v2)
        {
            return new LuminousIntensity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator -(LuminousIntensity v1, LuminousIntensity v2)
        {
            return new LuminousIntensity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator -(LuminousIntensity v)
        {
            return new LuminousIntensity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator *(LuminousIntensity v1, int v2)
        {
            return new LuminousIntensity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator *(LuminousIntensity v1, double v2)
        {
            return new LuminousIntensity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator *(double v1, LuminousIntensity v2)
        {
            return new LuminousIntensity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator *(LuminousIntensity v1, Dimensionless v2)
        {
            return new LuminousIntensity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator /(LuminousIntensity v1, int v2)
        {
            return new LuminousIntensity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator /(LuminousIntensity v1, double v2)
        {
            return new LuminousIntensity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator /(LuminousIntensity v1, Dimensionless v2)
        {
            return new LuminousIntensity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(LuminousIntensity v1, LuminousIntensity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(LuminousIntensity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(LuminousIntensity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // LuminousIntensity * by SolidAngle gives LuminousFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator *(LuminousIntensity v1, SolidAngle v2)
        {
            return new LuminousFlux(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct Angle : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Angle; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Angle(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Angle(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Angle)
                throw new Exception("Invalid conversion from PhysicalQuantity to Angle");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Angle Parse(string s)
        {
            Angle q = UnitsSystem.Parse(s);
            return q;
        }

        public static Angle Parse(string s, UnitsSystem system)
        {
            Angle q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Angle q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Angle(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Angle q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Angle(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Angle v1, Angle v2)
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
        public static bool operator ==(Angle v1, Angle v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Angle v1, Angle v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Angle v1, Angle v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Angle v1, Angle v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Angle v1, Angle v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Angle v1, Angle v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator +(Angle v1, Angle v2)
        {
            return new Angle(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator -(Angle v1, Angle v2)
        {
            return new Angle(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator -(Angle v)
        {
            return new Angle(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator *(Angle v1, int v2)
        {
            return new Angle(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator *(Angle v1, double v2)
        {
            return new Angle(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator *(double v1, Angle v2)
        {
            return new Angle(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator *(Angle v1, Dimensionless v2)
        {
            return new Angle(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator /(Angle v1, int v2)
        {
            return new Angle(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator /(Angle v1, double v2)
        {
            return new Angle(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator /(Angle v1, Dimensionless v2)
        {
            return new Angle(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Angle v1, Angle v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Angle v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Angle v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Angle / by Time gives AngularVelocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator /(Angle v1, Time v2)
        {
            return new AngularVelocity(v1.Value / v2.Value);
        }
        // Angle / by AngularVelocity gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Angle v1, AngularVelocity v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // Angle * by Angle gives SolidAngle 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator *(Angle v1, Angle v2)
        {
            return new SolidAngle(v1.Value * v2.Value);
        }
        // Angle * by Frequency gives AngularVelocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator *(Angle v1, Frequency v2)
        {
            return new AngularVelocity(v1.Value * v2.Value);
        }
        #endregion
        #region powers
        public SolidAngle Squared()
        {
            return new SolidAngle(this.Value * this.Value);
        }
        #endregion

    }

    public readonly partial struct Area : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Area; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Area(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Area(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Area)
                throw new Exception("Invalid conversion from PhysicalQuantity to Area");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Area Parse(string s)
        {
            Area q = UnitsSystem.Parse(s);
            return q;
        }

        public static Area Parse(string s, UnitsSystem system)
        {
            Area q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Area q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Area(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Area q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Area(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Area v1, Area v2)
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
        public static bool operator ==(Area v1, Area v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Area v1, Area v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Area v1, Area v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Area v1, Area v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Area v1, Area v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Area v1, Area v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator +(Area v1, Area v2)
        {
            return new Area(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator -(Area v1, Area v2)
        {
            return new Area(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator -(Area v)
        {
            return new Area(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator *(Area v1, int v2)
        {
            return new Area(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator *(Area v1, double v2)
        {
            return new Area(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator *(double v1, Area v2)
        {
            return new Area(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator *(Area v1, Dimensionless v2)
        {
            return new Area(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Area v1, int v2)
        {
            return new Area(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Area v1, double v2)
        {
            return new Area(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Area v1, Dimensionless v2)
        {
            return new Area(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Area v1, Area v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Area v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Area v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Area / by Length gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Area v1, Length v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // Area * by Length gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator *(Area v1, Length v2)
        {
            return new Volume(v1.Value * v2.Value);
        }
        // Area / by Volume gives ByLength 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator /(Area v1, Volume v2)
        {
            return new ByLength(v1.Value / v2.Value);
        }
        // Area / by ByLength gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator /(Area v1, ByLength v2)
        {
            return new Volume(v1.Value / v2.Value);
        }
        // double / by Area gives ByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator /(double v1, Area v2)
        {
            return new ByArea(v1 / v2.Value);
        }
        // Area * by ByArea gives Dimensionless 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(Area v1, ByArea v2)
        {
            return new Dimensionless(v1.Value * v2.Value);
        }
        // Area * by MassByArea gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(Area v1, MassByArea v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        // Area * by Area gives FourDimensionalVolume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator *(Area v1, Area v2)
        {
            return new FourDimensionalVolume(v1.Value * v2.Value);
        }
        // Area * by AmountOfSubstanceByArea gives AmountOfSubstance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(Area v1, AmountOfSubstanceByArea v2)
        {
            return new AmountOfSubstance(v1.Value * v2.Value);
        }
        // Area * by DiffusionFlux gives AmountOfSubstanceByTime 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator *(Area v1, DiffusionFlux v2)
        {
            return new AmountOfSubstanceByTime(v1.Value * v2.Value);
        }
        // Area * by Mass gives MomentOfInertia 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator *(Area v1, Mass v2)
        {
            return new MomentOfInertia(v1.Value * v2.Value);
        }
        // Area * by SurfaceTension gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(Area v1, SurfaceTension v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // Area * by Pressure gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(Area v1, Pressure v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        // Area / by Time gives KinematicViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator /(Area v1, Time v2)
        {
            return new KinematicViscosity(v1.Value / v2.Value);
        }
        // Area / by KinematicViscosity gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Area v1, KinematicViscosity v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // Area * by VelocityGradient gives KinematicViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator *(Area v1, VelocityGradient v2)
        {
            return new KinematicViscosity(v1.Value * v2.Value);
        }
        // Area * by CoefficientOfViscosity gives Momentum 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(Area v1, CoefficientOfViscosity v2)
        {
            return new Momentum(v1.Value * v2.Value);
        }
        // Area * by Acceleration gives MassByAreaByTimeSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator *(Area v1, Acceleration v2)
        {
            return new MassByAreaByTimeSquared(v1.Value * v2.Value);
        }
        // Area * by EnergyFlux gives Power 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(Area v1, EnergyFlux v2)
        {
            return new Power(v1.Value * v2.Value);
        }
        // Area * by Resistance gives ResistanceTimesArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator *(Area v1, Resistance v2)
        {
            return new ResistanceTimesArea(v1.Value * v2.Value);
        }
        // Area * by Illuminance gives LuminousFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator *(Area v1, Illuminance v2)
        {
            return new LuminousFlux(v1.Value * v2.Value);
        }
        #endregion
        #region powers
        public FourDimensionalVolume Squared()
        {
            return new FourDimensionalVolume(this.Value * this.Value);
        }
        #endregion

    }

    public readonly partial struct Volume : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Volume; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Volume(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Volume(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Volume)
                throw new Exception("Invalid conversion from PhysicalQuantity to Volume");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Volume Parse(string s)
        {
            Volume q = UnitsSystem.Parse(s);
            return q;
        }

        public static Volume Parse(string s, UnitsSystem system)
        {
            Volume q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Volume q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Volume(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Volume q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Volume(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Volume v1, Volume v2)
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
        public static bool operator ==(Volume v1, Volume v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Volume v1, Volume v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Volume v1, Volume v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Volume v1, Volume v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Volume v1, Volume v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Volume v1, Volume v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator +(Volume v1, Volume v2)
        {
            return new Volume(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator -(Volume v1, Volume v2)
        {
            return new Volume(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator -(Volume v)
        {
            return new Volume(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator *(Volume v1, int v2)
        {
            return new Volume(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator *(Volume v1, double v2)
        {
            return new Volume(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator *(double v1, Volume v2)
        {
            return new Volume(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator *(Volume v1, Dimensionless v2)
        {
            return new Volume(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator /(Volume v1, int v2)
        {
            return new Volume(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator /(Volume v1, double v2)
        {
            return new Volume(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator /(Volume v1, Dimensionless v2)
        {
            return new Volume(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Volume v1, Volume v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Volume v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Volume v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Volume / by Area gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Volume v1, Area v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // Volume / by Length gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Volume v1, Length v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // Volume * by Density gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(Volume v1, Density v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        // Volume * by MolarConcentration gives AmountOfSubstance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(Volume v1, MolarConcentration v2)
        {
            return new AmountOfSubstance(v1.Value * v2.Value);
        }
        // Volume * by ByLength gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator *(Volume v1, ByLength v2)
        {
            return new Area(v1.Value * v2.Value);
        }
        // Volume * by ByArea gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(Volume v1, ByArea v2)
        {
            return new Length(v1.Value * v2.Value);
        }
        // Volume * by Length gives FourDimensionalVolume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator *(Volume v1, Length v2)
        {
            return new FourDimensionalVolume(v1.Value * v2.Value);
        }
        // Volume / by Time gives VolumeFlowRate 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator /(Volume v1, Time v2)
        {
            return new VolumeFlowRate(v1.Value / v2.Value);
        }
        // Volume / by VolumeFlowRate gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Volume v1, VolumeFlowRate v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // Volume * by ThermalCapacityByVolume gives ThermalCapacity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(Volume v1, ThermalCapacityByVolume v2)
        {
            return new ThermalCapacity(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct Density : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Density; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Density(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Density(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Density)
                throw new Exception("Invalid conversion from PhysicalQuantity to Density");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Density Parse(string s)
        {
            Density q = UnitsSystem.Parse(s);
            return q;
        }

        public static Density Parse(string s, UnitsSystem system)
        {
            Density q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Density q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Density(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Density q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Density(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Density v1, Density v2)
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
        public static bool operator ==(Density v1, Density v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Density v1, Density v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Density v1, Density v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Density v1, Density v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Density v1, Density v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Density v1, Density v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator +(Density v1, Density v2)
        {
            return new Density(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator -(Density v1, Density v2)
        {
            return new Density(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator -(Density v)
        {
            return new Density(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator *(Density v1, int v2)
        {
            return new Density(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator *(Density v1, double v2)
        {
            return new Density(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator *(double v1, Density v2)
        {
            return new Density(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator *(Density v1, Dimensionless v2)
        {
            return new Density(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator /(Density v1, int v2)
        {
            return new Density(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator /(Density v1, double v2)
        {
            return new Density(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator /(Density v1, Dimensionless v2)
        {
            return new Density(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Density v1, Density v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Density v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Density v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Density * by Volume gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(Density v1, Volume v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        // Density / by MolarMass gives MolarConcentration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator /(Density v1, MolarMass v2)
        {
            return new MolarConcentration(v1.Value / v2.Value);
        }
        // Density / by MolarConcentration gives MolarMass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator /(Density v1, MolarConcentration v2)
        {
            return new MolarMass(v1.Value / v2.Value);
        }
        // Density * by Length gives MassByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator *(Density v1, Length v2)
        {
            return new MassByArea(v1.Value * v2.Value);
        }
        // Density * by VelocityByDensity gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(Density v1, VelocityByDensity v2)
        {
            return new Velocity(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct MolarMass : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MolarMass; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarMass(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarMass(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MolarMass)
                throw new Exception("Invalid conversion from PhysicalQuantity to MolarMass");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MolarMass Parse(string s)
        {
            MolarMass q = UnitsSystem.Parse(s);
            return q;
        }

        public static MolarMass Parse(string s, UnitsSystem system)
        {
            MolarMass q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MolarMass q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MolarMass(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MolarMass q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MolarMass(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MolarMass v1, MolarMass v2)
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
        public static bool operator ==(MolarMass v1, MolarMass v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MolarMass v1, MolarMass v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MolarMass v1, MolarMass v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MolarMass v1, MolarMass v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MolarMass v1, MolarMass v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MolarMass v1, MolarMass v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator +(MolarMass v1, MolarMass v2)
        {
            return new MolarMass(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator -(MolarMass v1, MolarMass v2)
        {
            return new MolarMass(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator -(MolarMass v)
        {
            return new MolarMass(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator *(MolarMass v1, int v2)
        {
            return new MolarMass(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator *(MolarMass v1, double v2)
        {
            return new MolarMass(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator *(double v1, MolarMass v2)
        {
            return new MolarMass(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator *(MolarMass v1, Dimensionless v2)
        {
            return new MolarMass(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator /(MolarMass v1, int v2)
        {
            return new MolarMass(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator /(MolarMass v1, double v2)
        {
            return new MolarMass(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarMass operator /(MolarMass v1, Dimensionless v2)
        {
            return new MolarMass(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MolarMass v1, MolarMass v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MolarMass v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MolarMass v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MolarMass * by AmountOfSubstance gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(MolarMass v1, AmountOfSubstance v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        // MolarMass * by MolarConcentration gives Density 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator *(MolarMass v1, MolarConcentration v2)
        {
            return new Density(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct MolarConcentration : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MolarConcentration; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarConcentration(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarConcentration(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MolarConcentration)
                throw new Exception("Invalid conversion from PhysicalQuantity to MolarConcentration");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MolarConcentration Parse(string s)
        {
            MolarConcentration q = UnitsSystem.Parse(s);
            return q;
        }

        public static MolarConcentration Parse(string s, UnitsSystem system)
        {
            MolarConcentration q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MolarConcentration q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MolarConcentration(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MolarConcentration q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MolarConcentration(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MolarConcentration v1, MolarConcentration v2)
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
        public static bool operator ==(MolarConcentration v1, MolarConcentration v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MolarConcentration v1, MolarConcentration v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MolarConcentration v1, MolarConcentration v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MolarConcentration v1, MolarConcentration v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MolarConcentration v1, MolarConcentration v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MolarConcentration v1, MolarConcentration v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator +(MolarConcentration v1, MolarConcentration v2)
        {
            return new MolarConcentration(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator -(MolarConcentration v1, MolarConcentration v2)
        {
            return new MolarConcentration(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator -(MolarConcentration v)
        {
            return new MolarConcentration(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator *(MolarConcentration v1, int v2)
        {
            return new MolarConcentration(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator *(MolarConcentration v1, double v2)
        {
            return new MolarConcentration(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator *(double v1, MolarConcentration v2)
        {
            return new MolarConcentration(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator *(MolarConcentration v1, Dimensionless v2)
        {
            return new MolarConcentration(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator /(MolarConcentration v1, int v2)
        {
            return new MolarConcentration(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator /(MolarConcentration v1, double v2)
        {
            return new MolarConcentration(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator /(MolarConcentration v1, Dimensionless v2)
        {
            return new MolarConcentration(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MolarConcentration v1, MolarConcentration v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MolarConcentration v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MolarConcentration v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MolarConcentration * by Volume gives AmountOfSubstance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(MolarConcentration v1, Volume v2)
        {
            return new AmountOfSubstance(v1.Value * v2.Value);
        }
        // MolarConcentration * by MolarMass gives Density 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator *(MolarConcentration v1, MolarMass v2)
        {
            return new Density(v1.Value * v2.Value);
        }
        // MolarConcentration / by Length gives MolarConcentrationGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator /(MolarConcentration v1, Length v2)
        {
            return new MolarConcentrationGradient(v1.Value / v2.Value);
        }
        // MolarConcentration / by MolarConcentrationGradient gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(MolarConcentration v1, MolarConcentrationGradient v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // MolarConcentration * by MolarSpecificHeat gives ThermalCapacityByVolume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator *(MolarConcentration v1, MolarSpecificHeat v2)
        {
            return new ThermalCapacityByVolume(v1.Value * v2.Value);
        }
        // MolarConcentration * by AbsoluteTemperature gives MolarConcentrationTimesAbsoluteTemperature 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator *(MolarConcentration v1, AbsoluteTemperature v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct Velocity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Velocity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Velocity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Velocity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Velocity)
                throw new Exception("Invalid conversion from PhysicalQuantity to Velocity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Velocity Parse(string s)
        {
            Velocity q = UnitsSystem.Parse(s);
            return q;
        }

        public static Velocity Parse(string s, UnitsSystem system)
        {
            Velocity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Velocity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Velocity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Velocity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Velocity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Velocity v1, Velocity v2)
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
        public static bool operator ==(Velocity v1, Velocity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Velocity v1, Velocity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Velocity v1, Velocity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Velocity v1, Velocity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Velocity v1, Velocity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Velocity v1, Velocity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator +(Velocity v1, Velocity v2)
        {
            return new Velocity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator -(Velocity v1, Velocity v2)
        {
            return new Velocity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator -(Velocity v)
        {
            return new Velocity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(Velocity v1, int v2)
        {
            return new Velocity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(Velocity v1, double v2)
        {
            return new Velocity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(double v1, Velocity v2)
        {
            return new Velocity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(Velocity v1, Dimensionless v2)
        {
            return new Velocity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator /(Velocity v1, int v2)
        {
            return new Velocity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator /(Velocity v1, double v2)
        {
            return new Velocity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator /(Velocity v1, Dimensionless v2)
        {
            return new Velocity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Velocity v1, Velocity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Velocity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Velocity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Velocity * by Time gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(Velocity v1, Time v2)
        {
            return new Length(v1.Value * v2.Value);
        }
        // Velocity * by Velocity gives VelocitySquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator *(Velocity v1, Velocity v2)
        {
            return new VelocitySquared(v1.Value * v2.Value);
        }
        // Velocity / by Time gives Acceleration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator /(Velocity v1, Time v2)
        {
            return new Acceleration(v1.Value / v2.Value);
        }
        // Velocity / by Acceleration gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Velocity v1, Acceleration v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // Velocity * by Mass gives Momentum 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(Velocity v1, Mass v2)
        {
            return new Momentum(v1.Value * v2.Value);
        }
        // Velocity * by MassFlowRate gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(Velocity v1, MassFlowRate v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        // Velocity / by Length gives VelocityGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator /(Velocity v1, Length v2)
        {
            return new VelocityGradient(v1.Value / v2.Value);
        }
        // Velocity / by VelocityGradient gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Velocity v1, VelocityGradient v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // Velocity * by MassByArea gives CoefficientOfViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator *(Velocity v1, MassByArea v2)
        {
            return new CoefficientOfViscosity(v1.Value * v2.Value);
        }
        // Velocity / by Density gives VelocityByDensity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator /(Velocity v1, Density v2)
        {
            return new VelocityByDensity(v1.Value / v2.Value);
        }
        // Velocity / by VelocityByDensity gives Density 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator /(Velocity v1, VelocityByDensity v2)
        {
            return new Density(v1.Value / v2.Value);
        }
        #endregion
        #region powers
        public VelocitySquared Squared()
        {
            return new VelocitySquared(this.Value * this.Value);
        }
        #endregion

    }

    /// <summary>
    /// Special type introduced to resolve ambiguity with velocity gradient.
    /// </summary>
    public readonly partial struct TangentialVelocity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.TangentialVelocity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public TangentialVelocity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public TangentialVelocity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.TangentialVelocity)
                throw new Exception("Invalid conversion from PhysicalQuantity to TangentialVelocity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static TangentialVelocity Parse(string s)
        {
            TangentialVelocity q = UnitsSystem.Parse(s);
            return q;
        }

        public static TangentialVelocity Parse(string s, UnitsSystem system)
        {
            TangentialVelocity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out TangentialVelocity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new TangentialVelocity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out TangentialVelocity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new TangentialVelocity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(TangentialVelocity v1, TangentialVelocity v2)
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
        public static bool operator ==(TangentialVelocity v1, TangentialVelocity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(TangentialVelocity v1, TangentialVelocity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(TangentialVelocity v1, TangentialVelocity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(TangentialVelocity v1, TangentialVelocity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(TangentialVelocity v1, TangentialVelocity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(TangentialVelocity v1, TangentialVelocity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator +(TangentialVelocity v1, TangentialVelocity v2)
        {
            return new TangentialVelocity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator -(TangentialVelocity v1, TangentialVelocity v2)
        {
            return new TangentialVelocity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator -(TangentialVelocity v)
        {
            return new TangentialVelocity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator *(TangentialVelocity v1, int v2)
        {
            return new TangentialVelocity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator *(TangentialVelocity v1, double v2)
        {
            return new TangentialVelocity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator *(double v1, TangentialVelocity v2)
        {
            return new TangentialVelocity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator *(TangentialVelocity v1, Dimensionless v2)
        {
            return new TangentialVelocity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator /(TangentialVelocity v1, int v2)
        {
            return new TangentialVelocity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator /(TangentialVelocity v1, double v2)
        {
            return new TangentialVelocity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator /(TangentialVelocity v1, Dimensionless v2)
        {
            return new TangentialVelocity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(TangentialVelocity v1, TangentialVelocity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(TangentialVelocity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(TangentialVelocity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // TangentialVelocity / by Length gives AngularVelocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator /(TangentialVelocity v1, Length v2)
        {
            return new AngularVelocity(v1.Value / v2.Value);
        }
        // TangentialVelocity / by AngularVelocity gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(TangentialVelocity v1, AngularVelocity v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct AngularVelocity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.AngularVelocity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AngularVelocity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AngularVelocity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.AngularVelocity)
                throw new Exception("Invalid conversion from PhysicalQuantity to AngularVelocity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static AngularVelocity Parse(string s)
        {
            AngularVelocity q = UnitsSystem.Parse(s);
            return q;
        }

        public static AngularVelocity Parse(string s, UnitsSystem system)
        {
            AngularVelocity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out AngularVelocity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new AngularVelocity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out AngularVelocity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new AngularVelocity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(AngularVelocity v1, AngularVelocity v2)
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
        public static bool operator ==(AngularVelocity v1, AngularVelocity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(AngularVelocity v1, AngularVelocity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(AngularVelocity v1, AngularVelocity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(AngularVelocity v1, AngularVelocity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(AngularVelocity v1, AngularVelocity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(AngularVelocity v1, AngularVelocity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator +(AngularVelocity v1, AngularVelocity v2)
        {
            return new AngularVelocity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator -(AngularVelocity v1, AngularVelocity v2)
        {
            return new AngularVelocity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator -(AngularVelocity v)
        {
            return new AngularVelocity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator *(AngularVelocity v1, int v2)
        {
            return new AngularVelocity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator *(AngularVelocity v1, double v2)
        {
            return new AngularVelocity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator *(double v1, AngularVelocity v2)
        {
            return new AngularVelocity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator *(AngularVelocity v1, Dimensionless v2)
        {
            return new AngularVelocity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator /(AngularVelocity v1, int v2)
        {
            return new AngularVelocity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator /(AngularVelocity v1, double v2)
        {
            return new AngularVelocity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator /(AngularVelocity v1, Dimensionless v2)
        {
            return new AngularVelocity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(AngularVelocity v1, AngularVelocity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(AngularVelocity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(AngularVelocity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // AngularVelocity * by Time gives Angle 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator *(AngularVelocity v1, Time v2)
        {
            return new Angle(v1.Value * v2.Value);
        }
        // AngularVelocity * by Length gives TangentialVelocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TangentialVelocity operator *(AngularVelocity v1, Length v2)
        {
            return new TangentialVelocity(v1.Value * v2.Value);
        }
        // AngularVelocity * by AngularVelocity gives AngularVelocitySquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator *(AngularVelocity v1, AngularVelocity v2)
        {
            return new AngularVelocitySquared(v1.Value * v2.Value);
        }
        // AngularVelocity * by MomentOfInertia gives AngularMomentum 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator *(AngularVelocity v1, MomentOfInertia v2)
        {
            return new AngularMomentum(v1.Value * v2.Value);
        }
        // AngularVelocity / by Angle gives Frequency 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator /(AngularVelocity v1, Angle v2)
        {
            return new Frequency(v1.Value / v2.Value);
        }
        // AngularVelocity / by Frequency gives Angle 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator /(AngularVelocity v1, Frequency v2)
        {
            return new Angle(v1.Value / v2.Value);
        }
        #endregion
        #region powers
        public AngularVelocitySquared Squared()
        {
            return new AngularVelocitySquared(this.Value * this.Value);
        }
        #endregion

    }

    public readonly partial struct SolidAngle : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.SolidAngle; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public SolidAngle(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public SolidAngle(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.SolidAngle)
                throw new Exception("Invalid conversion from PhysicalQuantity to SolidAngle");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static SolidAngle Parse(string s)
        {
            SolidAngle q = UnitsSystem.Parse(s);
            return q;
        }

        public static SolidAngle Parse(string s, UnitsSystem system)
        {
            SolidAngle q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out SolidAngle q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new SolidAngle(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out SolidAngle q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new SolidAngle(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(SolidAngle v1, SolidAngle v2)
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
        public static bool operator ==(SolidAngle v1, SolidAngle v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(SolidAngle v1, SolidAngle v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(SolidAngle v1, SolidAngle v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(SolidAngle v1, SolidAngle v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(SolidAngle v1, SolidAngle v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(SolidAngle v1, SolidAngle v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator +(SolidAngle v1, SolidAngle v2)
        {
            return new SolidAngle(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator -(SolidAngle v1, SolidAngle v2)
        {
            return new SolidAngle(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator -(SolidAngle v)
        {
            return new SolidAngle(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator *(SolidAngle v1, int v2)
        {
            return new SolidAngle(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator *(SolidAngle v1, double v2)
        {
            return new SolidAngle(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator *(double v1, SolidAngle v2)
        {
            return new SolidAngle(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator *(SolidAngle v1, Dimensionless v2)
        {
            return new SolidAngle(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator /(SolidAngle v1, int v2)
        {
            return new SolidAngle(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator /(SolidAngle v1, double v2)
        {
            return new SolidAngle(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator /(SolidAngle v1, Dimensionless v2)
        {
            return new SolidAngle(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(SolidAngle v1, SolidAngle v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(SolidAngle v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(SolidAngle v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // SolidAngle / by Angle gives Angle 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Angle operator /(SolidAngle v1, Angle v2)
        {
            return new Angle(v1.Value / v2.Value);
        }
        // SolidAngle * by LuminousIntensity gives LuminousFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator *(SolidAngle v1, LuminousIntensity v2)
        {
            return new LuminousFlux(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct TimeSquared : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.TimeSquared; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public TimeSquared(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public TimeSquared(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.TimeSquared)
                throw new Exception("Invalid conversion from PhysicalQuantity to TimeSquared");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static TimeSquared Parse(string s)
        {
            TimeSquared q = UnitsSystem.Parse(s);
            return q;
        }

        public static TimeSquared Parse(string s, UnitsSystem system)
        {
            TimeSquared q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out TimeSquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new TimeSquared(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out TimeSquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new TimeSquared(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(TimeSquared v1, TimeSquared v2)
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
        public static bool operator ==(TimeSquared v1, TimeSquared v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(TimeSquared v1, TimeSquared v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(TimeSquared v1, TimeSquared v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(TimeSquared v1, TimeSquared v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(TimeSquared v1, TimeSquared v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(TimeSquared v1, TimeSquared v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator +(TimeSquared v1, TimeSquared v2)
        {
            return new TimeSquared(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator -(TimeSquared v1, TimeSquared v2)
        {
            return new TimeSquared(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator -(TimeSquared v)
        {
            return new TimeSquared(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator *(TimeSquared v1, int v2)
        {
            return new TimeSquared(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator *(TimeSquared v1, double v2)
        {
            return new TimeSquared(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator *(double v1, TimeSquared v2)
        {
            return new TimeSquared(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator *(TimeSquared v1, Dimensionless v2)
        {
            return new TimeSquared(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator /(TimeSquared v1, int v2)
        {
            return new TimeSquared(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator /(TimeSquared v1, double v2)
        {
            return new TimeSquared(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator /(TimeSquared v1, Dimensionless v2)
        {
            return new TimeSquared(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(TimeSquared v1, TimeSquared v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(TimeSquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(TimeSquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // TimeSquared / by Time gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(TimeSquared v1, Time v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // TimeSquared * by Acceleration gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(TimeSquared v1, Acceleration v2)
        {
            return new Length(v1.Value * v2.Value);
        }
        // TimeSquared * by MassByAreaByTimeSquared gives MassByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator *(TimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return new MassByArea(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct VelocitySquared : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.VelocitySquared; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public VelocitySquared(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public VelocitySquared(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.VelocitySquared)
                throw new Exception("Invalid conversion from PhysicalQuantity to VelocitySquared");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static VelocitySquared Parse(string s)
        {
            VelocitySquared q = UnitsSystem.Parse(s);
            return q;
        }

        public static VelocitySquared Parse(string s, UnitsSystem system)
        {
            VelocitySquared q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out VelocitySquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new VelocitySquared(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out VelocitySquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new VelocitySquared(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(VelocitySquared v1, VelocitySquared v2)
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
        public static bool operator ==(VelocitySquared v1, VelocitySquared v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(VelocitySquared v1, VelocitySquared v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(VelocitySquared v1, VelocitySquared v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(VelocitySquared v1, VelocitySquared v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(VelocitySquared v1, VelocitySquared v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(VelocitySquared v1, VelocitySquared v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator +(VelocitySquared v1, VelocitySquared v2)
        {
            return new VelocitySquared(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator -(VelocitySquared v1, VelocitySquared v2)
        {
            return new VelocitySquared(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator -(VelocitySquared v)
        {
            return new VelocitySquared(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator *(VelocitySquared v1, int v2)
        {
            return new VelocitySquared(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator *(VelocitySquared v1, double v2)
        {
            return new VelocitySquared(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator *(double v1, VelocitySquared v2)
        {
            return new VelocitySquared(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator *(VelocitySquared v1, Dimensionless v2)
        {
            return new VelocitySquared(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator /(VelocitySquared v1, int v2)
        {
            return new VelocitySquared(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator /(VelocitySquared v1, double v2)
        {
            return new VelocitySquared(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator /(VelocitySquared v1, Dimensionless v2)
        {
            return new VelocitySquared(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(VelocitySquared v1, VelocitySquared v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(VelocitySquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(VelocitySquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // VelocitySquared / by Velocity gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator /(VelocitySquared v1, Velocity v2)
        {
            return new Velocity(v1.Value / v2.Value);
        }
        // VelocitySquared / by Length gives Acceleration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator /(VelocitySquared v1, Length v2)
        {
            return new Acceleration(v1.Value / v2.Value);
        }
        // VelocitySquared / by Acceleration gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(VelocitySquared v1, Acceleration v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // VelocitySquared * by Mass gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(VelocitySquared v1, Mass v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct AngularVelocitySquared : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.AngularVelocitySquared; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AngularVelocitySquared(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AngularVelocitySquared(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.AngularVelocitySquared)
                throw new Exception("Invalid conversion from PhysicalQuantity to AngularVelocitySquared");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static AngularVelocitySquared Parse(string s)
        {
            AngularVelocitySquared q = UnitsSystem.Parse(s);
            return q;
        }

        public static AngularVelocitySquared Parse(string s, UnitsSystem system)
        {
            AngularVelocitySquared q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out AngularVelocitySquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new AngularVelocitySquared(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out AngularVelocitySquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new AngularVelocitySquared(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(AngularVelocitySquared v1, AngularVelocitySquared v2)
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
        public static bool operator ==(AngularVelocitySquared v1, AngularVelocitySquared v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(AngularVelocitySquared v1, AngularVelocitySquared v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(AngularVelocitySquared v1, AngularVelocitySquared v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(AngularVelocitySquared v1, AngularVelocitySquared v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(AngularVelocitySquared v1, AngularVelocitySquared v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(AngularVelocitySquared v1, AngularVelocitySquared v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator +(AngularVelocitySquared v1, AngularVelocitySquared v2)
        {
            return new AngularVelocitySquared(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator -(AngularVelocitySquared v1, AngularVelocitySquared v2)
        {
            return new AngularVelocitySquared(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator -(AngularVelocitySquared v)
        {
            return new AngularVelocitySquared(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator *(AngularVelocitySquared v1, int v2)
        {
            return new AngularVelocitySquared(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator *(AngularVelocitySquared v1, double v2)
        {
            return new AngularVelocitySquared(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator *(double v1, AngularVelocitySquared v2)
        {
            return new AngularVelocitySquared(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator *(AngularVelocitySquared v1, Dimensionless v2)
        {
            return new AngularVelocitySquared(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator /(AngularVelocitySquared v1, int v2)
        {
            return new AngularVelocitySquared(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator /(AngularVelocitySquared v1, double v2)
        {
            return new AngularVelocitySquared(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator /(AngularVelocitySquared v1, Dimensionless v2)
        {
            return new AngularVelocitySquared(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(AngularVelocitySquared v1, AngularVelocitySquared v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(AngularVelocitySquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(AngularVelocitySquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // AngularVelocitySquared / by AngularVelocity gives AngularVelocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator /(AngularVelocitySquared v1, AngularVelocity v2)
        {
            return new AngularVelocity(v1.Value / v2.Value);
        }
        // AngularVelocitySquared * by Length gives Acceleration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator *(AngularVelocitySquared v1, Length v2)
        {
            return new Acceleration(v1.Value * v2.Value);
        }
        // AngularVelocitySquared * by AngularMomentum gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(AngularVelocitySquared v1, AngularMomentum v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct ByLength : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ByLength; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ByLength(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ByLength(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ByLength)
                throw new Exception("Invalid conversion from PhysicalQuantity to ByLength");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ByLength Parse(string s)
        {
            ByLength q = UnitsSystem.Parse(s);
            return q;
        }

        public static ByLength Parse(string s, UnitsSystem system)
        {
            ByLength q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ByLength q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ByLength(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ByLength q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ByLength(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ByLength v1, ByLength v2)
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
        public static bool operator ==(ByLength v1, ByLength v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ByLength v1, ByLength v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ByLength v1, ByLength v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ByLength v1, ByLength v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ByLength v1, ByLength v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ByLength v1, ByLength v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator +(ByLength v1, ByLength v2)
        {
            return new ByLength(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator -(ByLength v1, ByLength v2)
        {
            return new ByLength(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator -(ByLength v)
        {
            return new ByLength(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator *(ByLength v1, int v2)
        {
            return new ByLength(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator *(ByLength v1, double v2)
        {
            return new ByLength(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator *(double v1, ByLength v2)
        {
            return new ByLength(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator *(ByLength v1, Dimensionless v2)
        {
            return new ByLength(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator /(ByLength v1, int v2)
        {
            return new ByLength(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator /(ByLength v1, double v2)
        {
            return new ByLength(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByLength operator /(ByLength v1, Dimensionless v2)
        {
            return new ByLength(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ByLength v1, ByLength v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ByLength v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ByLength v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ByLength * by Length gives Dimensionless 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(ByLength v1, Length v2)
        {
            return new Dimensionless(v1.Value * v2.Value);
        }
        // double / by ByLength gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(double v1, ByLength v2)
        {
            return new Length(v1 / v2.Value);
        }
        // ByLength * by Volume gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator *(ByLength v1, Volume v2)
        {
            return new Area(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct ByArea : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ByArea; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ByArea(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ByArea(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ByArea)
                throw new Exception("Invalid conversion from PhysicalQuantity to ByArea");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ByArea Parse(string s)
        {
            ByArea q = UnitsSystem.Parse(s);
            return q;
        }

        public static ByArea Parse(string s, UnitsSystem system)
        {
            ByArea q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ByArea q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ByArea(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ByArea q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ByArea(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ByArea v1, ByArea v2)
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
        public static bool operator ==(ByArea v1, ByArea v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ByArea v1, ByArea v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ByArea v1, ByArea v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ByArea v1, ByArea v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ByArea v1, ByArea v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ByArea v1, ByArea v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator +(ByArea v1, ByArea v2)
        {
            return new ByArea(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator -(ByArea v1, ByArea v2)
        {
            return new ByArea(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator -(ByArea v)
        {
            return new ByArea(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator *(ByArea v1, int v2)
        {
            return new ByArea(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator *(ByArea v1, double v2)
        {
            return new ByArea(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator *(double v1, ByArea v2)
        {
            return new ByArea(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator *(ByArea v1, Dimensionless v2)
        {
            return new ByArea(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator /(ByArea v1, int v2)
        {
            return new ByArea(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator /(ByArea v1, double v2)
        {
            return new ByArea(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ByArea operator /(ByArea v1, Dimensionless v2)
        {
            return new ByArea(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ByArea v1, ByArea v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ByArea v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ByArea v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ByArea * by Area gives Dimensionless 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(ByArea v1, Area v2)
        {
            return new Dimensionless(v1.Value * v2.Value);
        }
        // double / by ByArea gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(double v1, ByArea v2)
        {
            return new Area(v1 / v2.Value);
        }
        // ByArea * by Volume gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(ByArea v1, Volume v2)
        {
            return new Length(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct MassByLength : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MassByLength; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MassByLength(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MassByLength(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MassByLength)
                throw new Exception("Invalid conversion from PhysicalQuantity to MassByLength");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MassByLength Parse(string s)
        {
            MassByLength q = UnitsSystem.Parse(s);
            return q;
        }

        public static MassByLength Parse(string s, UnitsSystem system)
        {
            MassByLength q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MassByLength q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MassByLength(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MassByLength q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MassByLength(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MassByLength v1, MassByLength v2)
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
        public static bool operator ==(MassByLength v1, MassByLength v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MassByLength v1, MassByLength v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MassByLength v1, MassByLength v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MassByLength v1, MassByLength v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MassByLength v1, MassByLength v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MassByLength v1, MassByLength v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator +(MassByLength v1, MassByLength v2)
        {
            return new MassByLength(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator -(MassByLength v1, MassByLength v2)
        {
            return new MassByLength(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator -(MassByLength v)
        {
            return new MassByLength(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator *(MassByLength v1, int v2)
        {
            return new MassByLength(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator *(MassByLength v1, double v2)
        {
            return new MassByLength(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator *(double v1, MassByLength v2)
        {
            return new MassByLength(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator *(MassByLength v1, Dimensionless v2)
        {
            return new MassByLength(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator /(MassByLength v1, int v2)
        {
            return new MassByLength(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator /(MassByLength v1, double v2)
        {
            return new MassByLength(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByLength operator /(MassByLength v1, Dimensionless v2)
        {
            return new MassByLength(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MassByLength v1, MassByLength v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MassByLength v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MassByLength v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MassByLength * by Length gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(MassByLength v1, Length v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct MassByArea : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MassByArea; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MassByArea(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MassByArea(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MassByArea)
                throw new Exception("Invalid conversion from PhysicalQuantity to MassByArea");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MassByArea Parse(string s)
        {
            MassByArea q = UnitsSystem.Parse(s);
            return q;
        }

        public static MassByArea Parse(string s, UnitsSystem system)
        {
            MassByArea q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MassByArea q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MassByArea(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MassByArea q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MassByArea(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MassByArea v1, MassByArea v2)
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
        public static bool operator ==(MassByArea v1, MassByArea v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MassByArea v1, MassByArea v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MassByArea v1, MassByArea v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MassByArea v1, MassByArea v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MassByArea v1, MassByArea v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MassByArea v1, MassByArea v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator +(MassByArea v1, MassByArea v2)
        {
            return new MassByArea(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator -(MassByArea v1, MassByArea v2)
        {
            return new MassByArea(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator -(MassByArea v)
        {
            return new MassByArea(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator *(MassByArea v1, int v2)
        {
            return new MassByArea(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator *(MassByArea v1, double v2)
        {
            return new MassByArea(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator *(double v1, MassByArea v2)
        {
            return new MassByArea(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator *(MassByArea v1, Dimensionless v2)
        {
            return new MassByArea(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator /(MassByArea v1, int v2)
        {
            return new MassByArea(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator /(MassByArea v1, double v2)
        {
            return new MassByArea(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator /(MassByArea v1, Dimensionless v2)
        {
            return new MassByArea(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MassByArea v1, MassByArea v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MassByArea v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MassByArea v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MassByArea * by Area gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(MassByArea v1, Area v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        // MassByArea / by Length gives Density 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Density operator /(MassByArea v1, Length v2)
        {
            return new Density(v1.Value / v2.Value);
        }
        // MassByArea / by Density gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(MassByArea v1, Density v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // MassByArea * by Acceleration gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(MassByArea v1, Acceleration v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }
        // MassByArea * by Velocity gives CoefficientOfViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator *(MassByArea v1, Velocity v2)
        {
            return new CoefficientOfViscosity(v1.Value * v2.Value);
        }
        // MassByArea / by TimeSquared gives MassByAreaByTimeSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator /(MassByArea v1, TimeSquared v2)
        {
            return new MassByAreaByTimeSquared(v1.Value / v2.Value);
        }
        // MassByArea / by MassByAreaByTimeSquared gives TimeSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TimeSquared operator /(MassByArea v1, MassByAreaByTimeSquared v2)
        {
            return new TimeSquared(v1.Value / v2.Value);
        }
        #endregion

    }

    /// <summary>
    /// This is used in Poiselle's formula.
    /// </summary>
    public readonly partial struct FourDimensionalVolume : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.FourDimensionalVolume; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public FourDimensionalVolume(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public FourDimensionalVolume(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.FourDimensionalVolume)
                throw new Exception("Invalid conversion from PhysicalQuantity to FourDimensionalVolume");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static FourDimensionalVolume Parse(string s)
        {
            FourDimensionalVolume q = UnitsSystem.Parse(s);
            return q;
        }

        public static FourDimensionalVolume Parse(string s, UnitsSystem system)
        {
            FourDimensionalVolume q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out FourDimensionalVolume q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new FourDimensionalVolume(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out FourDimensionalVolume q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new FourDimensionalVolume(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(FourDimensionalVolume v1, FourDimensionalVolume v2)
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
        public static bool operator ==(FourDimensionalVolume v1, FourDimensionalVolume v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(FourDimensionalVolume v1, FourDimensionalVolume v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(FourDimensionalVolume v1, FourDimensionalVolume v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(FourDimensionalVolume v1, FourDimensionalVolume v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(FourDimensionalVolume v1, FourDimensionalVolume v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(FourDimensionalVolume v1, FourDimensionalVolume v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator +(FourDimensionalVolume v1, FourDimensionalVolume v2)
        {
            return new FourDimensionalVolume(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator -(FourDimensionalVolume v1, FourDimensionalVolume v2)
        {
            return new FourDimensionalVolume(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator -(FourDimensionalVolume v)
        {
            return new FourDimensionalVolume(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator *(FourDimensionalVolume v1, int v2)
        {
            return new FourDimensionalVolume(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator *(FourDimensionalVolume v1, double v2)
        {
            return new FourDimensionalVolume(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator *(double v1, FourDimensionalVolume v2)
        {
            return new FourDimensionalVolume(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator *(FourDimensionalVolume v1, Dimensionless v2)
        {
            return new FourDimensionalVolume(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator /(FourDimensionalVolume v1, int v2)
        {
            return new FourDimensionalVolume(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator /(FourDimensionalVolume v1, double v2)
        {
            return new FourDimensionalVolume(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator /(FourDimensionalVolume v1, Dimensionless v2)
        {
            return new FourDimensionalVolume(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(FourDimensionalVolume v1, FourDimensionalVolume v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(FourDimensionalVolume v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(FourDimensionalVolume v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // FourDimensionalVolume / by Volume gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(FourDimensionalVolume v1, Volume v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // FourDimensionalVolume / by Length gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator /(FourDimensionalVolume v1, Length v2)
        {
            return new Volume(v1.Value / v2.Value);
        }
        // FourDimensionalVolume / by Area gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(FourDimensionalVolume v1, Area v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // FourDimensionalVolume * by ResistanceToFlow gives MassFlowRate 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator *(FourDimensionalVolume v1, ResistanceToFlow v2)
        {
            return new MassFlowRate(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct MolarConcentrationGradient : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MolarConcentrationGradient; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarConcentrationGradient(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarConcentrationGradient(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MolarConcentrationGradient)
                throw new Exception("Invalid conversion from PhysicalQuantity to MolarConcentrationGradient");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MolarConcentrationGradient Parse(string s)
        {
            MolarConcentrationGradient q = UnitsSystem.Parse(s);
            return q;
        }

        public static MolarConcentrationGradient Parse(string s, UnitsSystem system)
        {
            MolarConcentrationGradient q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MolarConcentrationGradient q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MolarConcentrationGradient(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MolarConcentrationGradient q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MolarConcentrationGradient(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
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
        public static bool operator ==(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator +(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
        {
            return new MolarConcentrationGradient(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator -(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
        {
            return new MolarConcentrationGradient(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator -(MolarConcentrationGradient v)
        {
            return new MolarConcentrationGradient(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator *(MolarConcentrationGradient v1, int v2)
        {
            return new MolarConcentrationGradient(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator *(MolarConcentrationGradient v1, double v2)
        {
            return new MolarConcentrationGradient(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator *(double v1, MolarConcentrationGradient v2)
        {
            return new MolarConcentrationGradient(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator *(MolarConcentrationGradient v1, Dimensionless v2)
        {
            return new MolarConcentrationGradient(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator /(MolarConcentrationGradient v1, int v2)
        {
            return new MolarConcentrationGradient(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator /(MolarConcentrationGradient v1, double v2)
        {
            return new MolarConcentrationGradient(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator /(MolarConcentrationGradient v1, Dimensionless v2)
        {
            return new MolarConcentrationGradient(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MolarConcentrationGradient v1, MolarConcentrationGradient v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MolarConcentrationGradient v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MolarConcentrationGradient v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MolarConcentrationGradient * by Length gives MolarConcentration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator *(MolarConcentrationGradient v1, Length v2)
        {
            return new MolarConcentration(v1.Value * v2.Value);
        }
        // MolarConcentrationGradient * by KinematicViscosity gives DiffusionFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator *(MolarConcentrationGradient v1, KinematicViscosity v2)
        {
            return new DiffusionFlux(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct AmountOfSubstanceByArea : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.AmountOfSubstanceByArea; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AmountOfSubstanceByArea(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AmountOfSubstanceByArea(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.AmountOfSubstanceByArea)
                throw new Exception("Invalid conversion from PhysicalQuantity to AmountOfSubstanceByArea");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static AmountOfSubstanceByArea Parse(string s)
        {
            AmountOfSubstanceByArea q = UnitsSystem.Parse(s);
            return q;
        }

        public static AmountOfSubstanceByArea Parse(string s, UnitsSystem system)
        {
            AmountOfSubstanceByArea q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out AmountOfSubstanceByArea q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new AmountOfSubstanceByArea(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out AmountOfSubstanceByArea q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new AmountOfSubstanceByArea(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
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
        public static bool operator ==(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator +(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
        {
            return new AmountOfSubstanceByArea(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator -(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
        {
            return new AmountOfSubstanceByArea(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator -(AmountOfSubstanceByArea v)
        {
            return new AmountOfSubstanceByArea(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator *(AmountOfSubstanceByArea v1, int v2)
        {
            return new AmountOfSubstanceByArea(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator *(AmountOfSubstanceByArea v1, double v2)
        {
            return new AmountOfSubstanceByArea(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator *(double v1, AmountOfSubstanceByArea v2)
        {
            return new AmountOfSubstanceByArea(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator *(AmountOfSubstanceByArea v1, Dimensionless v2)
        {
            return new AmountOfSubstanceByArea(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator /(AmountOfSubstanceByArea v1, int v2)
        {
            return new AmountOfSubstanceByArea(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator /(AmountOfSubstanceByArea v1, double v2)
        {
            return new AmountOfSubstanceByArea(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator /(AmountOfSubstanceByArea v1, Dimensionless v2)
        {
            return new AmountOfSubstanceByArea(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(AmountOfSubstanceByArea v1, AmountOfSubstanceByArea v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(AmountOfSubstanceByArea v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(AmountOfSubstanceByArea v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // AmountOfSubstanceByArea * by Area gives AmountOfSubstance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(AmountOfSubstanceByArea v1, Area v2)
        {
            return new AmountOfSubstance(v1.Value * v2.Value);
        }
        // AmountOfSubstanceByArea / by Time gives DiffusionFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator /(AmountOfSubstanceByArea v1, Time v2)
        {
            return new DiffusionFlux(v1.Value / v2.Value);
        }
        // AmountOfSubstanceByArea / by DiffusionFlux gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(AmountOfSubstanceByArea v1, DiffusionFlux v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct AmountOfSubstanceByTime : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.AmountOfSubstanceByTime; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AmountOfSubstanceByTime(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AmountOfSubstanceByTime(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.AmountOfSubstanceByTime)
                throw new Exception("Invalid conversion from PhysicalQuantity to AmountOfSubstanceByTime");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static AmountOfSubstanceByTime Parse(string s)
        {
            AmountOfSubstanceByTime q = UnitsSystem.Parse(s);
            return q;
        }

        public static AmountOfSubstanceByTime Parse(string s, UnitsSystem system)
        {
            AmountOfSubstanceByTime q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out AmountOfSubstanceByTime q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new AmountOfSubstanceByTime(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out AmountOfSubstanceByTime q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new AmountOfSubstanceByTime(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
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
        public static bool operator ==(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator +(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
        {
            return new AmountOfSubstanceByTime(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator -(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
        {
            return new AmountOfSubstanceByTime(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator -(AmountOfSubstanceByTime v)
        {
            return new AmountOfSubstanceByTime(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator *(AmountOfSubstanceByTime v1, int v2)
        {
            return new AmountOfSubstanceByTime(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator *(AmountOfSubstanceByTime v1, double v2)
        {
            return new AmountOfSubstanceByTime(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator *(double v1, AmountOfSubstanceByTime v2)
        {
            return new AmountOfSubstanceByTime(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator *(AmountOfSubstanceByTime v1, Dimensionless v2)
        {
            return new AmountOfSubstanceByTime(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator /(AmountOfSubstanceByTime v1, int v2)
        {
            return new AmountOfSubstanceByTime(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator /(AmountOfSubstanceByTime v1, double v2)
        {
            return new AmountOfSubstanceByTime(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator /(AmountOfSubstanceByTime v1, Dimensionless v2)
        {
            return new AmountOfSubstanceByTime(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(AmountOfSubstanceByTime v1, AmountOfSubstanceByTime v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(AmountOfSubstanceByTime v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(AmountOfSubstanceByTime v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // AmountOfSubstanceByTime * by Time gives AmountOfSubstance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator *(AmountOfSubstanceByTime v1, Time v2)
        {
            return new AmountOfSubstance(v1.Value * v2.Value);
        }
        // AmountOfSubstanceByTime / by Area gives DiffusionFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator /(AmountOfSubstanceByTime v1, Area v2)
        {
            return new DiffusionFlux(v1.Value / v2.Value);
        }
        // AmountOfSubstanceByTime / by DiffusionFlux gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(AmountOfSubstanceByTime v1, DiffusionFlux v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct DiffusionFlux : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.DiffusionFlux; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public DiffusionFlux(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public DiffusionFlux(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.DiffusionFlux)
                throw new Exception("Invalid conversion from PhysicalQuantity to DiffusionFlux");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static DiffusionFlux Parse(string s)
        {
            DiffusionFlux q = UnitsSystem.Parse(s);
            return q;
        }

        public static DiffusionFlux Parse(string s, UnitsSystem system)
        {
            DiffusionFlux q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out DiffusionFlux q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new DiffusionFlux(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out DiffusionFlux q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new DiffusionFlux(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(DiffusionFlux v1, DiffusionFlux v2)
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
        public static bool operator ==(DiffusionFlux v1, DiffusionFlux v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(DiffusionFlux v1, DiffusionFlux v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(DiffusionFlux v1, DiffusionFlux v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(DiffusionFlux v1, DiffusionFlux v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(DiffusionFlux v1, DiffusionFlux v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(DiffusionFlux v1, DiffusionFlux v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator +(DiffusionFlux v1, DiffusionFlux v2)
        {
            return new DiffusionFlux(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator -(DiffusionFlux v1, DiffusionFlux v2)
        {
            return new DiffusionFlux(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator -(DiffusionFlux v)
        {
            return new DiffusionFlux(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator *(DiffusionFlux v1, int v2)
        {
            return new DiffusionFlux(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator *(DiffusionFlux v1, double v2)
        {
            return new DiffusionFlux(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator *(double v1, DiffusionFlux v2)
        {
            return new DiffusionFlux(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator *(DiffusionFlux v1, Dimensionless v2)
        {
            return new DiffusionFlux(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator /(DiffusionFlux v1, int v2)
        {
            return new DiffusionFlux(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator /(DiffusionFlux v1, double v2)
        {
            return new DiffusionFlux(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator /(DiffusionFlux v1, Dimensionless v2)
        {
            return new DiffusionFlux(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(DiffusionFlux v1, DiffusionFlux v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(DiffusionFlux v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(DiffusionFlux v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // DiffusionFlux * by Time gives AmountOfSubstanceByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByArea operator *(DiffusionFlux v1, Time v2)
        {
            return new AmountOfSubstanceByArea(v1.Value * v2.Value);
        }
        // DiffusionFlux / by KinematicViscosity gives MolarConcentrationGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationGradient operator /(DiffusionFlux v1, KinematicViscosity v2)
        {
            return new MolarConcentrationGradient(v1.Value / v2.Value);
        }
        // DiffusionFlux / by MolarConcentrationGradient gives KinematicViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator /(DiffusionFlux v1, MolarConcentrationGradient v2)
        {
            return new KinematicViscosity(v1.Value / v2.Value);
        }
        // DiffusionFlux * by Area gives AmountOfSubstanceByTime 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstanceByTime operator *(DiffusionFlux v1, Area v2)
        {
            return new AmountOfSubstanceByTime(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct KinematicViscosity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.KinematicViscosity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public KinematicViscosity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public KinematicViscosity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.KinematicViscosity)
                throw new Exception("Invalid conversion from PhysicalQuantity to KinematicViscosity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static KinematicViscosity Parse(string s)
        {
            KinematicViscosity q = UnitsSystem.Parse(s);
            return q;
        }

        public static KinematicViscosity Parse(string s, UnitsSystem system)
        {
            KinematicViscosity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out KinematicViscosity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new KinematicViscosity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out KinematicViscosity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new KinematicViscosity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(KinematicViscosity v1, KinematicViscosity v2)
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
        public static bool operator ==(KinematicViscosity v1, KinematicViscosity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(KinematicViscosity v1, KinematicViscosity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(KinematicViscosity v1, KinematicViscosity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(KinematicViscosity v1, KinematicViscosity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(KinematicViscosity v1, KinematicViscosity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(KinematicViscosity v1, KinematicViscosity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator +(KinematicViscosity v1, KinematicViscosity v2)
        {
            return new KinematicViscosity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator -(KinematicViscosity v1, KinematicViscosity v2)
        {
            return new KinematicViscosity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator -(KinematicViscosity v)
        {
            return new KinematicViscosity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator *(KinematicViscosity v1, int v2)
        {
            return new KinematicViscosity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator *(KinematicViscosity v1, double v2)
        {
            return new KinematicViscosity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator *(double v1, KinematicViscosity v2)
        {
            return new KinematicViscosity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator *(KinematicViscosity v1, Dimensionless v2)
        {
            return new KinematicViscosity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator /(KinematicViscosity v1, int v2)
        {
            return new KinematicViscosity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator /(KinematicViscosity v1, double v2)
        {
            return new KinematicViscosity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator /(KinematicViscosity v1, Dimensionless v2)
        {
            return new KinematicViscosity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(KinematicViscosity v1, KinematicViscosity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(KinematicViscosity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(KinematicViscosity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // KinematicViscosity * by MolarConcentrationGradient gives DiffusionFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static DiffusionFlux operator *(KinematicViscosity v1, MolarConcentrationGradient v2)
        {
            return new DiffusionFlux(v1.Value * v2.Value);
        }
        // KinematicViscosity * by Time gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator *(KinematicViscosity v1, Time v2)
        {
            return new Area(v1.Value * v2.Value);
        }
        // KinematicViscosity / by Area gives VelocityGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator /(KinematicViscosity v1, Area v2)
        {
            return new VelocityGradient(v1.Value / v2.Value);
        }
        // KinematicViscosity / by VelocityGradient gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(KinematicViscosity v1, VelocityGradient v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // KinematicViscosity * by CoefficientOfViscosity gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(KinematicViscosity v1, CoefficientOfViscosity v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct Acceleration : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Acceleration; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Acceleration(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Acceleration(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Acceleration)
                throw new Exception("Invalid conversion from PhysicalQuantity to Acceleration");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Acceleration Parse(string s)
        {
            Acceleration q = UnitsSystem.Parse(s);
            return q;
        }

        public static Acceleration Parse(string s, UnitsSystem system)
        {
            Acceleration q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Acceleration q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Acceleration(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Acceleration q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Acceleration(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Acceleration v1, Acceleration v2)
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
        public static bool operator ==(Acceleration v1, Acceleration v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Acceleration v1, Acceleration v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Acceleration v1, Acceleration v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Acceleration v1, Acceleration v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Acceleration v1, Acceleration v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Acceleration v1, Acceleration v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator +(Acceleration v1, Acceleration v2)
        {
            return new Acceleration(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator -(Acceleration v1, Acceleration v2)
        {
            return new Acceleration(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator -(Acceleration v)
        {
            return new Acceleration(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator *(Acceleration v1, int v2)
        {
            return new Acceleration(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator *(Acceleration v1, double v2)
        {
            return new Acceleration(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator *(double v1, Acceleration v2)
        {
            return new Acceleration(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator *(Acceleration v1, Dimensionless v2)
        {
            return new Acceleration(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator /(Acceleration v1, int v2)
        {
            return new Acceleration(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator /(Acceleration v1, double v2)
        {
            return new Acceleration(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator /(Acceleration v1, Dimensionless v2)
        {
            return new Acceleration(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Acceleration v1, Acceleration v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Acceleration v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Acceleration v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Acceleration * by Time gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(Acceleration v1, Time v2)
        {
            return new Velocity(v1.Value * v2.Value);
        }
        // Acceleration * by Length gives VelocitySquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator *(Acceleration v1, Length v2)
        {
            return new VelocitySquared(v1.Value * v2.Value);
        }
        // Acceleration * by TimeSquared gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator *(Acceleration v1, TimeSquared v2)
        {
            return new Length(v1.Value * v2.Value);
        }
        // Acceleration / by Length gives AngularVelocitySquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator /(Acceleration v1, Length v2)
        {
            return new AngularVelocitySquared(v1.Value / v2.Value);
        }
        // Acceleration / by AngularVelocitySquared gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Acceleration v1, AngularVelocitySquared v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // Acceleration * by Mass gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(Acceleration v1, Mass v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        // Acceleration * by MassByArea gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(Acceleration v1, MassByArea v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }
        // Acceleration * by Area gives MassByAreaByTimeSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator *(Acceleration v1, Area v2)
        {
            return new MassByAreaByTimeSquared(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct Momentum : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Momentum; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Momentum(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Momentum(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Momentum)
                throw new Exception("Invalid conversion from PhysicalQuantity to Momentum");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Momentum Parse(string s)
        {
            Momentum q = UnitsSystem.Parse(s);
            return q;
        }

        public static Momentum Parse(string s, UnitsSystem system)
        {
            Momentum q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Momentum q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Momentum(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Momentum q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Momentum(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Momentum v1, Momentum v2)
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
        public static bool operator ==(Momentum v1, Momentum v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Momentum v1, Momentum v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Momentum v1, Momentum v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Momentum v1, Momentum v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Momentum v1, Momentum v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Momentum v1, Momentum v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator +(Momentum v1, Momentum v2)
        {
            return new Momentum(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator -(Momentum v1, Momentum v2)
        {
            return new Momentum(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator -(Momentum v)
        {
            return new Momentum(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(Momentum v1, int v2)
        {
            return new Momentum(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(Momentum v1, double v2)
        {
            return new Momentum(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(double v1, Momentum v2)
        {
            return new Momentum(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(Momentum v1, Dimensionless v2)
        {
            return new Momentum(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator /(Momentum v1, int v2)
        {
            return new Momentum(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator /(Momentum v1, double v2)
        {
            return new Momentum(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator /(Momentum v1, Dimensionless v2)
        {
            return new Momentum(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Momentum v1, Momentum v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Momentum v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Momentum v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Momentum / by Mass gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator /(Momentum v1, Mass v2)
        {
            return new Velocity(v1.Value / v2.Value);
        }
        // Momentum / by Velocity gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator /(Momentum v1, Velocity v2)
        {
            return new Mass(v1.Value / v2.Value);
        }
        // Momentum / by Time gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator /(Momentum v1, Time v2)
        {
            return new Force(v1.Value / v2.Value);
        }
        // Momentum / by Force gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Momentum v1, Force v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // Momentum / by Area gives CoefficientOfViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator /(Momentum v1, Area v2)
        {
            return new CoefficientOfViscosity(v1.Value / v2.Value);
        }
        // Momentum / by CoefficientOfViscosity gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Momentum v1, CoefficientOfViscosity v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct Force : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Force; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Force(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Force(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Force)
                throw new Exception("Invalid conversion from PhysicalQuantity to Force");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Force Parse(string s)
        {
            Force q = UnitsSystem.Parse(s);
            return q;
        }

        public static Force Parse(string s, UnitsSystem system)
        {
            Force q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Force q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Force(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Force q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Force(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Force v1, Force v2)
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
        public static bool operator ==(Force v1, Force v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Force v1, Force v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Force v1, Force v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Force v1, Force v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Force v1, Force v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Force v1, Force v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator +(Force v1, Force v2)
        {
            return new Force(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator -(Force v1, Force v2)
        {
            return new Force(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator -(Force v)
        {
            return new Force(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(Force v1, int v2)
        {
            return new Force(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(Force v1, double v2)
        {
            return new Force(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(double v1, Force v2)
        {
            return new Force(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(Force v1, Dimensionless v2)
        {
            return new Force(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator /(Force v1, int v2)
        {
            return new Force(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator /(Force v1, double v2)
        {
            return new Force(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator /(Force v1, Dimensionless v2)
        {
            return new Force(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Force v1, Force v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Force v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Force v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Force / by Mass gives Acceleration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator /(Force v1, Mass v2)
        {
            return new Acceleration(v1.Value / v2.Value);
        }
        // Force / by Acceleration gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator /(Force v1, Acceleration v2)
        {
            return new Mass(v1.Value / v2.Value);
        }
        // Force * by Time gives Momentum 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(Force v1, Time v2)
        {
            return new Momentum(v1.Value * v2.Value);
        }
        // Force / by MassFlowRate gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator /(Force v1, MassFlowRate v2)
        {
            return new Velocity(v1.Value / v2.Value);
        }
        // Force / by Velocity gives MassFlowRate 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator /(Force v1, Velocity v2)
        {
            return new MassFlowRate(v1.Value / v2.Value);
        }
        // Force * by Length gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(Force v1, Length v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // Force / by Area gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator /(Force v1, Area v2)
        {
            return new Pressure(v1.Value / v2.Value);
        }
        // Force / by Pressure gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Force v1, Pressure v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // Force / by Length gives SurfaceTension 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator /(Force v1, Length v2)
        {
            return new SurfaceTension(v1.Value / v2.Value);
        }
        // Force / by SurfaceTension gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Force v1, SurfaceTension v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // Force / by KinematicViscosity gives CoefficientOfViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator /(Force v1, KinematicViscosity v2)
        {
            return new CoefficientOfViscosity(v1.Value / v2.Value);
        }
        // Force / by CoefficientOfViscosity gives KinematicViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator /(Force v1, CoefficientOfViscosity v2)
        {
            return new KinematicViscosity(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct MassFlowRate : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MassFlowRate; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MassFlowRate(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MassFlowRate(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MassFlowRate)
                throw new Exception("Invalid conversion from PhysicalQuantity to MassFlowRate");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MassFlowRate Parse(string s)
        {
            MassFlowRate q = UnitsSystem.Parse(s);
            return q;
        }

        public static MassFlowRate Parse(string s, UnitsSystem system)
        {
            MassFlowRate q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MassFlowRate q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MassFlowRate(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MassFlowRate q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MassFlowRate(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MassFlowRate v1, MassFlowRate v2)
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
        public static bool operator ==(MassFlowRate v1, MassFlowRate v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MassFlowRate v1, MassFlowRate v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MassFlowRate v1, MassFlowRate v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MassFlowRate v1, MassFlowRate v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MassFlowRate v1, MassFlowRate v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MassFlowRate v1, MassFlowRate v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator +(MassFlowRate v1, MassFlowRate v2)
        {
            return new MassFlowRate(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator -(MassFlowRate v1, MassFlowRate v2)
        {
            return new MassFlowRate(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator -(MassFlowRate v)
        {
            return new MassFlowRate(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator *(MassFlowRate v1, int v2)
        {
            return new MassFlowRate(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator *(MassFlowRate v1, double v2)
        {
            return new MassFlowRate(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator *(double v1, MassFlowRate v2)
        {
            return new MassFlowRate(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator *(MassFlowRate v1, Dimensionless v2)
        {
            return new MassFlowRate(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator /(MassFlowRate v1, int v2)
        {
            return new MassFlowRate(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator /(MassFlowRate v1, double v2)
        {
            return new MassFlowRate(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator /(MassFlowRate v1, Dimensionless v2)
        {
            return new MassFlowRate(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MassFlowRate v1, MassFlowRate v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MassFlowRate v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MassFlowRate v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MassFlowRate * by Velocity gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(MassFlowRate v1, Velocity v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        // MassFlowRate * by Time gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator *(MassFlowRate v1, Time v2)
        {
            return new Mass(v1.Value * v2.Value);
        }
        // MassFlowRate / by CoefficientOfViscosity gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(MassFlowRate v1, CoefficientOfViscosity v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // MassFlowRate / by Length gives CoefficientOfViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator /(MassFlowRate v1, Length v2)
        {
            return new CoefficientOfViscosity(v1.Value / v2.Value);
        }
        // MassFlowRate / by FourDimensionalVolume gives ResistanceToFlow 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator /(MassFlowRate v1, FourDimensionalVolume v2)
        {
            return new ResistanceToFlow(v1.Value / v2.Value);
        }
        // MassFlowRate / by ResistanceToFlow gives FourDimensionalVolume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static FourDimensionalVolume operator /(MassFlowRate v1, ResistanceToFlow v2)
        {
            return new FourDimensionalVolume(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct MomentOfInertia : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MomentOfInertia; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MomentOfInertia(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MomentOfInertia(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MomentOfInertia)
                throw new Exception("Invalid conversion from PhysicalQuantity to MomentOfInertia");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MomentOfInertia Parse(string s)
        {
            MomentOfInertia q = UnitsSystem.Parse(s);
            return q;
        }

        public static MomentOfInertia Parse(string s, UnitsSystem system)
        {
            MomentOfInertia q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MomentOfInertia q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MomentOfInertia(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MomentOfInertia q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MomentOfInertia(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MomentOfInertia v1, MomentOfInertia v2)
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
        public static bool operator ==(MomentOfInertia v1, MomentOfInertia v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MomentOfInertia v1, MomentOfInertia v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MomentOfInertia v1, MomentOfInertia v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MomentOfInertia v1, MomentOfInertia v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MomentOfInertia v1, MomentOfInertia v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MomentOfInertia v1, MomentOfInertia v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator +(MomentOfInertia v1, MomentOfInertia v2)
        {
            return new MomentOfInertia(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator -(MomentOfInertia v1, MomentOfInertia v2)
        {
            return new MomentOfInertia(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator -(MomentOfInertia v)
        {
            return new MomentOfInertia(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator *(MomentOfInertia v1, int v2)
        {
            return new MomentOfInertia(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator *(MomentOfInertia v1, double v2)
        {
            return new MomentOfInertia(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator *(double v1, MomentOfInertia v2)
        {
            return new MomentOfInertia(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator *(MomentOfInertia v1, Dimensionless v2)
        {
            return new MomentOfInertia(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator /(MomentOfInertia v1, int v2)
        {
            return new MomentOfInertia(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator /(MomentOfInertia v1, double v2)
        {
            return new MomentOfInertia(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator /(MomentOfInertia v1, Dimensionless v2)
        {
            return new MomentOfInertia(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MomentOfInertia v1, MomentOfInertia v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MomentOfInertia v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MomentOfInertia v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MomentOfInertia / by Mass gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(MomentOfInertia v1, Mass v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // MomentOfInertia / by Area gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator /(MomentOfInertia v1, Area v2)
        {
            return new Mass(v1.Value / v2.Value);
        }
        // MomentOfInertia * by AngularVelocity gives AngularMomentum 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator *(MomentOfInertia v1, AngularVelocity v2)
        {
            return new AngularMomentum(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct AngularMomentum : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.AngularMomentum; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AngularMomentum(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public AngularMomentum(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.AngularMomentum)
                throw new Exception("Invalid conversion from PhysicalQuantity to AngularMomentum");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static AngularMomentum Parse(string s)
        {
            AngularMomentum q = UnitsSystem.Parse(s);
            return q;
        }

        public static AngularMomentum Parse(string s, UnitsSystem system)
        {
            AngularMomentum q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out AngularMomentum q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new AngularMomentum(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out AngularMomentum q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new AngularMomentum(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(AngularMomentum v1, AngularMomentum v2)
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
        public static bool operator ==(AngularMomentum v1, AngularMomentum v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(AngularMomentum v1, AngularMomentum v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(AngularMomentum v1, AngularMomentum v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(AngularMomentum v1, AngularMomentum v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(AngularMomentum v1, AngularMomentum v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(AngularMomentum v1, AngularMomentum v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator +(AngularMomentum v1, AngularMomentum v2)
        {
            return new AngularMomentum(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator -(AngularMomentum v1, AngularMomentum v2)
        {
            return new AngularMomentum(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator -(AngularMomentum v)
        {
            return new AngularMomentum(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator *(AngularMomentum v1, int v2)
        {
            return new AngularMomentum(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator *(AngularMomentum v1, double v2)
        {
            return new AngularMomentum(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator *(double v1, AngularMomentum v2)
        {
            return new AngularMomentum(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator *(AngularMomentum v1, Dimensionless v2)
        {
            return new AngularMomentum(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator /(AngularMomentum v1, int v2)
        {
            return new AngularMomentum(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator /(AngularMomentum v1, double v2)
        {
            return new AngularMomentum(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator /(AngularMomentum v1, Dimensionless v2)
        {
            return new AngularMomentum(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(AngularMomentum v1, AngularMomentum v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(AngularMomentum v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(AngularMomentum v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // AngularMomentum / by MomentOfInertia gives AngularVelocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator /(AngularMomentum v1, MomentOfInertia v2)
        {
            return new AngularVelocity(v1.Value / v2.Value);
        }
        // AngularMomentum / by AngularVelocity gives MomentOfInertia 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MomentOfInertia operator /(AngularMomentum v1, AngularVelocity v2)
        {
            return new MomentOfInertia(v1.Value / v2.Value);
        }
        // AngularMomentum * by AngularVelocitySquared gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(AngularMomentum v1, AngularVelocitySquared v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct Energy : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Energy; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Energy(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Energy(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Energy)
                throw new Exception("Invalid conversion from PhysicalQuantity to Energy");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Energy Parse(string s)
        {
            Energy q = UnitsSystem.Parse(s);
            return q;
        }

        public static Energy Parse(string s, UnitsSystem system)
        {
            Energy q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Energy q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Energy(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Energy q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Energy(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Energy v1, Energy v2)
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
        public static bool operator ==(Energy v1, Energy v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Energy v1, Energy v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Energy v1, Energy v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Energy v1, Energy v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Energy v1, Energy v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Energy v1, Energy v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator +(Energy v1, Energy v2)
        {
            return new Energy(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator -(Energy v1, Energy v2)
        {
            return new Energy(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator -(Energy v)
        {
            return new Energy(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(Energy v1, int v2)
        {
            return new Energy(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(Energy v1, double v2)
        {
            return new Energy(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(double v1, Energy v2)
        {
            return new Energy(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(Energy v1, Dimensionless v2)
        {
            return new Energy(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator /(Energy v1, int v2)
        {
            return new Energy(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator /(Energy v1, double v2)
        {
            return new Energy(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator /(Energy v1, Dimensionless v2)
        {
            return new Energy(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Energy v1, Energy v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Energy v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Energy v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Energy / by Force gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Energy v1, Force v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // Energy / by Length gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator /(Energy v1, Length v2)
        {
            return new Force(v1.Value / v2.Value);
        }
        // Energy / by Mass gives VelocitySquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocitySquared operator /(Energy v1, Mass v2)
        {
            return new VelocitySquared(v1.Value / v2.Value);
        }
        // Energy / by VelocitySquared gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator /(Energy v1, VelocitySquared v2)
        {
            return new Mass(v1.Value / v2.Value);
        }
        // Energy / by AngularMomentum gives AngularVelocitySquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocitySquared operator /(Energy v1, AngularMomentum v2)
        {
            return new AngularVelocitySquared(v1.Value / v2.Value);
        }
        // Energy / by AngularVelocitySquared gives AngularMomentum 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularMomentum operator /(Energy v1, AngularVelocitySquared v2)
        {
            return new AngularMomentum(v1.Value / v2.Value);
        }
        // Energy / by SurfaceTension gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Energy v1, SurfaceTension v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // Energy / by Area gives SurfaceTension 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator /(Energy v1, Area v2)
        {
            return new SurfaceTension(v1.Value / v2.Value);
        }
        // Energy / by ElectricCharge gives ElectricPotential 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator /(Energy v1, ElectricCharge v2)
        {
            return new ElectricPotential(v1.Value / v2.Value);
        }
        // Energy / by ElectricPotential gives ElectricCharge 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator /(Energy v1, ElectricPotential v2)
        {
            return new ElectricCharge(v1.Value / v2.Value);
        }
        // Energy / by Time gives Power 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator /(Energy v1, Time v2)
        {
            return new Power(v1.Value / v2.Value);
        }
        // Energy / by Power gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(Energy v1, Power v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // Energy / by TemperatureChange gives ThermalCapacity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator /(Energy v1, TemperatureChange v2)
        {
            return new ThermalCapacity(v1.Value / v2.Value);
        }
        // Energy / by ThermalCapacity gives TemperatureChange 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator /(Energy v1, ThermalCapacity v2)
        {
            return new TemperatureChange(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct SurfaceTension : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.SurfaceTension; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public SurfaceTension(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public SurfaceTension(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.SurfaceTension)
                throw new Exception("Invalid conversion from PhysicalQuantity to SurfaceTension");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static SurfaceTension Parse(string s)
        {
            SurfaceTension q = UnitsSystem.Parse(s);
            return q;
        }

        public static SurfaceTension Parse(string s, UnitsSystem system)
        {
            SurfaceTension q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out SurfaceTension q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new SurfaceTension(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out SurfaceTension q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new SurfaceTension(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(SurfaceTension v1, SurfaceTension v2)
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
        public static bool operator ==(SurfaceTension v1, SurfaceTension v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(SurfaceTension v1, SurfaceTension v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(SurfaceTension v1, SurfaceTension v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(SurfaceTension v1, SurfaceTension v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(SurfaceTension v1, SurfaceTension v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(SurfaceTension v1, SurfaceTension v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator +(SurfaceTension v1, SurfaceTension v2)
        {
            return new SurfaceTension(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator -(SurfaceTension v1, SurfaceTension v2)
        {
            return new SurfaceTension(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator -(SurfaceTension v)
        {
            return new SurfaceTension(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator *(SurfaceTension v1, int v2)
        {
            return new SurfaceTension(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator *(SurfaceTension v1, double v2)
        {
            return new SurfaceTension(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator *(double v1, SurfaceTension v2)
        {
            return new SurfaceTension(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator *(SurfaceTension v1, Dimensionless v2)
        {
            return new SurfaceTension(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator /(SurfaceTension v1, int v2)
        {
            return new SurfaceTension(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator /(SurfaceTension v1, double v2)
        {
            return new SurfaceTension(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator /(SurfaceTension v1, Dimensionless v2)
        {
            return new SurfaceTension(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(SurfaceTension v1, SurfaceTension v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(SurfaceTension v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(SurfaceTension v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // SurfaceTension * by Area gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(SurfaceTension v1, Area v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // SurfaceTension * by Length gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(SurfaceTension v1, Length v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        // SurfaceTension / by Length gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator /(SurfaceTension v1, Length v2)
        {
            return new Pressure(v1.Value / v2.Value);
        }
        // SurfaceTension / by Pressure gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(SurfaceTension v1, Pressure v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct ElectricCharge : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ElectricCharge; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ElectricCharge(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ElectricCharge(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ElectricCharge)
                throw new Exception("Invalid conversion from PhysicalQuantity to ElectricCharge");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ElectricCharge Parse(string s)
        {
            ElectricCharge q = UnitsSystem.Parse(s);
            return q;
        }

        public static ElectricCharge Parse(string s, UnitsSystem system)
        {
            ElectricCharge q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ElectricCharge q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ElectricCharge(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ElectricCharge q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ElectricCharge(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ElectricCharge v1, ElectricCharge v2)
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
        public static bool operator ==(ElectricCharge v1, ElectricCharge v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ElectricCharge v1, ElectricCharge v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ElectricCharge v1, ElectricCharge v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ElectricCharge v1, ElectricCharge v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ElectricCharge v1, ElectricCharge v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ElectricCharge v1, ElectricCharge v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator +(ElectricCharge v1, ElectricCharge v2)
        {
            return new ElectricCharge(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator -(ElectricCharge v1, ElectricCharge v2)
        {
            return new ElectricCharge(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator -(ElectricCharge v)
        {
            return new ElectricCharge(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator *(ElectricCharge v1, int v2)
        {
            return new ElectricCharge(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator *(ElectricCharge v1, double v2)
        {
            return new ElectricCharge(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator *(double v1, ElectricCharge v2)
        {
            return new ElectricCharge(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator *(ElectricCharge v1, Dimensionless v2)
        {
            return new ElectricCharge(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator /(ElectricCharge v1, int v2)
        {
            return new ElectricCharge(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator /(ElectricCharge v1, double v2)
        {
            return new ElectricCharge(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricCharge operator /(ElectricCharge v1, Dimensionless v2)
        {
            return new ElectricCharge(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ElectricCharge v1, ElectricCharge v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ElectricCharge v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ElectricCharge v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ElectricCharge / by Current gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(ElectricCharge v1, Current v2)
        {
            return new Time(v1.Value / v2.Value);
        }
        // ElectricCharge / by Time gives Current 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator /(ElectricCharge v1, Time v2)
        {
            return new Current(v1.Value / v2.Value);
        }
        // ElectricCharge * by ElectricPotential gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(ElectricCharge v1, ElectricPotential v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct ElectricPotential : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ElectricPotential; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ElectricPotential(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ElectricPotential(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ElectricPotential)
                throw new Exception("Invalid conversion from PhysicalQuantity to ElectricPotential");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ElectricPotential Parse(string s)
        {
            ElectricPotential q = UnitsSystem.Parse(s);
            return q;
        }

        public static ElectricPotential Parse(string s, UnitsSystem system)
        {
            ElectricPotential q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ElectricPotential q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ElectricPotential(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ElectricPotential q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ElectricPotential(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ElectricPotential v1, ElectricPotential v2)
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
        public static bool operator ==(ElectricPotential v1, ElectricPotential v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ElectricPotential v1, ElectricPotential v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ElectricPotential v1, ElectricPotential v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ElectricPotential v1, ElectricPotential v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ElectricPotential v1, ElectricPotential v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ElectricPotential v1, ElectricPotential v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator +(ElectricPotential v1, ElectricPotential v2)
        {
            return new ElectricPotential(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator -(ElectricPotential v1, ElectricPotential v2)
        {
            return new ElectricPotential(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator -(ElectricPotential v)
        {
            return new ElectricPotential(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator *(ElectricPotential v1, int v2)
        {
            return new ElectricPotential(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator *(ElectricPotential v1, double v2)
        {
            return new ElectricPotential(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator *(double v1, ElectricPotential v2)
        {
            return new ElectricPotential(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator *(ElectricPotential v1, Dimensionless v2)
        {
            return new ElectricPotential(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator /(ElectricPotential v1, int v2)
        {
            return new ElectricPotential(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator /(ElectricPotential v1, double v2)
        {
            return new ElectricPotential(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator /(ElectricPotential v1, Dimensionless v2)
        {
            return new ElectricPotential(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ElectricPotential v1, ElectricPotential v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ElectricPotential v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ElectricPotential v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ElectricPotential * by ElectricCharge gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(ElectricPotential v1, ElectricCharge v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // ElectricPotential * by ElectricPotential gives ElectricPotentialSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator *(ElectricPotential v1, ElectricPotential v2)
        {
            return new ElectricPotentialSquared(v1.Value * v2.Value);
        }
        // ElectricPotential * by Current gives Power 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(ElectricPotential v1, Current v2)
        {
            return new Power(v1.Value * v2.Value);
        }
        // ElectricPotential / by Current gives Resistance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator /(ElectricPotential v1, Current v2)
        {
            return new Resistance(v1.Value / v2.Value);
        }
        // ElectricPotential / by Resistance gives Current 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator /(ElectricPotential v1, Resistance v2)
        {
            return new Current(v1.Value / v2.Value);
        }
        #endregion
        #region powers
        public ElectricPotentialSquared Squared()
        {
            return new ElectricPotentialSquared(this.Value * this.Value);
        }
        #endregion

    }

    public readonly partial struct ElectricPotentialSquared : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ElectricPotentialSquared; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ElectricPotentialSquared(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ElectricPotentialSquared(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ElectricPotentialSquared)
                throw new Exception("Invalid conversion from PhysicalQuantity to ElectricPotentialSquared");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ElectricPotentialSquared Parse(string s)
        {
            ElectricPotentialSquared q = UnitsSystem.Parse(s);
            return q;
        }

        public static ElectricPotentialSquared Parse(string s, UnitsSystem system)
        {
            ElectricPotentialSquared q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ElectricPotentialSquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ElectricPotentialSquared(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ElectricPotentialSquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ElectricPotentialSquared(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
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
        public static bool operator ==(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator +(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
        {
            return new ElectricPotentialSquared(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator -(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
        {
            return new ElectricPotentialSquared(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator -(ElectricPotentialSquared v)
        {
            return new ElectricPotentialSquared(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator *(ElectricPotentialSquared v1, int v2)
        {
            return new ElectricPotentialSquared(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator *(ElectricPotentialSquared v1, double v2)
        {
            return new ElectricPotentialSquared(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator *(double v1, ElectricPotentialSquared v2)
        {
            return new ElectricPotentialSquared(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator *(ElectricPotentialSquared v1, Dimensionless v2)
        {
            return new ElectricPotentialSquared(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator /(ElectricPotentialSquared v1, int v2)
        {
            return new ElectricPotentialSquared(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator /(ElectricPotentialSquared v1, double v2)
        {
            return new ElectricPotentialSquared(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator /(ElectricPotentialSquared v1, Dimensionless v2)
        {
            return new ElectricPotentialSquared(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ElectricPotentialSquared v1, ElectricPotentialSquared v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ElectricPotentialSquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ElectricPotentialSquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ElectricPotentialSquared / by ElectricPotential gives ElectricPotential 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator /(ElectricPotentialSquared v1, ElectricPotential v2)
        {
            return new ElectricPotential(v1.Value / v2.Value);
        }
        // ElectricPotentialSquared / by Resistance gives Power 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator /(ElectricPotentialSquared v1, Resistance v2)
        {
            return new Power(v1.Value / v2.Value);
        }
        // ElectricPotentialSquared / by Power gives Resistance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator /(ElectricPotentialSquared v1, Power v2)
        {
            return new Resistance(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct Power : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Power; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Power(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Power(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Power)
                throw new Exception("Invalid conversion from PhysicalQuantity to Power");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Power Parse(string s)
        {
            Power q = UnitsSystem.Parse(s);
            return q;
        }

        public static Power Parse(string s, UnitsSystem system)
        {
            Power q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Power q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Power(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Power q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Power(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Power v1, Power v2)
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
        public static bool operator ==(Power v1, Power v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Power v1, Power v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Power v1, Power v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Power v1, Power v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Power v1, Power v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Power v1, Power v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator +(Power v1, Power v2)
        {
            return new Power(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator -(Power v1, Power v2)
        {
            return new Power(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator -(Power v)
        {
            return new Power(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(Power v1, int v2)
        {
            return new Power(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(Power v1, double v2)
        {
            return new Power(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(double v1, Power v2)
        {
            return new Power(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(Power v1, Dimensionless v2)
        {
            return new Power(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator /(Power v1, int v2)
        {
            return new Power(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator /(Power v1, double v2)
        {
            return new Power(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator /(Power v1, Dimensionless v2)
        {
            return new Power(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Power v1, Power v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Power v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Power v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Power * by Time gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(Power v1, Time v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // Power / by ElectricPotential gives Current 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Current operator /(Power v1, ElectricPotential v2)
        {
            return new Current(v1.Value / v2.Value);
        }
        // Power / by Current gives ElectricPotential 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator /(Power v1, Current v2)
        {
            return new ElectricPotential(v1.Value / v2.Value);
        }
        // Power * by Resistance gives ElectricPotentialSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator *(Power v1, Resistance v2)
        {
            return new ElectricPotentialSquared(v1.Value * v2.Value);
        }
        // Power / by Area gives EnergyFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator /(Power v1, Area v2)
        {
            return new EnergyFlux(v1.Value / v2.Value);
        }
        // Power / by EnergyFlux gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(Power v1, EnergyFlux v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // Power / by Length gives PowerGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator /(Power v1, Length v2)
        {
            return new PowerGradient(v1.Value / v2.Value);
        }
        // Power / by PowerGradient gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Power v1, PowerGradient v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct Resistance : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Resistance; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Resistance(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Resistance(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Resistance)
                throw new Exception("Invalid conversion from PhysicalQuantity to Resistance");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Resistance Parse(string s)
        {
            Resistance q = UnitsSystem.Parse(s);
            return q;
        }

        public static Resistance Parse(string s, UnitsSystem system)
        {
            Resistance q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Resistance q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Resistance(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Resistance q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Resistance(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Resistance v1, Resistance v2)
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
        public static bool operator ==(Resistance v1, Resistance v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Resistance v1, Resistance v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Resistance v1, Resistance v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Resistance v1, Resistance v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Resistance v1, Resistance v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Resistance v1, Resistance v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator +(Resistance v1, Resistance v2)
        {
            return new Resistance(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator -(Resistance v1, Resistance v2)
        {
            return new Resistance(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator -(Resistance v)
        {
            return new Resistance(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator *(Resistance v1, int v2)
        {
            return new Resistance(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator *(Resistance v1, double v2)
        {
            return new Resistance(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator *(double v1, Resistance v2)
        {
            return new Resistance(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator *(Resistance v1, Dimensionless v2)
        {
            return new Resistance(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator /(Resistance v1, int v2)
        {
            return new Resistance(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator /(Resistance v1, double v2)
        {
            return new Resistance(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator /(Resistance v1, Dimensionless v2)
        {
            return new Resistance(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Resistance v1, Resistance v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Resistance v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Resistance v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Resistance * by Power gives ElectricPotentialSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotentialSquared operator *(Resistance v1, Power v2)
        {
            return new ElectricPotentialSquared(v1.Value * v2.Value);
        }
        // Resistance * by Current gives ElectricPotential 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ElectricPotential operator *(Resistance v1, Current v2)
        {
            return new ElectricPotential(v1.Value * v2.Value);
        }
        // Resistance * by Area gives ResistanceTimesArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator *(Resistance v1, Area v2)
        {
            return new ResistanceTimesArea(v1.Value * v2.Value);
        }
        // Resistance * by Length gives Resistivity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator *(Resistance v1, Length v2)
        {
            return new Resistivity(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct Pressure : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Pressure; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Pressure(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Pressure(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Pressure)
                throw new Exception("Invalid conversion from PhysicalQuantity to Pressure");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Pressure Parse(string s)
        {
            Pressure q = UnitsSystem.Parse(s);
            return q;
        }

        public static Pressure Parse(string s, UnitsSystem system)
        {
            Pressure q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Pressure q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Pressure(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Pressure q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Pressure(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Pressure v1, Pressure v2)
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
        public static bool operator ==(Pressure v1, Pressure v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Pressure v1, Pressure v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Pressure v1, Pressure v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Pressure v1, Pressure v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Pressure v1, Pressure v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Pressure v1, Pressure v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator +(Pressure v1, Pressure v2)
        {
            return new Pressure(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator -(Pressure v1, Pressure v2)
        {
            return new Pressure(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator -(Pressure v)
        {
            return new Pressure(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(Pressure v1, int v2)
        {
            return new Pressure(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(Pressure v1, double v2)
        {
            return new Pressure(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(double v1, Pressure v2)
        {
            return new Pressure(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(Pressure v1, Dimensionless v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator /(Pressure v1, int v2)
        {
            return new Pressure(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator /(Pressure v1, double v2)
        {
            return new Pressure(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator /(Pressure v1, Dimensionless v2)
        {
            return new Pressure(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Pressure v1, Pressure v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Pressure v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Pressure v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Pressure * by Area gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(Pressure v1, Area v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        // Pressure / by MassByArea gives Acceleration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator /(Pressure v1, MassByArea v2)
        {
            return new Acceleration(v1.Value / v2.Value);
        }
        // Pressure / by Acceleration gives MassByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator /(Pressure v1, Acceleration v2)
        {
            return new MassByArea(v1.Value / v2.Value);
        }
        // Pressure / by ResistanceToFlow gives VolumeFlowRate 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator /(Pressure v1, ResistanceToFlow v2)
        {
            return new VolumeFlowRate(v1.Value / v2.Value);
        }
        // Pressure / by VolumeFlowRate gives ResistanceToFlow 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator /(Pressure v1, VolumeFlowRate v2)
        {
            return new ResistanceToFlow(v1.Value / v2.Value);
        }
        // Pressure * by Length gives SurfaceTension 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SurfaceTension operator *(Pressure v1, Length v2)
        {
            return new SurfaceTension(v1.Value * v2.Value);
        }
        // Pressure / by VelocityGradient gives CoefficientOfViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator /(Pressure v1, VelocityGradient v2)
        {
            return new CoefficientOfViscosity(v1.Value / v2.Value);
        }
        // Pressure / by CoefficientOfViscosity gives VelocityGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator /(Pressure v1, CoefficientOfViscosity v2)
        {
            return new VelocityGradient(v1.Value / v2.Value);
        }
        // Pressure / by MolarConcentrationTimesAbsoluteTemperature gives MolarSpecificHeat 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator /(Pressure v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return new MolarSpecificHeat(v1.Value / v2.Value);
        }
        // Pressure / by MolarSpecificHeat gives MolarConcentrationTimesAbsoluteTemperature 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator /(Pressure v1, MolarSpecificHeat v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value / v2.Value);
        }
        // Pressure / by AbsoluteTemperature gives ThermalCapacityByVolume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator /(Pressure v1, AbsoluteTemperature v2)
        {
            return new ThermalCapacityByVolume(v1.Value / v2.Value);
        }
        // Pressure / by ThermalCapacityByVolume gives AbsoluteTemperature 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AbsoluteTemperature operator /(Pressure v1, ThermalCapacityByVolume v2)
        {
            return new AbsoluteTemperature(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct Frequency : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Frequency; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Frequency(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Frequency(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Frequency)
                throw new Exception("Invalid conversion from PhysicalQuantity to Frequency");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Frequency Parse(string s)
        {
            Frequency q = UnitsSystem.Parse(s);
            return q;
        }

        public static Frequency Parse(string s, UnitsSystem system)
        {
            Frequency q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Frequency q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Frequency(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Frequency q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Frequency(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Frequency v1, Frequency v2)
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
        public static bool operator ==(Frequency v1, Frequency v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Frequency v1, Frequency v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Frequency v1, Frequency v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Frequency v1, Frequency v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Frequency v1, Frequency v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Frequency v1, Frequency v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator +(Frequency v1, Frequency v2)
        {
            return new Frequency(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator -(Frequency v1, Frequency v2)
        {
            return new Frequency(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator -(Frequency v)
        {
            return new Frequency(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator *(Frequency v1, int v2)
        {
            return new Frequency(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator *(Frequency v1, double v2)
        {
            return new Frequency(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator *(double v1, Frequency v2)
        {
            return new Frequency(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator *(Frequency v1, Dimensionless v2)
        {
            return new Frequency(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator /(Frequency v1, int v2)
        {
            return new Frequency(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator /(Frequency v1, double v2)
        {
            return new Frequency(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Frequency operator /(Frequency v1, Dimensionless v2)
        {
            return new Frequency(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Frequency v1, Frequency v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Frequency v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Frequency v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Frequency * by Time gives Dimensionless 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(Frequency v1, Time v2)
        {
            return new Dimensionless(v1.Value * v2.Value);
        }
        // double / by Frequency gives Time 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Time operator /(double v1, Frequency v2)
        {
            return new Time(v1 / v2.Value);
        }
        // Frequency * by Angle gives AngularVelocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AngularVelocity operator *(Frequency v1, Angle v2)
        {
            return new AngularVelocity(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct VelocityGradient : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.VelocityGradient; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public VelocityGradient(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public VelocityGradient(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.VelocityGradient)
                throw new Exception("Invalid conversion from PhysicalQuantity to VelocityGradient");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static VelocityGradient Parse(string s)
        {
            VelocityGradient q = UnitsSystem.Parse(s);
            return q;
        }

        public static VelocityGradient Parse(string s, UnitsSystem system)
        {
            VelocityGradient q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out VelocityGradient q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new VelocityGradient(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out VelocityGradient q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new VelocityGradient(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(VelocityGradient v1, VelocityGradient v2)
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
        public static bool operator ==(VelocityGradient v1, VelocityGradient v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(VelocityGradient v1, VelocityGradient v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(VelocityGradient v1, VelocityGradient v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(VelocityGradient v1, VelocityGradient v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(VelocityGradient v1, VelocityGradient v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(VelocityGradient v1, VelocityGradient v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator +(VelocityGradient v1, VelocityGradient v2)
        {
            return new VelocityGradient(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator -(VelocityGradient v1, VelocityGradient v2)
        {
            return new VelocityGradient(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator -(VelocityGradient v)
        {
            return new VelocityGradient(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator *(VelocityGradient v1, int v2)
        {
            return new VelocityGradient(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator *(VelocityGradient v1, double v2)
        {
            return new VelocityGradient(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator *(double v1, VelocityGradient v2)
        {
            return new VelocityGradient(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator *(VelocityGradient v1, Dimensionless v2)
        {
            return new VelocityGradient(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator /(VelocityGradient v1, int v2)
        {
            return new VelocityGradient(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator /(VelocityGradient v1, double v2)
        {
            return new VelocityGradient(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityGradient operator /(VelocityGradient v1, Dimensionless v2)
        {
            return new VelocityGradient(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(VelocityGradient v1, VelocityGradient v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(VelocityGradient v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(VelocityGradient v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // VelocityGradient * by Length gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(VelocityGradient v1, Length v2)
        {
            return new Velocity(v1.Value * v2.Value);
        }
        // VelocityGradient * by Area gives KinematicViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static KinematicViscosity operator *(VelocityGradient v1, Area v2)
        {
            return new KinematicViscosity(v1.Value * v2.Value);
        }
        // VelocityGradient * by CoefficientOfViscosity gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(VelocityGradient v1, CoefficientOfViscosity v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct CoefficientOfViscosity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.CoefficientOfViscosity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public CoefficientOfViscosity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public CoefficientOfViscosity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.CoefficientOfViscosity)
                throw new Exception("Invalid conversion from PhysicalQuantity to CoefficientOfViscosity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static CoefficientOfViscosity Parse(string s)
        {
            CoefficientOfViscosity q = UnitsSystem.Parse(s);
            return q;
        }

        public static CoefficientOfViscosity Parse(string s, UnitsSystem system)
        {
            CoefficientOfViscosity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out CoefficientOfViscosity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new CoefficientOfViscosity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out CoefficientOfViscosity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new CoefficientOfViscosity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
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
        public static bool operator ==(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator +(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
        {
            return new CoefficientOfViscosity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator -(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
        {
            return new CoefficientOfViscosity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator -(CoefficientOfViscosity v)
        {
            return new CoefficientOfViscosity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator *(CoefficientOfViscosity v1, int v2)
        {
            return new CoefficientOfViscosity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator *(CoefficientOfViscosity v1, double v2)
        {
            return new CoefficientOfViscosity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator *(double v1, CoefficientOfViscosity v2)
        {
            return new CoefficientOfViscosity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator *(CoefficientOfViscosity v1, Dimensionless v2)
        {
            return new CoefficientOfViscosity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator /(CoefficientOfViscosity v1, int v2)
        {
            return new CoefficientOfViscosity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator /(CoefficientOfViscosity v1, double v2)
        {
            return new CoefficientOfViscosity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator /(CoefficientOfViscosity v1, Dimensionless v2)
        {
            return new CoefficientOfViscosity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(CoefficientOfViscosity v1, CoefficientOfViscosity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(CoefficientOfViscosity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(CoefficientOfViscosity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // CoefficientOfViscosity * by Length gives MassFlowRate 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator *(CoefficientOfViscosity v1, Length v2)
        {
            return new MassFlowRate(v1.Value * v2.Value);
        }
        // CoefficientOfViscosity * by KinematicViscosity gives Force 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Force operator *(CoefficientOfViscosity v1, KinematicViscosity v2)
        {
            return new Force(v1.Value * v2.Value);
        }
        // CoefficientOfViscosity * by VelocityGradient gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(CoefficientOfViscosity v1, VelocityGradient v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }
        // CoefficientOfViscosity * by Area gives Momentum 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Momentum operator *(CoefficientOfViscosity v1, Area v2)
        {
            return new Momentum(v1.Value * v2.Value);
        }
        // CoefficientOfViscosity / by MassByArea gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator /(CoefficientOfViscosity v1, MassByArea v2)
        {
            return new Velocity(v1.Value / v2.Value);
        }
        // CoefficientOfViscosity / by Velocity gives MassByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator /(CoefficientOfViscosity v1, Velocity v2)
        {
            return new MassByArea(v1.Value / v2.Value);
        }
        // CoefficientOfViscosity * by VelocityByDensity gives MassByAreaByTimeSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator *(CoefficientOfViscosity v1, VelocityByDensity v2)
        {
            return new MassByAreaByTimeSquared(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct VolumeFlowRate : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.VolumeFlowRate; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public VolumeFlowRate(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public VolumeFlowRate(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.VolumeFlowRate)
                throw new Exception("Invalid conversion from PhysicalQuantity to VolumeFlowRate");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static VolumeFlowRate Parse(string s)
        {
            VolumeFlowRate q = UnitsSystem.Parse(s);
            return q;
        }

        public static VolumeFlowRate Parse(string s, UnitsSystem system)
        {
            VolumeFlowRate q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out VolumeFlowRate q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new VolumeFlowRate(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out VolumeFlowRate q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new VolumeFlowRate(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(VolumeFlowRate v1, VolumeFlowRate v2)
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
        public static bool operator ==(VolumeFlowRate v1, VolumeFlowRate v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(VolumeFlowRate v1, VolumeFlowRate v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(VolumeFlowRate v1, VolumeFlowRate v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(VolumeFlowRate v1, VolumeFlowRate v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(VolumeFlowRate v1, VolumeFlowRate v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(VolumeFlowRate v1, VolumeFlowRate v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator +(VolumeFlowRate v1, VolumeFlowRate v2)
        {
            return new VolumeFlowRate(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator -(VolumeFlowRate v1, VolumeFlowRate v2)
        {
            return new VolumeFlowRate(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator -(VolumeFlowRate v)
        {
            return new VolumeFlowRate(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator *(VolumeFlowRate v1, int v2)
        {
            return new VolumeFlowRate(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator *(VolumeFlowRate v1, double v2)
        {
            return new VolumeFlowRate(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator *(double v1, VolumeFlowRate v2)
        {
            return new VolumeFlowRate(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator *(VolumeFlowRate v1, Dimensionless v2)
        {
            return new VolumeFlowRate(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator /(VolumeFlowRate v1, int v2)
        {
            return new VolumeFlowRate(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator /(VolumeFlowRate v1, double v2)
        {
            return new VolumeFlowRate(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VolumeFlowRate operator /(VolumeFlowRate v1, Dimensionless v2)
        {
            return new VolumeFlowRate(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(VolumeFlowRate v1, VolumeFlowRate v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(VolumeFlowRate v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(VolumeFlowRate v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // VolumeFlowRate * by Time gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator *(VolumeFlowRate v1, Time v2)
        {
            return new Volume(v1.Value * v2.Value);
        }
        // VolumeFlowRate * by ResistanceToFlow gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(VolumeFlowRate v1, ResistanceToFlow v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }
        #endregion

    }

    /// <summary>
    /// The hydrodymamic equivalent of resistance.
    /// </summary>
    public readonly partial struct ResistanceToFlow : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ResistanceToFlow; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ResistanceToFlow(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ResistanceToFlow(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ResistanceToFlow)
                throw new Exception("Invalid conversion from PhysicalQuantity to ResistanceToFlow");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ResistanceToFlow Parse(string s)
        {
            ResistanceToFlow q = UnitsSystem.Parse(s);
            return q;
        }

        public static ResistanceToFlow Parse(string s, UnitsSystem system)
        {
            ResistanceToFlow q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ResistanceToFlow q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ResistanceToFlow(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ResistanceToFlow q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ResistanceToFlow(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ResistanceToFlow v1, ResistanceToFlow v2)
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
        public static bool operator ==(ResistanceToFlow v1, ResistanceToFlow v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ResistanceToFlow v1, ResistanceToFlow v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ResistanceToFlow v1, ResistanceToFlow v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ResistanceToFlow v1, ResistanceToFlow v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ResistanceToFlow v1, ResistanceToFlow v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ResistanceToFlow v1, ResistanceToFlow v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator +(ResistanceToFlow v1, ResistanceToFlow v2)
        {
            return new ResistanceToFlow(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator -(ResistanceToFlow v1, ResistanceToFlow v2)
        {
            return new ResistanceToFlow(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator -(ResistanceToFlow v)
        {
            return new ResistanceToFlow(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator *(ResistanceToFlow v1, int v2)
        {
            return new ResistanceToFlow(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator *(ResistanceToFlow v1, double v2)
        {
            return new ResistanceToFlow(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator *(double v1, ResistanceToFlow v2)
        {
            return new ResistanceToFlow(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator *(ResistanceToFlow v1, Dimensionless v2)
        {
            return new ResistanceToFlow(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator /(ResistanceToFlow v1, int v2)
        {
            return new ResistanceToFlow(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator /(ResistanceToFlow v1, double v2)
        {
            return new ResistanceToFlow(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceToFlow operator /(ResistanceToFlow v1, Dimensionless v2)
        {
            return new ResistanceToFlow(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ResistanceToFlow v1, ResistanceToFlow v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ResistanceToFlow v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ResistanceToFlow v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ResistanceToFlow * by VolumeFlowRate gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(ResistanceToFlow v1, VolumeFlowRate v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }
        // ResistanceToFlow * by FourDimensionalVolume gives MassFlowRate 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassFlowRate operator *(ResistanceToFlow v1, FourDimensionalVolume v2)
        {
            return new MassFlowRate(v1.Value * v2.Value);
        }
        #endregion

    }

    /// <summary>
    /// Used for Stokes Law
    /// </summary>
    public readonly partial struct MassByAreaByTimeSquared : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MassByAreaByTimeSquared; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MassByAreaByTimeSquared(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MassByAreaByTimeSquared(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MassByAreaByTimeSquared)
                throw new Exception("Invalid conversion from PhysicalQuantity to MassByAreaByTimeSquared");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MassByAreaByTimeSquared Parse(string s)
        {
            MassByAreaByTimeSquared q = UnitsSystem.Parse(s);
            return q;
        }

        public static MassByAreaByTimeSquared Parse(string s, UnitsSystem system)
        {
            MassByAreaByTimeSquared q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MassByAreaByTimeSquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MassByAreaByTimeSquared(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MassByAreaByTimeSquared q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MassByAreaByTimeSquared(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
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
        public static bool operator ==(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator +(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return new MassByAreaByTimeSquared(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator -(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return new MassByAreaByTimeSquared(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator -(MassByAreaByTimeSquared v)
        {
            return new MassByAreaByTimeSquared(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator *(MassByAreaByTimeSquared v1, int v2)
        {
            return new MassByAreaByTimeSquared(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator *(MassByAreaByTimeSquared v1, double v2)
        {
            return new MassByAreaByTimeSquared(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator *(double v1, MassByAreaByTimeSquared v2)
        {
            return new MassByAreaByTimeSquared(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator *(MassByAreaByTimeSquared v1, Dimensionless v2)
        {
            return new MassByAreaByTimeSquared(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator /(MassByAreaByTimeSquared v1, int v2)
        {
            return new MassByAreaByTimeSquared(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator /(MassByAreaByTimeSquared v1, double v2)
        {
            return new MassByAreaByTimeSquared(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator /(MassByAreaByTimeSquared v1, Dimensionless v2)
        {
            return new MassByAreaByTimeSquared(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MassByAreaByTimeSquared v1, MassByAreaByTimeSquared v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MassByAreaByTimeSquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MassByAreaByTimeSquared v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MassByAreaByTimeSquared / by VelocityByDensity gives CoefficientOfViscosity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfViscosity operator /(MassByAreaByTimeSquared v1, VelocityByDensity v2)
        {
            return new CoefficientOfViscosity(v1.Value / v2.Value);
        }
        // MassByAreaByTimeSquared / by CoefficientOfViscosity gives VelocityByDensity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator /(MassByAreaByTimeSquared v1, CoefficientOfViscosity v2)
        {
            return new VelocityByDensity(v1.Value / v2.Value);
        }
        // MassByAreaByTimeSquared * by TimeSquared gives MassByArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByArea operator *(MassByAreaByTimeSquared v1, TimeSquared v2)
        {
            return new MassByArea(v1.Value * v2.Value);
        }
        // MassByAreaByTimeSquared / by Acceleration gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(MassByAreaByTimeSquared v1, Acceleration v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // MassByAreaByTimeSquared / by Area gives Acceleration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Acceleration operator /(MassByAreaByTimeSquared v1, Area v2)
        {
            return new Acceleration(v1.Value / v2.Value);
        }
        #endregion

    }

    /// <summary>
    /// Used for Stokes Law
    /// </summary>
    public readonly partial struct VelocityByDensity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.VelocityByDensity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public VelocityByDensity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public VelocityByDensity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.VelocityByDensity)
                throw new Exception("Invalid conversion from PhysicalQuantity to VelocityByDensity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static VelocityByDensity Parse(string s)
        {
            VelocityByDensity q = UnitsSystem.Parse(s);
            return q;
        }

        public static VelocityByDensity Parse(string s, UnitsSystem system)
        {
            VelocityByDensity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out VelocityByDensity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new VelocityByDensity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out VelocityByDensity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new VelocityByDensity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(VelocityByDensity v1, VelocityByDensity v2)
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
        public static bool operator ==(VelocityByDensity v1, VelocityByDensity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(VelocityByDensity v1, VelocityByDensity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(VelocityByDensity v1, VelocityByDensity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(VelocityByDensity v1, VelocityByDensity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(VelocityByDensity v1, VelocityByDensity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(VelocityByDensity v1, VelocityByDensity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator +(VelocityByDensity v1, VelocityByDensity v2)
        {
            return new VelocityByDensity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator -(VelocityByDensity v1, VelocityByDensity v2)
        {
            return new VelocityByDensity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator -(VelocityByDensity v)
        {
            return new VelocityByDensity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator *(VelocityByDensity v1, int v2)
        {
            return new VelocityByDensity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator *(VelocityByDensity v1, double v2)
        {
            return new VelocityByDensity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator *(double v1, VelocityByDensity v2)
        {
            return new VelocityByDensity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator *(VelocityByDensity v1, Dimensionless v2)
        {
            return new VelocityByDensity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator /(VelocityByDensity v1, int v2)
        {
            return new VelocityByDensity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator /(VelocityByDensity v1, double v2)
        {
            return new VelocityByDensity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VelocityByDensity operator /(VelocityByDensity v1, Dimensionless v2)
        {
            return new VelocityByDensity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(VelocityByDensity v1, VelocityByDensity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(VelocityByDensity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(VelocityByDensity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // VelocityByDensity * by CoefficientOfViscosity gives MassByAreaByTimeSquared 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MassByAreaByTimeSquared operator *(VelocityByDensity v1, CoefficientOfViscosity v2)
        {
            return new MassByAreaByTimeSquared(v1.Value * v2.Value);
        }
        // VelocityByDensity * by Density gives Velocity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Velocity operator *(VelocityByDensity v1, Density v2)
        {
            return new Velocity(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct ThermalCapacity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ThermalCapacity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ThermalCapacity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ThermalCapacity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ThermalCapacity)
                throw new Exception("Invalid conversion from PhysicalQuantity to ThermalCapacity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ThermalCapacity Parse(string s)
        {
            ThermalCapacity q = UnitsSystem.Parse(s);
            return q;
        }

        public static ThermalCapacity Parse(string s, UnitsSystem system)
        {
            ThermalCapacity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ThermalCapacity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ThermalCapacity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ThermalCapacity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ThermalCapacity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ThermalCapacity v1, ThermalCapacity v2)
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
        public static bool operator ==(ThermalCapacity v1, ThermalCapacity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ThermalCapacity v1, ThermalCapacity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ThermalCapacity v1, ThermalCapacity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ThermalCapacity v1, ThermalCapacity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ThermalCapacity v1, ThermalCapacity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ThermalCapacity v1, ThermalCapacity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator +(ThermalCapacity v1, ThermalCapacity v2)
        {
            return new ThermalCapacity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator -(ThermalCapacity v1, ThermalCapacity v2)
        {
            return new ThermalCapacity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator -(ThermalCapacity v)
        {
            return new ThermalCapacity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(ThermalCapacity v1, int v2)
        {
            return new ThermalCapacity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(ThermalCapacity v1, double v2)
        {
            return new ThermalCapacity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(double v1, ThermalCapacity v2)
        {
            return new ThermalCapacity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(ThermalCapacity v1, Dimensionless v2)
        {
            return new ThermalCapacity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator /(ThermalCapacity v1, int v2)
        {
            return new ThermalCapacity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator /(ThermalCapacity v1, double v2)
        {
            return new ThermalCapacity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator /(ThermalCapacity v1, Dimensionless v2)
        {
            return new ThermalCapacity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ThermalCapacity v1, ThermalCapacity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ThermalCapacity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ThermalCapacity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ThermalCapacity * by TemperatureChange gives Energy 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Energy operator *(ThermalCapacity v1, TemperatureChange v2)
        {
            return new Energy(v1.Value * v2.Value);
        }
        // ThermalCapacity / by Mass gives SpecificHeat 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator /(ThermalCapacity v1, Mass v2)
        {
            return new SpecificHeat(v1.Value / v2.Value);
        }
        // ThermalCapacity / by SpecificHeat gives Mass 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Mass operator /(ThermalCapacity v1, SpecificHeat v2)
        {
            return new Mass(v1.Value / v2.Value);
        }
        // ThermalCapacity / by AmountOfSubstance gives MolarSpecificHeat 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator /(ThermalCapacity v1, AmountOfSubstance v2)
        {
            return new MolarSpecificHeat(v1.Value / v2.Value);
        }
        // ThermalCapacity / by MolarSpecificHeat gives AmountOfSubstance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AmountOfSubstance operator /(ThermalCapacity v1, MolarSpecificHeat v2)
        {
            return new AmountOfSubstance(v1.Value / v2.Value);
        }
        // ThermalCapacity / by Volume gives ThermalCapacityByVolume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator /(ThermalCapacity v1, Volume v2)
        {
            return new ThermalCapacityByVolume(v1.Value / v2.Value);
        }
        // ThermalCapacity / by ThermalCapacityByVolume gives Volume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Volume operator /(ThermalCapacity v1, ThermalCapacityByVolume v2)
        {
            return new Volume(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct SpecificHeat : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.SpecificHeat; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public SpecificHeat(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public SpecificHeat(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.SpecificHeat)
                throw new Exception("Invalid conversion from PhysicalQuantity to SpecificHeat");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static SpecificHeat Parse(string s)
        {
            SpecificHeat q = UnitsSystem.Parse(s);
            return q;
        }

        public static SpecificHeat Parse(string s, UnitsSystem system)
        {
            SpecificHeat q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out SpecificHeat q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new SpecificHeat(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out SpecificHeat q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new SpecificHeat(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(SpecificHeat v1, SpecificHeat v2)
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
        public static bool operator ==(SpecificHeat v1, SpecificHeat v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(SpecificHeat v1, SpecificHeat v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(SpecificHeat v1, SpecificHeat v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(SpecificHeat v1, SpecificHeat v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(SpecificHeat v1, SpecificHeat v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(SpecificHeat v1, SpecificHeat v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator +(SpecificHeat v1, SpecificHeat v2)
        {
            return new SpecificHeat(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator -(SpecificHeat v1, SpecificHeat v2)
        {
            return new SpecificHeat(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator -(SpecificHeat v)
        {
            return new SpecificHeat(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator *(SpecificHeat v1, int v2)
        {
            return new SpecificHeat(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator *(SpecificHeat v1, double v2)
        {
            return new SpecificHeat(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator *(double v1, SpecificHeat v2)
        {
            return new SpecificHeat(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator *(SpecificHeat v1, Dimensionless v2)
        {
            return new SpecificHeat(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator /(SpecificHeat v1, int v2)
        {
            return new SpecificHeat(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator /(SpecificHeat v1, double v2)
        {
            return new SpecificHeat(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SpecificHeat operator /(SpecificHeat v1, Dimensionless v2)
        {
            return new SpecificHeat(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(SpecificHeat v1, SpecificHeat v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(SpecificHeat v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(SpecificHeat v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // SpecificHeat * by Mass gives ThermalCapacity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(SpecificHeat v1, Mass v2)
        {
            return new ThermalCapacity(v1.Value * v2.Value);
        }
        #endregion

    }

    /// <summary>
    /// Used in van't Hoff equation.
    /// </summary>
    public readonly partial struct MolarSpecificHeat : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MolarSpecificHeat; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarSpecificHeat(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarSpecificHeat(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MolarSpecificHeat)
                throw new Exception("Invalid conversion from PhysicalQuantity to MolarSpecificHeat");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MolarSpecificHeat Parse(string s)
        {
            MolarSpecificHeat q = UnitsSystem.Parse(s);
            return q;
        }

        public static MolarSpecificHeat Parse(string s, UnitsSystem system)
        {
            MolarSpecificHeat q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MolarSpecificHeat q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MolarSpecificHeat(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MolarSpecificHeat q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MolarSpecificHeat(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MolarSpecificHeat v1, MolarSpecificHeat v2)
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
        public static bool operator ==(MolarSpecificHeat v1, MolarSpecificHeat v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MolarSpecificHeat v1, MolarSpecificHeat v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MolarSpecificHeat v1, MolarSpecificHeat v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MolarSpecificHeat v1, MolarSpecificHeat v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MolarSpecificHeat v1, MolarSpecificHeat v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MolarSpecificHeat v1, MolarSpecificHeat v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator +(MolarSpecificHeat v1, MolarSpecificHeat v2)
        {
            return new MolarSpecificHeat(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator -(MolarSpecificHeat v1, MolarSpecificHeat v2)
        {
            return new MolarSpecificHeat(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator -(MolarSpecificHeat v)
        {
            return new MolarSpecificHeat(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator *(MolarSpecificHeat v1, int v2)
        {
            return new MolarSpecificHeat(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator *(MolarSpecificHeat v1, double v2)
        {
            return new MolarSpecificHeat(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator *(double v1, MolarSpecificHeat v2)
        {
            return new MolarSpecificHeat(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator *(MolarSpecificHeat v1, Dimensionless v2)
        {
            return new MolarSpecificHeat(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator /(MolarSpecificHeat v1, int v2)
        {
            return new MolarSpecificHeat(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator /(MolarSpecificHeat v1, double v2)
        {
            return new MolarSpecificHeat(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator /(MolarSpecificHeat v1, Dimensionless v2)
        {
            return new MolarSpecificHeat(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MolarSpecificHeat v1, MolarSpecificHeat v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MolarSpecificHeat v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MolarSpecificHeat v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MolarSpecificHeat * by AmountOfSubstance gives ThermalCapacity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(MolarSpecificHeat v1, AmountOfSubstance v2)
        {
            return new ThermalCapacity(v1.Value * v2.Value);
        }
        // MolarSpecificHeat * by MolarConcentrationTimesAbsoluteTemperature gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(MolarSpecificHeat v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }
        // MolarSpecificHeat * by MolarConcentration gives ThermalCapacityByVolume 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator *(MolarSpecificHeat v1, MolarConcentration v2)
        {
            return new ThermalCapacityByVolume(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct MolarConcentrationTimesAbsoluteTemperature : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.MolarConcentrationTimesAbsoluteTemperature; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarConcentrationTimesAbsoluteTemperature(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public MolarConcentrationTimesAbsoluteTemperature(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.MolarConcentrationTimesAbsoluteTemperature)
                throw new Exception("Invalid conversion from PhysicalQuantity to MolarConcentrationTimesAbsoluteTemperature");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static MolarConcentrationTimesAbsoluteTemperature Parse(string s)
        {
            MolarConcentrationTimesAbsoluteTemperature q = UnitsSystem.Parse(s);
            return q;
        }

        public static MolarConcentrationTimesAbsoluteTemperature Parse(string s, UnitsSystem system)
        {
            MolarConcentrationTimesAbsoluteTemperature q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out MolarConcentrationTimesAbsoluteTemperature q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new MolarConcentrationTimesAbsoluteTemperature(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out MolarConcentrationTimesAbsoluteTemperature q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new MolarConcentrationTimesAbsoluteTemperature(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
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
        public static bool operator ==(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator +(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator -(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator -(MolarConcentrationTimesAbsoluteTemperature v)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator *(MolarConcentrationTimesAbsoluteTemperature v1, int v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator *(MolarConcentrationTimesAbsoluteTemperature v1, double v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator *(double v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator *(MolarConcentrationTimesAbsoluteTemperature v1, Dimensionless v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator /(MolarConcentrationTimesAbsoluteTemperature v1, int v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator /(MolarConcentrationTimesAbsoluteTemperature v1, double v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentrationTimesAbsoluteTemperature operator /(MolarConcentrationTimesAbsoluteTemperature v1, Dimensionless v2)
        {
            return new MolarConcentrationTimesAbsoluteTemperature(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentrationTimesAbsoluteTemperature v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(MolarConcentrationTimesAbsoluteTemperature v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(MolarConcentrationTimesAbsoluteTemperature v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // MolarConcentrationTimesAbsoluteTemperature * by MolarSpecificHeat gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(MolarConcentrationTimesAbsoluteTemperature v1, MolarSpecificHeat v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }
        // MolarConcentrationTimesAbsoluteTemperature / by MolarConcentration gives AbsoluteTemperature 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static AbsoluteTemperature operator /(MolarConcentrationTimesAbsoluteTemperature v1, MolarConcentration v2)
        {
            return new AbsoluteTemperature(v1.Value / v2.Value);
        }
        // MolarConcentrationTimesAbsoluteTemperature / by AbsoluteTemperature gives MolarConcentration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator /(MolarConcentrationTimesAbsoluteTemperature v1, AbsoluteTemperature v2)
        {
            return new MolarConcentration(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct CoefficientOfThermalExpansion : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.CoefficientOfThermalExpansion; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public CoefficientOfThermalExpansion(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public CoefficientOfThermalExpansion(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.CoefficientOfThermalExpansion)
                throw new Exception("Invalid conversion from PhysicalQuantity to CoefficientOfThermalExpansion");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static CoefficientOfThermalExpansion Parse(string s)
        {
            CoefficientOfThermalExpansion q = UnitsSystem.Parse(s);
            return q;
        }

        public static CoefficientOfThermalExpansion Parse(string s, UnitsSystem system)
        {
            CoefficientOfThermalExpansion q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out CoefficientOfThermalExpansion q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new CoefficientOfThermalExpansion(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out CoefficientOfThermalExpansion q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new CoefficientOfThermalExpansion(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
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
        public static bool operator ==(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator +(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
        {
            return new CoefficientOfThermalExpansion(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator -(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
        {
            return new CoefficientOfThermalExpansion(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator -(CoefficientOfThermalExpansion v)
        {
            return new CoefficientOfThermalExpansion(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator *(CoefficientOfThermalExpansion v1, int v2)
        {
            return new CoefficientOfThermalExpansion(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator *(CoefficientOfThermalExpansion v1, double v2)
        {
            return new CoefficientOfThermalExpansion(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator *(double v1, CoefficientOfThermalExpansion v2)
        {
            return new CoefficientOfThermalExpansion(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator *(CoefficientOfThermalExpansion v1, Dimensionless v2)
        {
            return new CoefficientOfThermalExpansion(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator /(CoefficientOfThermalExpansion v1, int v2)
        {
            return new CoefficientOfThermalExpansion(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator /(CoefficientOfThermalExpansion v1, double v2)
        {
            return new CoefficientOfThermalExpansion(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static CoefficientOfThermalExpansion operator /(CoefficientOfThermalExpansion v1, Dimensionless v2)
        {
            return new CoefficientOfThermalExpansion(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(CoefficientOfThermalExpansion v1, CoefficientOfThermalExpansion v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(CoefficientOfThermalExpansion v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(CoefficientOfThermalExpansion v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // CoefficientOfThermalExpansion * by TemperatureChange gives Dimensionless 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator *(CoefficientOfThermalExpansion v1, TemperatureChange v2)
        {
            return new Dimensionless(v1.Value * v2.Value);
        }
        // double / by CoefficientOfThermalExpansion gives TemperatureChange 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator /(double v1, CoefficientOfThermalExpansion v2)
        {
            return new TemperatureChange(v1 / v2.Value);
        }
        #endregion

    }

    /// <summary>
    /// Used in van't Hoff equation.
    /// </summary>
    public readonly partial struct ThermalCapacityByVolume : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ThermalCapacityByVolume; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ThermalCapacityByVolume(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ThermalCapacityByVolume(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ThermalCapacityByVolume)
                throw new Exception("Invalid conversion from PhysicalQuantity to ThermalCapacityByVolume");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ThermalCapacityByVolume Parse(string s)
        {
            ThermalCapacityByVolume q = UnitsSystem.Parse(s);
            return q;
        }

        public static ThermalCapacityByVolume Parse(string s, UnitsSystem system)
        {
            ThermalCapacityByVolume q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ThermalCapacityByVolume q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ThermalCapacityByVolume(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ThermalCapacityByVolume q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ThermalCapacityByVolume(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
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
        public static bool operator ==(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator +(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
        {
            return new ThermalCapacityByVolume(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator -(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
        {
            return new ThermalCapacityByVolume(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator -(ThermalCapacityByVolume v)
        {
            return new ThermalCapacityByVolume(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator *(ThermalCapacityByVolume v1, int v2)
        {
            return new ThermalCapacityByVolume(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator *(ThermalCapacityByVolume v1, double v2)
        {
            return new ThermalCapacityByVolume(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator *(double v1, ThermalCapacityByVolume v2)
        {
            return new ThermalCapacityByVolume(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator *(ThermalCapacityByVolume v1, Dimensionless v2)
        {
            return new ThermalCapacityByVolume(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator /(ThermalCapacityByVolume v1, int v2)
        {
            return new ThermalCapacityByVolume(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator /(ThermalCapacityByVolume v1, double v2)
        {
            return new ThermalCapacityByVolume(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacityByVolume operator /(ThermalCapacityByVolume v1, Dimensionless v2)
        {
            return new ThermalCapacityByVolume(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ThermalCapacityByVolume v1, ThermalCapacityByVolume v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ThermalCapacityByVolume v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ThermalCapacityByVolume v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ThermalCapacityByVolume * by Volume gives ThermalCapacity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalCapacity operator *(ThermalCapacityByVolume v1, Volume v2)
        {
            return new ThermalCapacity(v1.Value * v2.Value);
        }
        // ThermalCapacityByVolume / by MolarConcentration gives MolarSpecificHeat 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarSpecificHeat operator /(ThermalCapacityByVolume v1, MolarConcentration v2)
        {
            return new MolarSpecificHeat(v1.Value / v2.Value);
        }
        // ThermalCapacityByVolume / by MolarSpecificHeat gives MolarConcentration 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static MolarConcentration operator /(ThermalCapacityByVolume v1, MolarSpecificHeat v2)
        {
            return new MolarConcentration(v1.Value / v2.Value);
        }
        // ThermalCapacityByVolume * by AbsoluteTemperature gives Pressure 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Pressure operator *(ThermalCapacityByVolume v1, AbsoluteTemperature v2)
        {
            return new Pressure(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct EnergyFlux : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.EnergyFlux; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public EnergyFlux(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public EnergyFlux(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.EnergyFlux)
                throw new Exception("Invalid conversion from PhysicalQuantity to EnergyFlux");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static EnergyFlux Parse(string s)
        {
            EnergyFlux q = UnitsSystem.Parse(s);
            return q;
        }

        public static EnergyFlux Parse(string s, UnitsSystem system)
        {
            EnergyFlux q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out EnergyFlux q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new EnergyFlux(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out EnergyFlux q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new EnergyFlux(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(EnergyFlux v1, EnergyFlux v2)
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
        public static bool operator ==(EnergyFlux v1, EnergyFlux v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(EnergyFlux v1, EnergyFlux v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(EnergyFlux v1, EnergyFlux v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(EnergyFlux v1, EnergyFlux v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(EnergyFlux v1, EnergyFlux v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(EnergyFlux v1, EnergyFlux v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator +(EnergyFlux v1, EnergyFlux v2)
        {
            return new EnergyFlux(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator -(EnergyFlux v1, EnergyFlux v2)
        {
            return new EnergyFlux(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator -(EnergyFlux v)
        {
            return new EnergyFlux(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator *(EnergyFlux v1, int v2)
        {
            return new EnergyFlux(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator *(EnergyFlux v1, double v2)
        {
            return new EnergyFlux(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator *(double v1, EnergyFlux v2)
        {
            return new EnergyFlux(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator *(EnergyFlux v1, Dimensionless v2)
        {
            return new EnergyFlux(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator /(EnergyFlux v1, int v2)
        {
            return new EnergyFlux(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator /(EnergyFlux v1, double v2)
        {
            return new EnergyFlux(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator /(EnergyFlux v1, Dimensionless v2)
        {
            return new EnergyFlux(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(EnergyFlux v1, EnergyFlux v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(EnergyFlux v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(EnergyFlux v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // EnergyFlux * by Area gives Power 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(EnergyFlux v1, Area v2)
        {
            return new Power(v1.Value * v2.Value);
        }
        // EnergyFlux / by TemperatureGradient gives ThermalConductivity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator /(EnergyFlux v1, TemperatureGradient v2)
        {
            return new ThermalConductivity(v1.Value / v2.Value);
        }
        // EnergyFlux / by ThermalConductivity gives TemperatureGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator /(EnergyFlux v1, ThermalConductivity v2)
        {
            return new TemperatureGradient(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct PowerGradient : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.PowerGradient; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public PowerGradient(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public PowerGradient(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.PowerGradient)
                throw new Exception("Invalid conversion from PhysicalQuantity to PowerGradient");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static PowerGradient Parse(string s)
        {
            PowerGradient q = UnitsSystem.Parse(s);
            return q;
        }

        public static PowerGradient Parse(string s, UnitsSystem system)
        {
            PowerGradient q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out PowerGradient q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new PowerGradient(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out PowerGradient q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new PowerGradient(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(PowerGradient v1, PowerGradient v2)
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
        public static bool operator ==(PowerGradient v1, PowerGradient v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(PowerGradient v1, PowerGradient v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(PowerGradient v1, PowerGradient v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(PowerGradient v1, PowerGradient v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(PowerGradient v1, PowerGradient v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(PowerGradient v1, PowerGradient v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator +(PowerGradient v1, PowerGradient v2)
        {
            return new PowerGradient(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator -(PowerGradient v1, PowerGradient v2)
        {
            return new PowerGradient(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator -(PowerGradient v)
        {
            return new PowerGradient(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator *(PowerGradient v1, int v2)
        {
            return new PowerGradient(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator *(PowerGradient v1, double v2)
        {
            return new PowerGradient(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator *(double v1, PowerGradient v2)
        {
            return new PowerGradient(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator *(PowerGradient v1, Dimensionless v2)
        {
            return new PowerGradient(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator /(PowerGradient v1, int v2)
        {
            return new PowerGradient(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator /(PowerGradient v1, double v2)
        {
            return new PowerGradient(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator /(PowerGradient v1, Dimensionless v2)
        {
            return new PowerGradient(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(PowerGradient v1, PowerGradient v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(PowerGradient v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(PowerGradient v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // PowerGradient * by Length gives Power 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Power operator *(PowerGradient v1, Length v2)
        {
            return new Power(v1.Value * v2.Value);
        }
        // PowerGradient / by TemperatureChange gives ThermalConductivity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator /(PowerGradient v1, TemperatureChange v2)
        {
            return new ThermalConductivity(v1.Value / v2.Value);
        }
        // PowerGradient / by ThermalConductivity gives TemperatureChange 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator /(PowerGradient v1, ThermalConductivity v2)
        {
            return new TemperatureChange(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct TemperatureGradient : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.TemperatureGradient; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public TemperatureGradient(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public TemperatureGradient(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.TemperatureGradient)
                throw new Exception("Invalid conversion from PhysicalQuantity to TemperatureGradient");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static TemperatureGradient Parse(string s)
        {
            TemperatureGradient q = UnitsSystem.Parse(s);
            return q;
        }

        public static TemperatureGradient Parse(string s, UnitsSystem system)
        {
            TemperatureGradient q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out TemperatureGradient q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new TemperatureGradient(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out TemperatureGradient q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new TemperatureGradient(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(TemperatureGradient v1, TemperatureGradient v2)
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
        public static bool operator ==(TemperatureGradient v1, TemperatureGradient v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(TemperatureGradient v1, TemperatureGradient v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(TemperatureGradient v1, TemperatureGradient v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(TemperatureGradient v1, TemperatureGradient v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(TemperatureGradient v1, TemperatureGradient v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(TemperatureGradient v1, TemperatureGradient v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator +(TemperatureGradient v1, TemperatureGradient v2)
        {
            return new TemperatureGradient(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator -(TemperatureGradient v1, TemperatureGradient v2)
        {
            return new TemperatureGradient(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator -(TemperatureGradient v)
        {
            return new TemperatureGradient(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator *(TemperatureGradient v1, int v2)
        {
            return new TemperatureGradient(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator *(TemperatureGradient v1, double v2)
        {
            return new TemperatureGradient(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator *(double v1, TemperatureGradient v2)
        {
            return new TemperatureGradient(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator *(TemperatureGradient v1, Dimensionless v2)
        {
            return new TemperatureGradient(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator /(TemperatureGradient v1, int v2)
        {
            return new TemperatureGradient(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator /(TemperatureGradient v1, double v2)
        {
            return new TemperatureGradient(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureGradient operator /(TemperatureGradient v1, Dimensionless v2)
        {
            return new TemperatureGradient(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(TemperatureGradient v1, TemperatureGradient v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(TemperatureGradient v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(TemperatureGradient v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // TemperatureGradient * by Length gives TemperatureChange 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static TemperatureChange operator *(TemperatureGradient v1, Length v2)
        {
            return new TemperatureChange(v1.Value * v2.Value);
        }
        // TemperatureGradient * by ThermalConductivity gives EnergyFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator *(TemperatureGradient v1, ThermalConductivity v2)
        {
            return new EnergyFlux(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct ThermalConductivity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ThermalConductivity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ThermalConductivity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ThermalConductivity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ThermalConductivity)
                throw new Exception("Invalid conversion from PhysicalQuantity to ThermalConductivity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ThermalConductivity Parse(string s)
        {
            ThermalConductivity q = UnitsSystem.Parse(s);
            return q;
        }

        public static ThermalConductivity Parse(string s, UnitsSystem system)
        {
            ThermalConductivity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ThermalConductivity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ThermalConductivity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ThermalConductivity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ThermalConductivity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ThermalConductivity v1, ThermalConductivity v2)
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
        public static bool operator ==(ThermalConductivity v1, ThermalConductivity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ThermalConductivity v1, ThermalConductivity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ThermalConductivity v1, ThermalConductivity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ThermalConductivity v1, ThermalConductivity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ThermalConductivity v1, ThermalConductivity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ThermalConductivity v1, ThermalConductivity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator +(ThermalConductivity v1, ThermalConductivity v2)
        {
            return new ThermalConductivity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator -(ThermalConductivity v1, ThermalConductivity v2)
        {
            return new ThermalConductivity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator -(ThermalConductivity v)
        {
            return new ThermalConductivity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator *(ThermalConductivity v1, int v2)
        {
            return new ThermalConductivity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator *(ThermalConductivity v1, double v2)
        {
            return new ThermalConductivity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator *(double v1, ThermalConductivity v2)
        {
            return new ThermalConductivity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator *(ThermalConductivity v1, Dimensionless v2)
        {
            return new ThermalConductivity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator /(ThermalConductivity v1, int v2)
        {
            return new ThermalConductivity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator /(ThermalConductivity v1, double v2)
        {
            return new ThermalConductivity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ThermalConductivity operator /(ThermalConductivity v1, Dimensionless v2)
        {
            return new ThermalConductivity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ThermalConductivity v1, ThermalConductivity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ThermalConductivity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ThermalConductivity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ThermalConductivity * by TemperatureGradient gives EnergyFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static EnergyFlux operator *(ThermalConductivity v1, TemperatureGradient v2)
        {
            return new EnergyFlux(v1.Value * v2.Value);
        }
        // ThermalConductivity * by TemperatureChange gives PowerGradient 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PowerGradient operator *(ThermalConductivity v1, TemperatureChange v2)
        {
            return new PowerGradient(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct ResistanceTimesArea : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.ResistanceTimesArea; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ResistanceTimesArea(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ResistanceTimesArea(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.ResistanceTimesArea)
                throw new Exception("Invalid conversion from PhysicalQuantity to ResistanceTimesArea");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static ResistanceTimesArea Parse(string s)
        {
            ResistanceTimesArea q = UnitsSystem.Parse(s);
            return q;
        }

        public static ResistanceTimesArea Parse(string s, UnitsSystem system)
        {
            ResistanceTimesArea q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out ResistanceTimesArea q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new ResistanceTimesArea(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out ResistanceTimesArea q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new ResistanceTimesArea(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(ResistanceTimesArea v1, ResistanceTimesArea v2)
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
        public static bool operator ==(ResistanceTimesArea v1, ResistanceTimesArea v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(ResistanceTimesArea v1, ResistanceTimesArea v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(ResistanceTimesArea v1, ResistanceTimesArea v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(ResistanceTimesArea v1, ResistanceTimesArea v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(ResistanceTimesArea v1, ResistanceTimesArea v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(ResistanceTimesArea v1, ResistanceTimesArea v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator +(ResistanceTimesArea v1, ResistanceTimesArea v2)
        {
            return new ResistanceTimesArea(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator -(ResistanceTimesArea v1, ResistanceTimesArea v2)
        {
            return new ResistanceTimesArea(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator -(ResistanceTimesArea v)
        {
            return new ResistanceTimesArea(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator *(ResistanceTimesArea v1, int v2)
        {
            return new ResistanceTimesArea(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator *(ResistanceTimesArea v1, double v2)
        {
            return new ResistanceTimesArea(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator *(double v1, ResistanceTimesArea v2)
        {
            return new ResistanceTimesArea(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator *(ResistanceTimesArea v1, Dimensionless v2)
        {
            return new ResistanceTimesArea(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator /(ResistanceTimesArea v1, int v2)
        {
            return new ResistanceTimesArea(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator /(ResistanceTimesArea v1, double v2)
        {
            return new ResistanceTimesArea(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator /(ResistanceTimesArea v1, Dimensionless v2)
        {
            return new ResistanceTimesArea(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(ResistanceTimesArea v1, ResistanceTimesArea v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(ResistanceTimesArea v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(ResistanceTimesArea v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // ResistanceTimesArea / by Resistance gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(ResistanceTimesArea v1, Resistance v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        // ResistanceTimesArea / by Area gives Resistance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator /(ResistanceTimesArea v1, Area v2)
        {
            return new Resistance(v1.Value / v2.Value);
        }
        // ResistanceTimesArea / by Length gives Resistivity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator /(ResistanceTimesArea v1, Length v2)
        {
            return new Resistivity(v1.Value / v2.Value);
        }
        // ResistanceTimesArea / by Resistivity gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(ResistanceTimesArea v1, Resistivity v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct Resistivity : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Resistivity; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Resistivity(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Resistivity(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Resistivity)
                throw new Exception("Invalid conversion from PhysicalQuantity to Resistivity");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Resistivity Parse(string s)
        {
            Resistivity q = UnitsSystem.Parse(s);
            return q;
        }

        public static Resistivity Parse(string s, UnitsSystem system)
        {
            Resistivity q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Resistivity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Resistivity(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Resistivity q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Resistivity(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Resistivity v1, Resistivity v2)
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
        public static bool operator ==(Resistivity v1, Resistivity v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Resistivity v1, Resistivity v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Resistivity v1, Resistivity v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Resistivity v1, Resistivity v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Resistivity v1, Resistivity v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Resistivity v1, Resistivity v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator +(Resistivity v1, Resistivity v2)
        {
            return new Resistivity(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator -(Resistivity v1, Resistivity v2)
        {
            return new Resistivity(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator -(Resistivity v)
        {
            return new Resistivity(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator *(Resistivity v1, int v2)
        {
            return new Resistivity(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator *(Resistivity v1, double v2)
        {
            return new Resistivity(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator *(double v1, Resistivity v2)
        {
            return new Resistivity(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator *(Resistivity v1, Dimensionless v2)
        {
            return new Resistivity(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator /(Resistivity v1, int v2)
        {
            return new Resistivity(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator /(Resistivity v1, double v2)
        {
            return new Resistivity(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistivity operator /(Resistivity v1, Dimensionless v2)
        {
            return new Resistivity(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Resistivity v1, Resistivity v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Resistivity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Resistivity v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Resistivity / by Resistance gives Length 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Length operator /(Resistivity v1, Resistance v2)
        {
            return new Length(v1.Value / v2.Value);
        }
        // Resistivity / by Length gives Resistance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Resistance operator /(Resistivity v1, Length v2)
        {
            return new Resistance(v1.Value / v2.Value);
        }
        // Resistivity * by Length gives ResistanceTimesArea 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ResistanceTimesArea operator *(Resistivity v1, Length v2)
        {
            return new ResistanceTimesArea(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct LuminousFlux : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.LuminousFlux; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public LuminousFlux(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public LuminousFlux(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.LuminousFlux)
                throw new Exception("Invalid conversion from PhysicalQuantity to LuminousFlux");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static LuminousFlux Parse(string s)
        {
            LuminousFlux q = UnitsSystem.Parse(s);
            return q;
        }

        public static LuminousFlux Parse(string s, UnitsSystem system)
        {
            LuminousFlux q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out LuminousFlux q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new LuminousFlux(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out LuminousFlux q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new LuminousFlux(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(LuminousFlux v1, LuminousFlux v2)
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
        public static bool operator ==(LuminousFlux v1, LuminousFlux v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(LuminousFlux v1, LuminousFlux v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(LuminousFlux v1, LuminousFlux v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(LuminousFlux v1, LuminousFlux v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(LuminousFlux v1, LuminousFlux v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(LuminousFlux v1, LuminousFlux v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator +(LuminousFlux v1, LuminousFlux v2)
        {
            return new LuminousFlux(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator -(LuminousFlux v1, LuminousFlux v2)
        {
            return new LuminousFlux(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator -(LuminousFlux v)
        {
            return new LuminousFlux(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator *(LuminousFlux v1, int v2)
        {
            return new LuminousFlux(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator *(LuminousFlux v1, double v2)
        {
            return new LuminousFlux(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator *(double v1, LuminousFlux v2)
        {
            return new LuminousFlux(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator *(LuminousFlux v1, Dimensionless v2)
        {
            return new LuminousFlux(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator /(LuminousFlux v1, int v2)
        {
            return new LuminousFlux(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator /(LuminousFlux v1, double v2)
        {
            return new LuminousFlux(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator /(LuminousFlux v1, Dimensionless v2)
        {
            return new LuminousFlux(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(LuminousFlux v1, LuminousFlux v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(LuminousFlux v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(LuminousFlux v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // LuminousFlux / by LuminousIntensity gives SolidAngle 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SolidAngle operator /(LuminousFlux v1, LuminousIntensity v2)
        {
            return new SolidAngle(v1.Value / v2.Value);
        }
        // LuminousFlux / by SolidAngle gives LuminousIntensity 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousIntensity operator /(LuminousFlux v1, SolidAngle v2)
        {
            return new LuminousIntensity(v1.Value / v2.Value);
        }
        // LuminousFlux / by Area gives Illuminance 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator /(LuminousFlux v1, Area v2)
        {
            return new Illuminance(v1.Value / v2.Value);
        }
        // LuminousFlux / by Illuminance gives Area 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Area operator /(LuminousFlux v1, Illuminance v2)
        {
            return new Area(v1.Value / v2.Value);
        }
        #endregion

    }

    public readonly partial struct Illuminance : IPhysicalQuantity
    {
        public readonly double Value { get; init; }
        public readonly Dimensions Dimensions { get { return Dimensions.Illuminance; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Illuminance(double v)
        {
            this.Value = v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Illuminance(PhysicalQuantity q)
        {
            if (q.Dimensions != Dimensions.Illuminance)
                throw new Exception("Invalid conversion from PhysicalQuantity to Illuminance");
            this.Value = q.Value;
        }

        #region String conversion methods
        public override string ToString()
        {
            return UnitsSystem.ToString(this, UnitsSystem.FormatOption.BestFit);
        }

        public string ToString(UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, option);
        }

        public string ToString(UnitsSystem system, UnitsSystem.FormatOption option)
        {
            return UnitsSystem.ToString(this, system, option);
        }

        public string ToString(params Unit[] units)
        {
            return UnitsSystem.ToString(this, units);
        }

        public string ToString(int precision, params Unit[] units)
        {
            return UnitsSystem.ToString(this, precision, units);
        }

        public static Illuminance Parse(string s)
        {
            Illuminance q = UnitsSystem.Parse(s);
            return q;
        }

        public static Illuminance Parse(string s, UnitsSystem system)
        {
            Illuminance q = UnitsSystem.Parse(s, system);
            return q;
        }

        public static bool TryParse(string s, out Illuminance q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, out qty);
            q = retVal ? qty : new Illuminance(0);
            return retVal;
        }

        public static bool TryParse(string s, UnitsSystem system, out Illuminance q)
        {
            PhysicalQuantity qty;
            bool retVal = UnitsSystem.TryParse(s, system, out qty);
            q = retVal ? qty : new Illuminance(0);
            return retVal;
        }
        #endregion

        #region Comparison Operators

        public static int Compare(Illuminance v1, Illuminance v2)
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
        public static bool operator ==(Illuminance v1, Illuminance v2)
        {
            return Compare(v1, v2) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Illuminance v1, Illuminance v2)
        {
            return Compare(v1, v2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >(Illuminance v1, Illuminance v2)
        {
            return Compare(v1, v2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator >=(Illuminance v1, Illuminance v2)
        {
            return Compare(v1, v2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <=(Illuminance v1, Illuminance v2)
        {
            return Compare(v1, v2) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator <(Illuminance v1, Illuminance v2)
        {
            return Compare(v1, v2) < 0;
        }

        #endregion

        #region Maths Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator +(Illuminance v1, Illuminance v2)
        {
            return new Illuminance(v1.Value + v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator -(Illuminance v1, Illuminance v2)
        {
            return new Illuminance(v1.Value - v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator -(Illuminance v)
        {
            return new Illuminance(-v.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator *(Illuminance v1, int v2)
        {
            return new Illuminance(v1.Value * (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator *(Illuminance v1, double v2)
        {
            return new Illuminance(v1.Value * v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator *(double v1, Illuminance v2)
        {
            return new Illuminance(v1 * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator *(Illuminance v1, Dimensionless v2)
        {
            return new Illuminance(v1.Value * v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator /(Illuminance v1, int v2)
        {
            return new Illuminance(v1.Value / (double)v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator /(Illuminance v1, double v2)
        {
            return new Illuminance(v1.Value / v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Illuminance operator /(Illuminance v1, Dimensionless v2)
        {
            return new Illuminance(v1.Value / v2.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Dimensionless operator /(Illuminance v1, Illuminance v2)
        {
            return new Dimensionless(v1.Value / v2.Value);
        }

#if PREFER_RUNTIME_CHECKS
 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
		public static PhysicalQuantity operator *(Illuminance v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value * v2.Value, v1.Dimensions * v2.Dimensions);
        }

 		[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static PhysicalQuantity operator /(Illuminance v1, IPhysicalQuantity v2)
        {
            return new PhysicalQuantity(v1.Value / v2.Value, v1.Dimensions / v2.Dimensions);
        }
#endif

        // Illuminance * by Area gives LuminousFlux 
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LuminousFlux operator *(Illuminance v1, Area v2)
        {
            return new LuminousFlux(v1.Value * v2.Value);
        }
        #endregion

    }

    public readonly partial struct PhysicalQuantity : IPhysicalQuantity
    {
        public static implicit operator Dimensionless(PhysicalQuantity q) { return new Dimensionless(q); }
        public static implicit operator Mass(PhysicalQuantity q) { return new Mass(q); }
        public static implicit operator Length(PhysicalQuantity q) { return new Length(q); }
        public static implicit operator Time(PhysicalQuantity q) { return new Time(q); }
        public static implicit operator Current(PhysicalQuantity q) { return new Current(q); }
        public static implicit operator TemperatureChange(PhysicalQuantity q) { return new TemperatureChange(q); }
        public static implicit operator AmountOfSubstance(PhysicalQuantity q) { return new AmountOfSubstance(q); }
        public static implicit operator LuminousIntensity(PhysicalQuantity q) { return new LuminousIntensity(q); }
        public static implicit operator Angle(PhysicalQuantity q) { return new Angle(q); }
        public static implicit operator Area(PhysicalQuantity q) { return new Area(q); }
        public static implicit operator Volume(PhysicalQuantity q) { return new Volume(q); }
        public static implicit operator Density(PhysicalQuantity q) { return new Density(q); }
        public static implicit operator MolarMass(PhysicalQuantity q) { return new MolarMass(q); }
        public static implicit operator MolarConcentration(PhysicalQuantity q) { return new MolarConcentration(q); }
        public static implicit operator Velocity(PhysicalQuantity q) { return new Velocity(q); }
        public static implicit operator TangentialVelocity(PhysicalQuantity q) { return new TangentialVelocity(q); }
        public static implicit operator AngularVelocity(PhysicalQuantity q) { return new AngularVelocity(q); }
        public static implicit operator SolidAngle(PhysicalQuantity q) { return new SolidAngle(q); }
        public static implicit operator TimeSquared(PhysicalQuantity q) { return new TimeSquared(q); }
        public static implicit operator VelocitySquared(PhysicalQuantity q) { return new VelocitySquared(q); }
        public static implicit operator AngularVelocitySquared(PhysicalQuantity q) { return new AngularVelocitySquared(q); }
        public static implicit operator ByLength(PhysicalQuantity q) { return new ByLength(q); }
        public static implicit operator ByArea(PhysicalQuantity q) { return new ByArea(q); }
        public static implicit operator MassByLength(PhysicalQuantity q) { return new MassByLength(q); }
        public static implicit operator MassByArea(PhysicalQuantity q) { return new MassByArea(q); }
        public static implicit operator FourDimensionalVolume(PhysicalQuantity q) { return new FourDimensionalVolume(q); }
        public static implicit operator MolarConcentrationGradient(PhysicalQuantity q) { return new MolarConcentrationGradient(q); }
        public static implicit operator AmountOfSubstanceByArea(PhysicalQuantity q) { return new AmountOfSubstanceByArea(q); }
        public static implicit operator AmountOfSubstanceByTime(PhysicalQuantity q) { return new AmountOfSubstanceByTime(q); }
        public static implicit operator DiffusionFlux(PhysicalQuantity q) { return new DiffusionFlux(q); }
        public static implicit operator KinematicViscosity(PhysicalQuantity q) { return new KinematicViscosity(q); }
        public static implicit operator Acceleration(PhysicalQuantity q) { return new Acceleration(q); }
        public static implicit operator Momentum(PhysicalQuantity q) { return new Momentum(q); }
        public static implicit operator Force(PhysicalQuantity q) { return new Force(q); }
        public static implicit operator MassFlowRate(PhysicalQuantity q) { return new MassFlowRate(q); }
        public static implicit operator MomentOfInertia(PhysicalQuantity q) { return new MomentOfInertia(q); }
        public static implicit operator AngularMomentum(PhysicalQuantity q) { return new AngularMomentum(q); }
        public static implicit operator Energy(PhysicalQuantity q) { return new Energy(q); }
        public static implicit operator SurfaceTension(PhysicalQuantity q) { return new SurfaceTension(q); }
        public static implicit operator ElectricCharge(PhysicalQuantity q) { return new ElectricCharge(q); }
        public static implicit operator ElectricPotential(PhysicalQuantity q) { return new ElectricPotential(q); }
        public static implicit operator ElectricPotentialSquared(PhysicalQuantity q) { return new ElectricPotentialSquared(q); }
        public static implicit operator Power(PhysicalQuantity q) { return new Power(q); }
        public static implicit operator Resistance(PhysicalQuantity q) { return new Resistance(q); }
        public static implicit operator Pressure(PhysicalQuantity q) { return new Pressure(q); }
        public static implicit operator Frequency(PhysicalQuantity q) { return new Frequency(q); }
        public static implicit operator VelocityGradient(PhysicalQuantity q) { return new VelocityGradient(q); }
        public static implicit operator CoefficientOfViscosity(PhysicalQuantity q) { return new CoefficientOfViscosity(q); }
        public static implicit operator VolumeFlowRate(PhysicalQuantity q) { return new VolumeFlowRate(q); }
        public static implicit operator ResistanceToFlow(PhysicalQuantity q) { return new ResistanceToFlow(q); }
        public static implicit operator MassByAreaByTimeSquared(PhysicalQuantity q) { return new MassByAreaByTimeSquared(q); }
        public static implicit operator VelocityByDensity(PhysicalQuantity q) { return new VelocityByDensity(q); }
        public static implicit operator ThermalCapacity(PhysicalQuantity q) { return new ThermalCapacity(q); }
        public static implicit operator SpecificHeat(PhysicalQuantity q) { return new SpecificHeat(q); }
        public static implicit operator MolarSpecificHeat(PhysicalQuantity q) { return new MolarSpecificHeat(q); }
        public static implicit operator MolarConcentrationTimesAbsoluteTemperature(PhysicalQuantity q) { return new MolarConcentrationTimesAbsoluteTemperature(q); }
        public static implicit operator CoefficientOfThermalExpansion(PhysicalQuantity q) { return new CoefficientOfThermalExpansion(q); }
        public static implicit operator ThermalCapacityByVolume(PhysicalQuantity q) { return new ThermalCapacityByVolume(q); }
        public static implicit operator AbsoluteTemperature(PhysicalQuantity q) { return new AbsoluteTemperature(q); }
        public static implicit operator EnergyFlux(PhysicalQuantity q) { return new EnergyFlux(q); }
        public static implicit operator PowerGradient(PhysicalQuantity q) { return new PowerGradient(q); }
        public static implicit operator TemperatureGradient(PhysicalQuantity q) { return new TemperatureGradient(q); }
        public static implicit operator ThermalConductivity(PhysicalQuantity q) { return new ThermalConductivity(q); }
        public static implicit operator ResistanceTimesArea(PhysicalQuantity q) { return new ResistanceTimesArea(q); }
        public static implicit operator Resistivity(PhysicalQuantity q) { return new Resistivity(q); }
        public static implicit operator LuminousFlux(PhysicalQuantity q) { return new LuminousFlux(q); }
        public static implicit operator Illuminance(PhysicalQuantity q) { return new Illuminance(q); }
    }
}

