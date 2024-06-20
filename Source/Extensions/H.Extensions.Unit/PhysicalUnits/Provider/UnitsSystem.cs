
using System;
using System.Collections.Generic;
using System.Linq;

namespace H.Extensions.Unit
{
    public abstract class UnitsSystem
    {
        public abstract string Name { get; }

        public enum FormatOption
        {
            BestFit,
            Multiple,
            Standard
        };

        // we should really have a stack of systems
        //        public static MetricUnits Metric = new MetricUnits();
        public static MetricUnits Metric = MetricUnits.System;
        public static UnitsSystem Current = Metric;

        internal static string ToString(PhysicalQuantity qty)
        {
            return ToString(qty, Current, 9, FormatOption.BestFit);
        }

        internal static string ToString(Dimensionless d, FormatOption formatOption)
        {
            return $"{d.Value}";
        }

        internal static string ToString(IPhysicalQuantity qty, FormatOption formatOption)
        {
            return ToString(qty, Current, 9, formatOption);
        }

        internal static string ToString(IPhysicalQuantity qty, params Unit[] units)
        {
            return ToString(qty, 9, units);
        }

        internal static string ToString(IPhysicalQuantity qty, UnitsSystem system, FormatOption formatOption)
        {
            return ToString(qty, system, 9, formatOption);
        }

        internal static string ToString(IPhysicalQuantity qty, UnitsSystem system, int precision, FormatOption formatOption)
        {
            string s = system.DoToString(qty, precision, formatOption);
            if (string.IsNullOrEmpty(s) && !ReferenceEquals(system, Metric))
                s = Metric.DoToString(qty, precision, formatOption);
            return s;
        }

        internal static string ToString(IPhysicalQuantity qty, int precision, params Unit[] units)
        {
            if (units.Count() > 0)
                return Current.DoToString(qty, precision, units);
            else
                return Current.DoToString(qty, precision, FormatOption.BestFit);
        }

        public abstract Unit[] GetAllUnits();

        protected abstract Unit[] GetDisplayUnits();

        protected abstract Unit[] GetDefaultUnits();

        public string DoToString(IPhysicalQuantity qty)
        {
            return DoToString(qty.Value, qty.Dimensions, 9, FormatOption.BestFit);
        }

        private string DoToString(IPhysicalQuantity qty, int precision, FormatOption formatOption)
        {
            return DoToString(qty.Value, qty.Dimensions, precision, formatOption);
        }

        private string DoToString(double value, Dimensions dimensions, int precision, FormatOption formatOption)
        {
            string result = null;
            switch (formatOption)
            {
                case FormatOption.BestFit:
                case FormatOption.Standard:
                    Unit bestUnit = FindBestUnitFor(value, dimensions, formatOption);
                    if (bestUnit != null)
                        result = bestUnit.FormatValue(value, precision);
                    break;
                case FormatOption.Multiple:
                    result = DoToString(value, dimensions, precision, GetDisplayUnits());
                    break;
            }
            if (string.IsNullOrEmpty(result) && value >= 1e-9)
                result = string.Format($"{value} {ConstructUnits(dimensions)}");
            return result;
        }

        internal string DoToString(IPhysicalQuantity qty, int precision, params Unit[] units)
        {
            return DoToString(qty.Value, qty.Dimensions, precision, units);
        }

        /// <summary>
        /// Convert a value to a string comprising multiple units, for example "5 ft 4 in"
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="dimensions">The dimensions of the value</param>
        /// <param name="precision">Precision to be used for the last unit</param>
        /// <param name="units">List of units to use.  They are assumed to be in descending order of size.</param>
        /// <returns></returns>
        private string DoToString(double value, Dimensions dimensions, int precision, Unit[] units)
        {
            Unit bestUnit = null;
            if (units.Count() == 1)
            {
                // only one unit (or the last in the list)
                bestUnit = units[0];
                Check.True(bestUnit.Dimensions == dimensions, "Unit has wrong dimensions for value");
                return bestUnit.FormatValue(value, precision);
            }
            else
            {
                // Not the last unit.  Find the best fit in the list of units, and add an integer value for that unit
                // then repeat for the remaining value and units
                bestUnit = FindBestUnitFor(value, dimensions, units);
                if (bestUnit != null)
                {
                    double scaledValue = bestUnit.ConvertValueFromSI(value);
                    const double rounding = 1e-6;
                    double intValue = Math.Truncate(scaledValue + rounding);
                    string sResult = string.Format($"{intValue} {bestUnit.ShortName}");
                    double remainder = scaledValue - intValue;
                    if (remainder > rounding)
                    {
                        Unit[] newUnits = units.Where(val => val.ConversionFactor < bestUnit.ConversionFactor && val.Dimensions == dimensions).ToArray();
                        if (newUnits.Count() > 0)
                        {
                            double newValue = bestUnit.ConvertValueToSI(remainder);
                            sResult += " ";
                            sResult += DoToString(newValue, dimensions, precision, newUnits);
                        }
                        else
                        {
                            // no units left, so use the best with the given precision
                            sResult = bestUnit.FormatValue(value, precision);
                        }
                    }
                    return sResult;
                }
                else
                    return DoToString(value, dimensions, precision, FormatOption.BestFit);
            }
        }

        public string ConstructUnits(Dimensions dimensions)
        {
            string result = "";
            result = ConstructUnit(dimensions.M, Dimensions.Mass, result);
            result = ConstructUnit(dimensions.L, Dimensions.Length, result);
            result = ConstructUnit(dimensions.A, Dimensions.Angle, result);
            result = ConstructUnit(dimensions.I, Dimensions.Current, result);
            result = ConstructUnit(dimensions.Θ, Dimensions.TemperatureChange, result);
            result = ConstructUnit(dimensions.N, Dimensions.AmountOfSubstance, result);
            result = ConstructUnit(dimensions.J, Dimensions.LuminousIntensity, result);
            result = ConstructUnit(dimensions.T, Dimensions.Time, result);
            return result;
        }

        private string ConstructUnit(short value, Dimensions dim, string result)
        {
            if (value != 0)
            {
                if (!string.IsNullOrEmpty(result))
                    result += "⋅";
                Unit unit = FindDefaultUnitFor(dim);
                if (unit != null)
                    result += unit.ShortName + Powers.ToString(value);
                else
                    result = dim.ToString();
            }
            return result;
        }


        internal Unit FindDefaultUnitFor(Dimensions dimensions)
        {
            Unit[] units = GetDefaultUnits();
            foreach (Unit u in units)
            {
                if (u.Dimensions == dimensions && u.Default)
                    return u;
            }
            if (this != Metric)
                return Metric.FindDefaultUnitFor(dimensions);
            return null;
        }

        private Unit FindBestUnitFor(double value, Dimensions dimensions, FormatOption formatOption)
        {
            Unit[] units = null;
            switch (formatOption)
            {
                case FormatOption.BestFit:
                case FormatOption.Multiple:
                    units = GetDisplayUnits();
                    break;
                case FormatOption.Standard:
                    units = GetDefaultUnits();
                    break;
            }
            return FindBestUnitFor(value, dimensions, units);
        }

        private static Unit FindBestUnitFor(double value, Dimensions dimensions, Unit[] units)
        {
            Unit bestUnit = null;
            Unit defaultUnit = null;
            double bestValue = double.MaxValue;
            foreach (Unit u in units)
            {
                if (u.Dimensions == dimensions)
                {
                    double scaledValue = u.ConvertValueFromSI(value);
                    if (scaledValue >= 1.0 && scaledValue < bestValue)
                    {
                        bestUnit = u;
                        bestValue = scaledValue;
                    }
                    if (u.Default)
                        defaultUnit = u;
                }
            }
            if (bestUnit == null)
                bestUnit = defaultUnit;
            return bestUnit;
        }

        internal Unit FindUnit(string part)
        {
            Unit[] units = GetAllUnits();
            foreach (Unit u in units)
            {
                if (u.Name == part || u.ShortName == part)
                    return u;
            }
            return null;
        }

        public static PhysicalQuantity ParseUnits(string s)
        {
            return Current.DoParseUnits(s);
        }


        public static PhysicalQuantity Parse(string s)
        {
            return Current.DoParse(s);
        }

        public static PhysicalQuantity Parse(string s, UnitsSystem system)
        {
            return system.DoParse(s);
        }

        public static bool TryParse(string s, out PhysicalQuantity qty)
        {
            return TryParse(s, Current, out qty);
        }

        public static bool TryParse(string s, UnitsSystem system, out PhysicalQuantity qty)
        {
            try
            {
                qty = system.DoParse(s);
                return true;
            }
            catch (Exception)
            {
                qty = new PhysicalQuantity(0.0, null);
                return false;
            }
        }

        private enum ParseState { needNumber, needUnits };

        public PhysicalQuantity DoParse(string str)
        {
            // parse a string in the form [number unit]*
            PhysicalQuantity qty = new PhysicalQuantity();
            string[] parts = str.Split(' ');
            ParseState state = ParseState.needNumber;
            double value = 0.0;
            foreach (string part in parts)
            {
                switch (state)
                {
                    case ParseState.needNumber:
                        {
                            if (!double.TryParse(part, out value))
                                throw new Exception($"Parsing error: Not a number: {part} ");
                            state = ParseState.needUnits;
                        }
                        break;
                    case ParseState.needUnits:
                        {
                            PhysicalQuantity unitQty = DoParseUnits(part);
                            PhysicalQuantity partQty = value * unitQty;
                            if (qty.Dimensions != null && qty.Dimensions != partQty.Dimensions)
                                throw new Exception($"Parsing error: Invalid dimension for units: {part} ");
                            qty += partQty;
                            state = ParseState.needNumber;
                        }
                        break;
                    default:
                        break;
                }
            }
            return qty;
        }

        private PhysicalQuantity DoParseUnits(string part)
        {
            Unit unit = FindUnit(part);
            if (unit != null)
            {
                PhysicalQuantity unitQty = new PhysicalQuantity(unit.ConversionFactor, unit.Dimensions);
                return unitQty;
            }

            // try splitting by dots:
            string[] parts = part.Split('.', '*', '⋅');
            if (parts.Count() > 1)
            {
                PhysicalQuantity product = new PhysicalQuantity(1.0, Dimensions.Dimensionless);
                foreach (var subpart in parts)
                {
                    PhysicalQuantity subQty = DoParseUnits(subpart);
                    product *= subQty;
                }
                return product;
            }

            parts = part.Split('/');
            if (parts.Count() > 1)
            {
                PhysicalQuantity numerator = DoParseUnits(parts[0]);
                PhysicalQuantity denominator = DoParseUnits(parts[1]);
                return numerator / denominator;
            }

            if (part.Length > 1)
            {
                string unitPart = "";
                string power = "";
                short p = 0;
                if (part.Length > 2 && part[^2] == '⁻')
                {
                    unitPart = part.Substring(0, part.Length - 2);
                    power = part.Substring(part.Length - 2, 2);
                }
                else
                {
                    unitPart = part.Substring(0, part.Length - 1);
                    power = part.Substring(part.Length - 1, 1);
                }
                if (Powers.TryParse(power, out p))
                {
                    PhysicalQuantity qty = DoParseUnits(unitPart);
                    return qty ^ p;
                }
            }

            throw new Exception($"Parsing error: Invalid units: {part} ");
        }

        public List<Unit> UnitsFor(Dimensions dimensions)
        {
            List<Unit> list = new List<Unit>();
            if (dimensions != null)
            {
                Unit[] units = GetAllUnits();
                foreach (Unit u in units)
                {
                    if (u.Dimensions == dimensions)
                        list.Add(u);
                }
            }
            return list;
        }


    }
}
