// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Geometrys;

public struct VisionCircle
{
    public VisionCircle(Point point, double radius)
    {
        this.Point = point;
        this.Radius = radius;
    }
    [Expressionable]
    [Display(Name = "圆心", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public Point Point { get; set; }
    [Expressionable]
    [Display(Name = "半径", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public double Radius { get; set; }

    public static VisionCircle Empty => default;

    public bool IsEmpty => this.Equals(default) || this.Radius == 0.0;
}

public static class VisionCircleExtensions
{
    public static bool IsEmpty(this VisionCircle circle)
    {
        return circle.Equals(VisionCircle.Empty);
    }
    public static double GetDiameter(this VisionCircle circle)
    {
        return circle.Radius * 2;
    }
    public static double GetArea(this VisionCircle circle)
    {
        return Math.PI * Math.Pow(circle.Radius, 2);
    }
}


