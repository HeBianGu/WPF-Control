namespace H.Extensions.Unit
{
    public static class Powers
    {
        public static string ToString(short p)
        {
            switch (p)
            {
                case -9:
                    return "⁻⁹";
                case -8:
                    return "⁻⁸";
                case -7:
                    return "⁻⁷";
                case -6:
                    return "⁻⁶";
                case -5:
                    return "⁻⁵";
                case -4:
                    return "⁻⁴";
                case -3:
                    return "⁻³";
                case -2:
                    return "⁻²";
                case -1:
                    return "⁻¹";
                case 0:
                case 1:
                    return "";
                case 2:
                    return "²";
                case 3:
                    return "³";
                case 4:
                    return "⁴";
                case 5:
                    return "⁵";
                case 6:
                    return "⁶";
                case 7:
                    return "⁷";
                case 8:
                    return "⁸";
                case 9:
                    return "⁹";

                default:
                    return $"^{p}";
            }
        }


        public static bool TryParse(string s, out short power)
        {
            bool bOK = true;
            power = 0;
            switch (s)
            {
                case "⁻⁹":
                    power = -9;
                    break;
                case "⁻⁸":
                    power = -8;
                    break;
                case "⁻⁷":
                    power = -7;
                    break;
                case "⁻⁶":
                    power = -6;
                    break;
                case "⁻⁵":
                    power = -5;
                    break;
                case "⁻⁴":
                    power = -4;
                    break;
                case "⁻³":
                    power = -3;
                    break;
                case "⁻²":
                    power = -2;
                    break;
                case "⁻¹":
                    power = -1;
                    break;
                case "⁰":
                    power = 0;
                    break;
                case "":
                    power = 1;
                    break;
                case "²":
                    power = 2;
                    break;
                case "³":
                    power = 3;
                    break;
                case "⁴":
                    power = 4;
                    break;
                case "⁵":
                    power = 5;
                    break;
                case "⁶":
                    power = 6;
                    break;
                case "⁷":
                    power = 7;
                    break;
                case "⁸":
                    power = 8;
                    break;
                case "⁹":
                    power = 9;
                    break;

                default:
                    bOK = false;
                    break;
            }
            return bOK;
        }

    }
}
