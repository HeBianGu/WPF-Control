// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Controls.Diagram.LinkDrawers;

public class BezierLinkDrawer : LinkDrawer
{
    public BezierLinkDrawer()
    {
        this.DisplayName = "贝塞尔曲线";
    }

    private double _span = 50.0;
    public double Span
    {
        get { return _span; }
        set
        {
            _span = value;
            RaisePropertyChanged();
            this.Diagram?.RefreshLinkDrawer();
        }
    }

    public override Geometry DrawPath(Link link, out Point center)
    {
        Point start = LinkLayer.GetStart(link);
        Point end = LinkLayer.GetEnd(link);
        Vector v = end - start;
        center = new Point(v.X / 2 + start.X, v.Y / 2 + start.Y);
        double w = v.X / 5.0;
        double h = v.Y / 5.0;

        w = 0;
        h = 0;

        Point point2 = new Point(start.X + (end.X - start.X) / 2.0, start.Y + (end.Y - start.Y) / 2.0);
        Point point1 = link.FromPort != null ? link.FromPort.ChangedPoint(start, this.Span) : new Point(start.X + w, start.Y);
        Point point3 = link.ToPort != null ? link.ToPort.ChangedPoint(end, this.Span) : new Point(end.X - w, end.Y);

        if (this.IsLinkCrossBound == true)
        {
            Vector? find_start = Intersects(link.FromPort == null ? link.FromNode.Bound : link.FromPort.Bound, start, point2);

            if (find_start.HasValue)
                start = (Point)find_start.Value;

            Vector? find_end = Intersects(link.ToPort == null ? link.ToNode.Bound : link.ToPort.Bound, point3, end);
            if (find_end.HasValue)
                end = (Point)find_end.Value;
        }

        Geometry geo = this.DrawBezier(start, point1, point2, point3, end);
        if (this.IsUseArrow == false)
            return geo;
        Vector vector = end - point3;
        Vector normalize = vector / vector.Length;
        Point end1 = end - normalize * this.ArrowLength;
        return this.GetArrowGeometry(geo, end1, end);
    }

    private Geometry DrawBezier(Point p1, Point p2, Point p3, Point p4, Point p5)
    {
        StreamGeometry geometry = new StreamGeometry();
        using (StreamGeometryContext ctx = geometry.Open())
        {
            // Begin the triangle at the point specified. Notice that the shape is set to 
            // be closed so only two lines need to be specified below to make the triangle.
            ctx.BeginFigure(p1, false /* is filled */, false /* is closed */);
            ////  Do ：添加箭头
            //if (this.IsUseArrow)
            //{
            //    Vector vector = p5 - p4;
            //    Vector normalize = vector / vector.Length;
            //    Point end = p5 - normalize * this.ArrowLength;
            //    ctx.BezierTo(p3, p4, end, true, true);
            //    Point[] arrow = this.GetArrowLinePoints(end.X, end.Y, p5.X, p5.Y);
            //    ctx.PolyLineTo(arrow, true, true);
            //}
            //else
            //{
            ctx.BezierTo(p3, p4, p5, true, true);
            //}
        }

        geometry.Freeze();
        return geometry;


    }

    //private Geometry DrawBezier(Point p1, Point p2, Point p3, Point p4, Point p5)
    //{
    //    //Path path = new Path();

    //    PathFigure pf = new PathFigure();

    //    {
    //        BezierSegment bzs = new BezierSegment(p1, p2, p3, true);

    //        pf.Segments.Add(bzs);
    //    }

    //    //  Do ：添加箭头
    //    if (this.IsUseArrow)
    //    {
    //        Vector vector = p5 - p4;

    //        Vector normalize = vector / vector.Length;

    //        Point end = p5 - normalize * this.ArrowLength;

    //        BezierSegment bzs = new BezierSegment(p3, p4, end, true);

    //        pf.Segments.Add(bzs);


    //        PolyLineSegment pls = new PolyLineSegment();

    //        Point[] arrow = this.DrawArrow(end.X, end.Y, p5.X, p5.Y);

    //        //  Do ：添加曲线
    //        foreach (Point item in arrow)
    //        {
    //            pls.Points.Add(item);
    //        }

    //        pf.Segments.Add(pls);
    //    }
    //    else
    //    {
    //        BezierSegment bzs = new BezierSegment(p3, p4, p5, true);

    //        pf.Segments.Add(bzs);
    //    }

    //    pf.StartPoint = p1;

    //    pf.IsClosed = false;
    //    //  Do ：设置true性能会下降
    //    pf.IsFilled = false;

    //    PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

    //    //path.Data = pg;

    //    return pg;
    //}

}
