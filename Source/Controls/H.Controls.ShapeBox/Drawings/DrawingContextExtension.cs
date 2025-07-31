// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;

namespace H.Controls.ShapeBox.Drawings
{
    public static class DrawingContextExtension
    {
        public static void DrawHandle(this DrawingContext dc, Point point, Brush stroke, double strokeThickness = 1, double len = 6)
        {
            double s = len / 2;
            dc.DrawEllipse(null, new Pen(stroke, strokeThickness), point, len, len);
        }

        public static void DrawCross(this DrawingContext dc, Point point, Brush stroke, double strokeThickness = 1, double wlen = 6, double hlen = 6, double angle = 0)
        {
            RotateTransform rotateTransform = new RotateTransform(angle, point.X, point.Y);
            dc.PushTransform(rotateTransform);
            var hv = new Vector(wlen, 0);
            var vv = new Vector(0, hlen);
            dc.DrawLine(new Pen(stroke, strokeThickness), point - vv, point + vv);
            dc.DrawLine(new Pen(stroke, strokeThickness), point - hv, point + hv);
            dc.Pop();
        }

        public static void DrawText(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, double offset = 5)
        {
            FormattedText formattedText = new FormattedText(
                                    $"{text}",
                                    System.Globalization.CultureInfo.CurrentCulture,
                                    FlowDirection.LeftToRight,
                                    new Typeface("Microsoft YaHei"),
                                    fontSize, brush, 1.2);
            dc.DrawText(formattedText, point + new Vector(offset, offset));
        }

        public static void DrawTextAtCenter(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0)
        {
            FormattedText formattedText = new FormattedText(
                                    $"{text}",
                                    System.Globalization.CultureInfo.CurrentCulture,
                                    FlowDirection.LeftToRight,
                                    new Typeface("Microsoft YaHei"),
                                    fontSize, brush, 1.2);
            dc.DrawText(formattedText, point - new Vector(formattedText.Width / 2, formattedText.Height / 2));
        }
    }
}
