// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;

namespace H.Controls.ShapeBox.Geometrys
{
    public struct Circle
    {
        public double X { get; }
        public double Y { get; }
        public double Radius { get; }

        public Circle(double x, double y, double radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }
    }

    public static class CircleExtension
    {
        public static bool Contains(this Circle outer, Circle inner)
        {
            // 计算圆心之间的距离
            double dx = outer.X - inner.X;
            double dy = outer.Y - inner.Y;
            double distanceSquared = dx * dx + dy * dy;
            double distance = Math.Sqrt(distanceSquared);
            // 比较距离与半径差
            return distance <= (outer.Radius - inner.Radius) && outer.Radius > inner.Radius;
        }
    }

}
