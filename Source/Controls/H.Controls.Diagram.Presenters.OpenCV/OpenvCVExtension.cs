namespace H.Controls.Diagram.Presenters.OpenCV;

public static class OpenvCVExtension
{
    public static Size ToCVSize(this System.Windows.Size size)
    {
        return new Size(size.Width, size.Height);
    }

    public static System.Windows.Size ToSize(this Size size)
    {
        return new System.Windows.Size(size.Width, size.Height);
    }

    public static Rect ToCVRect(this System.Windows.Int32Rect size)
    {
        return new Rect(size.X, size.Y, size.Width, size.Height);
    }

    public static System.Windows.Int32Rect ToRect(this Rect size)
    {
        return new System.Windows.Int32Rect(size.Left, size.Top, size.Width, size.Height);
    }

    public static Scalar ToScalar(this Color color)
    {
        return Scalar.FromRgb(color.R, color.G, color.B);
    }

    public static Color ToColor(this Scalar color)
    {
        return Color.FromRgb((byte)color.Val2, (byte)color.Val1, (byte)color.Val0);
    }
}
