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
    public class LineShape : TitleShapeBase
    {
        public LineShape()
        {

        }
        public LineShape(Point from, Point to)
        {
            this.From = from;
            this.To = to;
        }
        public Point From { get; set; }
        public Point To { get; set; }
        public bool UseCross { get; set; } = true;
        public override void Draw(DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
        {
            if (this.From == this.To)
                return;
            var p = new Pen(stroke, strokeThickness);
            drawingContext.DrawLine(p, this.From, this.To);

            if (this.UseCross)
            {
                var hv = new Vector(strokeThickness * 2, 0);
                drawingContext.DrawLine(p, this.From - hv, this.From + hv);
                drawingContext.DrawLine(p, this.To - hv, this.To + hv);
                var vv = new Vector(0, strokeThickness * 2);
                drawingContext.DrawLine(p, this.From - vv, this.From + vv);
                drawingContext.DrawLine(p, this.To - vv, this.To + vv);
            }
        }
    }
}
