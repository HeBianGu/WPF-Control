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
    public class ROIRectShape : PreviewShapeBase
    {
        public ROIRectShape()
        {
            this.Fill = new SolidColorBrush(Colors.Black) { Opacity = 0.2 };
            this.Fill.Freeze();
        }
        [TypeConverter(typeof(Round2RectConverter))]
        [Display(Name = "ROI范围", GroupName = "数据")]
        public Rect Rect { get; set; }

        [Display(Name = "句柄长度", GroupName = "样式")]
        public double HandleLength { get; set; } = 6.0;

        [Display(Name = "启用交线", GroupName = "样式")]
        public bool UseCross { get; set; } = true;

        [Display(Name = "启用文本", GroupName = "样式")]
        public bool UseText { get; set; } = true;

        public override void MatrixDrawing(IView view, DrawingContext dc, Pen pen, Brush fill = null)
        {
            if (this.Rect.IsEmpty)
                return;
            var boundingBox = new Rect(0, 0, view.Size.Width, view.Size.Height);
            var combine = new CombinedGeometry(GeometryCombineMode.Exclude, new RectangleGeometry(boundingBox), new RectangleGeometry(this.Rect));
            dc.PushClip(combine);
            dc.DrawRoundedRectangle(fill, null, boundingBox, pen.Thickness, pen.Thickness);
            dc.Pop();
            dc.DrawRoundedRectangle(null, pen, this.Rect, pen.Thickness, pen.Thickness);

            var center = new Point(this.Rect.Left + this.Rect.Width / 2, this.Rect.Top + this.Rect.Height / 2);

            if (this.UseCross)
            {
                DashStyle dashStyle = new DashStyle();
                dashStyle.Dashes = new DoubleCollection(new double[] { pen.Thickness * 1, pen.Thickness * 1 });
                var dashpen = new Pen(pen.Brush, pen.Thickness / 2) { DashStyle = dashStyle };
                //dc.DrawLine(dashpen, new Point(0, center.Y), new Point(this._box.BoundingBox.Right, center.Y));
                //dc.DrawLine(dashpen, new Point(center.X, 0), new Point(center.X, this._box.BoundingBox.Bottom));
                dc.DrawLine(dashpen, new Point(0, center.Y), new Point(this.Rect.Left, center.Y));
                dc.DrawLine(dashpen, new Point(this.Rect.Right, center.Y), new Point(boundingBox.Right, center.Y));
                dc.DrawLine(dashpen, new Point(center.X, 0), new Point(center.X, this.Rect.Top));
                dc.DrawLine(dashpen, new Point(center.X, this.Rect.Bottom), new Point(center.X, boundingBox.Bottom));
            }

            double hanldeLength = this.HandleLength / view.Scale;
            double hthickness = hanldeLength / 2;
            dc.DrawHandle(this.Rect.TopLeft, pen.Brush, hthickness, hanldeLength);
            dc.DrawHandle(this.Rect.BottomRight, pen.Brush, hthickness, hanldeLength);
            dc.DrawHandle(this.Rect.TopRight, pen.Brush, hthickness, hanldeLength);
            dc.DrawHandle(this.Rect.BottomLeft, pen.Brush, hthickness, hanldeLength);

            dc.DrawHandle(new Point(this.Rect.Left, center.Y), pen.Brush, hthickness, hanldeLength);
            dc.DrawHandle(new Point(this.Rect.Right, center.Y), pen.Brush, hthickness, hanldeLength);
            dc.DrawHandle(new Point(center.X, this.Rect.Top), pen.Brush, hthickness, hanldeLength);
            dc.DrawHandle(new Point(center.X, this.Rect.Bottom), pen.Brush, hthickness, hanldeLength);

            if (this.UseText)
            {
                var txt = $"ROI x:{(int)this.Rect.Left} y:{(int)this.Rect.Top} w:{(int)this.Rect.Width} h:{(int)this.Rect.Height}";
                dc.DrawTextAt(txt, this.Rect.BottomLeft, pen.Brush, 15.0 / view.Scale);
            }
            base.MatrixDrawing(view, dc, pen, fill);
        }

        public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
        {
            double length = 20.0 / view.Scale;
            var rect = new Rect(0, 0, length, length / 2);
            drawingContext.DrawRectangle(null, new Pen(stroke, strokeThickness), rect);
            this.DrawPoint(view, drawingContext, rect.TopLeft, fill, strokeThickness * 2);
            this.DrawPoint(view, drawingContext, rect.BottomRight, fill, strokeThickness * 2);
        }
    }
}
