// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Extension;

namespace H.Controls.ShapeBox.Shapes;

public class PointsShape : PointsShapeBase
{
    public PointsShape()
    {

    }
    public PointsShape(IEnumerable<Point> points) : base(points)
    {

    }

    public double Radius { get; set; } = 1.0;

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        if (this.Points == null || this.Points.Count == 0)
            return;
        drawingContext.DrawPoints(pen, fill, this.Radius.ToView(view), this.Points.ToArray());
        base.MatrixDrawing(view, drawingContext, pen, fill);
    }
}
