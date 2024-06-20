namespace H.Extensions.Unit
{
    public static class Prefixes
    {
        private static (string name, double factor)[] lookup = new[]
        {
            ("quetta",  1.0e30),
            ("ronna",   1.0e27),
            ("yotta",   1.0e24),
            ("zetta",   1.0e21),
            ("exa",     1.0e18),
            ("peta",    1.0e15),
            ("tera",    1.0e12),
            ("giga",    1.0e9),
            ("mega",    1.0e6),
            ("kilo",    1000.0),
            ("hecto",   100.0),
            ("deca",    10.0),
            ("deci",    0.1),
            ("centi",   0.01),
            ("milli",   0.001),
            ("micro",   1.0e-6),
            ("nano",    1.0e-9),
            ("pico",    1.0e-12),
            ("femto",   1.0e-15),
            ("atto",    1.0e-18),
            ("zepto",   1.0e-21),
            ("yocto",   1.0e-24),
            ("ronto",   1.0e-27),
            ("quecto",  1.0e-30),
        };

        public static bool Parse(string word, out double factor, out string unit)
        {
            factor = 0.0;
            unit = null;
            if (word.Length < 3)
                return false;
            for (int i = 0; i < lookup.Length; i++)
            {
                string s = lookup[i].name;
                if (word.StartsWith(s))
                {
                    factor = lookup[i].factor;
                    unit = word.Substring(s.Length);
                    return true;
                }
            }
            return false;
        }
    }

    public static class PrefixExtensions
    {
        public static double Quetta(this int v) { return v * 1.0e30; }
        public static double Ronna(this int v) { return v * 1.0e27; }
        public static double Yotta(this int v) { return v * 1.0e24; }
        public static double Zetta(this int v) { return v * 1.0e21; }
        public static double Exa(this int v) { return v * 1.0e18; }
        public static double Peta(this int v) { return v * 1.0e15; }
        public static double Tera(this int v) { return v * 1.0e12; }
        public static double Giga(this int v) { return v * 1.0e9; }
        public static double Mega(this int v) { return v * 1.0e6; }
        public static double Kilo(this int v) { return v * 1000.0; }
        public static double Hecto(this int v) { return v * 100.0; }
        public static double Deca(this int v) { return v * 10.0; }
        public static double Deci(this int v) { return v * 0.1; }
        public static double Centi(this int v) { return v * 0.01; }
        public static double Milli(this int v) { return v * 0.001; }
        public static double Micro(this int v) { return v * 1.0e-6; }
        public static double Nano(this int v) { return v * 1.0e-9; }
        public static double Pico(this int v) { return v * 1.0e-12; }
        public static double Femto(this int v) { return v * 1.0e-15; }
        public static double Atto(this int v) { return v * 1.0e-18; }
        public static double Zepto(this int v) { return v * 1.0e-21; }
        public static double Yocto(this int v) { return v * 1.0e-24; }
        public static double Ronto(this int v) { return v * 1.0e-27; }
        public static double Quecto(this int v) { return v * 1.0e-30; }


        public static double Quetta(this double v) { return v * 1.0e30; }
        public static double Ronna(this double v) { return v * 1.0e27; }
        public static double Yotta(this double v) { return v * 1.0e24; }
        public static double Zetta(this double v) { return v * 1.0e21; }
        public static double Exa(this double v) { return v * 1.0e18; }
        public static double Peta(this double v) { return v * 1.0e15; }
        public static double Tera(this double v) { return v * 1.0e12; }
        public static double Giga(this double v) { return v * 1.0e9; }
        public static double Mega(this double v) { return v * 1.0e6; }
        public static double Kilo(this double v) { return v * 1000.0; }
        public static double Hecto(this double v) { return v * 100.0; }
        public static double Deca(this double v) { return v * 10.0; }
        public static double Deci(this double v) { return v * 0.1; }
        public static double Centi(this double v) { return v * 0.01; }
        public static double Milli(this double v) { return v * 0.001; }
        public static double Micro(this double v) { return v * 1.0e-6; }
        public static double Nano(this double v) { return v * 1.0e-9; }
        public static double Pico(this double v) { return v * 1.0e-12; }
        public static double Femto(this double v) { return v * 1.0e-15; }
        public static double Atto(this double v) { return v * 1.0e-18; }
        public static double Zepto(this double v) { return v * 1.0e-21; }
        public static double Yocto(this double v) { return v * 1.0e-24; }
        public static double Ronto(this double v) { return v * 1.0e-27; }
        public static double Quecto(this double v) { return v * 1.0e-30; }
    }
}
