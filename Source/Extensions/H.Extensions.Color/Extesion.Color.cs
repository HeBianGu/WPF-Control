using System.Windows.Media;

namespace H.Extensions.Color;

public static class ColorExtesion
{
    public static HexColorEx ToHexColorEx(this string hex) => new HexColorEx(hex);
}

public class HexColorEx
{
    public string Hex { get; }
    public HexColorEx(string hex)
    {
        this.Hex = hex;
    }

    public System.Windows.Media.Color ToColor()
    {
        System.Windows.Media.Color color;
        if (this.Hex.Substring(0, 1) == "#") color = (System.Windows.Media.Color)ColorConverter.ConvertFromString(this.Hex);
        else color = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#" + this.Hex);
        return color;
    }
}
