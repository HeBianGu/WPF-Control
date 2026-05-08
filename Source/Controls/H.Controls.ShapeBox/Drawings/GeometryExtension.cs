// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Collections.Generic;
global using System.Linq;

namespace H.Controls.ShapeBox.Drawings;
public static class GeometryExtension
{
    public static Geometry ToGeometry(this IEnumerable<Point> points, bool isFilled = true, bool isClosed = true)
    {
        StreamGeometry streamGeom = new StreamGeometry();
        using (StreamGeometryContext context = streamGeom.Open())
        {
            context.BeginFigure(points.FirstOrDefault(), isFilled, isClosed);
            foreach (Point item in points)
            {
                context.LineTo(item, true, true);
            }
        }
        return streamGeom;
    }

    public static Rect GetBoundingBox(this IEnumerable<Point> points)
    {
        return points.ToGeometry().Bounds;
    }

    public static bool IsZoreOrEmpty(this Rect rect)
    {
        return rect.IsEmpty || rect.Size.IsZoreOrEmpty() || rect.Height == 0 || rect.Width == 0;
    }

    public static bool IsZoreOrEmpty(this Size size)
    {
        return size.IsEmpty || size.Height == 0 || size.Width == 0;
    }

    public static Rect ToRect(this Size size)
    {
        return new Rect(size);
    }

    /// <summary>
    /// 判断点是否在线段上（包括端点）
    /// </summary>
    public static bool IsPointOnSegment(this Point point, Point segmentStart, Point segmentEnd, double tolerance = 1e-6)
    {
        // 首先检查是否在直线上
        if (Math.Abs((segmentEnd.X - segmentStart.X) * (point.Y - segmentStart.Y) -
                   (segmentEnd.Y - segmentStart.Y) * (point.X - segmentStart.X)) > tolerance)
            return false;

        // 然后检查是否在线段范围内
        double minX = Math.Min(segmentStart.X, segmentEnd.X) - tolerance;
        double maxX = Math.Max(segmentStart.X, segmentEnd.X) + tolerance;
        double minY = Math.Min(segmentStart.Y, segmentEnd.Y) - tolerance;
        double maxY = Math.Max(segmentStart.Y, segmentEnd.Y) + tolerance;

        return point.X >= minX && point.X <= maxX &&
               point.Y >= minY && point.Y <= maxY;
    }

    /// <summary>
    /// 计算点到线段的最短距离
    /// </summary>
    public static double DistanceToSegment(this Point point, Point segmentStart, Point segmentEnd)
    {
        double vx = segmentEnd.X - segmentStart.X;
        double vy = segmentEnd.Y - segmentStart.Y;

        double wx = point.X - segmentStart.X;
        double wy = point.Y - segmentStart.Y;

        double len2 = vx * vx + vy * vy;
        // 退化为点
        if (len2 <= double.Epsilon)
        {
            double dx0 = point.X - segmentStart.X;
            double dy0 = point.Y - segmentStart.Y;
            return Math.Sqrt(dx0 * dx0 + dy0 * dy0);
        }

        // 投影系数 t 并截断到 [0,1]
        double t = (wx * vx + wy * vy) / len2;
        if (t < 0) t = 0;
        else if (t > 1) t = 1;

        // 最近点 C
        double cx = segmentStart.X + t * vx;
        double cy = segmentStart.Y + t * vy;

        double dx = point.X - cx;
        double dy = point.Y - cy;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    /// <summary>
    /// 计算点到线段的最近点
    /// </summary>
    public static Point ClosestPointOnSegment(this Point point, Point segmentStart, Point segmentEnd)
    {
        double vx = segmentEnd.X - segmentStart.X;
        double vy = segmentEnd.Y - segmentStart.Y;

        double len2 = vx * vx + vy * vy;
        if (len2 <= double.Epsilon) return segmentStart;

        double t = ((point.X - segmentStart.X) * vx + (point.Y - segmentStart.Y) * vy) / len2;
        if (t < 0) t = 0;
        else if (t > 1) t = 1;

        return new Point(segmentStart.X + t * vx, segmentStart.Y + t * vy);
    }

    /// <summary>
    /// 计算点到圆周的最短距离（无符号，点在圆内返回 r-‖P-C‖，在圆外返回 ‖P-C‖-r，统一取绝对值）
    /// </summary>
    public static double DistanceToCircle(this Point point, Point center, double radius)
    {
        radius = Math.Abs(radius);
        double dx = point.X - center.X;
        double dy = point.Y - center.Y;
        double d = Math.Sqrt(dx * dx + dy * dy);
        return Math.Abs(d - radius);
    }

    /// <summary>
    /// 计算点到圆周的带符号距离（外正内负，圆上为0）
    /// </summary>
    public static double SignedDistanceToCircle(this Point point, Point center, double radius)
    {
        radius = Math.Abs(radius);
        double dx = point.X - center.X;
        double dy = point.Y - center.Y;
        double d = Math.Sqrt(dx * dx + dy * dy);
        return d - radius;
    }

    /// <summary>
    /// 计算点在圆周上的最近点（P 与 C 重合时取 X 轴正向上的点）
    /// </summary>
    public static Point ClosestPointOnCircle(this Point point, Point center, double radius)
    {
        radius = Math.Abs(radius);
        double dx = point.X - center.X;
        double dy = point.Y - center.Y;
        double len = Math.Sqrt(dx * dx + dy * dy);

        if (len <= double.Epsilon)
        {
            // P 与圆心重合，取一个稳定方向
            return new Point(center.X + radius, center.Y);
        }

        double scale = radius / len;
        return new Point(center.X + dx * scale, center.Y + dy * scale);
    }

    /// <summary>
    /// 计算点到矩形边界的最短距离（无符号）。
    /// - 点在矩形内部：返回到最近边的距离（>0 或 0）。
    /// - 点在矩形外部：返回到矩形的欧氏最短距离。
    /// 退化处理：
    /// - 宽为0且高>0：退化为竖直线段；
    /// - 高为0且宽>0：退化为水平线段；
    /// - 宽高均为0：退化为一个点。
    /// </summary>
    public static double DistanceToRect(this Point point, Rect rect)
    {
        // 退化为线段或点
        if (rect.Width <= 0 && rect.Height <= 0)
            return Math.Sqrt((point.X - rect.Left) * (point.X - rect.Left) + (point.Y - rect.Top) * (point.Y - rect.Top));
        if (rect.Width <= 0)
            return point.DistanceToSegment(new Point(rect.Left, rect.Top), new Point(rect.Left, rect.Bottom));
        if (rect.Height <= 0)
            return point.DistanceToSegment(new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Top));

        double left = rect.Left, right = rect.Right, top = rect.Top, bottom = rect.Bottom;

        bool insideX = point.X >= left && point.X <= right;
        bool insideY = point.Y >= top && point.Y <= bottom;

        if (insideX && insideY)
        {
            double dx = Math.Min(point.X - left, right - point.X);
            double dy = Math.Min(point.Y - top, bottom - point.Y);
            return Math.Min(dx, dy);
        }

        double hx = point.X < left ? left - point.X : (point.X > right ? point.X - right : 0);
        double hy = point.Y < top ? top - point.Y : (point.Y > bottom ? point.Y - bottom : 0);
        return Math.Sqrt(hx * hx + hy * hy);
    }

    /// <summary>
    /// 计算点到矩形边界的带符号距离（外正内负，边界为0）。
    /// 退化矩形时返回非负值（等价于无符号距离）。
    /// </summary>
    public static double SignedDistanceToRect(this Point point, Rect rect)
    {
        // 退化：无符号距离即为返回值
        if (rect.Width <= 0 || rect.Height <= 0)
            return DistanceToRect(point, rect);

        double left = rect.Left, right = rect.Right, top = rect.Top, bottom = rect.Bottom;

        bool insideX = point.X >= left && point.X <= right;
        bool insideY = point.Y >= top && point.Y <= bottom;

        if (insideX && insideY)
        {
            double dx = Math.Min(point.X - left, right - point.X);
            double dy = Math.Min(point.Y - top, bottom - point.Y);
            return -Math.Min(dx, dy);
        }

        double hx = point.X < left ? left - point.X : (point.X > right ? point.X - right : 0);
        double hy = point.Y < top ? top - point.Y : (point.Y > bottom ? point.Y - bottom : 0);
        return Math.Sqrt(hx * hx + hy * hy);
    }

    /// <summary>
    /// 计算点在矩形边界上的最近点。
    /// - 点在外部：对坐标分别进行夹取到 [Left,Right] / [Top,Bottom]。
    /// - 点在内部：投影到最近的一条边。
    /// 退化处理：线段或点的最近点。
    /// </summary>
    public static Point ClosestPointOnRect(this Point point, Rect rect)
    {
        // 退化：线段或点
        if (rect.Width <= 0 && rect.Height <= 0)
            return new Point(rect.Left, rect.Top);
        if (rect.Width <= 0)
            return point.ClosestPointOnSegment(new Point(rect.Left, rect.Top), new Point(rect.Left, rect.Bottom));
        if (rect.Height <= 0)
            return point.ClosestPointOnSegment(new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Top));

        double left = rect.Left, right = rect.Right, top = rect.Top, bottom = rect.Bottom;

        bool insideX = point.X >= left && point.X <= right;
        bool insideY = point.Y >= top && point.Y <= bottom;

        if (!insideX || !insideY)
        {
            double cx = Math.Min(Math.Max(point.X, left), right);
            double cy = Math.Min(Math.Max(point.Y, top), bottom);
            return new Point(cx, cy);
        }

        // 在内部：投影到最近边
        double dl = Math.Abs(point.X - left);
        double dr = Math.Abs(right - point.X);
        double dt = Math.Abs(point.Y - top);
        double db = Math.Abs(bottom - point.Y);

        double min = Math.Min(Math.Min(dl, dr), Math.Min(dt, db));

        if (min == dl) return new Point(left, point.Y);
        if (min == dr) return new Point(right, point.Y);
        if (min == dt) return new Point(point.X, top);
        return new Point(point.X, bottom);
    }

    /// <summary>
    /// 计算点到一组点（Point[] / IEnumerable&lt;Point&gt;）的最小欧氏距离。
    /// 当集合为空或 null 时返回 +∞。
    /// </summary>
    public static double DistanceToPoints(this Point point, IEnumerable<Point> points)
    {
        if (points is null) return double.PositiveInfinity;

        double best2 = double.PositiveInfinity;
        if (points is IReadOnlyList<Point> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var p = list[i];
                double dx = point.X - p.X;
                double dy = point.Y - p.Y;
                double d2 = dx * dx + dy * dy;
                if (d2 < best2) best2 = d2;
            }
        }
        else
        {
            foreach (var p in points)
            {
                double dx = point.X - p.X;
                double dy = point.Y - p.Y;
                double d2 = dx * dx + dy * dy;
                if (d2 < best2) best2 = d2;
            }
        }

        return best2 < double.PositiveInfinity ? Math.Sqrt(best2) : double.PositiveInfinity;
    }

    public static double DistanceToPolygon(this Point point, IEnumerable<Point> points)
    {
        if (points is null) return double.PositiveInfinity;

        IReadOnlyList<Point> list = points as IReadOnlyList<Point> ?? points.ToList();
        if (list.Count == 0) return double.PositiveInfinity;
        if (list.Count == 1) return point.DistanceToPoints(list);
        if (list.Count == 2) return point.DistanceToSegment(list[0], list[1]);

        double best = double.PositiveInfinity;
        for (int i = 0; i < list.Count; i++)
        {
            Point start = list[i];
            Point end = list[(i + 1) % list.Count];
            double distance = point.DistanceToSegment(start, end);
            if (distance < best) best = distance;
        }

        return best;

    }

    public static double DistanceToPolyLine(this Point point, IEnumerable<Point> points)
    {
        if (points is null) return double.PositiveInfinity;

        IReadOnlyList<Point> list = points as IReadOnlyList<Point> ?? points.ToList();
        if (list.Count == 0) return double.PositiveInfinity;
        if (list.Count == 1) return point.DistanceToPoints(list);

        double best = double.PositiveInfinity;
        for (int i = 0; i < list.Count - 1; i++)
        {
            double distance = point.DistanceToSegment(list[i], list[i + 1]);
            if (distance < best) best = distance;
        }

        return best;
    }

    /// <summary>
    /// 尝试获取点在一组点中的最近点。
    /// 返回值：是否找到（集合非空）；输出最近点、索引与距离。
    /// 空或 null 集合时：index = -1，distance = +∞，closest = default(Point)。
    /// </summary>
    public static bool TryClosestPointIn(this Point point, IEnumerable<Point> points, out Point closest, out int index, out double distance)
    {
        closest = default;
        index = -1;
        distance = double.PositiveInfinity;

        if (points is null) return false;

        double best2 = double.PositiveInfinity;

        if (points is IReadOnlyList<Point> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var p = list[i];
                double dx = point.X - p.X;
                double dy = point.Y - p.Y;
                double d2 = dx * dx + dy * dy;
                if (d2 < best2)
                {
                    best2 = d2;
                    closest = p;
                    index = i;
                }
            }
        }
        else
        {
            int i = 0;
            foreach (var p in points)
            {
                double dx = point.X - p.X;
                double dy = point.Y - p.Y;
                double d2 = dx * dx + dy * dy;
                if (d2 < best2)
                {
                    best2 = d2;
                    closest = p;
                    index = i;
                }
                i++;
            }
        }

        if (index >= 0)
        {
            distance = Math.Sqrt(best2);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 获取最近点（便捷版）。集合为空或 null 时抛出 InvalidOperationException。
    /// </summary>
    public static Point ClosestPointIn(this Point point, IEnumerable<Point> points, out int index, out double distance)
    {
        if (TryClosestPointIn(point, points, out var closest, out index, out distance))
            return closest;

        throw new InvalidOperationException("Points collection is null or empty.");
    }
}
