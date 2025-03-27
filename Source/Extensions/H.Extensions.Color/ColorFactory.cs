using System.Windows.Media;

namespace H.Extensions.Color;

public class ColorFactory
{
    public static IEnumerable<System.Windows.Media.Color> CreateStandardColors()
    {
        yield return Colors.Transparent;
        yield return Colors.White;
        yield return Colors.Gray;
        yield return Colors.Black;
        yield return Colors.Red;
        yield return Colors.Green;
        yield return Colors.Blue;
        yield return Colors.Yellow;
        yield return Colors.Orange;
        yield return Colors.Purple;
    }

    public static IEnumerable<System.Windows.Media.Color> CreateAvailableColors()
    {
        List<System.Windows.Media.Color> result = typeof(Colors).GetProperties().Select(x => x.GetValue(null)).OfType<System.Windows.Media.Color>().ToList();
        result.Remove(Colors.Transparent);
        result.Sort(new ColorSorter());
        return result;
    }

    public static IEnumerable<System.Windows.Media.Color> CreateSystemColors()
    {
        return typeof(Colors).GetProperties().Select(x => x.GetValue(null)).OfType<System.Windows.Media.Color>();
    }

    public static IEnumerable<SolidColorBrush> CreateSystemSolidColorBrushes()
    {
        return typeof(Brushes).GetProperties().Select(x => x.GetValue(null)).OfType<SolidColorBrush>();
    }

    public static IEnumerable<System.Windows.Media.Color> CreateDepths(params double[] values)
    {
        foreach (var value in values)
        {
            foreach (var item in CreateDepthB(value))
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<System.Windows.Media.Color> CreateDepthB(params double[] bs)
    {
        return CreateDepthB(3, 0, 100, bs);
    }
    public static IEnumerable<System.Windows.Media.Color> CreateDepthB(int from, int to, int step, params double[] values)
    {
        return CreateDepth(from, to, step, (i, v) => new HsbaColor(i, 1.0, v, 1.0), values);
    }

    public static IEnumerable<System.Windows.Media.Color> CreateDepthA(int from, int to, int step, params double[] values)
    {
        return CreateDepth(from, to, step, (i, v) => new HsbaColor(i, 1.0, 1.0, v), values);
    }

    public static IEnumerable<System.Windows.Media.Color> CreateDepth(int from, int to, int step, Func<int, double, HsbaColor> create, params double[] values)
    {
        for (int i = from; i <= to; i = i + step)
        {
            foreach (var value in values)
            {
                yield return create.Invoke(i, value).Color;
            }
        }
    }

    public static IEnumerable<System.Windows.Media.Color> CreateDepth(int from, int to, int step, double vStep = 0.2)
    {
        for (int i = from; i < to; i++)
        {
            if (i % step == 0)
            {
                for (double r = 0; r < 1.0; r = r + vStep)
                {
                    for (double g = 0; g < 1.0; g = g + vStep)
                    {
                        for (double b = 0; b < 1.0; b = b + vStep)
                        {
                            yield return new HsbaColor(i, r, g, b).Color;
                        }
                    }
                }
            }
        }
    }
}
