// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Handles;
using H.Extensions.Common;
using System.Windows.Input;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes;
public class PolygonShape : PointsShapeBase
{
    public PolygonShape()
    {

    }
    public PolygonShape(IEnumerable<Point> points) : base(points)
    {

    }

    protected override double GetHandleViewSize()
    {
        return 100.0;
    }
    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        if (this.Points == null || this.Points.Count == 0)
            return;
        drawingContext.DrawPloygon(pen, fill, this.Points.ToArray());
        base.MatrixDrawing(view, drawingContext, pen, fill);
    }

    public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
    {
        double length = 10.0 / view.Scale;
        var p = new Pen(stroke, strokeThickness);
        if (p.CanFreeze)
            p.Freeze();
        drawingContext.DrawPloygon(p, fill, new Point(0, 0), new Point(length, -length), new Point(length * 2, length), new Point(length * 3, 0));
    }

    public override void DrawState(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
    {
        if (this.Points == null || this.Points.Count == 0)
            return;
        var p = new Pen(stroke, strokeThickness);
        if (p.CanFreeze)
            p.Freeze();
        var ps = this.Points.ToArray();
        drawingContext.DrawPloyLine(p, ps);
        if (fill == null && stroke is SolidColorBrush colorBrush)
            drawingContext.DrawPloygon(null, new SolidColorBrush(colorBrush.Color) { Opacity = 0.2 }, ps);
        else
            drawingContext.DrawPloygon(null, fill, ps);
    }

    protected override IEnumerable<IHandle> CreateHandles()
    {
        yield return new ActionCircleHandle(x =>
        {
            var center = this.Points.GetBoundingBox().GetCenter();
            for (var i = 0; i < this.Points.Count; i++)
            {
                Vector vector = this.Points[i] - center;
                this.Points[i] = x + vector;
            }
        }, this.Points.GetBoundingBox().GetCenter(), this);

        for (var i = 0; i < this.Points.Count; i++)
        {
            yield return new ActionHandle(x => this.Points[i] = x, this.Points[i], this) { Cursor = Cursors.ScrollAll };
        }
    }
}
