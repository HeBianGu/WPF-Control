// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using H.Controls.ShapeBox.Drawings;
using H.Controls.ShapeBox.Shapes.Base;
using H.Extensions.Common;
using H.Extensions.TypeConverter;

namespace H.Controls.ShapeBox.Shapes
{
    public class RectShape : TitleShapeBase
    {
        public RectShape()
        {

        }
        public RectShape(Rect rect)
        {
            this.Rect = rect;
        }
        [TypeConverter(typeof(Round2RectConverter))]
        [Display(Name = "矩形范围", GroupName = "数据")]
        public Rect Rect { get; set; }
        public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            if (this.Rect.IsEmpty)
                return;
            drawingContext.DrawRectangle(fill, pen, Rect);
            this.DrawTitle(view, drawingContext, this.Rect.TopLeft, pen.Brush, 10 / view.Scale, 10 / view.Scale);
            base.MatrixDrawing(view, drawingContext, pen, fill);
        }

        public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
        {
            var r = 10.0 / view.Scale;
            var rect = new Rect(new Point(0, 0), new Point(r * 2, r));
            drawingContext.DrawRectangle(null, new Pen(stroke, strokeThickness), rect);
            this.DrawPoint(view, drawingContext, rect.TopLeft, fill, strokeThickness);
            this.DrawPoint(view, drawingContext, rect.BottomRight, fill, strokeThickness);
        }

        public override void DrawTitle(IView view, DrawingContext drawingContext, Point point, Brush brush, double fontsize = 10, double offset = 5)
        {
            if (string.IsNullOrEmpty(this.Title))
                return;
            drawingContext.DrawTextAtTopLeft(this.Title, point, brush, fontsize);
        }

        protected override IEnumerable<IHandle> CreateHandles()
        {
            //Matrix matrix = this.GetInvertMatrix();
            //var normalToPoint = matrix.Transform(this.To);
            //var offsetPoint = normalToPoint - new Vector(0, this.Offset);
            yield return new ActionCircleHandle(x =>
            {
                Vector vector = x - this.Rect.GetCenter();
                this.Rect = new Rect(this.Rect.TopLeft + vector, this.Rect.BottomRight + vector);
            }, this.Rect.GetCenter());
            yield return new ActionHandle(x => this.Rect = new Rect(x, this.Rect.BottomRight), this.Rect.TopLeft);
            yield return new ActionHandle(x => this.Rect = new Rect(this.Rect.TopLeft, x), this.Rect.BottomRight);
        }
    }
}
