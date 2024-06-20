namespace H.Extensions.Unit.TimeUnits
{
    public partial class TimeUnits : UnitsSystem
    {
        public override string Name => "Time";
        public static readonly TimeUnits System = new TimeUnits();

        public static readonly Unit Minutes = new Unit(System, "Minutes", "min", "MIN", Dimensions.Time, 60.0, Unit.DisplayOption.Explicit);
        public static readonly Unit Hours = new Unit(System, "Hours", "hr", "HUR", Dimensions.Time, 3600.0, Unit.DisplayOption.Explicit);
        public static readonly Unit Days = new Unit(System, "Days", "day", "DAY", Dimensions.Time, 3600.0 * 24.0, Unit.DisplayOption.Explicit);
        public static readonly Unit Weeks = new Unit(System, "Weeks", "week", "WEE", Dimensions.Time, 3600.0 * 24.0 * 7.0, Unit.DisplayOption.Explicit);
        public static readonly Unit Months = new Unit(System, "Months", "month", "MON", Dimensions.Time, 2.6298e4, Unit.DisplayOption.Explicit);
        public static readonly Unit JulianYears = new Unit(System, "JulianYears", "yr", "ANN", Dimensions.Time, 3600.0 * 24.0 * 365.25, Unit.DisplayOption.Explicit);
        public static readonly Unit SiderialYears = new Unit(System, "SiderialYears", "aₛ", "L96", Dimensions.Time, 3600.0 * 24.0 * 365.256363004, Unit.DisplayOption.Explicit);

        private static readonly Unit[] allUnits = new Unit[]
        {
            Minutes,
            Hours,
            Days,
            Weeks,
            Months,
            JulianYears,
            SiderialYears,
        };
        public override Unit[] GetAllUnits() { return allUnits; }

        private static readonly Unit[] displayUnits = new Unit[]
        {
        };
        protected override Unit[] GetDisplayUnits() { return displayUnits; }

        private static readonly Unit[] defaultUnits = new Unit[]
        {
        };
        protected override Unit[] GetDefaultUnits() { return defaultUnits; }

    } // end of TimeUnits

    public static class TimeUnitsExtensions
    {
        public static Time Minutes(this int v) { return ((double)v).Minutes(); }
        public static Time Minutes(this double v)
        {
            return new Time(TimeUnits.Minutes.ConvertValueToSI(v));
        }
        public static double InMinutes(this Time v)
        {
            return TimeUnits.Minutes.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Time Hours(this int v) { return ((double)v).Hours(); }
        public static Time Hours(this double v)
        {
            return new Time(TimeUnits.Hours.ConvertValueToSI(v));
        }
        public static double InHours(this Time v)
        {
            return TimeUnits.Hours.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Time Days(this int v) { return ((double)v).Days(); }
        public static Time Days(this double v)
        {
            return new Time(TimeUnits.Days.ConvertValueToSI(v));
        }
        public static double InDays(this Time v)
        {
            return TimeUnits.Days.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Time Weeks(this int v) { return ((double)v).Weeks(); }
        public static Time Weeks(this double v)
        {
            return new Time(TimeUnits.Weeks.ConvertValueToSI(v));
        }
        public static double InWeeks(this Time v)
        {
            return TimeUnits.Weeks.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Time Months(this int v) { return ((double)v).Months(); }
        public static Time Months(this double v)
        {
            return new Time(TimeUnits.Months.ConvertValueToSI(v));
        }
        public static double InMonths(this Time v)
        {
            return TimeUnits.Months.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Time JulianYears(this int v) { return ((double)v).JulianYears(); }
        public static Time JulianYears(this double v)
        {
            return new Time(TimeUnits.JulianYears.ConvertValueToSI(v));
        }
        public static double InJulianYears(this Time v)
        {
            return TimeUnits.JulianYears.ConvertValueFromSI((IPhysicalQuantity)v);
        }
        public static Time SiderialYears(this int v) { return ((double)v).SiderialYears(); }
        public static Time SiderialYears(this double v)
        {
            return new Time(TimeUnits.SiderialYears.ConvertValueToSI(v));
        }
        public static double InSiderialYears(this Time v)
        {
            return TimeUnits.SiderialYears.ConvertValueFromSI((IPhysicalQuantity)v);
        }
    }
}
