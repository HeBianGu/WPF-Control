// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Controls.Adorner.Adorner.ControlTemplateAdorners;

public class SelectedControlTemplateAdorner : ControlTemplateAdorner
{
    public Brush Fill { get; set; }
    public double ScaleLen { get; set; }
    public Pen Pen { get; set; }

    public SelectedControlTemplateAdorner(UIElement adornedElement) : base(adornedElement)
    {
        this.Pen = new Pen(new SolidColorBrush(Colors.Orange), 2) { DashStyle = new DashStyle(new double[] { 2, 2 }, 0) };
        this.ScaleLen = 0;
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        Rect rect = new Rect(this.AdornedElement.RenderSize);
        drawingContext.DrawRectangle(this.Fill, this.Pen, new Rect(rect.Left - this.ScaleLen, rect.Top - this.ScaleLen, rect.Width + 2 * this.ScaleLen, rect.Height + 2 * this.ScaleLen));
    }
}
