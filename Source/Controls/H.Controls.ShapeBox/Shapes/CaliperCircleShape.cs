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
            this.FromRadius = radius;
        }
        public Point Center { get; set; }
        public double FromRadius { get; set; }
        public double ToRadius { get; set; }
        public int CaliperCount { get; set; }

        public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            if (this.FromRadius < 0 || this.ToRadius < 0)
                return;
            drawingContext.DrawEllipse(fill, pen, this.Center, this.FromRadius, this.FromRadius);
            //drawingContext.DrawEllipse(fill, pen, this.Center, this.ToRadius, this.ToRadius);
            this.DrawCross(view, drawingContext, this.Center, pen);

            //  Do ：说明
            int caliperCount = this.CaliperCount > 0 ? this.CaliperCount : 36;
            double angle = 360.0 / caliperCount;
            double l = Math.Abs(this.FromRadius - this.ToRadius);
            double tangentLength = this.FromRadius * Math.Tan(Math.PI / caliperCount);

            var linePen = new Pen(Brushes.LightBlue, pen.Thickness / 2);
            for (int i = 0; i < caliperCount; i++)
            {
                drawingContext.PushTransform(new RotateTransform() { Angle = angle * i, CenterX = this.Center.X, CenterY = this.Center.Y });
                Rect rect = new Rect(this.Center.X - this.FromRadius - l, this.Center.Y - tangentLength / 2, l * 2, tangentLength);
                drawingContext.DrawRectangle(null, linePen, rect);
                drawingContext.Pop();
            }

            base.MatrixDrawing(view, drawingContext, pen, fill);
        }

        protected override IEnumerable<IHandle> CreateHandles()
        {
            yield return new ActionHandle(x => this.Center = x, this.Center);
            yield return new ActionHandle(x => this.FromRadius = (x - this.Center).Length, new Point(this.Center.X + this.FromRadius, this.Center.Y));
            yield return new ActionHandle(x => this.ToRadius = (x - this.Center).Length, new Point(this.Center.X + this.ToRadius, this.Center.Y));
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
