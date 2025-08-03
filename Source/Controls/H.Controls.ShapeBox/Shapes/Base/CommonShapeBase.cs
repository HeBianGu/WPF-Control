// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Drawings;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes.Base
{
    public abstract class CommonShapeBase : MatrixShapeBase
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
            double angle = this.CalculateAngle(from.X, from.Y, to.X, to.Y);
            this.DrawCross(view, dc, from, pen, angle + 45);
            this.DrawCross(view, dc, to, pen, angle + 45);
            double length = (from - to).Length;
            var center = from + (to - from) / 2;
            dc.DrawTextAtCenter(length.ToString("F2"), center, pen.Brush, 15.0 / view.Scale);
        }

        protected double CalculateAngle(double x1, double y1, double x2, double y2)
        {
            double deltaY = y2 - y1;
            double deltaX = x2 - x1;
            double angleInRadians = Math.Atan2(deltaY, deltaX);
            double angleInDegrees = angleInRadians * (180 / Math.PI);
            if (angleInDegrees < 0)
                angleInDegrees += 360;
            return angleInDegrees;
        }
    }

}
