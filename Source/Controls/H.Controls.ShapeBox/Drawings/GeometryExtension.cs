// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;

namespace H.Controls.ShapeBox.Drawings
{
    public static class GeometryExtension
    {
        public static Geometry ToGeometry(this IEnumerable<Point> points, bool isFilled = true, bool isClosed = true)
        {
            StreamGeometry streamGeom = new StreamGeometry();
            using (StreamGeometryContext context = streamGeom.Open())
            {
                context.BeginFigure(points.FirstOrDefault(), true, true);
                foreach (Point item in points)
                {
                    context.LineTo(item, true, true);
                }
            }
            return streamGeom;
        }


        public static bool IsZoreOrEmpty(this Rect rect)
        {
            return rect.IsEmpty || rect.Size.IsEmpty || rect.Height == 0 || rect.Width == 0;
        }

    }
}
