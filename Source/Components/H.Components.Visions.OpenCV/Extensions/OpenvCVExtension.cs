// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Windows.Media;

namespace H.Components.Visions.OpenCV.Extensions;
public static class OpenvCVExtension
{
    public static string ToDetectSuccessMessage(this int count)
    {
        return $"识别目标数量:{count} 个";
    }
    public static System.Windows.Rect ToWindowRect(this Rect2d rect)
    {
        return new System.Windows.Rect(rect.Left, rect.Top, rect.Width, rect.Height);
    }

    public static System.Windows.Rect ToWindowRect(this OpenCvSharp.Rect rect)
    {
        return new System.Windows.Rect(rect.Left, rect.Top, rect.Width, rect.Height);
    }

    public static OpenCvSharp.Point[] ToPoints(this OpenCvSharp.Rect rect)
    {
      IEnumerable<OpenCvSharp.Point> ToPoints()
        {
            yield return new OpenCvSharp.Point(rect.Left, rect.Top);
            yield return new OpenCvSharp.Point(rect.Right, rect.Top);
            yield return new OpenCvSharp.Point(rect.Right, rect.Bottom);
            yield return new OpenCvSharp.Point(rect.Left, rect.Bottom);
        }
         return ToPoints().ToArray();
    }

    public static OpenCvSharp.Point2f[] ToPoints(this OpenCvSharp.RotatedRect rect)
    {
        return Cv2.BoxPoints(rect);
    }


    public static OpenCvSharp.Rect ToCVRect(this System.Windows.Rect rect)
    {
        return new OpenCvSharp.Rect((int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height);
    }

    public static Rect2f ToCVRect2f(this System.Windows.Rect rect)
    {
        return new Rect2f((float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
    }

    public static System.Windows.Rect ToWindowRect(this KeyPoint keyPoint)
    {
        return new System.Windows.Rect(keyPoint.Pt.X - keyPoint.Size / 2, keyPoint.Pt.Y - keyPoint.Size / 2, keyPoint.Size, keyPoint.Size);
    }

    public static System.Windows.Point[] ToPoints(this OpenCvSharp.Point[] points)
    {
        if (points is null)
            return Array.Empty<System.Windows.Point>();

        System.Windows.Point[] wpfPoints = new System.Windows.Point[points.Length];
        for (int j = 0; j < points.Length; j++)
        {
            OpenCvSharp.Point p = points[j];
            wpfPoints[j] = new System.Windows.Point(p.X, p.Y);
        }
        return wpfPoints;
    }

    public static OpenCvSharp.Point[] ToPoints(this System.Windows.Point[] points)
    {
        if (points is null)
            return Array.Empty<OpenCvSharp.Point>();

        OpenCvSharp.Point[] cvPoints = new OpenCvSharp.Point[points.Length];
        for (int j = 0; j < points.Length; j++)
        {
            System.Windows.Point p = points[j];
            cvPoints[j] = new OpenCvSharp.Point(p.X, p.Y);
        }
        return cvPoints;
    }

    public static System.Windows.Point[][] ToPointss(this OpenCvSharp.Point[][] points)
    {
        if (points is null)
            return Array.Empty<System.Windows.Point[]>();

        System.Windows.Point[][] result = new System.Windows.Point[points.Length][];
        for (int i = 0; i < points.Length; i++)
        {
            OpenCvSharp.Point[] contour = points[i];
            if (contour is null || contour.Length == 0)
            {
                result[i] = Array.Empty<System.Windows.Point>();
                continue;
            }

            System.Windows.Point[] wpfPoints = new System.Windows.Point[contour.Length];
            for (int j = 0; j < contour.Length; j++)
            {
                OpenCvSharp.Point p = contour[j];
                wpfPoints[j] = new System.Windows.Point(p.X, p.Y);
            }

            result[i] = wpfPoints;
        }

        return result;
    }

    public static OpenCvSharp.Point[][] ToPointss(this System.Windows.Point[][] points)
    {
        if (points is null)
            return Array.Empty<OpenCvSharp.Point[]>();

        OpenCvSharp.Point[][] result = new OpenCvSharp.Point[points.Length][];
        for (int i = 0; i < points.Length; i++)
        {
            var contour = points[i];
            if (contour is null || contour.Length == 0)
            {
                result[i] = Array.Empty<OpenCvSharp.Point>();
                continue;
            }

            OpenCvSharp.Point[] wpfPoints = new OpenCvSharp.Point[contour.Length];
            for (int j = 0; j < contour.Length; j++)
            {
                var p = contour[j];
                wpfPoints[j] = new OpenCvSharp.Point(p.X, p.Y);
            }

            result[i] = wpfPoints;
        }

        return result;
    }


    public static System.Windows.Rect ToWindowRect(this IEnumerable<Point2f> points)
    {
        float minX = points.Min(x => x.X);
        float minY = points.Min(x => x.Y);
        float maxX = points.Max(x => x.X);
        float maxY = points.Max(x => x.Y);
        return new System.Windows.Rect(minX, minY, maxX - minX, maxY - minY);
    }

    public static System.Windows.Rect ToWindowRect(this IEnumerable<OpenCvSharp.Point> points)
    {
        int minX = points.Min(x => x.X);
        int minY = points.Min(x => x.Y);
        int maxX = points.Max(x => x.X);
        int maxY = points.Max(x => x.Y);
        return new System.Windows.Rect(minX, minY, maxX - minX, maxY - minY);
    }
    public static Tuple<Scalar, Scalar> GetHSVRange(this Color color, int hRange = 30, int sRange = 20, int vRange = 20)
    {
        GetHSVRange(color, hRange, sRange, vRange, out HSVRange lower, out HSVRange upper);
        Scalar lowerScalar = new Scalar(lower.HueMin / 2, lower.SaturationMin * 2.55, lower.ValueMin * 2.55);
        Scalar upperScalar = new Scalar(upper.HueMax / 2, upper.SaturationMax * 2.55, upper.ValueMax * 2.55);
        return Tuple.Create(lowerScalar, upperScalar);
    }

    private static void GetHSVRange(Color rgbColor, int hRange, int sRange, int vRange,
                                  out HSVRange lower, out HSVRange upper)
    {
        // 将RGB转换为HSV
        double r = rgbColor.R / 255.0;
        double g = rgbColor.G / 255.0;
        double b = rgbColor.B / 255.0;

        double max = Math.Max(r, Math.Max(g, b));
        double min = Math.Min(r, Math.Min(g, b));

        double h = 0;
        double s = 0;
        double v = max;

        double delta = max - min;

        if (max != 0)
            s = delta / max;

        if (delta != 0)
        {
            if (max == r)
                h = (g - b) / delta;
            else
            {
                h = max == g ? 2 + (b - r) / delta : 4 + (r - g) / delta;
            }

            h *= 60;
            if (h < 0) h += 360;
        }

        // 计算范围
        lower = new HSVRange
        {
            HueMin = Math.Max(0, h - hRange),
            HueMax = Math.Min(360, h + hRange),
            SaturationMin = Math.Max(0, s * 100 - sRange),
            SaturationMax = Math.Min(100, s * 100 + sRange),
            ValueMin = Math.Max(0, v * 100 - vRange),
            ValueMax = Math.Min(100, v * 100 + vRange)
        };

        upper = new HSVRange
        {
            HueMin = Math.Max(0, h - hRange / 2),
            HueMax = Math.Min(360, h + hRange / 2),
            SaturationMin = Math.Max(0, s * 100 - sRange / 2),
            SaturationMax = Math.Min(100, s * 100 + sRange / 2),
            ValueMin = Math.Max(0, v * 100 - vRange / 2),
            ValueMax = Math.Min(100, v * 100 + vRange / 2)
        };
    }

    public static VisionLine ToVisionLine(this LineSegmentPoint line)
    {
        return new VisionLine(line.P1.ToPoint(), line.P2.ToPoint());
    }
    public static VisionCircle ToLineShape(this CircleSegment circle)
    {
        return new VisionCircle(circle.Center.ToPoint().ToPoint(), circle.Radius);
    }

    public static OpenCvSharp.Size ToCVSize(this System.Windows.Size size)
    {
        return new OpenCvSharp.Size(size.Width, size.Height);
    }

    public static System.Windows.Size ToSize(this OpenCvSharp.Size size)
    {
        return new System.Windows.Size(size.Width, size.Height);
    }
    public static OpenCvSharp.Point ToCVPoint(this System.Windows.Point size)
    {
        return new OpenCvSharp.Point((int)size.X, (int)size.Y);
    }

    public static System.Windows.Point ToPoint(this OpenCvSharp.Point size)
    {
        return new System.Windows.Point(size.X, size.Y);
    }

    public static OpenCvSharp.Point2f ToPoint2F(this OpenCvSharp.Point size)
    {
        return new OpenCvSharp.Point2f(size.X, size.Y);
    }

    public static OpenCvSharp.Rect ToCVRect(this Int32Rect size)
    {
        return new OpenCvSharp.Rect(size.X, size.Y, size.Width, size.Height);
    }

    public static Int32Rect ToRect(this OpenCvSharp.Rect size)
    {
        return new Int32Rect(size.Left, size.Top, size.Width, size.Height);
    }

    public static OpenCvSharp.Rect ToCVRect(this Rect2f size)
    {
        return new OpenCvSharp.Rect((int)size.Left, (int)size.Top, (int)size.Width, (int)size.Height);
    }

    public static bool IsValid(this Rect2f size)
    {
        return size.X >= 0 && size.Y >= 0 && size.Left >= 0 && size.Top >= 0 && size.Width > 0 && size.Height > 0;
    }

    public static bool IsRoiInRange(this Mat src, OpenCvSharp.Rect roi)
    {
        return roi.X >= 0 &&
               roi.Y >= 0 &&
               roi.X + roi.Width <= src.Width &&
               roi.Y + roi.Height <= src.Height;
    }

    public static System.Windows.Rect ToWindowRect(this Rect2f size)
    {
        return new System.Windows.Rect(size.Left, size.Top, size.Width, size.Height);
    }

    public static Scalar ToScalar(this Color color)
    {
        return Scalar.FromRgb(color.R, color.G, color.B);
    }

    public static Color ToColor(this Scalar color)
    {
        return Color.FromRgb((byte)color.Val2, (byte)color.Val1, (byte)color.Val0);
    }

    public static float GetCorrectAngle(this RotatedRect rr)
    {
        float angle = rr.Angle;
        var size = rr.Size;
        if (size.Width < size.Height)
        {
            if (angle == 90)
                angle -= 90f;
            else
                angle += 90f;
        }
        return angle;
    }


    public static (Size2f size, float angle) GetCorrect(this RotatedRect rr)
    {
        // 规范化：确保宽度始终是较长的一边
        if (rr.Size.Height > rr.Size.Width)
            return (new Size2f(rr.Size.Height, rr.Size.Width), rr.Angle + 90);
        else
        {
            return (rr.Size, rr.Angle);
        }
    }
}



