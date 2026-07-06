// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.Base;

public interface INinePointCalibrationNodeData
{
    IReadOnlyList<Point> PixelPoints { get; }
    IReadOnlyList<Point> WorldPoints { get; }

    /// <summary>
    /// 像素坐标 -> 世界坐标（mm）
    /// </summary>
    bool IsValid { get; }

    Point PixelToWorld(Point pixelPoint);

    /// <summary>
    /// 设置九点并重新计算变换
    /// </summary>
    void SetPoints(IEnumerable<Point> pixelPoints, IEnumerable<Point> worldPoints);
}