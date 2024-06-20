namespace H.Extensions.Unit
{
    public class Unit
    {
        public readonly UnitsSystem System;
        public readonly string Name;
        public readonly string ShortName;
        public readonly string CommonCode;
        public readonly Dimensions Dimensions;
        public readonly double ConversionFactor; //to convert from ISO units

        public enum DisplayOption
        {
            Standard,   // this is the default unit for the standard.  There should only be one of these for a given dimensions value
            Multi,      // can be used as best fit or in multiple format
            Explicit    // can be used explicitly in a format
        };
        public DisplayOption displayOption;

        public bool Default { get { return displayOption == DisplayOption.Standard; } }

        public Unit(UnitsSystem system, string name, string shortname, string commonCode, Dimensions dim, double conv, DisplayOption opt = DisplayOption.Explicit)
        {
            System = system;
            Name = name;
            ShortName = shortname;
            CommonCode = commonCode;
            Dimensions = dim;
            ConversionFactor = conv;
            displayOption = opt;
        }

        public override string ToString()
        {
            return ShortName;
        }

        internal string FormatValue(double SIValue, int precision)
        {
            // The value will be in SI Units
            string format = string.Format("{{0:G{0}}} {{1}}", precision);
            return string.Format(format, ConvertValueFromSI(SIValue), ShortName);
        }

        public double ConvertValueFromSI(IPhysicalQuantity pq)
        {
            // the dimensions have to match
            Check.True(Dimensions == pq.Dimensions, "Dimensions must match");
            return ConvertValueFromSI(pq.Value);
        }

        internal virtual double ConvertValueFromSI(double value)
        {
            return value / ConversionFactor;
        }

        internal virtual double ConvertValueToSI(double value)
        {
            return value * ConversionFactor;
        }

        internal PhysicalQuantity GetPhysicalQuantity(double quantity)
        {
            double value = ConvertValueToSI(quantity);
            return new PhysicalQuantity(value, Dimensions);
        }
    }
}
