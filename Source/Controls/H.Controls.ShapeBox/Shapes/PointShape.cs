// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;
using H.Controls.ShapeBox.Shapes.Base;

namespace H.Controls.ShapeBox.Shapes
{
    public class PointShape : TitleShapeBase
    {
        public PointShape()
        {

        }
        public PointShape(Point center)
        {
            this.Point = center;
        }
        public Point Point { get; set; }
        public bool UseCross { get; set; } = true;
        public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            if (this.UseCross)
                this.DrawCross(view, drawingContext, this.Point, pen, 45);
            this.DrawPoint(view, drawingContext, this.Point, fill);
            base.MatrixDrawing(view, drawingContext, pen, fill);
        }

        public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
        {
            double r = 10.0 / view.Scale;
            if (this.UseCross)
                this.DrawCross(view, drawingContext, new Point(r, r), new Pen(stroke, strokeThickness), 45);
            this.DrawPoint(view, drawingContext, new Point(r, r), fill, strokeThickness);
        }

        protected override IEnumerable<IHandle> CreateHandles()
        {
            yield return new ActionHandle(x => this.Point = x, this.Point);
        }
    }
}
