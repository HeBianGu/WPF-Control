// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Media;

namespace H.Controls.ShapeBox.Drawings;
public enum PointStyleStype
{
    Cross,
    Arrow,
    Circle,
    None
}

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
        dc.DrawPointStyleStype(PointStyleStype.Cross, point, stroke, strokeThickness, wlen, hlen, angle);
    }

    public static void DrawArrow(this DrawingContext dc, Point point, Brush stroke, double strokeThickness = 1, double wlen = 6, double hlen = 6, double angle = 0)
    {
        dc.DrawPointStyleStype(PointStyleStype.Arrow, point, stroke, strokeThickness, wlen, hlen, angle);
    }
    public static void DrawArrowPolygon(this DrawingContext dc, Point from, Point to, Brush stroke, Brush fill, double strokeThickness = 1, double len = 6, double arrowAngle = 30)
    {
        var pen = new Pen(stroke, strokeThickness)
        {
            LineJoin = PenLineJoin.Round,
            StartLineCap = PenLineCap.Round,
            EndLineCap = PenLineCap.Round,
        };
        if (pen.CanFreeze)
            pen.Freeze();

        dc.DrawLine(pen, from, to);

        Vector vector = from - to;
        if (vector.Length <= double.Epsilon)
            return;

        vector.Normalize();
        Vector v1 = vector * len;
        Vector v2 = vector * len;

        RotateTransform rt1 = new RotateTransform(20);
        RotateTransform rt2 = new RotateTransform(-20);

        v1 = rt1.Value.Transform(v1);
        v2 = rt2.Value.Transform(v2);

        dc.DrawPloygon(pen, fill,
            to,
            to + v1,
            to + v2);

    }


    public static void DrawArrowPolyLine(this DrawingContext dc, Point from, Point to, Brush stroke, double strokeThickness = 1, double len = 6, double arrowAngle = 30)
    {
        var pen = new Pen(stroke, strokeThickness)
        {
            LineJoin = PenLineJoin.Round,
            StartLineCap = PenLineCap.Round,
            EndLineCap = PenLineCap.Round,
        };
        if (pen.CanFreeze)
            pen.Freeze();
        dc.DrawLine(pen, from, to);

        Vector vector = from - to;
        double angle = Math.Atan2(vector.Y, vector.X) * 180 / Math.PI;

        RotateTransform rotateTransform = new RotateTransform(angle, to.X, to.Y);
        dc.PushTransform(rotateTransform);

        Vector v1 = new Vector(len, 0);
        Vector v2 = new Vector(len, 0);

        RotateTransform rt1 = new RotateTransform(arrowAngle);
        RotateTransform rt2 = new RotateTransform(-arrowAngle);

        v1 = rt1.Value.Transform(v1);
        v2 = rt2.Value.Transform(v2);

        dc.DrawLine(pen, to, to + v1);
        dc.DrawLine(pen, to, to + v2);

        dc.Pop();
    }

    public static void DrawPointStyleStype(this DrawingContext dc, PointStyleStype pointStyleStype, Point point, Brush stroke, double strokeThickness = 1, double wlen = 6, double hlen = 6, double angle = 0)
    {
        RotateTransform rotateTransform = new RotateTransform(angle, point.X, point.Y);
        dc.PushTransform(rotateTransform);
        var pen = new Pen(stroke, strokeThickness) { StartLineCap = PenLineCap.Round, EndLineCap = PenLineCap.Round, LineJoin = PenLineJoin.Round };
        if (pointStyleStype == PointStyleStype.Cross)
        {
            var hv = new Vector(wlen, 0);
            var vv = new Vector(0, hlen);
            dc.DrawLine(pen, point - vv, point + vv);
            dc.DrawLine(pen, point - hv, point + hv);
        }
        else if (pointStyleStype == PointStyleStype.Arrow)
        {
            var hv = new Vector(wlen, -wlen * 0.7);
            var vv = new Vector(-hlen * 0.7, hlen);
            //dc.DrawLine(new Pen(stroke, strokeThickness), point - vv, point);
            //dc.DrawLine(new Pen(stroke, strokeThickness), point, point + hv);
            dc.DrawPloygon(pen, stroke,
                point + hv,
                point,
                point - vv);
        }
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

    public static Rect DrawTextAt(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, double offset = 5, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(offset, offset), fill, beforeAction);
    }

    public static Rect DrawTextAtCenter(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(-x.Width / 2, -x.Height / 2), fill, beforeAction);
    }

    public static Rect DrawTextAtTopCenter(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(-x.Width / 2, -x.Height), fill, beforeAction);
    }

    public static Rect DrawTextAtTopRight(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(0, -x.Height), fill, beforeAction);
    }

    public static Rect DrawTextAtTopLeft(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(0, -x.Height), fill, beforeAction);
    }

    public static Rect DrawTextAt(this DrawingContext dc, string text, Point point, Brush brush, double fontSize, Func<FormattedText, Vector> getOffset, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        fontSize = fontSize.ToFontSize();
        FormattedText formattedText = text.ToForematedText(brush, fontSize);
        var p = point + getOffset(formattedText);
        if (fill != null)
        {
            var hgeo = formattedText.BuildHighlightGeometry(p);
            dc.DrawGeometry(fill, null, hgeo);
        }
        var result = new Rect(p, new Size(formattedText.Width, formattedText.Height));
        beforeAction?.Invoke(formattedText, result);
        dc.DrawText(formattedText, p);
        return result;
    }

    public static Rect DrawTextAtBottomCenter(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(-x.Width / 2, 0), fill, beforeAction);
    }

    public static Rect DrawTextAtBottomRight(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(0, 0), fill, beforeAction);
    }

    public static Rect DrawTextAtBottomLeft(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(-x.Width, 0), fill, beforeAction);
    }


    public static Rect DrawTextAtRight(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(0, -x.Height / 2), fill, beforeAction);
    }

    public static Rect DrawTextAtLeft(this DrawingContext dc, string text, Point point, Brush brush, double fontSize = 10.0, Brush fill = null, Action<FormattedText, Rect> beforeAction = null)
    {
        return dc.DrawTextAt(text, point, brush, fontSize, x => new Vector(-x.Width, -x.Height / 2), fill, beforeAction);
    }

    private static FormattedText ToForematedText(this string text, Brush brush, double fontSize = 10.0)
    {
        if (fontSize <= 0)
            fontSize = 1;
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
