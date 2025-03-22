// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.Diagram.LinkDrawers;

/// <summary>
//根据箭头发向，创建内联点。 
//创建内联点的两种连接方式。 
//判断是否经过起点终点，如果只有一种方式满足，输出这种方式；如果都满足，转到4；如果都不满足，转到5。 
//判断哪种方式弯折次数少，输出这种方式。 
//添加一条平行于箭头的线，连接内联点和线的两端。输出这种连接方式。 
/// </summary>
public class BrokenLinkDrawer : LinkDrawer
{
    public BrokenLinkDrawer()
    {
        this.DisplayName = "折线";
    }
    //public int InnerSpan
    //{ get; set; } = 30;


    private int _innerSpan = 30;
    [Display(Name = "内部间隔", GroupName = "折线信息", Order = 0)]
    public int InnerSpan
    {
        get { return _innerSpan; }
        set
        {
            _innerSpan = value;
            RaisePropertyChanged();
            this.Diagram?.RefreshLinkDrawer();
        }
    }

    public override Geometry DrawPath(Link link, out Point center)
    {
        Point start = LinkLayer.GetStart(link);
        Point end = LinkLayer.GetEnd(link);
        Vector v1 = end - start;
        center = new Point(v1.X / 2 + start.X, v1.Y / 2 + start.Y);
        List<Point> points = new List<Point>();

        //  Do ：创建内连点
        Point inner1 = link.FromPort != null ? link.FromPort.ChangedPoint(start, this.InnerSpan) : start;
        Point inner2 = link.ToPort != null ? link.ToPort.ChangedPoint(end, this.InnerSpan) : end;

        if (this.IsLinkCrossBound == true)
        {
            Vector? find_start = Intersects(link.FromPort == null ? link.FromNode.Bound : link.FromPort.Bound, start, inner1);

            if (find_start.HasValue)
                start = (Point)find_start.Value;

            Vector? find_end = Intersects(link.ToPort == null ? link.ToNode.Bound : link.ToPort.Bound, inner2, end);
            if (find_end.HasValue)
                end = (Point)find_end.Value;
        }

        //  Do ：创建内连点连点方式 
        Point cross1 = new Point(inner1.X, inner2.Y);
        Point cross2 = new Point(inner2.X, inner1.Y);

        //  Do ：绘制箭头
        Vector v = inner2 - inner1;
        //Point[] arrow = link.Content is IFlowable ? this.GetArrowLinePoints(inner2.X, inner2.Y, end.X, end.Y) : new Point[] { inner2, end };
        bool isCross1 = !this.OnSegment(inner1, cross1, start) && !this.OnSegment(inner2, cross1, end);
        bool isCross2 = !this.OnSegment(inner1, cross2, start) && !this.OnSegment(inner2, cross2, end);
        if (isCross1 == true && isCross2 == false)
        {
            points.Add(start);
            if (!this.OnSegment(start, inner1, cross1))
                points.Add(inner1);
            points.Add(cross1);
            if (!this.OnSegment(end, inner2, cross1))
                points.Add(inner2);
            points.Add(end);
            center = this.GetCenter(points);
            return this.GetBrokenGeometry(points.ToArray());
        }

        if (isCross1 == false && isCross2 == true)
        {
            points.Add(start);
            if (!this.OnSegment(start, inner1, cross2))
                points.Add(inner1);
            points.Add(cross2);
            if (!this.OnSegment(end, inner2, cross2))
                points.Add(inner2);
            else
            {
                points.Add(cross1);
            }
            points.Add(end);
            center = this.GetCenter(points);
            return this.GetBrokenGeometry(points.ToArray());
        }

        //  ToDo ：取折次少的
        if (isCross1 == true && isCross2 == true)
        {
            List<Point> points1 = new List<Point>();
            points1.Add(start);
            if (!this.OnSegment(start, inner1, cross1))
                points1.Add(inner1);
            points1.Add(cross1);
            if (!this.OnSegment(end, inner2, cross1))
                points1.Add(inner2);
            points1.Add(end);
            List<Point> points2 = new List<Point>();
            points2.Add(start);

            if (!this.OnSegment(start, inner1, cross2))
                points2.Add(inner1);
            points2.Add(cross2);

            if (!this.OnSegment(end, inner2, cross2))
                points2.Add(inner2);
            points2.Add(end);
            int c1 = this.GetBrokenCount(points1.ToArray());
            int c2 = this.GetBrokenCount(points2.ToArray());

            if (c1 > c2)
            {
                center = this.GetCenter(points2);
                return this.GetBrokenGeometry(points2.ToArray());
            }
            else
            {
                center = this.GetCenter(points1);
                return this.GetBrokenGeometry(points1.ToArray());
            }
        }

        double liney = (inner1.Y + inner2.Y) / 2;
        Point lineyStart = new Point(inner1.X, liney);
        Point lineyEnd = new Point(inner2.X, liney);
        double linex = (inner1.X + inner2.X) / 2;
        Point linexStart = new Point(linex, inner1.Y);
        Point linexEnd = new Point(linex, inner2.Y);
        bool islinexCross = !this.OnSegment(inner1, lineyStart, start) && !this.OnSegment(inner2, lineyEnd, end);
        bool islineyCross = !this.OnSegment(inner1, linexStart, start) && !this.OnSegment(inner2, linexEnd, end);

        if (islinexCross)
        {
            points.Add(start);
            if (!this.OnSegment(start, inner1, lineyStart))
                points.Add(inner1);
            points.Add(lineyStart);
            points.Add(lineyEnd);
            if (!this.OnSegment(end, inner2, lineyEnd))
                points.Add(inner2);
            points.Add(end);
            center = this.GetCenter(points);
            return this.GetBrokenGeometry(points.ToArray());
        }
        else
        {
            points.Add(start);
            if (!this.OnSegment(start, inner1, linexStart))
                points.Add(inner1);
            points.Add(linexStart);
            points.Add(linexEnd);
            if (!this.OnSegment(end, inner2, linexEnd))
                points.Add(inner2);
            points.Add(end);
            center = this.GetCenter(points);
            return this.GetBrokenGeometry(points.ToArray());
        }
    }

    public Geometry GetBrokenGeometry(Point[] points)
    {
        Geometry geo = this.GetPolyLineGeometry(points.ToArray());
        if (this.IsUseArrow == false)
            return geo;
        Point start = points[points.Length - 2];
        Point end = points[points.Length - 1];
        return this.GetArrowGeometry(geo, start, end);
    }

    private Point GetCenter(List<Point> points)
    {
        if (points.Count > 2)
            return new Point(points[2].X / 2 + points[1].X / 2, points[2].Y / 2 + points[1].Y / 2);
        return points.Count > 1 ? points[1] : points[0];
        //if (points.Count > 5)
        //{
        //    points = points.Take(points.Count - 5).ToList();
        //}
        //if (points.Count % 2 == 0)
        //{
        //    var start = points.Count == 2 ? points[0] : points[points.Count / 2];
        //    var end = points.Count == 2 ? points[1] : points[points.Count / 2 + 1];
        //    return new Point(end.X / 2 + start.X / 2, end.Y / 2 + start.Y / 2);

        //}
        //if (points.Count == 1)
        //{
        //    return points[0];
        //}
        //int center = (points.Count - 1) / 2;
        //return points[center + 1];
    }

    /// <summary>
    /// 点是否在线段上
    /// </summary>
    /// <param name="pi"></param>
    /// <param name="pj"></param>
    /// <param name="Q"></param>
    /// <returns></returns>
    private bool OnSegment(Point pi, Point pj, Point Q)
    {
        return (Q.X - pi.X) * (pj.Y - pi.Y) == (pj.X - pi.X) * (Q.Y - pi.Y) && Math.Min(pi.X, pj.X) <= Q.X && Q.X <= Math.Max(pi.X, pj.X) && Math.Min(pi.Y, pj.Y) <= Q.Y && Q.Y <= Math.Max(pi.Y, pj.Y);
    }

    /// <summary>
    /// 获取折线个数
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    private int GetBrokenCount(Point[] points)
    {
        List<bool> rs = new List<bool>();

        for (int i = 0; i < points.Count(); i++)
        {
            if (i == 0 || i == 1) continue;

            Point p1 = points[i - 2];
            Point p2 = points[i - 1];
            Point c = points[i];

            bool r = this.OnSegment(p1, c, p2);

            rs.Add(r);
        }

        return rs.Count(l => l == false);
    }

    //private Path DrawLine(params Point[] points)
    //{
    //    Path path = new Path();

    //    PolyLineSegment pls = new PolyLineSegment();

    //    //  Do ：添加曲线
    //    foreach (Point item in points)
    //    {
    //        pls.Points.Add(item);
    //    }

    //    PathFigure pf = new PathFigure();
    //    pf.StartPoint = pls.Points.FirstOrDefault();
    //    pf.Segments.Add(pls);

    //    pf.IsClosed = false;
    //    pf.IsFilled = false;

    //    path.Data = new PathGeometry(new List<PathFigure>() { pf });

    //    StreamGeometry geometry = new StreamGeometry();
    //    geometry.

    //    return path;
    //}


}
