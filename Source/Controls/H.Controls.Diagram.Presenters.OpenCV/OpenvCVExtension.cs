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

    public static Scalar ToScalar(this Color color)
    {
        return Scalar.FromRgb(color.R,color.G,color.B);
    }

    public static Color ToColor(this Scalar color)
    {
        return Color.FromRgb((byte)color.Val2, (byte)color.Val1, (byte)color.Val0);
    }
}
