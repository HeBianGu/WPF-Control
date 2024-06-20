namespace H.Extensions.Unit
{
    public partial class MetricUnits : UnitsSystem
    {
        public override string Name => "Metric";
        public static readonly MetricUnits System = new MetricUnits();

        public static readonly Unit Units = new Unit(System, "Units", "1", "C62", Dimensions.Dimensionless, 1.0, Unit.DisplayOption.Explicit);
        public static readonly Unit Metres = new Unit(System, "Metres", "m", "MTR", Dimensions.Length, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit SquareMetres = new Unit(System, "SquareMetres", "m²", "MTK", Dimensions.Area, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit CubicMetres = new Unit(System, "CubicMetres", "m³", "MTQ", Dimensions.Volume, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Kilograms = new Unit(System, "Kilograms", "kg", "KGM", Dimensions.Mass, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Seconds = new Unit(System, "Seconds", "s", "SEC", Dimensions.Time, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Ampere = new Unit(System, "Ampere", "amp", "AMP", Dimensions.Current, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit DegreesKelvin = new Unit(System, "DegreesKelvin", "K", "KEL", Dimensions.TemperatureChange, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Mole = new Unit(System, "Mole", "mol", "C34", Dimensions.AmountOfSubstance, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Candela = new Unit(System, "Candela", "cd", "CDL", Dimensions.LuminousIntensity, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Radians = new Unit(System, "Radians", "rad", "C81", Dimensions.Angle, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Steradians = new Unit(System, "Steradians", "sr", "D27", Dimensions.SolidAngle, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit MetresPerSecond = new Unit(System, "MetresPerSecond", "m/s", "MTS", Dimensions.Velocity, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit MetresPerSecondSquared = new Unit(System, "MetresPerSecondSquared", "m/s²", "MSK", Dimensions.Acceleration, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit KilogramMetresPerSecond = new Unit(System, "KilogramMetresPerSecond", "kg⋅m/s", "B31", Dimensions.Momentum, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit KilogramsPerCubicMetre = new Unit(System, "KilogramsPerCubicMetre", "kg/m³", "KMQ", Dimensions.Density, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit KilogramsPerMole = new Unit(System, "KilogramsPerMole", "kg/mol", "D74", Dimensions.MolarMass, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit MolesPerCubicMetre = new Unit(System, "MolesPerCubicMetre", "mol/m3", "C36", Dimensions.MolarConcentration, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Newtons = new Unit(System, "Newtons", "N", "NEW", Dimensions.Force, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Joules = new Unit(System, "Joules", "J", "JOU", Dimensions.Energy, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Watts = new Unit(System, "Watts", "W", "WTT", Dimensions.Power, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Pascals = new Unit(System, "Pascals", "Pa", "PAL", Dimensions.Pressure, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Hertz = new Unit(System, "Hertz", "Hz", "HTZ", Dimensions.Frequency, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit KilogramsPerSecond = new Unit(System, "KilogramsPerSecond", "kg/s", "KGS", Dimensions.MassFlowRate, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit SquareMetresPerSecond = new Unit(System, "SquareMetresPerSecond", "m²/s", "S4", Dimensions.KinematicViscosity, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit CubicMetresPerSecond = new Unit(System, "CubicMetresPerSecond", "m³/s", "MQS", Dimensions.VolumeFlowRate, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Volts = new Unit(System, "Volts", "volt", "VLT", Dimensions.ElectricPotential, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Ohms = new Unit(System, "Ohms", "Ω", "OHM", Dimensions.Resistance, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit KilogramMetreSquared = new Unit(System, "KilogramMetreSquared", "kg⋅m²", "B32", Dimensions.MomentOfInertia, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit KilogramMetreSquaredPerSecond = new Unit(System, "KilogramMetreSquaredPerSecond", "kg⋅m²/s", "B33", Dimensions.AngularMomentum, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit NewtonsPerMetre = new Unit(System, "NewtonsPerMetre", "N/m", "4P", Dimensions.SurfaceTension, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit PascalSeconds = new Unit(System, "PascalSeconds", "Pa⋅s", "C65", Dimensions.CoefficientOfViscosity, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit PerDegreeKelvin = new Unit(System, "PerDegreeKelvin", "K⁻¹", "C91", Dimensions.CoefficientOfThermalExpansion, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit JoulesPerDegreeKelvin = new Unit(System, "JoulesPerDegreeKelvin", "J/K", "JE", Dimensions.ThermalCapacity, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit JoulesPerKilogramPerDegreeKelvin = new Unit(System, "JoulesPerKilogramPerDegreeKelvin", "J⋅kg⁻¹⋅K⁻¹", "B11", Dimensions.SpecificHeat, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit JoulesPerDegreeKelvinPerMole = new Unit(System, "JoulesPerDegreeKelvinPerMole", "J⋅K⁻¹⋅mol⁻¹", "B16", Dimensions.MolarSpecificHeat, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit WattsPerMetrePerDegree = new Unit(System, "WattsPerMetrePerDegree", "W⋅m⁻¹⋅K⁻¹", "D53", Dimensions.ThermalConductivity, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit OhmMetres = new Unit(System, "OhmMetres", "Ω⋅m", "C61", Dimensions.Resistivity, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Lumen = new Unit(System, "Lumen", "lm", "LUM", Dimensions.LuminousFlux, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Lux = new Unit(System, "Lux", "lux", "LUX", Dimensions.Illuminance, 1.0, Unit.DisplayOption.Standard);
        public static readonly Unit Percent = new Unit(System, "Percent", "%", "P1", Dimensions.Dimensionless, 0.01, Unit.DisplayOption.Explicit);
        public static readonly Unit Dozen = new Unit(System, "Dozen", "doz", "doz", Dimensions.Dimensionless, 12, Unit.DisplayOption.Explicit);
        public static readonly Unit Hundreds = new Unit(System, "Hundreds", "hundred", "CEN", Dimensions.Dimensionless, 100, Unit.DisplayOption.Explicit);
        public static readonly Unit Thousands = new Unit(System, "Thousands", "thousand", "MIL", Dimensions.Dimensionless, 1000, Unit.DisplayOption.Explicit);
        public static readonly Unit Millions = new Unit(System, "Millions", "million", "MIO", Dimensions.Dimensionless, 1000000, Unit.DisplayOption.Explicit);
        public static readonly Unit Billions = new Unit(System, "Billions", "billion", "BIL", Dimensions.Dimensionless, 1000000000, Unit.DisplayOption.Explicit);
        public static readonly Unit Trillions = new Unit(System, "Trillions", "trillion", "TRL", Dimensions.Dimensionless, 1000000000000, Unit.DisplayOption.Explicit);
        public static readonly Unit Grams = new Unit(System, "Grams", "g", "GRM", Dimensions.Mass, 0.001, Unit.DisplayOption.Multi);
        public static readonly Unit MilliGrams = new Unit(System, "MilliGrams", "mg", "MGM", Dimensions.Mass, 0.000001, Unit.DisplayOption.Multi);
        public static readonly Unit MicroGrams = new Unit(System, "MicroGrams", "μg", "MC", Dimensions.Mass, 0.000000001, Unit.DisplayOption.Multi);
        public static readonly Unit NanoGrams = new Unit(System, "NanoGrams", "ng", "ng", Dimensions.Mass, 0.000000000001, Unit.DisplayOption.Multi);
        public static readonly Unit Tonnes = new Unit(System, "Tonnes", "t", "TNE", Dimensions.Mass, 1000.0, Unit.DisplayOption.Multi);
        public static readonly Unit Daltons = new Unit(System, "Daltons", "Da", "Da", Dimensions.Mass, 1.660539040E-27, Unit.DisplayOption.Explicit);
        public static readonly Unit NanoMoles = new Unit(System, "NanoMoles", "nmol", "nmol", Dimensions.AmountOfSubstance, 1e-9, Unit.DisplayOption.Multi);
        public static readonly Unit Kilometres = new Unit(System, "Kilometres", "km", "KMT", Dimensions.Length, 1000.0, Unit.DisplayOption.Multi);
        public static readonly Unit Centimetres = new Unit(System, "Centimetres", "cm", "CMT", Dimensions.Length, 0.01, Unit.DisplayOption.Multi);
        public static readonly Unit Millimetres = new Unit(System, "Millimetres", "mm", "MMT", Dimensions.Length, 0.001, Unit.DisplayOption.Multi);
        public static readonly Unit Micrometres = new Unit(System, "Micrometres", "μ", "4H", Dimensions.Length, 0.000001, Unit.DisplayOption.Multi);
        public static readonly Unit Nanometres = new Unit(System, "Nanometres", "nm", "C45", Dimensions.Length, 0.000000001, Unit.DisplayOption.Multi);
        public static readonly Unit Angstroms = new Unit(System, "Angstroms", "Å", "A11", Dimensions.Length, 1e-10, Unit.DisplayOption.Explicit);
        public static readonly Unit AstronomicalUnits = new Unit(System, "AstronomicalUnits", "au", "A12", Dimensions.Length, 149597870700.0, Unit.DisplayOption.Explicit);
        public static readonly Unit SquareCentimetres = new Unit(System, "SquareCentimetres", "cm²", "CMK", Dimensions.Area, 0.0001, Unit.DisplayOption.Explicit);
        public static readonly Unit Hectares = new Unit(System, "Hectares", "ha", "HAR", Dimensions.Area, 10000.0, Unit.DisplayOption.Multi);
        public static readonly Unit CubicCentimetres = new Unit(System, "CubicCentimetres", "cc", "CMQ", Dimensions.Volume, 1e-6, Unit.DisplayOption.Explicit);
        public static readonly Unit Litres = new Unit(System, "Litres", "L", "LTR", Dimensions.Volume, 0.001, Unit.DisplayOption.Explicit);
        public static readonly Unit CentimetersPerSecond = new Unit(System, "CentimetersPerSecond", "cm/s", "2M", Dimensions.Velocity, 0.01, Unit.DisplayOption.Explicit);
        public static readonly Unit KilometresPerHour = new Unit(System, "KilometresPerHour", "kph", "KMH", Dimensions.Velocity, 0.2777777777777778, Unit.DisplayOption.Explicit);
        public static readonly Unit MillimetresOfMercury = new Unit(System, "MillimetresOfMercury", "mmHg", "HN", Dimensions.Pressure, 133.322368, Unit.DisplayOption.Explicit);
        public static readonly Unit AccelerationOfGravity = new Unit(System, "AccelerationOfGravity", "g0", "K40", Dimensions.Acceleration, 9.80665, Unit.DisplayOption.Explicit);
        public static readonly Unit Colories = new Unit(System, "Colories", "cal", "K53", Dimensions.Energy, 4.184, Unit.DisplayOption.Explicit);
        public static readonly Unit ElectronVolts = new Unit(System, "ElectronVolts", "eV", "A53", Dimensions.Energy, 1.602176634E-19, Unit.DisplayOption.Explicit);
        public static readonly Unit Kilowatts = new Unit(System, "Kilowatts", "kW", "KWT", Dimensions.Power, 1000.0, Unit.DisplayOption.Multi);
        public static readonly Unit KilowattHours = new Unit(System, "KilowattHours", "kWh", "KWH", Dimensions.Energy, 3.6e6, Unit.DisplayOption.Explicit);
        public static readonly Unit TonnesOfOilEquivalent = new Unit(System, "TonnesOfOilEquivalent", "toe", "toe", Dimensions.Energy, 41.868e9, Unit.DisplayOption.Explicit);
        public static readonly Unit DegreesCelsius = new Unit(System, "DegreesCelsius", "°C", "CEL", Dimensions.TemperatureChange, 1.0, Unit.DisplayOption.Explicit);
        public static readonly Unit CaloriesPerDegreeKelvin = new Unit(System, "CaloriesPerDegreeKelvin", "cal/K", "cal/K", Dimensions.ThermalCapacity, 4.186, Unit.DisplayOption.Explicit);
        public static readonly Unit DynesPerCentimetre = new Unit(System, "DynesPerCentimetre", "dyne/cm", "DX", Dimensions.SurfaceTension, 0.001, Unit.DisplayOption.Explicit);
        public static readonly Unit MilliSeconds = new Unit(System, "MilliSeconds", "ms", "C26", Dimensions.Time, 0.001, Unit.DisplayOption.Multi);
        public static readonly Unit SquareCentimetresPerSecond = new Unit(System, "SquareCentimetresPerSecond", "cm²/s", "M81", Dimensions.KinematicViscosity, 0.0001, Unit.DisplayOption.Explicit);
        public static readonly Unit CubicCentimetresPerSecond = new Unit(System, "CubicCentimetresPerSecond", "cc/s", "2J", Dimensions.VolumeFlowRate, 1e-6, Unit.DisplayOption.Explicit);
        public static readonly Unit GramsPerMole = new Unit(System, "GramsPerMole", "gm/mol", "A94", Dimensions.MolarMass, 0.001, Unit.DisplayOption.Explicit);
        public static readonly Unit MolesPerLitre = new Unit(System, "MolesPerLitre", "mol/L", "C38", Dimensions.MolarConcentration, 1000, Unit.DisplayOption.Explicit);
        public static readonly Unit Degrees = new Unit(System, "Degrees", "°", "DD", Dimensions.Angle, 0.01745329251994329576923690768489, Unit.DisplayOption.Explicit);
        public static readonly Unit SquareDegrees = new Unit(System, "SquareDegrees", "°²", "°²", Dimensions.SolidAngle, 3.0461741978670859934674354937889e-4, Unit.DisplayOption.Explicit);
        public static readonly Unit Dynes = new Unit(System, "Dynes", "dyn", "DU", Dimensions.Force, 1e-5, Unit.DisplayOption.Explicit);
        public static readonly Unit GramWeight = new Unit(System, "GramWeight", "gm⋅wt", "gm⋅wt", Dimensions.Force, 0.0098066500286389, Unit.DisplayOption.Explicit);
        public static readonly Unit KilogramWeight = new Unit(System, "KilogramWeight", "kg⋅wt", "B37", Dimensions.Force, 9.8066500286389, Unit.DisplayOption.Explicit);
        public static readonly Unit DynesPerSquareCentimetre = new Unit(System, "DynesPerSquareCentimetre", "dyn/cm²", "D9", Dimensions.Pressure, 0.1, Unit.DisplayOption.Explicit);
        public static readonly Unit GramsPerCC = new Unit(System, "GramsPerCC", "gm/cc", "23", Dimensions.Density, 1000, Unit.DisplayOption.Explicit);
        public static readonly Unit GramsPerLitre = new Unit(System, "GramsPerLitre", "gm/Ltr", "GL", Dimensions.Density, 1, Unit.DisplayOption.Explicit);
        public static readonly Unit MilligramsPerCC = new Unit(System, "MilligramsPerCC", "mg/cc", "mg/cc", Dimensions.Density, 1, Unit.DisplayOption.Explicit);
        public static readonly Unit Ergs = new Unit(System, "Ergs", "erg", "A57", Dimensions.Energy, 1e-7, Unit.DisplayOption.Explicit);
        public static readonly Unit Poises = new Unit(System, "Poises", "P", "89", Dimensions.CoefficientOfViscosity, 0.1, Unit.DisplayOption.Explicit);

        private static readonly Unit[] allUnits = new Unit[]
        {
            Units,
            Metres,
            SquareMetres,
            CubicMetres,
            Kilograms,
            Seconds,
            Ampere,
            DegreesKelvin,
            Mole,
            Candela,
            Radians,
            Steradians,
            MetresPerSecond,
            MetresPerSecondSquared,
            KilogramMetresPerSecond,
            KilogramsPerCubicMetre,
            KilogramsPerMole,
            MolesPerCubicMetre,
            Newtons,
            Joules,
            Watts,
            Pascals,
            Hertz,
            KilogramsPerSecond,
            SquareMetresPerSecond,
            CubicMetresPerSecond,
            Volts,
            Ohms,
            KilogramMetreSquared,
            KilogramMetreSquaredPerSecond,
            NewtonsPerMetre,
            PascalSeconds,
            PerDegreeKelvin,
            JoulesPerDegreeKelvin,
            JoulesPerKilogramPerDegreeKelvin,
            JoulesPerDegreeKelvinPerMole,
            WattsPerMetrePerDegree,
            OhmMetres,
            Lumen,
            Lux,
            Percent,
            Dozen,
            Hundreds,
            Thousands,
            Millions,
            Billions,
            Trillions,
            Grams,
            MilliGrams,
            MicroGrams,
            NanoGrams,
            Tonnes,
            Daltons,
            NanoMoles,
            Kilometres,
            Centimetres,
            Millimetres,
            Micrometres,
            Nanometres,
            Angstroms,
            AstronomicalUnits,
            SquareCentimetres,
            Hectares,
            CubicCentimetres,
            Litres,
            CentimetersPerSecond,
            KilometresPerHour,
            MillimetresOfMercury,
            AccelerationOfGravity,
            Colories,
            ElectronVolts,
            Kilowatts,
            KilowattHours,
            TonnesOfOilEquivalent,
            DegreesCelsius,
            CaloriesPerDegreeKelvin,
            DynesPerCentimetre,
            MilliSeconds,
            SquareCentimetresPerSecond,
            CubicCentimetresPerSecond,
            GramsPerMole,
            MolesPerLitre,
            Degrees,
            SquareDegrees,
            Dynes,
            GramWeight,
            KilogramWeight,
            DynesPerSquareCentimetre,
            GramsPerCC,
            GramsPerLitre,
            MilligramsPerCC,
            Ergs,
            Poises,
        };
        public override Unit[] GetAllUnits() { return allUnits; }

        private static readonly Unit[] displayUnits = new Unit[]
        {
            Metres,
            SquareMetres,
            CubicMetres,
            Kilograms,
            Seconds,
            Ampere,
            DegreesKelvin,
            Mole,
            Candela,
            Radians,
            Steradians,
            MetresPerSecond,
            MetresPerSecondSquared,
            KilogramMetresPerSecond,
            KilogramsPerCubicMetre,
            KilogramsPerMole,
            MolesPerCubicMetre,
            Newtons,
            Joules,
            Watts,
            Pascals,
            Hertz,
            KilogramsPerSecond,
            SquareMetresPerSecond,
            CubicMetresPerSecond,
            Volts,
            Ohms,
            KilogramMetreSquared,
            KilogramMetreSquaredPerSecond,
            NewtonsPerMetre,
            PascalSeconds,
            PerDegreeKelvin,
            JoulesPerDegreeKelvin,
            JoulesPerKilogramPerDegreeKelvin,
            JoulesPerDegreeKelvinPerMole,
            WattsPerMetrePerDegree,
            OhmMetres,
            Lumen,
            Lux,
            Grams,
            MilliGrams,
            MicroGrams,
            NanoGrams,
            Tonnes,
            NanoMoles,
            Kilometres,
            Centimetres,
            Millimetres,
            Micrometres,
            Nanometres,
            Hectares,
            Kilowatts,
            MilliSeconds,
        };
        protected override Unit[] GetDisplayUnits() { return displayUnits; }

        private static readonly Unit[] defaultUnits = new Unit[]
        {
            Metres,
            SquareMetres,
            CubicMetres,
            Kilograms,
            Seconds,
            Ampere,
            DegreesKelvin,
            Mole,
            Candela,
            Radians,
            Steradians,
            MetresPerSecond,
            MetresPerSecondSquared,
            KilogramMetresPerSecond,
            KilogramsPerCubicMetre,
            KilogramsPerMole,
            MolesPerCubicMetre,
            Newtons,
            Joules,
            Watts,
            Pascals,
            Hertz,
            KilogramsPerSecond,
            SquareMetresPerSecond,
            CubicMetresPerSecond,
            Volts,
            Ohms,
            KilogramMetreSquared,
            KilogramMetreSquaredPerSecond,
            NewtonsPerMetre,
            PascalSeconds,
            PerDegreeKelvin,
            JoulesPerDegreeKelvin,
            JoulesPerKilogramPerDegreeKelvin,
            JoulesPerDegreeKelvinPerMole,
            WattsPerMetrePerDegree,
            OhmMetres,
            Lumen,
            Lux,
        };
        protected override Unit[] GetDefaultUnits() { return defaultUnits; }

    } // end of MetricUnits

    public static class MetricUnitsExtensions
    {
        public static Dimensionless Units(this int v) { return ((double)v).Units(); }
        public static Dimensionless Units(this double v)
        {
            return new Dimensionless(v);
        }
        public static double InUnits(this Dimensionless v)
        {
            return v.Value;
        }
        public static Length Metres(this int v) { return ((double)v).Metres(); }
        public static Length Metres(this double v)
        {
            return new Length(v);
        }
        public static double InMetres(this Length v)
        {
            return v.Value;
        }
        public static Area SquareMetres(this int v) { return ((double)v).SquareMetres(); }
        public static Area SquareMetres(this double v)
        {
            return new Area(v);
        }
        public static double InSquareMetres(this Area v)
        {
            return v.Value;
        }
        public static Volume CubicMetres(this int v) { return ((double)v).CubicMetres(); }
        public static Volume CubicMetres(this double v)
        {
            return new Volume(v);
        }
        public static double InCubicMetres(this Volume v)
        {
            return v.Value;
        }
        public static Mass Kilograms(this int v) { return ((double)v).Kilograms(); }
        public static Mass Kilograms(this double v)
        {
            return new Mass(v);
        }
        public static double InKilograms(this Mass v)
        {
            return v.Value;
        }
        public static Time Seconds(this int v) { return ((double)v).Seconds(); }
        public static Time Seconds(this double v)
        {
            return new Time(v);
        }
        public static double InSeconds(this Time v)
        {
            return v.Value;
        }
        public static Current Ampere(this int v) { return ((double)v).Ampere(); }
        public static Current Ampere(this double v)
        {
            return new Current(v);
        }
        public static double InAmpere(this Current v)
        {
            return v.Value;
        }
        public static TemperatureChange DegreesKelvin(this int v) { return ((double)v).DegreesKelvin(); }
        public static TemperatureChange DegreesKelvin(this double v)
        {
            return new TemperatureChange(v);
        }
        public static double InDegreesKelvin(this TemperatureChange v)
        {
            return v.Value;
        }
        public static AmountOfSubstance Mole(this int v) { return ((double)v).Mole(); }
        public static AmountOfSubstance Mole(this double v)
        {
            return new AmountOfSubstance(v);
        }
        public static double InMole(this AmountOfSubstance v)
        {
            return v.Value;
        }
        public static LuminousIntensity Candela(this int v) { return ((double)v).Candela(); }
        public static LuminousIntensity Candela(this double v)
        {
            return new LuminousIntensity(v);
        }
        public static double InCandela(this LuminousIntensity v)
        {
            return v.Value;
        }
        public static Angle Radians(this int v) { return ((double)v).Radians(); }
        public static Angle Radians(this double v)
        {
            return new Angle(v);
        }
        public static double InRadians(this Angle v)
        {
            return v.Value;
        }
        public static SolidAngle Steradians(this int v) { return ((double)v).Steradians(); }
        public static SolidAngle Steradians(this double v)
        {
            return new SolidAngle(v);
        }
        public static double InSteradians(this SolidAngle v)
        {
            return v.Value;
        }
        public static Velocity MetresPerSecond(this int v) { return ((double)v).MetresPerSecond(); }
        public static Velocity MetresPerSecond(this double v)
        {
            return new Velocity(v);
        }
        public static double InMetresPerSecond(this Velocity v)
        {
            return v.Value;
        }
        public static Acceleration MetresPerSecondSquared(this int v) { return ((double)v).MetresPerSecondSquared(); }
        public static Acceleration MetresPerSecondSquared(this double v)
        {
            return new Acceleration(v);
        }
        public static double InMetresPerSecondSquared(this Acceleration v)
        {
            return v.Value;
        }
        public static Momentum KilogramMetresPerSecond(this int v) { return ((double)v).KilogramMetresPerSecond(); }
        public static Momentum KilogramMetresPerSecond(this double v)
        {
            return new Momentum(v);
        }
        public static double InKilogramMetresPerSecond(this Momentum v)
        {
            return v.Value;
        }
        public static Density KilogramsPerCubicMetre(this int v) { return ((double)v).KilogramsPerCubicMetre(); }
        public static Density KilogramsPerCubicMetre(this double v)
        {
            return new Density(v);
        }
        public static double InKilogramsPerCubicMetre(this Density v)
        {
            return v.Value;
        }
        public static MolarMass KilogramsPerMole(this int v) { return ((double)v).KilogramsPerMole(); }
        public static MolarMass KilogramsPerMole(this double v)
        {
            return new MolarMass(v);
        }
        public static double InKilogramsPerMole(this MolarMass v)
        {
            return v.Value;
        }
        public static MolarConcentration MolesPerCubicMetre(this int v) { return ((double)v).MolesPerCubicMetre(); }
        public static MolarConcentration MolesPerCubicMetre(this double v)
        {
            return new MolarConcentration(v);
        }
        public static double InMolesPerCubicMetre(this MolarConcentration v)
        {
            return v.Value;
        }
        public static Force Newtons(this int v) { return ((double)v).Newtons(); }
        public static Force Newtons(this double v)
        {
            return new Force(v);
        }
        public static double InNewtons(this Force v)
        {
            return v.Value;
        }
        public static Energy Joules(this int v) { return ((double)v).Joules(); }
        public static Energy Joules(this double v)
        {
            return new Energy(v);
        }
        public static double InJoules(this Energy v)
        {
            return v.Value;
        }
        public static Power Watts(this int v) { return ((double)v).Watts(); }
        public static Power Watts(this double v)
        {
            return new Power(v);
        }
        public static double InWatts(this Power v)
        {
            return v.Value;
        }
        public static Pressure Pascals(this int v) { return ((double)v).Pascals(); }
        public static Pressure Pascals(this double v)
        {
            return new Pressure(v);
        }
        public static double InPascals(this Pressure v)
        {
            return v.Value;
        }
        public static Frequency Hertz(this int v) { return ((double)v).Hertz(); }
        public static Frequency Hertz(this double v)
        {
            return new Frequency(v);
        }
        public static double InHertz(this Frequency v)
        {
            return v.Value;
        }
        public static MassFlowRate KilogramsPerSecond(this int v) { return ((double)v).KilogramsPerSecond(); }
        public static MassFlowRate KilogramsPerSecond(this double v)
        {
            return new MassFlowRate(v);
        }
        public static double InKilogramsPerSecond(this MassFlowRate v)
        {
            return v.Value;
        }
        public static KinematicViscosity SquareMetresPerSecond(this int v) { return ((double)v).SquareMetresPerSecond(); }
        public static KinematicViscosity SquareMetresPerSecond(this double v)
        {
            return new KinematicViscosity(v);
        }
        public static double InSquareMetresPerSecond(this KinematicViscosity v)
        {
            return v.Value;
        }
        public static VolumeFlowRate CubicMetresPerSecond(this int v) { return ((double)v).CubicMetresPerSecond(); }
        public static VolumeFlowRate CubicMetresPerSecond(this double v)
        {
            return new VolumeFlowRate(v);
        }
        public static double InCubicMetresPerSecond(this VolumeFlowRate v)
        {
            return v.Value;
        }
        public static ElectricPotential Volts(this int v) { return ((double)v).Volts(); }
        public static ElectricPotential Volts(this double v)
        {
            return new ElectricPotential(v);
        }
        public static double InVolts(this ElectricPotential v)
        {
            return v.Value;
        }
        public static Resistance Ohms(this int v) { return ((double)v).Ohms(); }
        public static Resistance Ohms(this double v)
        {
            return new Resistance(v);
        }
        public static double InOhms(this Resistance v)
        {
            return v.Value;
        }
        public static MomentOfInertia KilogramMetreSquared(this int v) { return ((double)v).KilogramMetreSquared(); }
        public static MomentOfInertia KilogramMetreSquared(this double v)
        {
            return new MomentOfInertia(v);
        }
        public static double InKilogramMetreSquared(this MomentOfInertia v)
        {
            return v.Value;
        }
        public static AngularMomentum KilogramMetreSquaredPerSecond(this int v) { return ((double)v).KilogramMetreSquaredPerSecond(); }
        public static AngularMomentum KilogramMetreSquaredPerSecond(this double v)
        {
            return new AngularMomentum(v);
        }
        public static double InKilogramMetreSquaredPerSecond(this AngularMomentum v)
        {
            return v.Value;
        }
        public static SurfaceTension NewtonsPerMetre(this int v) { return ((double)v).NewtonsPerMetre(); }
        public static SurfaceTension NewtonsPerMetre(this double v)
        {
            return new SurfaceTension(v);
        }
        public static double InNewtonsPerMetre(this SurfaceTension v)
        {
            return v.Value;
        }
        public static CoefficientOfViscosity PascalSeconds(this int v) { return ((double)v).PascalSeconds(); }
        public static CoefficientOfViscosity PascalSeconds(this double v)
        {
            return new CoefficientOfViscosity(v);
        }
        public static double InPascalSeconds(this CoefficientOfViscosity v)
        {
            return v.Value;
        }
        public static CoefficientOfThermalExpansion PerDegreeKelvin(this int v) { return ((double)v).PerDegreeKelvin(); }
        public static CoefficientOfThermalExpansion PerDegreeKelvin(this double v)
        {
            return new CoefficientOfThermalExpansion(v);
        }
        public static double InPerDegreeKelvin(this CoefficientOfThermalExpansion v)
        {
            return v.Value;
        }
        public static ThermalCapacity JoulesPerDegreeKelvin(this int v) { return ((double)v).JoulesPerDegreeKelvin(); }
        public static ThermalCapacity JoulesPerDegreeKelvin(this double v)
        {
            return new ThermalCapacity(v);
        }
        public static double InJoulesPerDegreeKelvin(this ThermalCapacity v)
        {
            return v.Value;
        }
        public static SpecificHeat JoulesPerKilogramPerDegreeKelvin(this int v) { return ((double)v).JoulesPerKilogramPerDegreeKelvin(); }
        public static SpecificHeat JoulesPerKilogramPerDegreeKelvin(this double v)
        {
            return new SpecificHeat(v);
        }
        public static double InJoulesPerKilogramPerDegreeKelvin(this SpecificHeat v)
        {
            return v.Value;
        }
        public static MolarSpecificHeat JoulesPerDegreeKelvinPerMole(this int v) { return ((double)v).JoulesPerDegreeKelvinPerMole(); }
        public static MolarSpecificHeat JoulesPerDegreeKelvinPerMole(this double v)
        {
            return new MolarSpecificHeat(v);
        }
        public static double InJoulesPerDegreeKelvinPerMole(this MolarSpecificHeat v)
        {
            return v.Value;
        }
        public static ThermalConductivity WattsPerMetrePerDegree(this int v) { return ((double)v).WattsPerMetrePerDegree(); }
        public static ThermalConductivity WattsPerMetrePerDegree(this double v)
        {
            return new ThermalConductivity(v);
        }
        public static double InWattsPerMetrePerDegree(this ThermalConductivity v)
        {
            return v.Value;
        }
        public static Resistivity OhmMetres(this int v) { return ((double)v).OhmMetres(); }
        public static Resistivity OhmMetres(this double v)
        {
            return new Resistivity(v);
        }
        public static double InOhmMetres(this Resistivity v)
        {
            return v.Value;
        }
        public static LuminousFlux Lumen(this int v) { return ((double)v).Lumen(); }
        public static LuminousFlux Lumen(this double v)
        {
            return new LuminousFlux(v);
        }
        public static double InLumen(this LuminousFlux v)
        {
            return v.Value;
        }
        public static Illuminance Lux(this int v) { return ((double)v).Lux(); }
        public static Illuminance Lux(this double v)
        {
            return new Illuminance(v);
        }
        public static double InLux(this Illuminance v)
        {
            return v.Value;
        }
        public static Dimensionless Percent(this int v) { return ((double)v).Percent(); }
        public static Dimensionless Percent(this double v)
        {
            return new Dimensionless(MetricUnits.Percent.ConvertValueToSI(v));
        }
        public static double InPercent(this Dimensionless v)
        {
            return MetricUnits.Percent.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Dimensionless Dozen(this int v) { return ((double)v).Dozen(); }
        public static Dimensionless Dozen(this double v)
        {
            return new Dimensionless(MetricUnits.Dozen.ConvertValueToSI(v));
        }
        public static double InDozen(this Dimensionless v)
        {
            return MetricUnits.Dozen.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Dimensionless Hundreds(this int v) { return ((double)v).Hundreds(); }
        public static Dimensionless Hundreds(this double v)
        {
            return new Dimensionless(MetricUnits.Hundreds.ConvertValueToSI(v));
        }
        public static double InHundreds(this Dimensionless v)
        {
            return MetricUnits.Hundreds.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Dimensionless Thousands(this int v) { return ((double)v).Thousands(); }
        public static Dimensionless Thousands(this double v)
        {
            return new Dimensionless(MetricUnits.Thousands.ConvertValueToSI(v));
        }
        public static double InThousands(this Dimensionless v)
        {
            return MetricUnits.Thousands.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Dimensionless Millions(this int v) { return ((double)v).Millions(); }
        public static Dimensionless Millions(this double v)
        {
            return new Dimensionless(MetricUnits.Millions.ConvertValueToSI(v));
        }
        public static double InMillions(this Dimensionless v)
        {
            return MetricUnits.Millions.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Dimensionless Billions(this int v) { return ((double)v).Billions(); }
        public static Dimensionless Billions(this double v)
        {
            return new Dimensionless(MetricUnits.Billions.ConvertValueToSI(v));
        }
        public static double InBillions(this Dimensionless v)
        {
            return MetricUnits.Billions.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Dimensionless Trillions(this int v) { return ((double)v).Trillions(); }
        public static Dimensionless Trillions(this double v)
        {
            return new Dimensionless(MetricUnits.Trillions.ConvertValueToSI(v));
        }
        public static double InTrillions(this Dimensionless v)
        {
            return MetricUnits.Trillions.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass Grams(this int v) { return ((double)v).Grams(); }
        public static Mass Grams(this double v)
        {
            return new Mass(MetricUnits.Grams.ConvertValueToSI(v));
        }
        public static double InGrams(this Mass v)
        {
            return MetricUnits.Grams.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass MilliGrams(this int v) { return ((double)v).MilliGrams(); }
        public static Mass MilliGrams(this double v)
        {
            return new Mass(MetricUnits.MilliGrams.ConvertValueToSI(v));
        }
        public static double InMilliGrams(this Mass v)
        {
            return MetricUnits.MilliGrams.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass MicroGrams(this int v) { return ((double)v).MicroGrams(); }
        public static Mass MicroGrams(this double v)
        {
            return new Mass(MetricUnits.MicroGrams.ConvertValueToSI(v));
        }
        public static double InMicroGrams(this Mass v)
        {
            return MetricUnits.MicroGrams.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass NanoGrams(this int v) { return ((double)v).NanoGrams(); }
        public static Mass NanoGrams(this double v)
        {
            return new Mass(MetricUnits.NanoGrams.ConvertValueToSI(v));
        }
        public static double InNanoGrams(this Mass v)
        {
            return MetricUnits.NanoGrams.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass Tonnes(this int v) { return ((double)v).Tonnes(); }
        public static Mass Tonnes(this double v)
        {
            return new Mass(MetricUnits.Tonnes.ConvertValueToSI(v));
        }
        public static double InTonnes(this Mass v)
        {
            return MetricUnits.Tonnes.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass Daltons(this int v) { return ((double)v).Daltons(); }
        public static Mass Daltons(this double v)
        {
            return new Mass(MetricUnits.Daltons.ConvertValueToSI(v));
        }
        public static double InDaltons(this Mass v)
        {
            return MetricUnits.Daltons.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static AmountOfSubstance NanoMoles(this int v) { return ((double)v).NanoMoles(); }
        public static AmountOfSubstance NanoMoles(this double v)
        {
            return new AmountOfSubstance(MetricUnits.NanoMoles.ConvertValueToSI(v));
        }
        public static double InNanoMoles(this AmountOfSubstance v)
        {
            return MetricUnits.NanoMoles.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Kilometres(this int v) { return ((double)v).Kilometres(); }
        public static Length Kilometres(this double v)
        {
            return new Length(MetricUnits.Kilometres.ConvertValueToSI(v));
        }
        public static double InKilometres(this Length v)
        {
            return MetricUnits.Kilometres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Centimetres(this int v) { return ((double)v).Centimetres(); }
        public static Length Centimetres(this double v)
        {
            return new Length(MetricUnits.Centimetres.ConvertValueToSI(v));
        }
        public static double InCentimetres(this Length v)
        {
            return MetricUnits.Centimetres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Millimetres(this int v) { return ((double)v).Millimetres(); }
        public static Length Millimetres(this double v)
        {
            return new Length(MetricUnits.Millimetres.ConvertValueToSI(v));
        }
        public static double InMillimetres(this Length v)
        {
            return MetricUnits.Millimetres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Micrometres(this int v) { return ((double)v).Micrometres(); }
        public static Length Micrometres(this double v)
        {
            return new Length(MetricUnits.Micrometres.ConvertValueToSI(v));
        }
        public static double InMicrometres(this Length v)
        {
            return MetricUnits.Micrometres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Nanometres(this int v) { return ((double)v).Nanometres(); }
        public static Length Nanometres(this double v)
        {
            return new Length(MetricUnits.Nanometres.ConvertValueToSI(v));
        }
        public static double InNanometres(this Length v)
        {
            return MetricUnits.Nanometres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Angstroms(this int v) { return ((double)v).Angstroms(); }
        public static Length Angstroms(this double v)
        {
            return new Length(MetricUnits.Angstroms.ConvertValueToSI(v));
        }
        public static double InAngstroms(this Length v)
        {
            return MetricUnits.Angstroms.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length AstronomicalUnits(this int v) { return ((double)v).AstronomicalUnits(); }
        public static Length AstronomicalUnits(this double v)
        {
            return new Length(MetricUnits.AstronomicalUnits.ConvertValueToSI(v));
        }
        public static double InAstronomicalUnits(this Length v)
        {
            return MetricUnits.AstronomicalUnits.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area SquareCentimetres(this int v) { return ((double)v).SquareCentimetres(); }
        public static Area SquareCentimetres(this double v)
        {
            return new Area(MetricUnits.SquareCentimetres.ConvertValueToSI(v));
        }
        public static double InSquareCentimetres(this Area v)
        {
            return MetricUnits.SquareCentimetres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area Hectares(this int v) { return ((double)v).Hectares(); }
        public static Area Hectares(this double v)
        {
            return new Area(MetricUnits.Hectares.ConvertValueToSI(v));
        }
        public static double InHectares(this Area v)
        {
            return MetricUnits.Hectares.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume CubicCentimetres(this int v) { return ((double)v).CubicCentimetres(); }
        public static Volume CubicCentimetres(this double v)
        {
            return new Volume(MetricUnits.CubicCentimetres.ConvertValueToSI(v));
        }
        public static double InCubicCentimetres(this Volume v)
        {
            return MetricUnits.CubicCentimetres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume Litres(this int v) { return ((double)v).Litres(); }
        public static Volume Litres(this double v)
        {
            return new Volume(MetricUnits.Litres.ConvertValueToSI(v));
        }
        public static double InLitres(this Volume v)
        {
            return MetricUnits.Litres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Velocity CentimetersPerSecond(this int v) { return ((double)v).CentimetersPerSecond(); }
        public static Velocity CentimetersPerSecond(this double v)
        {
            return new Velocity(MetricUnits.CentimetersPerSecond.ConvertValueToSI(v));
        }
        public static double InCentimetersPerSecond(this Velocity v)
        {
            return MetricUnits.CentimetersPerSecond.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Velocity KilometresPerHour(this int v) { return ((double)v).KilometresPerHour(); }
        public static Velocity KilometresPerHour(this double v)
        {
            return new Velocity(MetricUnits.KilometresPerHour.ConvertValueToSI(v));
        }
        public static double InKilometresPerHour(this Velocity v)
        {
            return MetricUnits.KilometresPerHour.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Pressure MillimetresOfMercury(this int v) { return ((double)v).MillimetresOfMercury(); }
        public static Pressure MillimetresOfMercury(this double v)
        {
            return new Pressure(MetricUnits.MillimetresOfMercury.ConvertValueToSI(v));
        }
        public static double InMillimetresOfMercury(this Pressure v)
        {
            return MetricUnits.MillimetresOfMercury.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Acceleration AccelerationOfGravity(this int v) { return ((double)v).AccelerationOfGravity(); }
        public static Acceleration AccelerationOfGravity(this double v)
        {
            return new Acceleration(MetricUnits.AccelerationOfGravity.ConvertValueToSI(v));
        }
        public static double InAccelerationOfGravity(this Acceleration v)
        {
            return MetricUnits.AccelerationOfGravity.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Energy Colories(this int v) { return ((double)v).Colories(); }
        public static Energy Colories(this double v)
        {
            return new Energy(MetricUnits.Colories.ConvertValueToSI(v));
        }
        public static double InColories(this Energy v)
        {
            return MetricUnits.Colories.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Energy ElectronVolts(this int v) { return ((double)v).ElectronVolts(); }
        public static Energy ElectronVolts(this double v)
        {
            return new Energy(MetricUnits.ElectronVolts.ConvertValueToSI(v));
        }
        public static double InElectronVolts(this Energy v)
        {
            return MetricUnits.ElectronVolts.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Power Kilowatts(this int v) { return ((double)v).Kilowatts(); }
        public static Power Kilowatts(this double v)
        {
            return new Power(MetricUnits.Kilowatts.ConvertValueToSI(v));
        }
        public static double InKilowatts(this Power v)
        {
            return MetricUnits.Kilowatts.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Energy KilowattHours(this int v) { return ((double)v).KilowattHours(); }
        public static Energy KilowattHours(this double v)
        {
            return new Energy(MetricUnits.KilowattHours.ConvertValueToSI(v));
        }
        public static double InKilowattHours(this Energy v)
        {
            return MetricUnits.KilowattHours.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Energy TonnesOfOilEquivalent(this int v) { return ((double)v).TonnesOfOilEquivalent(); }
        public static Energy TonnesOfOilEquivalent(this double v)
        {
            return new Energy(MetricUnits.TonnesOfOilEquivalent.ConvertValueToSI(v));
        }
        public static double InTonnesOfOilEquivalent(this Energy v)
        {
            return MetricUnits.TonnesOfOilEquivalent.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static TemperatureChange DegreesCelsius(this int v) { return ((double)v).DegreesCelsius(); }
        public static TemperatureChange DegreesCelsius(this double v)
        {
            return new TemperatureChange(v);
        }
        public static double InDegreesCelsius(this TemperatureChange v)
        {
            return v.Value;
        }
        public static ThermalCapacity CaloriesPerDegreeKelvin(this int v) { return ((double)v).CaloriesPerDegreeKelvin(); }
        public static ThermalCapacity CaloriesPerDegreeKelvin(this double v)
        {
            return new ThermalCapacity(MetricUnits.CaloriesPerDegreeKelvin.ConvertValueToSI(v));
        }
        public static double InCaloriesPerDegreeKelvin(this ThermalCapacity v)
        {
            return MetricUnits.CaloriesPerDegreeKelvin.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static SurfaceTension DynesPerCentimetre(this int v) { return ((double)v).DynesPerCentimetre(); }
        public static SurfaceTension DynesPerCentimetre(this double v)
        {
            return new SurfaceTension(MetricUnits.DynesPerCentimetre.ConvertValueToSI(v));
        }
        public static double InDynesPerCentimetre(this SurfaceTension v)
        {
            return MetricUnits.DynesPerCentimetre.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Time MilliSeconds(this int v) { return ((double)v).MilliSeconds(); }
        public static Time MilliSeconds(this double v)
        {
            return new Time(MetricUnits.MilliSeconds.ConvertValueToSI(v));
        }
        public static double InMilliSeconds(this Time v)
        {
            return MetricUnits.MilliSeconds.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static KinematicViscosity SquareCentimetresPerSecond(this int v) { return ((double)v).SquareCentimetresPerSecond(); }
        public static KinematicViscosity SquareCentimetresPerSecond(this double v)
        {
            return new KinematicViscosity(MetricUnits.SquareCentimetresPerSecond.ConvertValueToSI(v));
        }
        public static double InSquareCentimetresPerSecond(this KinematicViscosity v)
        {
            return MetricUnits.SquareCentimetresPerSecond.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static VolumeFlowRate CubicCentimetresPerSecond(this int v) { return ((double)v).CubicCentimetresPerSecond(); }
        public static VolumeFlowRate CubicCentimetresPerSecond(this double v)
        {
            return new VolumeFlowRate(MetricUnits.CubicCentimetresPerSecond.ConvertValueToSI(v));
        }
        public static double InCubicCentimetresPerSecond(this VolumeFlowRate v)
        {
            return MetricUnits.CubicCentimetresPerSecond.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static MolarMass GramsPerMole(this int v) { return ((double)v).GramsPerMole(); }
        public static MolarMass GramsPerMole(this double v)
        {
            return new MolarMass(MetricUnits.GramsPerMole.ConvertValueToSI(v));
        }
        public static double InGramsPerMole(this MolarMass v)
        {
            return MetricUnits.GramsPerMole.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static MolarConcentration MolesPerLitre(this int v) { return ((double)v).MolesPerLitre(); }
        public static MolarConcentration MolesPerLitre(this double v)
        {
            return new MolarConcentration(MetricUnits.MolesPerLitre.ConvertValueToSI(v));
        }
        public static double InMolesPerLitre(this MolarConcentration v)
        {
            return MetricUnits.MolesPerLitre.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Angle Degrees(this int v) { return ((double)v).Degrees(); }
        public static Angle Degrees(this double v)
        {
            return new Angle(MetricUnits.Degrees.ConvertValueToSI(v));
        }
        public static double InDegrees(this Angle v)
        {
            return MetricUnits.Degrees.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static SolidAngle SquareDegrees(this int v) { return ((double)v).SquareDegrees(); }
        public static SolidAngle SquareDegrees(this double v)
        {
            return new SolidAngle(MetricUnits.SquareDegrees.ConvertValueToSI(v));
        }
        public static double InSquareDegrees(this SolidAngle v)
        {
            return MetricUnits.SquareDegrees.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Force Dynes(this int v) { return ((double)v).Dynes(); }
        public static Force Dynes(this double v)
        {
            return new Force(MetricUnits.Dynes.ConvertValueToSI(v));
        }
        public static double InDynes(this Force v)
        {
            return MetricUnits.Dynes.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Force GramWeight(this int v) { return ((double)v).GramWeight(); }
        public static Force GramWeight(this double v)
        {
            return new Force(MetricUnits.GramWeight.ConvertValueToSI(v));
        }
        public static double InGramWeight(this Force v)
        {
            return MetricUnits.GramWeight.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Force KilogramWeight(this int v) { return ((double)v).KilogramWeight(); }
        public static Force KilogramWeight(this double v)
        {
            return new Force(MetricUnits.KilogramWeight.ConvertValueToSI(v));
        }
        public static double InKilogramWeight(this Force v)
        {
            return MetricUnits.KilogramWeight.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Pressure DynesPerSquareCentimetre(this int v) { return ((double)v).DynesPerSquareCentimetre(); }
        public static Pressure DynesPerSquareCentimetre(this double v)
        {
            return new Pressure(MetricUnits.DynesPerSquareCentimetre.ConvertValueToSI(v));
        }
        public static double InDynesPerSquareCentimetre(this Pressure v)
        {
            return MetricUnits.DynesPerSquareCentimetre.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Density GramsPerCC(this int v) { return ((double)v).GramsPerCC(); }
        public static Density GramsPerCC(this double v)
        {
            return new Density(MetricUnits.GramsPerCC.ConvertValueToSI(v));
        }
        public static double InGramsPerCC(this Density v)
        {
            return MetricUnits.GramsPerCC.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Density GramsPerLitre(this int v) { return ((double)v).GramsPerLitre(); }
        public static Density GramsPerLitre(this double v)
        {
            return new Density(MetricUnits.GramsPerLitre.ConvertValueToSI(v));
        }
        public static double InGramsPerLitre(this Density v)
        {
            return MetricUnits.GramsPerLitre.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Density MilligramsPerCC(this int v) { return ((double)v).MilligramsPerCC(); }
        public static Density MilligramsPerCC(this double v)
        {
            return new Density(MetricUnits.MilligramsPerCC.ConvertValueToSI(v));
        }
        public static double InMilligramsPerCC(this Density v)
        {
            return MetricUnits.MilligramsPerCC.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Energy Ergs(this int v) { return ((double)v).Ergs(); }
        public static Energy Ergs(this double v)
        {
            return new Energy(MetricUnits.Ergs.ConvertValueToSI(v));
        }
        public static double InErgs(this Energy v)
        {
            return MetricUnits.Ergs.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static CoefficientOfViscosity Poises(this int v) { return ((double)v).Poises(); }
        public static CoefficientOfViscosity Poises(this double v)
        {
            return new CoefficientOfViscosity(MetricUnits.Poises.ConvertValueToSI(v));
        }
        public static double InPoises(this CoefficientOfViscosity v)
        {
            return MetricUnits.Poises.ConvertValueFromSI((IPhysicalQuantity)v);
        }
    }
}
