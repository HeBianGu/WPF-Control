// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;

namespace H.Extensions.StoryBoard.EasingFunction;

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
