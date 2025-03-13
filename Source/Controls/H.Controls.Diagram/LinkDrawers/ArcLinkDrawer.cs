// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Controls.Diagram.LinkDrawers;

public class ArcLinkDrawer : LinkDrawer
{
    public ArcLinkDrawer()
    {
        this.DisplayName = "弧线";
    }

    private Orientation _orientation;
    [Display(Name = "圆弧方向", GroupName = "基础信息", Order = 0)]
    public Orientation Orientation
    {
        get { return _orientation; }
        set
        {
            _orientation = value;
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

        Point point = this.Orientation == Orientation.Vertical ? new Point(start.X, end.Y) : new Point(end.X, start.Y);

        if (this.IsLinkCrossBound == true)
        {
            Vector? find_start = Intersects(link.FromPort == null ? link.FromNode.Bound : link.FromPort.Bound, start, point);

            if (find_start.HasValue)
                start = (Point)find_start.Value;

            Vector? find_end = Intersects(link.ToPort == null ? link.ToNode.Bound : link.ToPort.Bound, point, end);
            if (find_end.HasValue)
                end = (Point)find_end.Value;
        }
        Geometry geo = GetDrawBezierGeometry(start, point, end);
        if (this.IsUseArrow == false)
            return geo;
        Vector vector = end - point;
        Vector normalize = vector / vector.Length;
        Point end1 = end - normalize * this.ArrowLength;
        return this.GetArrowGeometry(geo, end1, end);
    }

    private Geometry GetDrawBezierGeometry(Point p1, Point p2, Point p3)
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
            //    Vector vector = p3 - p2;
            //    Vector normalize = vector / vector.Length;
            //    Point end = p3 - normalize * this.ArrowLength;
            //    //Size size = new Size(vector.X, vector.Y);
            //    //ctx.ArcTo(p1, size, 80, true, SweepDirection.Counterclockwise, true, true);
            //    ctx.BezierTo(p1, p2, end, true, true);
            //    Point[] arrow = this.GetArrowLinePoints(end.X, end.Y, p3.X, p3.Y);
            //    ctx.PolyLineTo(arrow, true, true);
            //}
            //else
            //{
            ctx.BezierTo(p1, p2, p3, true, true);
            //}
        }

        geometry.Freeze();
        return geometry;
    }
}
