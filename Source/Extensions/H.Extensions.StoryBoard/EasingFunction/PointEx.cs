// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Extensions.StoryBoard
{
    public static class PointEx
    {
        public static Point Add(this Point point, Point value)
        {
            return new Point(point.X + value.X, point.Y + value.Y);
        }

        public static Point Multiply(this Point point, double value)
        {
            return new Point(point.X * value, point.Y * value);
        }
    }
}
