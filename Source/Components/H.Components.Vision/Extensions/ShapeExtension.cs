// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.Geometrys;

namespace H.Components.Vision.Extensions;

public static class ShapeExtension
{

    public static VisionCircle ToVisionCircle(this CircleShape circleShape)
    {
        return new VisionCircle(circleShape.Center, circleShape.Radius);
    }

    public static CircleShape ToCircleShape(this VisionCircle visionCircle, Action<CircleShape> action = null)
    {
        var r = new CircleShape(visionCircle.Point, visionCircle.Radius);
        action?.Invoke(r);
        return r;
    }

    public static VisionLine ToVisionLine(this LineShape lineShape)
    {
        return new VisionLine(lineShape.From, lineShape.To);
    }

    public static LineShape ToLineShape(this VisionLine visionLine, Action<LineShape> action = null)
    {
        var r = new LineShape(visionLine.Start, visionLine.End);
        action?.Invoke(r);
        return r;
    }

    public static DimensionShape ToDimensionShape(this VisionLine visionLine, Action<DimensionShape> action = null)
    {
        var r = new DimensionShape(visionLine.Start, visionLine.End);
        action?.Invoke(r);
        return r;
    }

}
