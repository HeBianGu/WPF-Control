// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
namespace H.Controls.ShapeBox.Geometrys
{
    public struct Line
    {
        public Line(Point from, Point to)
        {
            this.From = from;
            this.To = to;
        }
        public Point From { get; set; }
        public Point To { get; set; }
    }

    public static class LineExtension
    {

        #region - 判断直线在另一条直线的左侧还是右侧 -
        // 2D叉积（实际上是z分量的值）
        public static double CrossProduct(Vector a, Vector b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        // 判断点相对于直线的位置
        public static RelativePosition GetPointPosition(Line referenceLine, Point testPoint)
        {
            Vector ab = referenceLine.To - referenceLine.From;
            Vector ac = testPoint - referenceLine.From;

            double cross = CrossProduct(ab, ac);

            if (Math.Abs(cross) < double.Epsilon)
                return RelativePosition.OnLine;

            return cross > 0 ? RelativePosition.Left : RelativePosition.Right;
        }

        // 判断直线相对于参考直线的位置
        public static RelativePosition GetLinePosition(Line referenceLine, Line testLine)
        {
            RelativePosition startPos = GetPointPosition(referenceLine, testLine.From);
            RelativePosition endPos = GetPointPosition(referenceLine, testLine.To);

            if (startPos == RelativePosition.OnLine && endPos == RelativePosition.OnLine)
                return RelativePosition.OnLine;

            if (startPos == RelativePosition.Left && endPos == RelativePosition.Left)
                return RelativePosition.Left;

            if (startPos == RelativePosition.Right && endPos == RelativePosition.Right)
                return RelativePosition.Right;

            return RelativePosition.Crossing; // 跨越参考线
        }

        public enum RelativePosition
        {
            Left,
            Right,
            OnLine,
            Crossing
        }

        // 判断线段是否完全在某一侧
        public static bool IsLineCompletelyOnSide(Line referenceLine, Line testLine, RelativePosition side)
        {
            RelativePosition actualPosition = GetLinePosition(referenceLine, testLine);
            return actualPosition == side || actualPosition == RelativePosition.OnLine;
        }
        #endregion

        #region - 直线在多边形内部 -
        // 判断点是否在多边形内（射线法）
        public static bool IsPointInPolygon(Point point, params Point[] polygon)
        {
            bool inside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                    (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) /
                    (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    inside = !inside;
                }
            }
            return inside;
        }

        // 判断两条线段是否相交
        public static bool DoLinesIntersect(Point p1, Point p2, Point p3, Point p4)
        {
            // 实现线段相交检测算法
            // 这里可以使用叉积法或其他几何算法
            // 简化实现，实际需要完整实现
            double d1 = Direction(p3, p4, p1);
            double d2 = Direction(p3, p4, p2);
            double d3 = Direction(p1, p2, p3);
            double d4 = Direction(p1, p2, p4);

            if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) &&
                ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0)))
                return true;

            return false;
        }

        private static double Direction(Point pi, Point pj, Point pk)
        {
            return (pk.X - pi.X) * (pj.Y - pi.Y) - (pj.X - pi.X) * (pk.Y - pi.Y);
        }

        // 判断直线是否完全在多边形内
        public static bool IsLineInPolygon(Point lineStart, Point lineEnd, params Point[] polygon)
        {
            // 检查端点是否都在多边形内
            if (!IsPointInPolygon(lineStart, polygon) || !IsPointInPolygon(lineEnd, polygon))
                return false;

            // 检查直线是否与任何多边形边相交
            for (int i = 0; i < polygon.Length; i++)
            {
                Point p1 = polygon[i];
                Point p2 = polygon[(i + 1) % polygon.Length];

                if (DoLinesIntersect(lineStart, lineEnd, p1, p2))
                    return false;
            }

            return true;
        }
        #endregion

        #region -  直线在圆形内部 -
        public static bool IsLineInCircle(Point lineStart, Point lineEnd, Point center, double radius)
        {
            // 检查端点是否在圆内
            if (!IsPointInCircle(lineStart, center, radius) || !IsPointInCircle(lineEnd, center, radius))
                return false;

            // 计算直线到圆心的距离
            double distance = DistanceFromLineToPoint(lineStart, lineEnd, center);

            return distance <= radius;
        }

        private static bool IsPointInCircle(Point point, Point center, double radius)
        {
            double dx = point.X - center.X;
            double dy = point.Y - center.Y;
            return dx * dx + dy * dy <= radius * radius;
        }

        private static double DistanceFromLineToPoint(Point lineStart, Point lineEnd, Point point)
        {
            // 直线方程: Ax + By + C = 0
            double A = lineEnd.Y - lineStart.Y;
            double B = lineStart.X - lineEnd.X;
            double C = lineEnd.X * lineStart.Y - lineStart.X * lineEnd.Y;

            // 点到直线距离公式
            return Math.Abs(A * point.X + B * point.Y + C) / Math.Sqrt(A * A + B * B);
        }
        #endregion

        #region - 直线在矩形内部 -
        public static bool IsLineInRectangle(Point lineStart, Point lineEnd,
                                   Point rectTopLeft, Point rectBottomRight)
        {
            // 检查端点是否在矩形内
            if (!IsPointInRectangle(lineStart, rectTopLeft, rectBottomRight) ||
                !IsPointInRectangle(lineEnd, rectTopLeft, rectBottomRight))
                return false;

            // 检查直线是否与矩形边相交
            Point rectTopRight = new Point(rectBottomRight.X, rectTopLeft.Y);
            Point rectBottomLeft = new Point(rectTopLeft.X, rectBottomRight.Y);

            // 检查四条边
            if (DoLinesIntersect(lineStart, lineEnd, rectTopLeft, rectTopRight) ||
                DoLinesIntersect(lineStart, lineEnd, rectTopRight, rectBottomRight) ||
                DoLinesIntersect(lineStart, lineEnd, rectBottomRight, rectBottomLeft) ||
                DoLinesIntersect(lineStart, lineEnd, rectBottomLeft, rectTopLeft))
                return false;

            return true;
        }

        private static bool IsPointInRectangle(Point point, Point topLeft, Point bottomRight)
        {
            return point.X >= topLeft.X && point.X <= bottomRight.X &&
                   point.Y >= topLeft.Y && point.Y <= bottomRight.Y;
        }
        #endregion

    }
}
