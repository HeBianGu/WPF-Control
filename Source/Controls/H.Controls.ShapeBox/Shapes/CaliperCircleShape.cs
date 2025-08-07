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

        [Display(Name = "中心坐标", GroupName = "数据")]
        public Point Center { get; set; }
        [Display(Name = "起始半径", GroupName = "数据")]
        public double FromRadius { get; set; }
        [Display(Name = "终止半径", GroupName = "数据")]
        public double ToRadius { get; set; }
        [Display(Name = "卡尺数量", GroupName = "数据")]
        public int CaliperCount { get; set; } = 36;
        [Display(Name = "卡尺颜色", GroupName = "数据")]
        public Brush MinorStroke { get; set; } = Brushes.LightBlue;

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

            var linePen = new Pen(this.MinorStroke, pen.Thickness / 2);
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

        public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
        {
            var r = 10.0 / view.Scale;
            var c = new Point(r, r);
            drawingContext.DrawEllipse(null, new Pen(stroke, strokeThickness), c, r, r);
            this.DrawPoint(view, drawingContext, c, fill, strokeThickness);
            this.DrawPoint(view, drawingContext, c + new Vector(r, 0), fill, strokeThickness);
        }

        public bool Contains(Point point, double radius)
        {
            Circle outcirle = new Circle(this.Center.X, this.Center.Y, this.ToRadius);
            Circle innercirle = new Circle(this.Center.X, this.Center.Y, this.FromRadius - (this.ToRadius - this.FromRadius));
            Circle incircle = new Circle(point.X, point.Y, radius);
            return outcirle.Contains(incircle) && incircle.Contains(innercirle);
        }
    }
}
