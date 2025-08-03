// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Text;
using System.Windows.Ink;
using System.Windows.Media;
using H.Controls.ShapeBox.Drawings;
using H.Controls.ShapeBox.Shapes.Base;
using H.Extensions.Common;

namespace H.Controls.ShapeBox.Shapes
{
    public class PixelColorPointShape : TitleShapeBase
    {
        public Point? Point { get; set; }
        public Color Color { get; set; }
        public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            if (this.Point == null)
                return;
            this.DrawPoint(view, drawingContext, this.Point.Value, fill);
            drawingContext.DrawCircle(this.Point.Value, pen, 5 / view.Scale);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Pixel:{(int)this.Point.Value.X},{(int)this.Point.Value.Y}");
            stringBuilder.AppendLine("HEX:" + this.Color.ToString());
            stringBuilder.AppendLine($"R:{this.Color.R} G:{this.Color.G} B:{this.Color.B} A:{this.Color.A}");
            var rect = drawingContext.DrawTextAt(stringBuilder.ToString(), this.Point.Value, pen.Brush, 15 / view.Scale, 5 / view.Scale);
            drawingContext.DrawRoundedRectangle(new SolidColorBrush(this.Color), pen, new Rect(rect.Left, rect.Bottom, 20 / view.Scale, 10 / view.Scale), 1 / view.Scale, 1 / view.Scale);
            base.MatrixDrawing(view, drawingContext, pen, fill);
        }

        public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
        {
            if (this.Point == null)
                return;
            drawingContext.DrawCircle(new Point(-offset, -offset), new Pen(stroke, strokeThickness), 5 / view.Scale);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Pixel Color");
            stringBuilder.AppendLine($"Pixel:{(int)this.Point.Value.X},{(int)this.Point.Value.Y}");
            stringBuilder.AppendLine("HEX:" + this.Color.ToString());
            stringBuilder.AppendLine($"R:{this.Color.R} G:{this.Color.G} B:{this.Color.B} A:{this.Color.A}");
            var rect = drawingContext.DrawTextAt(stringBuilder.ToString(), new Point(0, 0), stroke, 15 / view.Scale, 5 / view.Scale);
            drawingContext.DrawRoundedRectangle(new SolidColorBrush(this.Color), new Pen(stroke, strokeThickness), new Rect(rect.Left, rect.Bottom, 20 / view.Scale, 10 / view.Scale), 1 / view.Scale, 1 / view.Scale);
        }
    }
}
