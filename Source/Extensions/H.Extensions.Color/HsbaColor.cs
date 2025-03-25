// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Media;

namespace H.Extensions.Color;

public class HsbaColor
{
    private double h = 0, s = 0, b = 0, a = 0;
    /// <summary> 0 - 359，360 = 0  </summary>
    public double H { get { return h; } set { h = value < 0 ? 0 : value >= 360 ? 0 : value; } }

    /// <summary> 0 - 1 </summary>
    public double S { get { return s; } set { s = value < 0 ? 0 : value > 1 ? 1 : value; } }

    /// <summary> 0 - 1 </summary>
    public double B { get { return b; } set { b = value < 0 ? 0 : value > 1 ? 1 : value; } }

    /// <summary> 0 - 1 </summary>
    public double A { get { return a; } set { a = value < 0 ? 0 : value > 1 ? 1 : value; } }

    /// <summary> 亮度 0 - 100 </summary>
    public int Y { get { return this.RgbaColor.Y; } }

    public HsbaColor() { this.H = 0; this.S = 0; this.B = 1; this.A = 1; }

    public HsbaColor(double h, double s, double b, double a = 1) { this.H = h; this.S = s; this.B = b; this.A = a; }

    public HsbaColor(int r, int g, int b, int a = 255)
    {
        HsbaColor hsba = Utility.RgbaToHsba(new RgbaColor(r, g, b, a));
        this.H = hsba.H;
        this.S = hsba.S;
        this.B = hsba.B;
        this.A = hsba.A;
    }

    public HsbaColor(Brush brush)
    {
        HsbaColor hsba = Utility.RgbaToHsba(new RgbaColor(brush));
        this.H = hsba.H;
        this.S = hsba.S;
        this.B = hsba.B;
        this.A = hsba.A;
    }

    public HsbaColor(string hexColor)
    {
        HsbaColor hsba = Utility.RgbaToHsba(new RgbaColor(hexColor));
        this.H = hsba.H;
        this.S = hsba.S;
        this.B = hsba.B;
        this.A = hsba.A;
    }

    public System.Windows.Media.Color Color
    {
        get { return this.RgbaColor.Color; }
    }

    public System.Windows.Media.Color OpaqueColor
    {
        get { return this.RgbaColor.OpaqueColor; }
    }
    public SolidColorBrush SolidColorBrush
    {
        get { return this.RgbaColor.SolidColorBrush; }
    }

    public SolidColorBrush OpaqueSolidColorBrush
    {
        get { return this.RgbaColor.OpaqueSolidColorBrush; }
    }

    public string HexString
    {
        get { return this.Color.ToString(); }
    }
    public string RgbaString
    {
        get { return this.RgbaColor.RgbaString; }
    }

    public RgbaColor RgbaColor
    {
        get { return Utility.HsbaToRgba(this); }
    }
}
