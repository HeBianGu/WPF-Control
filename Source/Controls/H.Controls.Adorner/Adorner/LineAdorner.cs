// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Controls.Adorner.Adorner;

public class LineAdorner : BorderAdorner
{
    public LineAdorner(UIElement adornedElement) : base(adornedElement)
    {
        this.Pen = new Pen(Brushes.DeepSkyBlue, 4);
        this.Dock = Dock.Top;
        this.ScaleLen = 0;
        this.Fill = new SolidColorBrush(Colors.LightSkyBlue) { Opacity = 0.2 };
    }

    public Dock Dock { get; set; }
    protected override void OnRender(DrawingContext dc)
    {
        Rect rect = new Rect(this.AdornedElement.RenderSize);
        rect = new Rect(rect.Left - this.ScaleLen, rect.Top - this.ScaleLen, rect.Width + 2 * this.ScaleLen, rect.Height + 2 * this.ScaleLen);
        dc.DrawRectangle(this.Fill, null, rect);

        if (this.Dock == Dock.Left)
            dc.DrawLine(this.Pen, rect.TopLeft, rect.BottomLeft);
        if (this.Dock == Dock.Top)
            dc.DrawLine(this.Pen, rect.TopLeft, rect.TopRight);
        if (this.Dock == Dock.Right)
            dc.DrawLine(this.Pen, rect.BottomRight, rect.TopRight);
        if (this.Dock == Dock.Bottom)
            dc.DrawLine(this.Pen, rect.BottomLeft, rect.BottomRight);
    }
}
