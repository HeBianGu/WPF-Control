// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Runtime.CompilerServices;
using System.Windows.Ink;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace H.Controls.ShapeBox.Drawings
{
    public static class DrawingContextExtension
    {
        public static void DrawHandle(this DrawingContext dc, Point point, Brush stroke, double strokeThickness = 1, double len = 6)
        {
            dc.DrawHandle(point, new Pen(stroke, strokeThickness), len);
        }

        public static void DrawHandle(this DrawingContext dc, Point point, Pen pen, double len = 6)
        {
            double s = len / 2;
            //dc.DrawEllipse(null, pen, point, len, len);
            dc.DrawRectangle(Brushes.White, pen, new Rect(point.X - s, point.Y - s, len, len));

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

        public static void DrawCircle(this DrawingContext dc, Point point, Pen pen, double radius, Brush fill = null)
        {
            dc.DrawEllipse(fill, pen, point, radius, radius);

        }

        public static void DrawPoints(this DrawingContext dc, Pen pen, Brush fill = null, bool isFilled = true, bool isClosed = true, params Point[] points)
        {
            var gemetry = points.ToGeometry(isFilled, isClosed);
            dc.DrawGeometry(fill, pen, gemetry);
        }

        public static void DrawPloyLine(this DrawingContext dc, Pen pen, params Point[] points)
        {
            dc.DrawPoints(pen, null, false, false, points);
        }

        public static void DrawPloygon(this DrawingContext dc, Pen pen, Brush fill = null, params Point[] points)
        {
            dc.DrawPoints(pen, fill, true, true, points);
        }

        public static Rect DrawTextAt(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, double offset = 5)
        {
            return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(offset, offset));
        }

        public static Rect DrawTextAtCenter(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0)
        {
            return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(-x.Width / 2, -x.Height / 2));
        }

        public static Rect DrawTextAtTopCenter(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0)
        {
            return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(-x.Width / 2, -x.Height));
        }

        public static Rect DrawTextAtTopLeft(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0)
        {
            return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(0, -x.Height));
        }

        public static Rect DrawTextAt(this DrawingContext dc, string text, Point point, Brush brush, double fontSize, Func<FormattedText, Vector> getOffset)
        {
            fontSize = fontSize.ToFontSize();
            FormattedText formattedText = text.ToForematedText(brush, fontSize);
            var p = point + getOffset(formattedText);
            dc.DrawText(formattedText, p);
            return new Rect(p, new Size(formattedText.Width, formattedText.Height));
        }

        public static Rect DrawTextAtBottomCenter(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0)
        {
            return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(x.Width / 2, x.Height));
        }

        public static Rect DrawTextAtRight(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0)
        {
            return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(0, -x.Height / 2));
        }

        public static Rect DrawTextAtLeft(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0)
        {
            return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(x.Width, -x.Height / 2));
        }

        private static FormattedText ToForematedText(this string text, Brush brush, double fontSize = 10.0)
        {
            return new FormattedText(
                                      $"{text}",
                                      System.Globalization.CultureInfo.CurrentCulture,
                                      FlowDirection.LeftToRight,
                                      new Typeface("Microsoft YaHei"),
                                      fontSize, brush, 1.2);
        }

        private static double ToFontSize(this double fontSize)
        {
            return double.IsNaN(fontSize) ? 10 : fontSize;
        }
    }
}
