// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace H.Controls.Diagram.LinkDrawers;

public interface ILinkDrawer
{
    Diagram Diagram { get; set; }

    Geometry DrawPath(Link link, out Point center);

    bool IsUseArrow { get; set; }
}

public abstract class LinkDrawer : DependencyObject, ILinkDrawer
{
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    public Diagram Diagram { get; set; }

    [Display(Name = "连线环绕边框", GroupName = "基础信息", Order = 0)]
    public bool IsLinkCrossBound
    {
        get { return (bool)GetValue(IsLinkCrossBoundProperty); }
        set { SetValue(IsLinkCrossBoundProperty, value); }
    }


    public static readonly DependencyProperty IsLinkCrossBoundProperty =
        DependencyProperty.Register("IsLinkCrossBound", typeof(bool), typeof(LinkDrawer), new PropertyMetadata(true, (d, e) =>
        {
            Layout control = d as Layout;

            if (control == null) return;

            //bool config = e.NewValue as bool;

            //control.Diagram?.RefreshLayout();

            control.Diagram?.RefreshLinkDrawer();

        }));


    [Display(Name = "显示箭头", GroupName = "基础信息", Order = 0)]
    public bool IsUseArrow
    {
        get { return (bool)GetValue(IsUseArrowProperty); }
        set { SetValue(IsUseArrowProperty, value); }
    }


    public static readonly DependencyProperty IsUseArrowProperty =
        DependencyProperty.Register("IsUseArrow", typeof(bool), typeof(LinkDrawer), new FrameworkPropertyMetadata(true, (d, e) =>
         {
             LinkDrawer control = d as LinkDrawer;

             if (control == null) return;

             if (e.OldValue is bool o)
             {

             }

             if (e.NewValue is bool n)
             {

             }

             control.Diagram?.RefreshLinkDrawer();

         }));

    //[Display(Name = "显示箭头", GroupName = "基础信息", Order = 0)]
    //public bool IsUseArrow { get; set; } = true;

    [Display(Name = "箭头角度", GroupName = "基础信息", Order = 0)]
    public double ArrowAngle
    {
        get { return (double)GetValue(ArrowAngleProperty); }
        set { SetValue(ArrowAngleProperty, value); }
    }


    public static readonly DependencyProperty ArrowAngleProperty =
        DependencyProperty.Register("ArrowAngle", typeof(double), typeof(LinkDrawer), new FrameworkPropertyMetadata(Math.PI / 6, (d, e) =>
         {
             LinkDrawer control = d as LinkDrawer;

             if (control == null) return;

             if (e.OldValue is double o)
             {

             }

             if (e.NewValue is double n)
             {

             }

         }));



    [Display(Name = "箭头长度", GroupName = "基础信息", Order = 0)]
    public double ArrowLength
    {
        get { return (double)GetValue(ArrowLengthProperty); }
        set { SetValue(ArrowLengthProperty, value); }
    }


    public static readonly DependencyProperty ArrowLengthProperty =
        DependencyProperty.Register("ArrowLength", typeof(double), typeof(LinkDrawer), new FrameworkPropertyMetadata(5.0, (d, e) =>
         {
             LinkDrawer control = d as LinkDrawer;

             if (control == null) return;

             if (e.OldValue is double o)
             {

             }

             if (e.NewValue is double n)
             {

             }

         }));

    [Display(Name = "显示名称", GroupName = "基础信息", Order = 0)]
    public string DisplayName { get; set; }

    public abstract Geometry DrawPath(Link link, out Point center);

    /// <summary> 绘制箭头 </summary>
    protected Point[] GetArrowLinePoints(double x1, double y1, double x2, double y2)
    {
        Point point1 = new Point(x1, y1);     // 箭头起点
        Point point2 = new Point(x2, y2);     // 箭头终点
        if (!this.IsUseArrow) return new Point[] { point1, point2 };
        Point[] arrows = this.GetArrowPoints(x1, y1, x2, y2);
        return new Point[] { point1, point2, arrows[0], arrows[1], arrows[2] };
    }

    protected Point[] GetArrowPoints(double x1, double y1, double x2, double y2)
    {
        Point point1 = new Point(x1, y1);     // 箭头起点
        Point point2 = new Point(x2, y2);     // 箭头终点

        bool b = x2 == x1;
        double aa = Math.Atan(Math.PI / 2);

        double angleOri = x2 == x1 ? Math.Atan(y2 > y1 ? double.NegativeInfinity : double.PositiveInfinity) : Math.Atan((y2 - y1) / (x2 - x1));      // 起始点线段夹角
        double angleDown = angleOri - this.ArrowAngle;   // 箭头扩张角度
        double angleUp = angleOri + this.ArrowAngle;     // 箭头扩张角度
        int directionFlag = x2 > x1 ? -1 : 1;     // 方向标识

        double x3 = x2 + directionFlag * this.ArrowLength * Math.Cos(angleDown);   // 箭头第三个点的坐标
        double y3 = y2 + directionFlag * this.ArrowLength * Math.Sin(angleDown);

        double x4 = x2 + directionFlag * this.ArrowLength * Math.Cos(angleUp);     // 箭头第四个点的坐标
        double y4 = y2 + directionFlag * this.ArrowLength * Math.Sin(angleUp);

        Point point3 = new Point(x3, y3);   // 箭头第三个点
        Point point4 = new Point(x4, y4);   // 箭头第四个点

        return new Point[] { point3, point4, point2 };   // 多边形，起点 --> 终点 --> 第三点 --> 第四点 --> 终点

    }

    protected Geometry GetPolyLineGeometry(params Point[] points)
    {
        return this.GetPolyLineGeometry(false, false, points);
    }

    protected Geometry GetPolyLineGeometry(bool filled, bool closed, params Point[] points)
    {
        StreamGeometry geometry = new StreamGeometry();
        using (StreamGeometryContext ctx = geometry.Open())
        {
            Point first = points.FirstOrDefault();
            // Begin the triangle at the point specified. Notice that the shape is set to 
            // be closed so only two lines need to be specified below to make the triangle.
            ctx.BeginFigure(first, filled /* is filled */, closed /* is closed */);
            ctx.PolyLineTo(points, true, true);
        }
        //geometry.Freeze();
        return geometry;
    }

    protected Geometry GetArrowGeometry(Geometry geo, Point start, Point end)
    {
        PathGeometry pathGeometry = PathGeometry.CreateFromGeometry(geo);
        Point[] arrowPoints = this.GetArrowPoints(start.X, start.Y, end.X, end.Y);
        Geometry arrowGeo = this.GetPolyLineGeometry(true, true, arrowPoints);
        pathGeometry.AddGeometry(arrowGeo);
        pathGeometry.Freeze();
        return pathGeometry;
    }


    //protected Geometry DrawPolyLine(params Point[] points)
    //{
    //    PolyLineSegment pls = new PolyLineSegment(points, false);

    //    PathFigure pf = new PathFigure();
    //    pf.StartPoint = pls.Points.FirstOrDefault();
    //    pf.IsClosed = false;
    //    pf.Segments.Add(pls);
    //    return new PathGeometry(new List<PathFigure>() { pf });
    //}

    //public virtual Point GetCrossBoundStartPoint(Point from, Point to,)
    //{
    //    Point from = LinkLayer.GetStart(link);
    //    Point to = LinkLayer.GetEnd(link);

    //    if (this.IsLinkCrossBound == true)
    //    {
    //        Vector? find_start = Intersects(link.FromPort == null ? link.FromNode.Bound : link.FromPort.Bound, from, to);

    //        if (find_start.HasValue)
    //        {
    //            return (Point)find_start.Value;
    //        }
    //    }

    //    return from;
    //}

    //public virtual Point GetCrossBoundEndPoint(Link link)
    //{
    //    Point from = LinkLayer.GetStart(link);
    //    Point to = LinkLayer.GetEnd(link);

    //    if (this.IsLinkCrossBound == true)
    //    {
    //        //  Do ：设置End点交点 
    //        Vector? find_end = Intersects(link.ToPort == null ? link.ToNode.Bound : link.ToPort.Bound, from, to);

    //        if (find_end.HasValue)
    //        {
    //            return (Point)find_end.Value;
    //        }
    //    }
    //    return to;
    //}


    /// <summary> 矩形和线段求交点 </summary>
    protected Vector? Intersects(Rect rect, Point start, Point end)
    {
        {
            Vector a1 = (Vector)rect.TopLeft;
            Vector a2 = (Vector)rect.TopRight;

            Vector b1 = (Vector)start;
            Vector b2 = (Vector)end;

            Vector? v = Intersects(a1, a2, b1, b2);

            if (v != null) return v.Value;
        }

        {
            Vector a1 = (Vector)rect.TopRight;
            Vector a2 = (Vector)rect.BottomRight;

            Vector b1 = (Vector)start;
            Vector b2 = (Vector)end;

            Vector? v = Intersects(a1, a2, b1, b2);

            if (v != null) return v.Value;
        }

        {
            Vector a1 = (Vector)rect.BottomRight;
            Vector a2 = (Vector)rect.BottomLeft;

            Vector b1 = (Vector)start;
            Vector b2 = (Vector)end;

            Vector? v = Intersects(a1, a2, b1, b2);

            if (v != null) return v.Value;
        }

        {
            Vector a1 = (Vector)rect.BottomLeft;
            Vector a2 = (Vector)rect.TopLeft;

            Vector b1 = (Vector)start;
            Vector b2 = (Vector)end;

            Vector? v = Intersects(a1, a2, b1, b2);

            if (v != null) return v.Value;
        }

        return null;
    }

    /// <summary>
    /// Get Intersection point
    /// </summary>
    /// <param name="a1">a1 is line1 start</param>
    /// <param name="a2">a2 is line1 end</param>
    /// <param name="b1">b1 is line2 start</param>
    /// <param name="b2">b2 is line2 end</param>
    /// <returns></returns>
    private Vector? Intersects(Vector a1, Vector a2, Vector b1, Vector b2)
    {
        Vector b = a2 - a1;
        Vector d = b2 - b1;
        double bDotDPerp = b.X * d.Y - b.Y * d.X;

        // if b dot d == 0, it means the lines are parallel so have infinite intersection points
        if (bDotDPerp == 0)
            return null;

        Vector c = b1 - a1;
        double t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
        if (t < 0 || t > 1)
            return null;

        double u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
        return u < 0 || u > 1 ? null : a1 + t * b;
    }

    public override string ToString()
    {
        return this.DisplayName;
    }
}
