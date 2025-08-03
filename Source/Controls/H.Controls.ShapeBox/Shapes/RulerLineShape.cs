// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Shapes.Base;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes
{
    public class RulerLineShape : LineShape
    {
        public RulerLineShape()
        {
            this.UseHandle = true;
            this.UseCross = false;
        }
        public RulerLineShape(Point start, Point end) : this()
        {
            this.From = start;
            this.To = end;
        }
        public int MajorRularCount { get; set; } = 10;
        public int MinorRularCount { get; set; } = 5;
        public double MajorOffset { get; set; } = 20.0;

        public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            if (this.From == this.To)
                return;
            Matrix matrix = this.GetInvertMatrix();
            var normalToPoint = matrix.Transform(this.To);
            int majorRularCount = this.MajorRularCount;
            int minorRularCount = this.MinorRularCount * this.MajorRularCount;
            var linePen = new Pen(Brushes.LightBlue, pen.Thickness / 2);
            var offsetVector = new Vector(0, this.MajorOffset / view.Scale);
            var majorstep = (normalToPoint - this.From) / majorRularCount;
            var minorstep = (normalToPoint - this.From) / minorRularCount;

            for (int i = 0; i < minorRularCount; i++)
            {
                if (i % majorRularCount == 0)
                    continue;
                var top = this.From + minorstep * i + offsetVector / 2;
                var bottom = this.From + minorstep * i;
                drawingContext.DrawLine(linePen, top, bottom);
            }
            for (int i = 0; i <= majorRularCount; i++)
            {
                var top = this.From + majorstep * i + offsetVector;
                var bottom = this.From + majorstep * i;
                drawingContext.DrawLine(linePen, top, bottom);
            }
            base.MatrixDrawing(view, drawingContext, pen, fill);
        }
    }
}
