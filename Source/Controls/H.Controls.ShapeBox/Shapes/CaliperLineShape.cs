// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Drawings;
using H.Controls.ShapeBox.Geometrys;
using H.Controls.ShapeBox.Shapes.Base;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes
{
    public class CaliperLineShape : LineShape
    {
        public CaliperLineShape()
        {
            this.UseHandle = true;
            this.UseCross = false;
        }
        public CaliperLineShape(Point start, Point end) : this()
        {
            this.From = start;
            this.To = end;
        }

        [Display(Name = "卡尺数量", GroupName = "数据")]
        public int CaliperCount { get; set; }
        [Display(Name = "偏移位置", GroupName = "数据")]
        public double Offset { get; set; } = 10.0;
        [Display(Name = "卡尺颜色", GroupName = "数据")]
        public Brush MinorStroke { get; set; } = Brushes.LightBlue;

        public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            if (this.From == this.To)
                return;
            Matrix matrix = this.GetInvertMatrix();
            var normalToPoint = matrix.Transform(this.To);
            int capliperCount = this.CaliperCount > 0 ? this.CaliperCount : 20;
            var linePen = new Pen(this.MinorStroke, pen.Thickness / 2);
            var offsetVector = new Vector(0, this.Offset);
            var step = (normalToPoint - this.From) / capliperCount;
            double w = step.Length;
            Rect rect = new Rect(this.From - offsetVector, normalToPoint + offsetVector);
            drawingContext.DrawRectangle(null, linePen, rect);
            for (int i = 0; i < capliperCount; i++)
            {
                var top = this.From + step * i - offsetVector;
                var bottom = this.From + step * i + offsetVector;
                drawingContext.DrawLine(linePen, top, bottom);
            }
            base.MatrixDrawing(view, drawingContext, pen, fill);
        }

        protected override IEnumerable<IHandle> CreateHandles()
        {
            Matrix matrix = this.GetInvertMatrix();
            var normalToPoint = matrix.Transform(this.To);
            var offsetPoint = normalToPoint - new Vector(0, this.Offset);
            yield return new ActionHandle(x => this.From = x, this.From);
            yield return new ActionHandle(x => this.To = x, normalToPoint);
            yield return new ActionHandle(x =>
            {
                var normalOffsetTo = matrix.Transform(x);
                this.Offset = (this.To - x).Length;
            }, offsetPoint);
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

        public bool Contains(Point from, Point to)
        {
            return this.Contains(from) && this.Contains(to);
        }

        public bool Contains(Point from)
        {
            Matrix matrix = this.GetInvertMatrix();
            from = matrix.Transform(from);
            var v = new Vector(0, this.Offset);
            var normalToPoint = matrix.Transform(this.To);
            Rect rect = new Rect(this.From - v, normalToPoint + v);
            return rect.Contains(from);
        }
    }
}
