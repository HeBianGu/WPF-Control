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
    public class LineShape : PreviewShapeBase
    {
        public LineShape()
        {

        }
        public LineShape(Point from, Point to)
        {
            this.From = from;
            this.To = to;
        }
        public Point From { get; set; }
        public Point To { get; set; }
        public bool UseCross { get; set; } = true;
        public bool UseText { get; set; } = true;
        public override void Drawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            base.Drawing(view, drawingContext, pen, fill);
            if (this.From == this.To)
                return;
            drawingContext.DrawLine(pen, this.From, this.To);
            if (this.UseCross)
            {
                this.DrawCross(view, drawingContext, this.From, pen, this.Angle + 45);
                this.DrawCross(view, drawingContext, this.To, pen, this.Angle + 45);
            }

            if (this.UseText)
            {
                double length = (this.From - this.To).Length;
                drawingContext.DrawTextAtCenter(length.ToString("F2"), this.Center, pen.Brush, 10.0 / view.Scale);
            }
        }

        public double Angle => this.CalculateAngle(this.From.X, this.From.Y, this.To.X, this.To.Y);

        public Point Center => this.From + (this.To - this.From) / 2;

        double CalculateAngle(double x1, double y1, double x2, double y2)
        {
            double deltaY = y2 - y1;
            double deltaX = x2 - x1;
            double angleInRadians = Math.Atan2(deltaY, deltaX);
            double angleInDegrees = angleInRadians * (180 / Math.PI);
            if (angleInDegrees < 0)
                angleInDegrees += 360;
            return angleInDegrees;
        }

        public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
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
}
