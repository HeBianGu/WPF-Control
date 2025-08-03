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
    public interface IFromToShape : IShape
    {
        Point From { get; set; }
        Point To { get; set; }
    }

    public class LineShape : TitleShapeBase, IFromToShape
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
        public bool UseText { get; set; } = false;

        public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            if (this.From == this.To)
                return;
            Matrix matrix = this.GetInvertMatrix();
            var normalToPoint = matrix.Transform(this.To);
            drawingContext.DrawLine(pen, this.From, normalToPoint);
            if (this.UseCross)
            {
                this.DrawCross(view, drawingContext, this.From, pen, 45);
                this.DrawCross(view, drawingContext, normalToPoint, pen, 45);
            }
            var normalToCenter = matrix.Transform(this.Center);
            if (this.UseText)
            {
                double length = (this.From - normalToPoint).Length;
                drawingContext.DrawTextAtTopCenter(length.ToString("F2"), normalToCenter, pen.Brush, 15.0 / view.Scale);
            }

            this.DrawTitle(view, drawingContext, normalToCenter, pen.Brush, 15.0 / view.Scale);
            base.MatrixDrawing(view, drawingContext, pen, fill);
        }

        public override void DrawTitle(IView view, DrawingContext drawingContext, Point point, Brush brush, double fontsize = 10.0)
        {
            if (string.IsNullOrEmpty(this.Title))
                return;
            drawingContext.DrawTextAtTopCenter(this.Title, point, brush, fontsize);
        }

        public double Angle => this.CalculateAngle(this.From.X, this.From.Y, this.To.X, this.To.Y);

        public double Length => (this.From - this.To).Length;

        public Point Center => this.From + (this.To - this.From) / 2;

        protected Matrix GetRotateAtFromMatrix()
        {
            Matrix matrix = new Matrix();
            matrix.RotateAt(-this.Angle, this.From.X, this.From.Y);
            return matrix;
        }

        protected override Matrix GetMatrix()
        {
            Matrix matrix = new Matrix();
            matrix.RotateAt(this.Angle, this.From.X, this.From.Y);
            return matrix;
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

        protected override IEnumerable<IHandle> CreateHandles()
        {
            Matrix matrix = this.GetInvertMatrix();
            var normalToPoint = matrix.Transform(this.To);
            yield return new ActionHandle(x => this.From = x, this.From);
            yield return new ActionHandle(x => this.To = x, normalToPoint);
        }

        //public override IHandle HitIHandle(IView view, Point position)
        //{
        //    Matrix matrix = this.GetRotateAtFromMatrix();
        //    return base.HitIHandle(view, matrix.Transform(position));
        //}
    }
}
