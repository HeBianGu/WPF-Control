// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;

namespace H.Controls.ShapeBox.Shapes;

public interface IDimensionShape : IShape, ITextable, IFromToShape
{

}

public class DimensionShape : FromToShapeBase, IDimensionShape
{
    public DimensionShape()
    {

    }
    public DimensionShape(Point from, Point to)
    {
        this.From = from;
        this.To = to;
    }

    [Display(Name = "显示文本", GroupName = "样式")]
    public string Text { get; set; }

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Point normalToPoint, Pen pen, Brush fill = null)
    {
        this.DrawDimensionLine(view, drawingContext, this.From, normalToPoint, pen, PointStyleStype.Arrow, this.Text);
    }

    public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
    {
        double length = 20.0 / view.Scale;
        var from = new Point(0, 0);
        var to = new Point(length, 0);
        var p = new Pen(stroke, strokeThickness);
        drawingContext.DrawLine(p, from, to);
        this.DrawDimensionLine(view, drawingContext, from, to, p, PointStyleStype.Arrow);
    }
}
