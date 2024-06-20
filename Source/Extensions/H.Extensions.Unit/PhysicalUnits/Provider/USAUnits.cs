namespace H.Extensions.Unit.USA
{
    public partial class USAUnits : UnitsSystem
    {
        public override string Name => "USA";
        public static readonly USAUnits System = new USAUnits();
        public static readonly double poundsToKilogrammes = 0.45359237;
        public static readonly double feetToMetres = 0.3048;
        public static readonly double squareFeetToSquareMetres = (feetToMetres * feetToMetres);
        public static readonly double cubicFeetToCubicMetres = (feetToMetres * feetToMetres * feetToMetres);
        public static readonly double litresToCubicMetres = 0.001;

        public static readonly Unit LongTons = new Unit(System, "LongTons", "ton", "LTN", Dimensions.Mass, poundsToKilogrammes * 2240.0, Unit.DisplayOption.Multi);
        public static readonly Unit Pounds = new Unit(System, "Pounds", "lb", "LBR", Dimensions.Mass, poundsToKilogrammes, Unit.DisplayOption.Standard);
        public static readonly Unit Yards = new Unit(System, "Yards", "yrd", "YRD", Dimensions.Length, (feetToMetres * 3.0), Unit.DisplayOption.Explicit);
        public static readonly Unit Feet = new Unit(System, "Feet", "ft", "FOT", Dimensions.Length, feetToMetres, Unit.DisplayOption.Explicit);
        public static readonly Unit Inches = new Unit(System, "Inches", "in", "INH", Dimensions.Length, (feetToMetres / 12.0), Unit.DisplayOption.Explicit);
        public static readonly Unit NauticalMiles = new Unit(System, "NauticalMiles", "nmi", "NMI", Dimensions.Length, 1852, Unit.DisplayOption.Explicit);
        public static readonly Unit SquareFeet = new Unit(System, "SquareFeet", "ft²", "FTK", Dimensions.Area, (feetToMetres * feetToMetres), Unit.DisplayOption.Explicit);
        public static readonly Unit Acres = new Unit(System, "Acres", "acre", "ACR", Dimensions.Area, 43560 * squareFeetToSquareMetres, Unit.DisplayOption.Multi);
        public static readonly Unit SquareYards = new Unit(System, "SquareYards", "yd²", "YDK", Dimensions.Area, 9 * squareFeetToSquareMetres, Unit.DisplayOption.Explicit);
        public static readonly Unit SquareInches = new Unit(System, "SquareInches", "in²", "INK", Dimensions.Area, 0.0069 * squareFeetToSquareMetres, Unit.DisplayOption.Explicit);
        public static readonly Unit CubicFeet = new Unit(System, "CubicFeet", "ft³", "FTQ", Dimensions.Volume, cubicFeetToCubicMetres, Unit.DisplayOption.Standard);
        public static readonly Unit CubicYards = new Unit(System, "CubicYards", "yd³", "YDQ", Dimensions.Volume, 27 * cubicFeetToCubicMetres, Unit.DisplayOption.Standard);
        public static readonly Unit CubicInches = new Unit(System, "CubicInches", "in³", "INQ", Dimensions.Volume, 0.00058 * cubicFeetToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit Gallons = new Unit(System, "Gallons", "gal", "GLL", Dimensions.Volume, 3.78541 * litresToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit Quarts = new Unit(System, "Quarts", "qt", "QT", Dimensions.Volume, 0.946353 * litresToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit DryGallons = new Unit(System, "DryGallons", "dry gal", "GLD", Dimensions.Volume, 4.404884 * litresToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit DryQuarts = new Unit(System, "DryQuarts", "dry qt", "QTD", Dimensions.Volume, 1.101221 * litresToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit Bushels = new Unit(System, "Bushels", "bu", "BUA", Dimensions.Volume, 35.23907016688 * litresToCubicMetres, Unit.DisplayOption.Multi);
        public static readonly Unit Pecks = new Unit(System, "Pecks", "pk", "G23", Dimensions.Volume, 8.809768e-3, Unit.DisplayOption.Multi);
        public static readonly Unit Cups = new Unit(System, "Cups", "cup", "G21", Dimensions.Volume, 2.365882e-4, Unit.DisplayOption.Multi);
        public static readonly Unit DegreesFahrenheit = new Unit(System, "DegreesFahrenheit", "°F", "FAH", Dimensions.TemperatureChange, 5.0 / 9.0, Unit.DisplayOption.Standard);

        private static readonly Unit[] allUnits = new Unit[]
        {
            LongTons,
            Pounds,
            Yards,
            Feet,
            Inches,
            NauticalMiles,
            SquareFeet,
            Acres,
            SquareYards,
            SquareInches,
            CubicFeet,
            CubicYards,
            CubicInches,
            Gallons,
            Quarts,
            DryGallons,
            DryQuarts,
            Bushels,
            Pecks,
            Cups,
            DegreesFahrenheit,
        };
        public override Unit[] GetAllUnits() { return allUnits; }

        private static readonly Unit[] displayUnits = new Unit[]
        {
            LongTons,
            Pounds,
            Acres,
            CubicFeet,
            CubicYards,
            CubicInches,
            Gallons,
            Quarts,
            DryGallons,
            DryQuarts,
            Bushels,
            Pecks,
            Cups,
            DegreesFahrenheit,
        };
        protected override Unit[] GetDisplayUnits() { return displayUnits; }

        private static readonly Unit[] defaultUnits = new Unit[]
        {
            Pounds,
            CubicFeet,
            CubicYards,
            DegreesFahrenheit,
        };
        protected override Unit[] GetDefaultUnits() { return defaultUnits; }

    } // end of USAUnits

    public static class USAUnitsExtensions
    {
        public static Mass LongTons(this int v) { return ((double)v).LongTons(); }
        public static Mass LongTons(this double v)
        {
            return new Mass(USAUnits.LongTons.ConvertValueToSI(v));
        }
        public static double InLongTons(this Mass v)
        {
            return USAUnits.LongTons.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Mass Pounds(this int v) { return ((double)v).Pounds(); }
        public static Mass Pounds(this double v)
        {
            return new Mass(USAUnits.Pounds.ConvertValueToSI(v));
        }
        public static double InPounds(this Mass v)
        {
            return USAUnits.Pounds.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Yards(this int v) { return ((double)v).Yards(); }
        public static Length Yards(this double v)
        {
            return new Length(USAUnits.Yards.ConvertValueToSI(v));
        }
        public static double InYards(this Length v)
        {
            return USAUnits.Yards.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Feet(this int v) { return ((double)v).Feet(); }
        public static Length Feet(this double v)
        {
            return new Length(USAUnits.Feet.ConvertValueToSI(v));
        }
        public static double InFeet(this Length v)
        {
            return USAUnits.Feet.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length Inches(this int v) { return ((double)v).Inches(); }
        public static Length Inches(this double v)
        {
            return new Length(USAUnits.Inches.ConvertValueToSI(v));
        }
        public static double InInches(this Length v)
        {
            return USAUnits.Inches.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Length NauticalMiles(this int v) { return ((double)v).NauticalMiles(); }
        public static Length NauticalMiles(this double v)
        {
            return new Length(USAUnits.NauticalMiles.ConvertValueToSI(v));
        }
        public static double InNauticalMiles(this Length v)
        {
            return USAUnits.NauticalMiles.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area SquareFeet(this int v) { return ((double)v).SquareFeet(); }
        public static Area SquareFeet(this double v)
        {
            return new Area(USAUnits.SquareFeet.ConvertValueToSI(v));
        }
        public static double InSquareFeet(this Area v)
        {
            return USAUnits.SquareFeet.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area Acres(this int v) { return ((double)v).Acres(); }
        public static Area Acres(this double v)
        {
            return new Area(USAUnits.Acres.ConvertValueToSI(v));
        }
        public static double InAcres(this Area v)
        {
            return USAUnits.Acres.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area SquareYards(this int v) { return ((double)v).SquareYards(); }
        public static Area SquareYards(this double v)
        {
            return new Area(USAUnits.SquareYards.ConvertValueToSI(v));
        }
        public static double InSquareYards(this Area v)
        {
            return USAUnits.SquareYards.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Area SquareInches(this int v) { return ((double)v).SquareInches(); }
        public static Area SquareInches(this double v)
        {
            return new Area(USAUnits.SquareInches.ConvertValueToSI(v));
        }
        public static double InSquareInches(this Area v)
        {
            return USAUnits.SquareInches.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume CubicFeet(this int v) { return ((double)v).CubicFeet(); }
        public static Volume CubicFeet(this double v)
        {
            return new Volume(USAUnits.CubicFeet.ConvertValueToSI(v));
        }
        public static double InCubicFeet(this Volume v)
        {
            return USAUnits.CubicFeet.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume CubicYards(this int v) { return ((double)v).CubicYards(); }
        public static Volume CubicYards(this double v)
        {
            return new Volume(USAUnits.CubicYards.ConvertValueToSI(v));
        }
        public static double InCubicYards(this Volume v)
        {
            return USAUnits.CubicYards.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume CubicInches(this int v) { return ((double)v).CubicInches(); }
        public static Volume CubicInches(this double v)
        {
            return new Volume(USAUnits.CubicInches.ConvertValueToSI(v));
        }
        public static double InCubicInches(this Volume v)
        {
            return USAUnits.CubicInches.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume Gallons(this int v) { return ((double)v).Gallons(); }
        public static Volume Gallons(this double v)
        {
            return new Volume(USAUnits.Gallons.ConvertValueToSI(v));
        }
        public static double InGallons(this Volume v)
        {
            return USAUnits.Gallons.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume Quarts(this int v) { return ((double)v).Quarts(); }
        public static Volume Quarts(this double v)
        {
            return new Volume(USAUnits.Quarts.ConvertValueToSI(v));
        }
        public static double InQuarts(this Volume v)
        {
            return USAUnits.Quarts.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume DryGallons(this int v) { return ((double)v).DryGallons(); }
        public static Volume DryGallons(this double v)
        {
            return new Volume(USAUnits.DryGallons.ConvertValueToSI(v));
        }
        public static double InDryGallons(this Volume v)
        {
            return USAUnits.DryGallons.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume DryQuarts(this int v) { return ((double)v).DryQuarts(); }
        public static Volume DryQuarts(this double v)
        {
            return new Volume(USAUnits.DryQuarts.ConvertValueToSI(v));
        }
        public static double InDryQuarts(this Volume v)
        {
            return USAUnits.DryQuarts.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume Bushels(this int v) { return ((double)v).Bushels(); }
        public static Volume Bushels(this double v)
        {
            return new Volume(USAUnits.Bushels.ConvertValueToSI(v));
        }
        public static double InBushels(this Volume v)
        {
            return USAUnits.Bushels.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume Pecks(this int v) { return ((double)v).Pecks(); }
        public static Volume Pecks(this double v)
        {
            return new Volume(USAUnits.Pecks.ConvertValueToSI(v));
        }
        public static double InPecks(this Volume v)
        {
            return USAUnits.Pecks.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Volume Cups(this int v) { return ((double)v).Cups(); }
        public static Volume Cups(this double v)
        {
            return new Volume(USAUnits.Cups.ConvertValueToSI(v));
        }
        public static double InCups(this Volume v)
        {
            return USAUnits.Cups.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static TemperatureChange DegreesFahrenheit(this int v) { return ((double)v).DegreesFahrenheit(); }
        public static TemperatureChange DegreesFahrenheit(this double v)
        {
            return new TemperatureChange(USAUnits.DegreesFahrenheit.ConvertValueToSI(v));
        }
        public static double InDegreesFahrenheit(this TemperatureChange v)
        {
            return USAUnits.DegreesFahrenheit.ConvertValueFromSI((IPhysicalQuantity)v);
        }
    }
}
