// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Drawings;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes.Base
{
    public abstract class PointsShapeBase : TitleShapeBase
    {
        protected PointsShapeBase()
        {

        }
        protected PointsShapeBase(IEnumerable<Point> points)
        {
            PointCollection ps = new PointCollection(points);
            ps.Freeze();
            this.Points = ps;
        }
        [Display(Name = "坐标列表", GroupName = "数据", Order = -1)]
        public PointCollection Points { get; set; }
        [Display(Name = "启用交线", GroupName = "样式")]
        public bool UseCross { get; set; } = false;

        public Rect BoundingBox
        {
            get
            {
                var minx = this.Points.Min(x => x.X);
                var miny = this.Points.Min(x => x.Y);
                return new Rect(minx, miny, this.Points.Max(x => x.X) - minx, this.Points.Max(x => x.Y) - miny);
            }
        }

        public virtual void DrawingCross(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            if (this.Points == null || this.Points.Count == 0)
                return;
            foreach (var item in this.Points)
            {
                this.DrawCross(view, drawingContext, item, pen);
            }
        }

        public override void MatrixDrawing(IView view, DrawingContext dc, Pen pen, Brush fill = null)
        {
            if (this.UseCross)
                this.DrawingCross(view, dc, pen, fill);
            base.MatrixDrawing(view, dc, pen, fill);
        }

        //protected override IEnumerable<IHandle> CreateHandles()
        //{
        //    Matrix matrix = this.GetInvertMatrix();
        //    var normalToPoint = matrix.Transform(this.To);
        //    yield return new ActionHandle(x => this.From = x, this.From);
        //    yield return new ActionHandle(x => this.To = x, normalToPoint);
        //}
    }
}
