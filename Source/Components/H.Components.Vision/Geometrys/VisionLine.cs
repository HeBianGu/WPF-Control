// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Geometrys;

public struct VisionLine
{
    public VisionLine()
    {

    }
    public VisionLine(Point start, Point end)
    {
        this.Start = start;
        this.End = end;
    }
    [Expressionable]
    [Display(Name = "起点", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public Point Start { get; set; }
    [Expressionable]
    [Display(Name = "终点", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public Point End { get; set; }
    [Expressionable]
    [Display(Name = "中点", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public Point Center => new Point((this.Start.X + this.End.X) / 2, (this.Start.Y + this.End.Y) / 2);

    public static VisionLine Empty => default;
    public bool IsEmpty => this.Equals(default) || this.GetLength() == 0.0;
}

public static class VisionLineExtensions
{
    public static bool IsEmpty(this VisionLine line)
    {
        return line.Equals(VisionLine.Empty);
    }
    public static double GetLength(this VisionLine line)
    {
        return (line.End - line.Start).Length;
    }

    /// <summary>
    /// 计算点 P 到线段 AB 的投影点（垂足）
    /// </summary>
    /// <param name="P">目标点</param>
    /// <param name="A">线段起点</param>
    /// <param name="B">线段终点</param>
    /// <returns>投影点坐标</returns>
    public static Point GetProjectionPoint(this VisionLine line, Point P)
    {
        var A = line.Start;
        var B = line.End;
        // 向量 AP
        double APx = P.X - A.X;
        double APy = P.Y - A.Y;

        // 向量 AB
        double ABx = B.X - A.X;
        double ABy = B.Y - A.Y;

        // 计算 AB 长度的平方（避免开根号）
        double ABLengthSquared = ABx * ABx + ABy * ABy;

        // 如果 AB 长度为 0（A 和 B 重合），返回 A
        if (ABLengthSquared == 0)
            return A;

        // 计算 AP 在 AB 上的投影参数 t
        double t = (APx * ABx + APy * ABy) / ABLengthSquared;

        // 限制 t 在 [0, 1] 范围内
        t = Math.Max(0, Math.Min(1, t));

        // 计算投影点 Q = A + t * AB
        double Qx = A.X + t * ABx;
        double Qy = A.Y + t * ABy;

        return new Point(Qx, Qy);
    }
}
