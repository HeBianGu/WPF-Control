// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Media;

namespace H.Extensions.Color;

public class RgbaColor
{
    private int r = 0, g = 0, b = 0, a = 0;
    /// <summary> 0 - 255 </summary>
    public int R { get { return r; } set { r = value < 0 ? 0 : value > 255 ? 255 : value; } }

    /// <summary> 0 - 255 </summary>
    public int G { get { return g; } set { g = value < 0 ? 0 : value > 255 ? 255 : value; } }

    /// <summary> 0 - 255 </summary>
    public int B { get { return b; } set { b = value < 0 ? 0 : value > 255 ? 255 : value; } }

    /// <summary> 0 - 255 </summary>
    public int A { get { return a; } set { a = value < 0 ? 0 : value > 255 ? 255 : value; } }

    /// <summary> 亮度 0 - 100 </summary>
    public int Y { get { return Utility.GetBrightness(this.R, this.G, this.B); } }

    public RgbaColor() { this.R = 255; this.G = 255; this.B = 255; this.A = 255; }

    public RgbaColor(int r, int g, int b, int a = 255) { this.R = r; this.G = g; this.B = b; this.A = a; }

    public RgbaColor(Brush brush)
    {
        if (brush != null)
        {
            this.R = ((SolidColorBrush)brush).Color.R;
            this.G = ((SolidColorBrush)brush).Color.G;
            this.B = ((SolidColorBrush)brush).Color.B;
            this.A = ((SolidColorBrush)brush).Color.A;
        }
        else
        {
            this.R = this.G = this.B = this.A = 255;
        }
    }

    public RgbaColor(double h, double s, double b, double a = 1)
    {
        RgbaColor rgba = Utility.HsbaToRgba(new HsbaColor(h, s, b, a));
        this.R = rgba.R;
        this.G = rgba.G;
        this.B = rgba.B;
        this.A = rgba.A;

    }

    public RgbaColor(string hexColor)
    {
        try
        {
            System.Windows.Media.Color color;
            if (hexColor.Substring(0, 1) == "#") color = (System.Windows.Media.Color)ColorConverter.ConvertFromString(hexColor);
            else color = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#" + hexColor);
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
            this.A = color.A;
        }
        catch
        {

        }
    }

    public System.Windows.Media.Color Color { get { return System.Windows.Media.Color.FromArgb((byte)this.A, (byte)this.R, (byte)this.G, (byte)this.B); } }

    public System.Windows.Media.Color OpaqueColor { get { return System.Windows.Media.Color.FromArgb((byte)255.0, (byte)this.R, (byte)this.G, (byte)this.B); } }

    public SolidColorBrush SolidColorBrush { get { return new SolidColorBrush(this.Color); } }

    public SolidColorBrush OpaqueSolidColorBrush { get { return new SolidColorBrush(this.OpaqueColor); } }

    public string HexString { get { return this.Color.ToString(); } }

    public string RgbaString { get { return this.R + "," + this.G + "," + this.B + "," + this.A; } }

    public HsbaColor HsbaColor { get { return Utility.RgbaToHsba(this); } }
}
