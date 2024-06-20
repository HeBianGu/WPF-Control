namespace H.Extensions.Unit.British
{
    public partial class BritishUnits : UnitsSystem
    {
        public override string Name => "British";
        public static readonly BritishUnits System = new BritishUnits();
        public static readonly double poundsToKilogrammes = 0.45359237;
        public static readonly double feetToMetres = 0.3048;
        public static readonly double squareFeetToSquareMetres = (feetToMetres * feetToMetres);
        public static readonly double cubicFeetToCubicMetres = (feetToMetres * feetToMetres * feetToMetres);
        public static readonly double litresToCubicMetres = 0.001;

        public static readonly Unit LongTons = new Unit(System, "LongTons", "ton", "LTN", Dimensions.Mass, poundsToKilogrammes * 2240.0, Unit.DisplayOption.Multi);
        public static readonly Unit HundredWeight = new Unit(System, "HundredWeight", "cwt", "CWI", Dimensions.Mass, poundsToKilogrammes * 112.0, Unit.DisplayOption.Multi);
        public static readonly Unit Stones = new Unit(System, "Stones", "st", "STI", Dimensions.Mass, poundsToKilogrammes * 14.0, Unit.DisplayOption.Multi);
        public static readonly Unit Pounds = new Unit(System, "Pounds", "lb", "LBR", Dimensions.Mass, poundsToKilogrammes, Unit.DisplayOption.Standard);
        public static readonly Unit Ounces = new Unit(System, "Ounces", "oz", "ONZ", Dimensions.Mass, poundsToKilogrammes / 16.0, Unit.DisplayOption.Multi);
        public static readonly Unit Dram = new Unit(System, "Dram", "dr", "dr", Dimensions.Mass, 0.00177185, Unit.DisplayOption.Explicit);
        public static readonly Unit Grain = new Unit(System, "Grain", "gr", "GRN", Dimensions.Mass, 6.47989e-5, Unit.DisplayOption.Explicit);
        public static readonly Unit Miles = new Unit(System, "Miles", "mi", "SMI", Dimensions.Length, (feetToMetres * 5280.0), Unit.DisplayOption.Multi);
        public static readonly Unit Yards = new Unit(System, "Yards", "yd", "YRD", Dimensions.Length, (feetToMetres * 3.0), Unit.DisplayOption.Multi);
        public static readonly Unit Feet = new Unit(System, "Feet", "ft", "FOT", Dimensions.Length, feetToMetres, Unit.DisplayOption.Standard);
        public static readonly Unit Inches = new Unit(System, "Inches", "in", "INH", Dimensions.Length, (feetToMetres / 12.0), Unit.DisplayOption.Multi);
        public static readonly Unit NauticalMiles = new Unit(System, "NauticalMiles", "nmi", "NMI", Dimensions.Length, 1852, Unit.DisplayOption.Explicit);
        public static readonly Unit Furlongs = new Unit(System, "Furlongs", "fur", "M50", Dimensions.Length, 201.168, Unit.DisplayOption.Explicit);
        public static readonly Unit Rods = new Unit(System, "Rods", "rd", "rd", Dimensions.Length, 5.029, Unit.DisplayOption.Explicit);
        public static readonly Unit Fathoms = new Unit(System, "Fathoms", "fth", "AK", Dimensions.Length, 1.8288, Unit.DisplayOption.Explicit);
        public static readonly Unit Fortnights = new Unit(System, "Fortnights", "fn", "fn", Dimensions.Time, 3600.0 * 24.0 * 14.0, Unit.DisplayOption.Explicit);
        public static readonly Unit SquareFeet = new Unit(System, "SquareFeet", "ft²", "FTK", Dimensions.Area, squareFeetToSquareMetres, Unit.DisplayOption.Standard);
        public static readonly Unit SquareMiles = new Unit(System, "SquareMiles", "mi²", "MIK", Dimensions.Area, 2.788e+7 * squareFeetToSquareMetres, Unit.DisplayOption.Multi);
        public static readonly Unit Acres = new Unit(System, "Acres", "acre", "ACR", Dimensions.Area, 43560 * squareFeetToSquareMetres, Unit.DisplayOption.Multi);
        public static readonly Unit SquareYards = new Unit(System, "SquareYards", "yd²", "YDK", Dimensions.Area, 9 * squareFeetToSquareMetres, Unit.DisplayOption.Explicit);
        public static readonly Unit SquareInches = new Unit(System, "SquareInches", "in²", "INK", Dimensions.Area, 0.0069 * squareFeetToSquareMetres, Unit.DisplayOption.Explicit);
        public static readonly Unit SquareRods = new Unit(System, "SquareRods", "rd²", "rd²", Dimensions.Area, 272.25 * squareFeetToSquareMetres, Unit.DisplayOption.Explicit);
        public static readonly Unit CubicFeet = new Unit(System, "CubicFeet", "ft³", "FTQ", Dimensions.Volume, cubicFeetToCubicMetres, Unit.DisplayOption.Standard);
        public static readonly Unit CubicYards = new Unit(System, "CubicYards", "yd³", "YDQ", Dimensions.Volume, 27 * cubicFeetToCubicMetres, Unit.DisplayOption.Standard);
        public static readonly Unit CubicInches = new Unit(System, "CubicInches", "in³", "INQ", Dimensions.Volume, 0.00058 * cubicFeetToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit Gallons = new Unit(System, "Gallons", "gal", "GLI", Dimensions.Volume, 4.54609 * litresToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit Quarts = new Unit(System, "Quarts", "qt", "QTI", Dimensions.Volume, 1.136 * litresToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit Pints = new Unit(System, "Pints", "pt", "PTI", Dimensions.Volume, 0.568261 * litresToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit FluidOunces = new Unit(System, "FluidOunces", "floz", "OZI", Dimensions.Volume, 0.0284131 * litresToCubicMetres, Unit.DisplayOption.Explicit);
        public static readonly Unit FluidDrams = new Unit(System, "FluidDrams", "fldr", "fldr", Dimensions.Volume, 0.00355163 * litresToCubicMetres, Unit.DisplayOption.Explicit);
        public static readonly Unit DegreesFahrenheit = new Unit(System, "DegreesFahrenheit", "°F", "FAH", Dimensions.TemperatureChange, 5.0 / 9.0, Unit.DisplayOption.Standard);
        public static readonly Unit PoundsPerCubicInch = new Unit(System, "PoundsPerCubicInch", "lb/in³", "LA", Dimensions.Density, 27679.9047, Unit.DisplayOption.Explicit);
        public static readonly Unit MilesPerHour = new Unit(System, "MilesPerHour", "mph", "HM", Dimensions.Velocity, 1.0 / 2.23694, Unit.DisplayOption.Explicit);
        public static readonly Unit FeetPerSecond = new Unit(System, "FeetPerSecond", "ft/s", "FS", Dimensions.Velocity, 0.3048, Unit.DisplayOption.Explicit);
        public static readonly Unit FeetPerSecondSquared = new Unit(System, "FeetPerSecondSquared", "ft/s²", "A73", Dimensions.Acceleration, 0.3048, Unit.DisplayOption.Explicit);
        public static readonly Unit PoundsWeight = new Unit(System, "PoundsWeight", "lb⋅wt", "lb⋅wt", Dimensions.Force, 4.4482216282509, Unit.DisplayOption.Explicit);
        public static readonly Unit PoundsForce = new Unit(System, "PoundsForce", "lbf", "C78", Dimensions.Force, 4.4482216282509, Unit.DisplayOption.Explicit);
        public static readonly Unit PoundFootSquared = new Unit(System, "PoundFootSquared", "lb⋅ft²", "K65", Dimensions.MomentOfInertia, 0.0421401101, Unit.DisplayOption.Explicit);
        public static readonly Unit PoundFootSquaredPerSecond = new Unit(System, "PoundFootSquaredPerSecond", "lb⋅ft²/s", "lb⋅ft²/s", Dimensions.AngularMomentum, 0.0421401101, Unit.DisplayOption.Explicit);
        public static readonly Unit FootCandles = new Unit(System, "FootCandles", "lm/ft²", "P25", Dimensions.Illuminance, 10.76, Unit.DisplayOption.Explicit);

        private static readonly Unit[] allUnits = new Unit[]
        {
            LongTons,
            HundredWeight,
            Stones,
            Pounds,
            Ounces,
            Dram,
            Grain,
            Miles,
            Yards,
            Feet,
            Inches,
            NauticalMiles,
            Furlongs,
            Rods,
            Fathoms,
            Fortnights,
            SquareFeet,
            SquareMiles,
            Acres,
            SquareYards,
            SquareInches,
            SquareRods,
            CubicFeet,
            CubicYards,
            CubicInches,
            Gallons,
            Quarts,
            Pints,
            FluidOunces,
            FluidDrams,
            DegreesFahrenheit,
            PoundsPerCubicInch,
            MilesPerHour,
            FeetPerSecond,
            FeetPerSecondSquared,
            PoundsWeight,
            PoundsForce,
            PoundFootSquared,
            PoundFootSquaredPerSecond,
            FootCandles,
        };
        public override Unit[] GetAllUnits() { return allUnits; }

        private static readonly Unit[] displayUnits = new Unit[]
        {
            LongTons,
            HundredWeight,
            Stones,
            Pounds,
            Ounces,
            Miles,
            Yards,
            Feet,
            Inches,
            SquareFeet,
            SquareMiles,
            Acres,
            CubicFeet,
            CubicYards,
            CubicInches,
            Gallons,
            Quarts,
            Pints,
            DegreesFahrenheit,
        };
        protected override Unit[] GetDisplayUnits() { return displayUnits; }

        private static readonly Unit[] defaultUnits = new Unit[]
        {
            Pounds,
            Feet,
            SquareFeet,
            CubicFeet,
            CubicYards,
            DegreesFahrenheit,
        };
        protected override Unit[] GetDefaultUnits() { return defaultUnits; }

    } // end of BritishUnits

    public static class BritishUnitsExtensions
    {
        public static Mass LongTons(this int v) { return ((double)v).LongTons(); }
        public static Mass LongTons(this double v)
        {
            return new Mass(BritishUnits.LongTons.ConvertValueToSI(v));
        }
        public static double InLongTons(this Mass v)
        {
            return BritishUnits.LongTons.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass HundredWeight(this int v) { return ((double)v).HundredWeight(); }
        public static Mass HundredWeight(this double v)
        {
            return new Mass(BritishUnits.HundredWeight.ConvertValueToSI(v));
        }
        public static double InHundredWeight(this Mass v)
        {
            return BritishUnits.HundredWeight.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass Stones(this int v) { return ((double)v).Stones(); }
        public static Mass Stones(this double v)
        {
            return new Mass(BritishUnits.Stones.ConvertValueToSI(v));
        }
        public static double InStones(this Mass v)
        {
            return BritishUnits.Stones.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass Pounds(this int v) { return ((double)v).Pounds(); }
        public static Mass Pounds(this double v)
        {
            return new Mass(BritishUnits.Pounds.ConvertValueToSI(v));
        }
        public static double InPounds(this Mass v)
        {
            return BritishUnits.Pounds.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass Ounces(this int v) { return ((double)v).Ounces(); }
        public static Mass Ounces(this double v)
        {
            return new Mass(BritishUnits.Ounces.ConvertValueToSI(v));
        }
        public static double InOunces(this Mass v)
        {
            return BritishUnits.Ounces.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass Dram(this int v) { return ((double)v).Dram(); }
        public static Mass Dram(this double v)
        {
            return new Mass(BritishUnits.Dram.ConvertValueToSI(v));
        }
        public static double InDram(this Mass v)
        {
            return BritishUnits.Dram.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass Grain(this int v) { return ((double)v).Grain(); }
        public static Mass Grain(this double v)
        {
            return new Mass(BritishUnits.Grain.ConvertValueToSI(v));
        }
        public static double InGrain(this Mass v)
        {
            return BritishUnits.Grain.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Miles(this int v) { return ((double)v).Miles(); }
        public static Length Miles(this double v)
        {
            return new Length(BritishUnits.Miles.ConvertValueToSI(v));
        }
        public static double InMiles(this Length v)
        {
            return BritishUnits.Miles.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Yards(this int v) { return ((double)v).Yards(); }
        public static Length Yards(this double v)
        {
            return new Length(BritishUnits.Yards.ConvertValueToSI(v));
        }
        public static double InYards(this Length v)
        {
            return BritishUnits.Yards.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Feet(this int v) { return ((double)v).Feet(); }
        public static Length Feet(this double v)
        {
            return new Length(BritishUnits.Feet.ConvertValueToSI(v));
        }
        public static double InFeet(this Length v)
        {
            return BritishUnits.Feet.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Inches(this int v) { return ((double)v).Inches(); }
        public static Length Inches(this double v)
        {
            return new Length(BritishUnits.Inches.ConvertValueToSI(v));
        }
        public static double InInches(this Length v)
        {
            return BritishUnits.Inches.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length NauticalMiles(this int v) { return ((double)v).NauticalMiles(); }
        public static Length NauticalMiles(this double v)
        {
            return new Length(BritishUnits.NauticalMiles.ConvertValueToSI(v));
        }
        public static double InNauticalMiles(this Length v)
        {
            return BritishUnits.NauticalMiles.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Furlongs(this int v) { return ((double)v).Furlongs(); }
        public static Length Furlongs(this double v)
        {
            return new Length(BritishUnits.Furlongs.ConvertValueToSI(v));
        }
        public static double InFurlongs(this Length v)
        {
            return BritishUnits.Furlongs.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Rods(this int v) { return ((double)v).Rods(); }
        public static Length Rods(this double v)
        {
            return new Length(BritishUnits.Rods.ConvertValueToSI(v));
        }
        public static double InRods(this Length v)
        {
            return BritishUnits.Rods.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Fathoms(this int v) { return ((double)v).Fathoms(); }
        public static Length Fathoms(this double v)
        {
            return new Length(BritishUnits.Fathoms.ConvertValueToSI(v));
        }
        public static double InFathoms(this Length v)
        {
            return BritishUnits.Fathoms.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Time Fortnights(this int v) { return ((double)v).Fortnights(); }
        public static Time Fortnights(this double v)
        {
            return new Time(BritishUnits.Fortnights.ConvertValueToSI(v));
        }
        public static double InFortnights(this Time v)
        {
            return BritishUnits.Fortnights.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area SquareFeet(this int v) { return ((double)v).SquareFeet(); }
        public static Area SquareFeet(this double v)
        {
            return new Area(BritishUnits.SquareFeet.ConvertValueToSI(v));
        }
        public static double InSquareFeet(this Area v)
        {
            return BritishUnits.SquareFeet.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area SquareMiles(this int v) { return ((double)v).SquareMiles(); }
        public static Area SquareMiles(this double v)
        {
            return new Area(BritishUnits.SquareMiles.ConvertValueToSI(v));
        }
        public static double InSquareMiles(this Area v)
        {
            return BritishUnits.SquareMiles.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area Acres(this int v) { return ((double)v).Acres(); }
        public static Area Acres(this double v)
        {
            return new Area(BritishUnits.Acres.ConvertValueToSI(v));
        }
        public static double InAcres(this Area v)
        {
            return BritishUnits.Acres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area SquareYards(this int v) { return ((double)v).SquareYards(); }
        public static Area SquareYards(this double v)
        {
            return new Area(BritishUnits.SquareYards.ConvertValueToSI(v));
        }
        public static double InSquareYards(this Area v)
        {
            return BritishUnits.SquareYards.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area SquareInches(this int v) { return ((double)v).SquareInches(); }
        public static Area SquareInches(this double v)
        {
            return new Area(BritishUnits.SquareInches.ConvertValueToSI(v));
        }
        public static double InSquareInches(this Area v)
        {
            return BritishUnits.SquareInches.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area SquareRods(this int v) { return ((double)v).SquareRods(); }
        public static Area SquareRods(this double v)
        {
            return new Area(BritishUnits.SquareRods.ConvertValueToSI(v));
        }
        public static double InSquareRods(this Area v)
        {
            return BritishUnits.SquareRods.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume CubicFeet(this int v) { return ((double)v).CubicFeet(); }
        public static Volume CubicFeet(this double v)
        {
            return new Volume(BritishUnits.CubicFeet.ConvertValueToSI(v));
        }
        public static double InCubicFeet(this Volume v)
        {
            return BritishUnits.CubicFeet.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume CubicYards(this int v) { return ((double)v).CubicYards(); }
        public static Volume CubicYards(this double v)
        {
            return new Volume(BritishUnits.CubicYards.ConvertValueToSI(v));
        }
        public static double InCubicYards(this Volume v)
        {
            return BritishUnits.CubicYards.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume CubicInches(this int v) { return ((double)v).CubicInches(); }
        public static Volume CubicInches(this double v)
        {
            return new Volume(BritishUnits.CubicInches.ConvertValueToSI(v));
        }
        public static double InCubicInches(this Volume v)
        {
            return BritishUnits.CubicInches.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume Gallons(this int v) { return ((double)v).Gallons(); }
        public static Volume Gallons(this double v)
        {
            return new Volume(BritishUnits.Gallons.ConvertValueToSI(v));
        }
        public static double InGallons(this Volume v)
        {
            return BritishUnits.Gallons.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume Quarts(this int v) { return ((double)v).Quarts(); }
        public static Volume Quarts(this double v)
        {
            return new Volume(BritishUnits.Quarts.ConvertValueToSI(v));
        }
        public static double InQuarts(this Volume v)
        {
            return BritishUnits.Quarts.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume Pints(this int v) { return ((double)v).Pints(); }
        public static Volume Pints(this double v)
        {
            return new Volume(BritishUnits.Pints.ConvertValueToSI(v));
        }
        public static double InPints(this Volume v)
        {
            return BritishUnits.Pints.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume FluidOunces(this int v) { return ((double)v).FluidOunces(); }
        public static Volume FluidOunces(this double v)
        {
            return new Volume(BritishUnits.FluidOunces.ConvertValueToSI(v));
        }
        public static double InFluidOunces(this Volume v)
        {
            return BritishUnits.FluidOunces.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume FluidDrams(this int v) { return ((double)v).FluidDrams(); }
        public static Volume FluidDrams(this double v)
        {
            return new Volume(BritishUnits.FluidDrams.ConvertValueToSI(v));
        }
        public static double InFluidDrams(this Volume v)
        {
            return BritishUnits.FluidDrams.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static TemperatureChange DegreesFahrenheit(this int v) { return ((double)v).DegreesFahrenheit(); }
        public static TemperatureChange DegreesFahrenheit(this double v)
        {
            return new TemperatureChange(BritishUnits.DegreesFahrenheit.ConvertValueToSI(v));
        }
        public static double InDegreesFahrenheit(this TemperatureChange v)
        {
            return BritishUnits.DegreesFahrenheit.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Density PoundsPerCubicInch(this int v) { return ((double)v).PoundsPerCubicInch(); }
        public static Density PoundsPerCubicInch(this double v)
        {
            return new Density(BritishUnits.PoundsPerCubicInch.ConvertValueToSI(v));
        }
        public static double InPoundsPerCubicInch(this Density v)
        {
            return BritishUnits.PoundsPerCubicInch.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Velocity MilesPerHour(this int v) { return ((double)v).MilesPerHour(); }
        public static Velocity MilesPerHour(this double v)
        {
            return new Velocity(BritishUnits.MilesPerHour.ConvertValueToSI(v));
        }
        public static double InMilesPerHour(this Velocity v)
        {
            return BritishUnits.MilesPerHour.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Velocity FeetPerSecond(this int v) { return ((double)v).FeetPerSecond(); }
        public static Velocity FeetPerSecond(this double v)
        {
            return new Velocity(BritishUnits.FeetPerSecond.ConvertValueToSI(v));
        }
        public static double InFeetPerSecond(this Velocity v)
        {
            return BritishUnits.FeetPerSecond.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Acceleration FeetPerSecondSquared(this int v) { return ((double)v).FeetPerSecondSquared(); }
        public static Acceleration FeetPerSecondSquared(this double v)
        {
            return new Acceleration(BritishUnits.FeetPerSecondSquared.ConvertValueToSI(v));
        }
        public static double InFeetPerSecondSquared(this Acceleration v)
        {
            return BritishUnits.FeetPerSecondSquared.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Force PoundsWeight(this int v) { return ((double)v).PoundsWeight(); }
        public static Force PoundsWeight(this double v)
        {
            return new Force(BritishUnits.PoundsWeight.ConvertValueToSI(v));
        }
        public static double InPoundsWeight(this Force v)
        {
            return BritishUnits.PoundsWeight.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Force PoundsForce(this int v) { return ((double)v).PoundsForce(); }
        public static Force PoundsForce(this double v)
        {
            return new Force(BritishUnits.PoundsForce.ConvertValueToSI(v));
        }
        public static double InPoundsForce(this Force v)
        {
            return BritishUnits.PoundsForce.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static MomentOfInertia PoundFootSquared(this int v) { return ((double)v).PoundFootSquared(); }
        public static MomentOfInertia PoundFootSquared(this double v)
        {
            return new MomentOfInertia(BritishUnits.PoundFootSquared.ConvertValueToSI(v));
        }
        public static double InPoundFootSquared(this MomentOfInertia v)
        {
            return BritishUnits.PoundFootSquared.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static AngularMomentum PoundFootSquaredPerSecond(this int v) { return ((double)v).PoundFootSquaredPerSecond(); }
        public static AngularMomentum PoundFootSquaredPerSecond(this double v)
        {
            return new AngularMomentum(BritishUnits.PoundFootSquaredPerSecond.ConvertValueToSI(v));
        }
        public static double InPoundFootSquaredPerSecond(this AngularMomentum v)
        {
            return BritishUnits.PoundFootSquaredPerSecond.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Illuminance FootCandles(this int v) { return ((double)v).FootCandles(); }
        public static Illuminance FootCandles(this double v)
        {
            return new Illuminance(BritishUnits.FootCandles.ConvertValueToSI(v));
        }
        public static double InFootCandles(this Illuminance v)
        {
            return BritishUnits.FootCandles.ConvertValueFromSI((IPhysicalQuantity)v);
        }
    }
}
