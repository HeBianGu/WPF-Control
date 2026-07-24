// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes;
using H.Controls.ShapeBox.Shapes.Base;

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


    public static PointShape ToReferencePointShape(this PositionCorrectionInfo positionCorrectionInfo)
    {
        return new PointShape(positionCorrectionInfo.ReferencePoint)
        {
            Stroke = Brushes.Chartreuse,
            UseCross = true,
            CrossAngle = positionCorrectionInfo.ReferenceAngle,
            Title = $"基准角度: {positionCorrectionInfo.ReferenceAngle.ToString("F2")}"
        };
    }

    public static PointShape ToInvokePointPointShape(this PositionCorrectionInfo positionCorrectionInfo)
    {
        return new PointShape(positionCorrectionInfo.InvokePoint)
        {
            Stroke = Brushes.Red,
            UseCross = true,
            CrossAngle = positionCorrectionInfo.InvokeAngle,
            Title = $"运行角度: {positionCorrectionInfo.InvokeAngle.ToString("F2")}"
        };
    }

    public static IEnumerable<PointShape> ToPointShapes(this PositionCorrectionInfo positionCorrectionInfo)
    {
        yield return positionCorrectionInfo.ToInvokePointPointShape();
        yield return positionCorrectionInfo.ToReferencePointShape();
    }
}
