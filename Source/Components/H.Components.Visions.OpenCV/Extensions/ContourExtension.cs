// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Components.Vision.Extensions;
using H.Components.Visions.OpenCV.NodeDatas.Detector;
using H.Controls.ShapeBox.Drawings;
using OpenCvSharp;
using System.Reflection.Metadata;

namespace H.Components.Visions.OpenCV.Extensions;

public static class ContourShapeExtension
{
    public static IShape ToShape(this OpenCvSharp.Point[] points, DrawContourType drawContourType, Action<ITitleShape> action = null)
    {
        if (drawContourType == DrawContourType.BoundingRect)
            return points.ToBoundingRect().ToWindowRect().ToRectShape(action);
        if (drawContourType == DrawContourType.MinAreaRect)
            return points.ToRotatedRect().ToRotatedRectShape(s =>
           {
               s.UseAngle = true;
               s.UseDimension = true;
               s.UseTitle = true;
               action?.Invoke(s);
           });
        if (drawContourType == DrawContourType.ConvexHull)
            points.ToConvexHull().ToPolygonShape(action);
        if (drawContourType == DrawContourType.ApproxPolyDP)
            return points.ToApproxPolyDP().ToPolygonShape(action);
        if (drawContourType == DrawContourType.Circle)
            return points.ToCircle().ToCircleShape(action);
        return points.ToPolygonShape(action);
    }

    public static IEnumerable<DimensionShape> ToDimensionShapes(this OpenCvSharp.Point[] points, DrawContourType drawContourType)
    {
        if (drawContourType == DrawContourType.BoundingRect)
            return points.ToBoundingRect().ToWindowRect().ToDimensionShapes();
        if (drawContourType == DrawContourType.MinAreaRect)
            return points.ToRotatedRect().ToDimensionShapes();
        if (drawContourType == DrawContourType.ConvexHull)
            points.ToConvexHull().ToDimensionShapes();
        if (drawContourType == DrawContourType.ApproxPolyDP)
            return points.ToApproxPolyDP().ToDimensionShapes();
        return points.ToDimensionShapes();
    }
}

public static class ContourExtension
{
    public static OpenCvSharp.Rect ToBoundingRect(this OpenCvSharp.Point[] points)
    {
        return Cv2.BoundingRect(points);
    }
    public static OpenCvSharp.RotatedRect ToRotatedRect(this OpenCvSharp.Point[] points)
    {
        return Cv2.MinAreaRect(points);
    }
    public static VisionCircle ToCircle(this OpenCvSharp.Point[] points)
    {
        Cv2.MinEnclosingCircle(points, out Point2f center, out float radius);
        return new VisionCircle(center.ToPoint().ToPoint(), radius);
    }

    /// <summary>
    /// 将一个轮廓（由多个点组成的曲线）用顶点更少、形状更近似的多边形来表示，从而简化数据并去除“毛刺”
    /// </summary>
    /// <param name="points"></param>
    /// <param name="epsilon"></param>
    /// <param name="closed"></param>
    /// <returns></returns>
    public static OpenCvSharp.Point[] ToApproxPolyDP(this OpenCvSharp.Point[] points)
    {
        var perimeter = Cv2.ArcLength(points, true);
        double epsilon = 0.02 * perimeter;
        return Cv2.ApproxPolyDP(points, epsilon, true);
    }

    public static OpenCvSharp.Point[] ToApproxPolyDP(this OpenCvSharp.Point[] points, double epsilon, bool closed)
    {
        return Cv2.ApproxPolyDP(points, epsilon, closed);
    }

    /// <summary>
    /// 找到包围一个点集（通常是轮廓）的最小凸多边形，可以形象地理解为“用一个橡皮筋紧紧包裹住所有点”后形成的形状
    /// </summary>
    /// <param name="points"></param>
    /// <param name="clockwise"></param>
    /// <returns></returns>
    public static OpenCvSharp.Point[] ToConvexHull(this OpenCvSharp.Point[] points, bool clockwise = false)
    {
        return Cv2.ConvexHull(points, clockwise);
    }

    /// <summary>
    /// 用一个最佳拟合椭圆来近似一组点（通常是轮廓），从而获得该轮廓的中心、长短轴长度和旋转角度
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static OpenCvSharp.RotatedRect ToEllipse(this OpenCvSharp.Point[] points)
    {
        return Cv2.FitEllipse(points);
    }

    /// <summary>
    /// 用一条最佳拟合直线来近似一组二维或三维点集，在图像处理中常用于找到物体的主轴方向或边缘的直线方程
    /// </summary>
    /// <param name="points"></param>
    /// <param name="distType"></param>
    /// <param name="param"></param>
    /// <param name="reps"></param>
    /// <param name="aeps"></param> 
    /// <returns></returns>
    public static OpenCvSharp.Line2D ToLine(this OpenCvSharp.Point[] points, DistanceTypes distType,
        double param, double reps, double aeps)
    {
        return Cv2.FitLine(points, distType, param, reps, aeps);
    }

    /// <summary>
    /// 用于点到多边形距离测试的函数。它的核心作用是计算一个点到一个多边形（通常是轮廓）的最短距离，并判断该点位于多边形内部、外部还是边界上
    /// </summary>
    /// <param name="points"></param>
    /// <param name="pt"></param>
    /// <param name="measureDist"></param>
    /// <returns></returns>
    public static double ToPointPolygonTest(this OpenCvSharp.Point[] points, Point2f pt, bool measureDist)
    {
        return Cv2.PointPolygonTest(points, pt, measureDist);
    }

    /// <summary>
    /// 判断轮廓是否为凸形状的函数。它的核心作用是快速检测一个多边形（通常是轮廓）是凸的还是凹的，返回一个布尔值
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static bool IsContourConvex(this OpenCvSharp.Point[] points)
    {
        return Cv2.IsContourConvex(points);
    }

    /// <summary>
    /// 面积
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static double ToContourArea(this OpenCvSharp.Point[] points)
    {
        return Cv2.ContourArea(points);
    }
    /// <summary>
    /// 是计算一个闭合或非闭合曲线的长度（总边长），是轮廓几何分析中最常用的基础函数之一
    /// </summary>
    /// <param name="points"></param>
    /// <param name="closed"></param>
    /// <returns></returns>
    public static double ToArcLength(this OpenCvSharp.Point[] points, bool closed)
    {
        return Cv2.ArcLength(points, closed);
    }


    /// <summary>
    /// 识别图像中的基本形状
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static string ToContourConvexType(this OpenCvSharp.Point[] points)
    {
        if (Cv2.IsContourConvex(points))
        {
            // 凸形可能是：三角形、矩形、圆等
            OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(points, 0.02 * Cv2.ArcLength(points, true), true);

            switch (approx.Length)
            {
                case 3:
                    return "凸三角形";
                case 4:
                    return "凸四边形（可能是矩形或正方形）";
                default:
                    return "凸多边形（可能是圆形或椭圆）";
            }
        }
        else
        {
            return "凹形（如星形、C形、有缺口的物体）";
        }
    }

    /// <summary>
    /// 检查零件是否有凹陷或损坏
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static bool CheckPartQuality(this OpenCvSharp.Point[] points)
    {
        // 计算凸包
        var hull = Cv2.ConvexHull(points);
        // 判断原始轮廓是否为凸
        bool isConvex = Cv2.IsContourConvex(points);
        if (!isConvex)
        {
            // 计算凹陷面积（凸包面积 - 实际面积）
            double hullArea = Cv2.ContourArea(hull);
            double partArea = Cv2.ContourArea(points);
            double defectArea = hullArea - partArea;
            double defectRatio = defectArea / hullArea;

            Console.WriteLine($"发现缺陷，凹陷占比: {defectRatio:P1}");

            if (defectRatio > 0.1) // 超过10%的凹陷
            {
                Console.WriteLine("不合格：凹陷过大");
                return false;
            }
        }

        Console.WriteLine("合格");
        return true;
    }
}



