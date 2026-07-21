// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.Base;

/// <summary>
/// 位置校正信息
/// </summary>
public struct PositionCorrectionInfo
{
    public Point ReferencePoint;
    public double ReferenceAngle;
    public Point InvokePoint;
    public double InvokeAngle;
}

public static class PositionCorrectionInfoExtension
{
    public static Point ToReferencePoint(this PositionCorrectionInfo positionCorrectionInfo, Point invokePoint)
    {
        return positionCorrectionInfo.ToMatrix().Transform(invokePoint);
    }

    public static Matrix ToMatrix(this PositionCorrectionInfo positionCorrectionInfo)
    {
        var angle = (positionCorrectionInfo.ReferenceAngle - positionCorrectionInfo.InvokeAngle) * Math.PI / 180d;
        var cos = Math.Cos(angle);
        var sin = Math.Sin(angle);

        return new Matrix(
            cos,
            sin,
            -sin,
            cos,
            positionCorrectionInfo.ReferencePoint.X - positionCorrectionInfo.InvokePoint.X * cos + positionCorrectionInfo.InvokePoint.Y * sin,
            positionCorrectionInfo.ReferencePoint.Y - positionCorrectionInfo.InvokePoint.X * sin - positionCorrectionInfo.InvokePoint.Y * cos);

    }
}
