// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Controls.ShapeBox.Shapes.Base;

namespace H.Controls.ShapeBox.Shapes;
public class AngleShape : PreviewShapeBase, ITextable
{
    public AngleShape()
    {

    }

    public AngleShape(Point vertex, Point point1, Point point2)
    {
        this.Vertex = vertex;
        this.Point1 = point1;
        this.Point2 = point2;
    }

    [Display(Name = "顶点", GroupName = "数据", Order = -1)]
    public Point Vertex { get; set; }

    [Display(Name = "边1点", GroupName = "数据", Order = -1)]
    public Point Point1 { get; set; }

    [Display(Name = "边2点", GroupName = "数据", Order = -1)]
    public Point Point2 { get; set; }

    [Display(Name = "显示文本", GroupName = "样式")]
    public string Text { get; set; }

    [Display(Name = "弧半径", GroupName = "样式")]
    public double Radius { get; set; } = 30;

    [Display(Name = "显示辅助边", GroupName = "样式")]
    public bool ShowSides { get; set; } = true;

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        if (this.Vertex == this.Point1 || this.Vertex == this.Point2)
            return;

        Vector v1 = this.Point1 - this.Vertex;
        Vector v2 = this.Point2 - this.Vertex;

        if (v1.LengthSquared < 1e-10 || v2.LengthSquared < 1e-10)
            return;

        v1.Normalize();
        v2.Normalize();

        double r = Math.Max(1, this.Radius / view.Scale);

        Point p1 = this.Vertex + v1 * r;
        Point p2 = this.Vertex + v2 * r;

        if (this.ShowSides)
        {
            drawingContext.DrawLine(pen, this.Vertex, p1);
            drawingContext.DrawLine(pen, this.Vertex, p2);
        }

        // 画弧线（小夹角）
        double a1 = Math.Atan2(v1.Y, v1.X);
        double a2 = Math.Atan2(v2.Y, v2.X);

        double delta = NormalizeAngle(a2 - a1);
        if (delta > Math.PI)
        {
            // 改为走短弧：从 a2 -> a1
            (p1, p2) = (p2, p1);
            delta = 2.0 * Math.PI - delta;
        }

        bool isLargeArc = delta > Math.PI;
        var geom = new StreamGeometry();
        using (StreamGeometryContext ctx = geom.Open())
        {
            ctx.BeginFigure(p1, false, false);
            ctx.ArcTo(
                p2,
                new Size(r, r),
                0,
                isLargeArc,
                SweepDirection.Counterclockwise,
                true,
                false);
        }
        geom.Freeze();
        drawingContext.DrawGeometry(null, pen, geom);

        if (!string.IsNullOrWhiteSpace(this.Text))
        {
            // 文本放在角平分线方向
            Vector bisector = v1 + v2;
            if (bisector.LengthSquared < 1e-10)
                bisector = v1; // 180° 的极端情况兜底

            bisector.Normalize();

            Point textPoint = this.Vertex + bisector * (r + (12.0 / view.Scale));
            drawingContext.DrawTextAtTopCenter(this.Text, textPoint, pen.Brush, 15.0 / view.Scale);
        }
    }

    public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
    {
        var pen = new Pen(stroke, strokeThickness);
        var center = new Point(0, 0);
        double r = 20.0 / view.Scale;

        Point p1 = new Point(r, 0);
        Point p2 = new Point(0, r);

        AngleShape preview = new AngleShape(center, p1, p2)
        {
            Radius = 20,
            ShowSides = true,
            Text = this.Text
        };

        preview.MatrixDrawing(view, drawingContext, pen, fill);
    }

    private static double NormalizeAngle(double angle)
    {
        double twoPi = 2.0 * Math.PI;
        angle %= twoPi;
        if (angle < 0)
            angle += twoPi;
        return angle;
    }
}