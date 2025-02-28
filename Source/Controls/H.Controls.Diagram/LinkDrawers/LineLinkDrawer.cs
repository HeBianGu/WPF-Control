// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Controls.Diagram.LinkDrawers;

public class LineLinkDrawer : LinkDrawer
{
    public LineLinkDrawer()
    {
        this.DisplayName = "直线";
    }
    public override Geometry DrawPath(Link link, out Point center)
    {
        Point start = LinkLayer.GetStart(link);
        Point end = LinkLayer.GetEnd(link);
        Vector v = end - start;
        center = new Point(v.X / 2 + start.X, v.Y / 2 + start.Y);
        if (this.IsLinkCrossBound == true)
        {
            Vector? find_start = Intersects(link.FromPort == null ? link.FromNode.Bound : link.FromPort.Bound, start, end);

            if (find_start.HasValue)
                start = (Point)find_start.Value;

            Vector? find_end = Intersects(link.ToPort == null ? link.ToNode.Bound : link.ToPort.Bound, start, end);
            if (find_end.HasValue)
                end = (Point)find_end.Value;
        }

        return this.GetLineGeometry(start.X, start.Y, end.X, end.Y);
    }

    protected Geometry GetLineGeometry(double x1, double y1, double x2, double y2)
    {
        Geometry geo = this.GetPolyLineGeometry(new Point(x1, y1), new Point(x2, y2));
        return !this.IsUseArrow ? geo : this.GetArrowGeometry(geo, new Point(x1, y1), new Point(x2, y2));
    }
}
