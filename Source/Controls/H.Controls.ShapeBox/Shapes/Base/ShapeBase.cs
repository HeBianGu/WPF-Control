// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Windows.Ink;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes.Base
{
    public abstract class ShapeBase : IShape
    {
        [JsonIgnore]
        [DefaultValue(null)]
        [Display(Name = "线条颜色",GroupName ="样式")]
        public Brush Stroke { get; set; }
        [JsonIgnore]
        [DefaultValue(-1)]
        [Display(Name = "线条粗细", GroupName = "样式")]
        public double StrokeThickness { get; set; } = -1;

        [JsonIgnore]
        [DefaultValue(null)]
        [Display(Name = "填充色", GroupName = "样式")]
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

}
