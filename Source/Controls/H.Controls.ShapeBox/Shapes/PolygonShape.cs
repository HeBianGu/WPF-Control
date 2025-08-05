// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;
using H.Controls.ShapeBox.Drawings;
using H.Controls.ShapeBox.Shapes.Base;

namespace H.Controls.ShapeBox.Shapes
{

    public class PolygonShape : PointsShapeBase
    {
        public PolygonShape()
        {

        }
        public PolygonShape(IEnumerable<Point> points) : base(points)
        {

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
            drawingContext.DrawPloygon(p, fill, new Point(0, 0), new Point(length, -length), new Point(length * 2, length), new Point(length * 3, 0));
        }
    }
}
