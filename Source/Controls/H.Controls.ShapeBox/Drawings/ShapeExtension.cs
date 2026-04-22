// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Collections.Generic;
global using System.Linq;
using H.Extensions.Common;

namespace H.Controls.ShapeBox.Drawings;
public static class ShapeExtension
{
    public static IEnumerable<DimensionShape> ToDimensionShapes(this IEnumerable<Point> points, Action<DimensionShape> action = null)
    {
        int c = points.Count();
        if (points == null || c == 0)
            yield break;
        for (int i = 0; i < c; i++)
        {
            if (i == 0)
                continue;
            var r = new DimensionShape(points.ElementAt(i - 1), points.ElementAt(i));
            action?.Invoke(r);
            yield return r;
        }
        if (points.Count() > 2)
        {
            var r = new DimensionShape(points.Last(), points.First());
            action?.Invoke(r);
            yield return r;
        }
    }

    public static IEnumerable<DimensionShape> ToDimensionShapes(this Rect rect, Action<DimensionShape> action = null)
    {
        if (rect.IsZoreOrEmpty())
            yield break;
        {
            var r = new DimensionShape(rect.TopLeft, rect.TopRight);
            action?.Invoke(r);
            yield return r;
        }
        {
            var r = new DimensionShape(rect.TopRight, rect.BottomRight);
            action?.Invoke(r);
            yield return r;
        }
        {
            var r = new DimensionShape(rect.BottomRight, rect.BottomLeft);
            action?.Invoke(r);
            yield return r;
        }
        {
            var r = new DimensionShape(rect.BottomLeft, rect.TopLeft);
            action?.Invoke(r);
            yield return r;
        }
    }

    public static PointShape ToPointShape(this Point p, Action<PointShape> action = null)
    {
        var r = new PointShape(p);
        action?.Invoke(r);
        return r;
    }
    public static RectShape ToRectShape(this Rect rect, Action<RectShape> action = null)
    {
        var r = new RectShape(rect);
        action?.Invoke(r);
        return r;
    }

    public static PolygonShape ToPolygonShape(this IEnumerable<Point> points, Action<PolygonShape> action = null)
    {
        var r = new PolygonShape(points);
        action?.Invoke(r);
        return r;
    }

    public static IEnumerable<PointShape> ToPointShapes(this IEnumerable<Point> points, Action<PointShape> action = null)
    {
        foreach (var item in points)
        {
            var r = new PointShape(item);
            action?.Invoke(r);
            yield return r;
        }

    }

    public static Rect GetBoundingBox(this IEnumerable<IBoundingBoxShape> boundingBoxShapes)
    {
        if (boundingBoxShapes == null)
            return Rect.Empty;

        bool has = false;
        Rect result = Rect.Empty;

        foreach (var item in boundingBoxShapes)
        {
            if (item == null)
                continue;

            var rect = item.BoundingBox;
            if (rect.IsZoreOrEmpty())
                continue;

            if (!has)
            {
                result = rect;
                has = true;
                continue;
            }

            result = Rect.Union(result, rect);
        }
        return has ? result : Rect.Empty;
    }


    public static ImageShape ToImageShape(this string base64)
    {
        if (base64 == null)
            return null;
        var imageSource = base64.ToBase64ImageSource();
        var rect = new Rect(0, 0, imageSource.Width, imageSource.Height);
        return new ImageShape(rect) { ImageSource = imageSource };
    }
}
