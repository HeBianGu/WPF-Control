// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using System.Text;
using System.Windows.Ink;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes;
public class CrossShape : CommonShapeBase, IPreviewShape
{
    public Point Point { get; set; }
    public bool UseMousePosition => false;
    public bool UsePixel { get; set; } = true;
    public bool UseHEX { get; set; } = true;
    public bool UseRGB { get; set; } = true;

    protected override Pen GetPen(Brush stroke, double strokeThickness, IView view)
    {
        var pen = base.GetPen(stroke, strokeThickness, view);
        pen.DashStyle = DashStyles.Dash;
        if (pen.CanFreeze)
            pen.Freeze();
        return pen;
    }
    public override void MatrixDrawing(IView view, DrawingContext dc, Pen pen, Brush fill = null)
    {
        if (!view.Size.ToRect().Contains(this.Point))
            return;
        dc.DrawLine(pen, new Point(0, Point.Y), new Point(view.Size.Width, Point.Y));
        dc.DrawLine(pen, new Point(Point.X, 0), new Point(Point.X, view.Size.Height));
        if (this.UsePixel || this.UseRGB || this.UseHEX)
            this.DrawLabel(view, dc, pen);
    }

    protected virtual void DrawLabel(IView view, DrawingContext dc, Pen pen, Brush fill = null)
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (this.UsePixel)
            stringBuilder.AppendLine($"x:{(int)this.Point.X} y:{(int)this.Point.Y}");

        Color pickColor = default;
        if (view is IImageView imageView && (this.UseHEX || this.UseRGB))
        {
            pickColor = imageView.PickColor(this.Point);
            if (this.UseHEX)
                stringBuilder.AppendLine(pickColor.ToString());
            if (this.UseRGB)
                stringBuilder.AppendLine($"R:{pickColor.R} G:{pickColor.G} B:{pickColor.B} A:{pickColor.A}");
        }
        var rc = dc.DrawTextAt(stringBuilder.ToString(), this.Point, pen.Brush, 15 / view.Scale, null, 20 / view.Scale);
        if (fill != null)
            dc.DrawRoundedRectangle(fill, pen, new Rect(rc.Left, rc.Bottom, 20 / view.Scale, 10 / view.Scale), 1 / view.Scale, 1 / view.Scale);

        if (pickColor != default)
            dc.DrawRoundedRectangle(pickColor.ToSolid(), pen, new Rect(rc.Left, rc.Bottom, 20 / view.Scale, 10 / view.Scale), 1 / view.Scale, 1 / view.Scale);

    }

    public void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
    {

    }

    public void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
    {
        this.Draw(view, drawingContext, stroke, strokeThickness, fill);
    }
}
