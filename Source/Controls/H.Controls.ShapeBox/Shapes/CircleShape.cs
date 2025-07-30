// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;
using H.Controls.ShapeBox.Shapes.Base;

namespace H.Controls.ShapeBox.Shapes
{
    public class CircleShape : TitleShapeBase
    {
        public CircleShape(Point center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }
        public Point Center { get; set; }

        public double Radius { get; set; }
        public override void Draw(DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
        {
            if (this.Radius < 0)
                return;
            drawingContext.DrawEllipse(fill, new Pen(stroke, strokeThickness), this.Center, this.Radius, this.Radius);
        }
    }
}
