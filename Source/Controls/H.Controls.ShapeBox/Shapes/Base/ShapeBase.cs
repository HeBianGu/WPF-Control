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
        public double StrokeThickness { get; set; }
        public Brush fill { get; set; }
        public abstract void Draw(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null);
    }


    public abstract class CommonShapeBase : ShapeBase
    {
        public void DrawCross(IView view, DrawingContext drawingContext, Point point, Brush stroke, double strokeThickness = 1, double angle = 0)
        {
            drawingContext.DrawCross(point, stroke, strokeThickness, strokeThickness * 4.0, strokeThickness * 4.0, angle);
        }

        public void DrawPoint(IView view, DrawingContext drawingContext, Point point, Brush fill, double radius = 1)
        {
            drawingContext.DrawEllipse(fill, null, point, radius, radius);
        }

        public void DrawDimensionLine(IView view, DrawingContext dc, Point from, Point to, Brush stroke, double strokeThickness = 1.0)
        {
            if (from == to)
                return;
            var p = new Pen(stroke, strokeThickness);
            dc.DrawLine(p, from, to);
            this.DrawCross(view, dc, from, stroke, strokeThickness);
            this.DrawCross(view, dc, to, stroke, strokeThickness);
            double length = (from - to).Length;
            var center = from + (to - from) / 2;
            dc.DrawTextAtCenter(length.ToString("F2"), center, stroke, 15.0 / view.Scale);
        }
    }

}
