// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Base;

namespace H.Controls.ShapeBox.Shapes;
public interface IFromToShape : IShape
{
    Point From { get; set; }
    Point To { get; set; }
}

public class LineShape : FromToShapeBase, IFromToShape, ITitleShape
{
    public LineShape()
    {

    }
    public LineShape(Point from, Point to)
    {
        this.From = from;
        this.To = to;
    }
    [Display(Name = "启用文本", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public bool UseText { get; set; } = false;
    [Display(Name = "端点样式", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public PointStyleStype PointStyleStype { get; set; } = PointStyleStype.None;
    public string Title { get; set; }

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Point normalToPoint, Pen pen, Brush fill = null)
    {
        Matrix matrix = this.GetInvertMatrix();
        if (this.PointStyleStype == PointStyleStype.Arrow)
        {
            drawingContext.DrawArrowPolygon(this.From, normalToPoint, pen, 6.ToGeo(view));
        }
        else
        {
            drawingContext.DrawLine(pen, this.From, normalToPoint);
            this.DrawPointStyleStype(view, this.PointStyleStype, drawingContext, this.From, pen, 45);
            this.DrawPointStyleStype(view, this.PointStyleStype, drawingContext, normalToPoint, pen, 45);
        }

        var normalToCenter = matrix.Transform(this.Center);
        if (this.UseText)
        {
            double length = (this.From - normalToPoint).Length;
            var txt = this.Title ?? length.ToString("F2");
            drawingContext.DrawTextAtTopCenter(length.ToString("F2"), normalToCenter, pen.Brush, 15.0 / view.Scale);
        }
    }

    public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
    {
        double length = 20.0 / view.Scale;
        var from = new Point(0, 0);
        var to = new Point(length, 0);
        var p = new Pen(stroke, strokeThickness);
        drawingContext.DrawLine(p, from, to);
        this.DrawPoint(view, drawingContext, from, fill, strokeThickness * 2);
        this.DrawPoint(view, drawingContext, to, fill, strokeThickness * 2);
    }
}
