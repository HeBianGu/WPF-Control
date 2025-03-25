// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Markup;

namespace H.Extensions.MarkupExtension;

public abstract class RandomExtension<T> : System.Windows.Markup.MarkupExtension
{
    public T From { get; set; }

    public T To { get; set; }

    public static Random random = new Random();
}

[MarkupExtensionReturnType(typeof(int))]
public class IntRandowmExtension : RandomExtension<int>
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return random.Next(this.From, this.To);
    }
}
[MarkupExtensionReturnType(typeof(double))]
public class DoubleRandowmExtension : RandomExtension<double>
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return (random.NextDouble() * (this.To - this.From)) + this.From;
    }
}
[MarkupExtensionReturnType(typeof(Point))]
public class PointExtension : System.Windows.Markup.MarkupExtension
{
    public double X { get; set; }

    public double Y { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new Point(this.X, this.Y);
    }
}
[MarkupExtensionReturnType(typeof(Point))]
public class PointRandomExtension : RandomExtension<Point>
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        double x = (random.NextDouble() * (this.To.X - this.From.X)) + this.From.X;

        double y = (random.NextDouble() * (this.To.Y - this.From.Y)) + this.From.Y;

        return new Point(x, y);
    }
}

[MarkupExtensionReturnType(typeof(Rect))]
public class RectMarkupExtension : System.Windows.Markup.MarkupExtension
{
    public double X { get; set; }

    public double Y { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new Rect(this.X, this.Y, this.Width, this.Height);
    }
}

[MarkupExtensionReturnType(typeof(Rect))]
public class RectRandomExtension : RandomExtension<Rect>
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        double x = (random.NextDouble() * (this.To.X - this.From.X)) + this.From.X;

        double y = (random.NextDouble() * (this.To.Y - this.From.Y)) + this.From.Y;

        double w = (random.NextDouble() * (this.To.Width - this.From.Width)) + this.From.Width;

        double h = (random.NextDouble() * (this.To.Height - this.From.Height)) + this.From.Height;

        return new Rect(x, y, w, h);
    }
}
