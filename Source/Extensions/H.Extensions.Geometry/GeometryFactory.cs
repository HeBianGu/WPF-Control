// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Extensions.Geometry;

public static class GeometryFactory
{
    #region - 基础 -
    public static System.Windows.Media.Geometry Create(string data)
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString(data) as System.Windows.Media.Geometry;
    }

    public static PathGeometry Create(Point start, bool isclose, params PathSegment[] segments)
    {
        PathGeometry geometry = new PathGeometry();
        geometry.Figures.Add(new PathFigure(start, segments, isclose));
        return geometry;
    }

    public static System.Windows.Media.Geometry CreateGroup(Action<GeometryGroup> action = null, params System.Windows.Media.Geometry[] geometries)
    {
        GeometryGroup group = new GeometryGroup();
        action?.Invoke(group);
        foreach (System.Windows.Media.Geometry item in geometries)
        {
            group.Children.Add(item);
        }
        return group;
    }

    public static System.Windows.Media.Geometry CreateGroup(params System.Windows.Media.Geometry[] geometries)
    {
        return CreateGroup(null, geometries);
    }

    public static System.Windows.Media.Geometry CreateGroup(params string[] datas)
    {
        GeometryConverter converter = new GeometryConverter();
        return CreateGroup(null, datas.Select(x => converter.ConvertFromString(x) as System.Windows.Media.Geometry).ToArray());
    }
    #endregion

    #region - 柱状图 -
    public static System.Windows.Media.Geometry Pillar => CreatePillar();
    public static System.Windows.Media.Geometry CreatePillar(double width = 100, double height = 60, double radiusX = 50, double radiusY = 10)
    {
        return CreateGroup(l => l.FillRule = FillRule.Nonzero, new RectangleGeometry(new Rect(0, 0, width, height), radiusX, radiusY), new EllipseGeometry(new Point(radiusX, radiusY), radiusX, radiusY));
    }
    #endregion

    #region - 双边矩形 -
    public static System.Windows.Media.Geometry LineRect => CreateLineRect();
    public static System.Windows.Media.Geometry CreateLineRect(double width = 100, double height = 60, double lineMargin = 10)
    {
        return CreateGroup(new RectangleGeometry(new Rect(0, 0, width, height)),
                           new LineGeometry(new Point(lineMargin, 0), new Point(lineMargin, height)),
                           new LineGeometry(new Point(width - lineMargin, 0), new Point(width - lineMargin, height)));
    }
    #endregion

    #region - 圆 -
    public static System.Windows.Media.Geometry Circle => CreateCircle();
    public static System.Windows.Media.Geometry CreateCircle(double len = 35)
    {
        return new EllipseGeometry(new Point(len, len), len, len);
    }
    #endregion 

    #region - 跑道 -
    public static System.Windows.Media.Geometry Runway => CreateRunway();

    public static System.Windows.Media.Geometry CreateRunway(double width = 70, double height = 60)
    {
        return new RectangleGeometry(new Rect(0, 0, width, height), height / 2.0, height / 2.0);
    }
    #endregion

    #region - 圆角 -
    public static System.Windows.Media.Geometry CornerRadius => CreateCornerRadius();

    public static System.Windows.Media.Geometry CreateCornerRadius(double width = 70, double height = 60, double cornerRadius = 5)
    {
        return new RectangleGeometry(new Rect(0, 0, width, height), cornerRadius, cornerRadius);
    }
    #endregion

    public static System.Windows.Media.Geometry File => Create("M0,0 L100,0 100,50 C75,30 25,80 0,50 L0,50 z");
    public static System.Windows.Media.Geometry Hexagon => Create("M0,30 20,0 80,0 100,30 80,60 20,60 Z");
    public static System.Windows.Media.Geometry Wave => Create("M0,10 C25,30 75,-10 100,10 L100,60 C75,30 25,80 0,50 Z");
    public static System.Windows.Media.Geometry Parallelogram => Create("M15,0 100,0 85,60 0,60 Z");
    public static System.Windows.Media.Geometry Diamond => Create("M0,30 50,0 100,30 50,60 Z");


}
