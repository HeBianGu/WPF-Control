using System.Windows.Markup;
using System.Windows.Media;

namespace H.Extensions.Color;

public class GetDepthColorsExtension : MarkupExtension
{
    public DoubleCollection Depthes { get; set; } = new DoubleCollection(new double[] { 1.0, 0.8, 0.6, 0.4, 0.2 });

    public int From { get; set; } = 0;

    public int To { get; set; } = 360;

    public int Step { get; set; } = 48;

    public double B { get; set; } = 1.0;

    public double A { get; set; } = 1.0;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        List<System.Windows.Media.Color> list = new List<System.Windows.Media.Color>();
        list.AddRange(ColorFactory.CreateDepth(this.From, this.To, this.Step, (i, v) => new HsbaColor(i, v, this.B, this.A), this.Depthes.ToArray()));
        //  Do ：黑白
        for (int i = 0; i < this.Depthes.Count; i++)
        {
            byte v = Convert.ToByte((i * 1.0 / (this.Depthes.Count - 1)) * 255);
            list.Add(System.Windows.Media.Color.FromRgb(v, v, v));
        }

        list.Add(Colors.Transparent);
        return list;
    }
}
