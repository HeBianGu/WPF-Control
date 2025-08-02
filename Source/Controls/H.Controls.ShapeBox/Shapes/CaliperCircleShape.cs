// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Drawings;
using H.Controls.ShapeBox.Shapes.Base;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes
{
    public class CaliperCircleShape : PreviewShapeBase
    {
        public CaliperCircleShape()
        {
            this.UseHandle = true;
        }
        public CaliperCircleShape(Point center, double radius) : this()
        {
            this.Center = center;
            this.Radius = radius;
        }
        public Point Center { get; set; }
        public double Radius { get; set; }
        public override void Drawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            base.Drawing(view, drawingContext, pen, fill);
            if (this.Radius < 0)
                return;
            drawingContext.DrawEllipse(fill, pen, this.Center, this.Radius, this.Radius);
            this.DrawCross(view, drawingContext, this.Center, pen);
        }

        protected override IEnumerable<IHandle> CreateHandles()
        {
            yield return new ActionHandle(this.Center);
        }

        public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
        {
            var r = 10.0 / view.Scale;
            var c = new Point(r, r);
            drawingContext.DrawEllipse(null, new Pen(stroke, strokeThickness), c, r, r);
            this.DrawPoint(view, drawingContext, c, fill, strokeThickness);
            this.DrawPoint(view, drawingContext, c + new Vector(r, 0), fill, strokeThickness);
        }
    }
}
