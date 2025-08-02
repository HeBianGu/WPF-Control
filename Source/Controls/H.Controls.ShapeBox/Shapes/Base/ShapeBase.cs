// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Drawings;
using System.Windows.Ink;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes.Base
{
    public abstract class ShapeBase : IShape
    {
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; } = -1;
        public Brush Fill { get; set; }
        public virtual void Draw(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
        {
            strokeThickness = this.GetStrokeThickness(strokeThickness, view);
            stroke = this.GetStroke(stroke);
            fill = this.GetFill(fill);
            var pen = new Pen(stroke, strokeThickness);
            this.Drawing(view, drawingContext, pen, fill);
        }

        protected double GetStrokeThickness(double strokeThickness, IView view)
        {
            if (this.StrokeThickness < 0)
                return strokeThickness;
            return this.StrokeThickness / view.Scale;
        }

        protected Brush GetStroke(Brush stroke) => this.Stroke ?? stroke;

        protected Brush GetFill(Brush fill) => this.Fill ?? fill;

        protected Pen GetPen(Brush stroke, double strokeThickness, IView view)
        {
            var s = this.GetStroke(stroke);
            var t = this.GetStrokeThickness(strokeThickness, view);
            return new Pen(s, t);
        }

        public abstract void Drawing(IView view, DrawingContext dc, Pen pen, Brush fill = null);
    }


    public abstract class CommonShapeBase : ShapeBase
    {
        public void DrawCross(IView view, DrawingContext drawingContext, Point point, Pen pen, double angle = 0)
        {
            drawingContext.DrawCross(point, pen.Brush, pen.Thickness, pen.Thickness * 4.0, pen.Thickness * 4.0, angle);
        }

        public void DrawPoint(IView view, DrawingContext drawingContext, Point point, Brush fill, double radius = 1)
        {
            drawingContext.DrawEllipse(fill, null, point, radius, radius);
        }

        public void DrawDimensionLine(IView view, DrawingContext dc, Point from, Point to, Pen pen)
        {
            if (from == to)
                return;
            dc.DrawLine(pen, from, to);
            this.DrawCross(view, dc, from, pen);
            this.DrawCross(view, dc, to, pen);
            double length = (from - to).Length;
            var center = from + (to - from) / 2;
            dc.DrawTextAtCenter(length.ToString("F2"), center, pen.Brush, 15.0 / view.Scale);
        }
    }

}
