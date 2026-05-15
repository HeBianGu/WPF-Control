// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Drawings;

namespace H.Components.Visions.OpenCV.Extensions;
public static class ShapeExtension
{
    public static PolygonShape ToPolygonShape(this RotatedRect rotatedRect, Action<PolygonShape> action = null)
    {
        var shape = new PolygonShape(rotatedRect.Points().Select(x => x.ToPoint().ToPoint())) { Title = "最小外接矩形" };
        action?.Invoke(shape);
        return shape;
    }

    public static RotatedRectShape ToRotatedRectShape(this RotatedRect rotatedRect, Action<RotatedRectShape> action = null)
    {
        var t = rotatedRect.GetCorrect();
        var shape = new RotatedRectShape(rotatedRect.Center.ToPoint().ToPoint(), t.size.ToSize().ToSize(), t.angle) { Title = "最小外接矩形" };
        action?.Invoke(shape);
        return shape;
    }

    public static IEnumerable<DimensionShape> ToDimensionShapes(this RotatedRect rotatedRect, Action<DimensionShape> action = null)
    {
        var points = rotatedRect.Points().Select(x => x.ToPoint().ToPoint());
        return points.ToDimensionShapes(action);
    }

    public static PolygonShape ToPolygonShape(this IEnumerable<OpenCvSharp.Point> points, Action<PolygonShape> action = null)
    {
        return points?.Select(x => x.ToPoint()).ToPolygonShape(action);
    }

    public static ObservableCollection<IShape> ToShapeObservable(this IShape shape)
    {
        return shape.ToEnumerable().ToObservable();
    }

    public static IEnumerable<PointShape> ToPointShapes(this IEnumerable<OpenCvSharp.Point> points, Action<PointShape> action = null)
    {
        return points?.Select(x => x.ToPoint()).ToPointShapes(action);
    }

    public static IEnumerable<PointShape> ToPointShapes(this IEnumerable<KeyPoint> points, Action<PointShape> action = null)
    {
        Dictionary<int, SolidColorBrush> s_keyPointTypeBrushCache = new();

        SolidColorBrush GetOrCreateBrushByKeyPointType(int classId)
        {
            // OpenCV 默认 ClassId 可能为 -1，统一映射到稳定种子
            var key = classId < 0 ? int.MaxValue : classId;
            if (s_keyPointTypeBrushCache.TryGetValue(key, out var cached))
                return cached;

            // Deterministic "random" color from key
            var rnd = new Random(unchecked(key * 397) ^ 0x5f3759df);

            // Avoid too dark colors: keep in [64, 255]
            byte NextByte() => (byte)rnd.Next(64, 256);

            var color = Color.FromRgb(NextByte(), NextByte(), NextByte());
            var brush = new SolidColorBrush(color);
            brush.Freeze();

            s_keyPointTypeBrushCache[key] = brush;
            return brush;
        }

        if (points == null)
            yield break;
        foreach (var point in points)
        {
            var brush = GetOrCreateBrushByKeyPointType(point.ClassId);
            PointShape pointShape = new PointShape(point.Pt.ToPoint().ToPoint());
            pointShape.Stroke = brush;
            action?.Invoke(pointShape);
            yield return pointShape;
        }
    }

    public static IEnumerable<PolygonShape> ToPolygonShapes(this OpenCvSharp.Point[][] points, Action<PolygonShape> action = null)
    {
        if (points == null)
            yield break;
        foreach (var item in points)
        {
            yield return item.ToPolygonShape(action);
        }
    }
}

