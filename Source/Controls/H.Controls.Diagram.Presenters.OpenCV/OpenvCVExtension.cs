using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace H.Controls.Diagram.Presenters.OpenCV;

public static class MatExtension
{
    public static void DrawRectangle(this Mat image, Rect box, Color boxColor, int boxThickness = 1)
    {
        Cv2.Rectangle(image, box, boxColor.ToScalar(), boxThickness, LineTypes.Link8);
    }

    public static void DrawCircle(this Mat image, Point point, int radius, Color color, int thickness = 1)
    {
        Cv2.Circle(image, point, radius, color.ToScalar(), thickness, LineTypes.Link8);
    }

    //public static void DrawEllipse(this Mat image, Point point, int radius, Color color, int thickness = 1)
    //{
    //    Cv2.Ellipse(image, point, radius, color.ToScalar(), thickness, LineTypes.Link8);
    //}
}

public static class ColorProvider
{
    public static IEnumerable<Color> GetColors()
    {
        yield return Colors.Red;
        yield return Colors.Blue;
        yield return Colors.Gray;
        yield return Colors.Orange;
        yield return Colors.DeepPink;
        yield return Colors.Green;
        yield return Colors.Purple;
        yield return Colors.Yellow;
        yield return Colors.Brown;
        yield return Colors.SkyBlue;
    }

    public static Color GetRandomColor()
    {
        var colors = GetColors().ToList();
        var random = new Random();
        int index = random.Next(colors.Count);
        return colors[index];
    }
}
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
    public static Point ToCVPoint(this System.Windows.Point size)
    {
        return new Point((int)size.X, (int)size.Y);
    }

    public static System.Windows.Point ToPoint(this Point size)
    {
        return new System.Windows.Point(size.X, size.Y);
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

