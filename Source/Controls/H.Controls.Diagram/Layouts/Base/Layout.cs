// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Mvvm.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Layouts.Base;

/// <summary> 布局  设置排列方式 </summary>
public abstract class Layout : BindableBase, ILayout
{
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    public Diagram Diagram { get; set; }

    /// <summary> 布局点和线 </summary>
    public abstract void DoLayout(params Node[] nodes);

    /// <summary> 设置Node外边框 </summary>
    protected internal virtual void DoLayoutNode(Node node)
    {
        Point postion = NodeLayer.GetPosition(node);
        node.MeasureBound(postion);
    }

    public virtual void UpdateNode(params Node[] nodes)
    {
        foreach (Node node in nodes)
        {
            this.DoLayoutNode(node);
            this.DoLayoutPort(node);
            this.DoLayoutLink(node);
        }
    }

    public virtual void RemoveNode(params Node[] nodes)
    {
        //  Do ：默认全部刷新
        this.DoLayout(this.Diagram.Nodes.ToArray());
    }

    protected virtual void DoLayoutPort(Node node)
    {
        //System.Diagnostics.Debug.WriteLine("DoLayoutPort");
        List<Port> ports = node.GetPorts();
        Point point = NodeLayer.GetPosition(node);
        //  Do ：更新Port 左
        List<Port> leftPorts = ports.Where(l => l.Dock == Dock.Left)?.ToList();
        double height_left_span = node.DesiredSize.Height / (leftPorts.Count() + 1);
        for (int i = 0; i < leftPorts.Count; i++)
        {
            Port port = leftPorts[i];
            //Rect rect = new Rect(new Point(0, height_left_span * (i + 1) - port.DesiredSize.Height / 2), port.DesiredSize);

            //port.Arrange(rect); 

            //port.Margin = new Thickness(100, 40, 0, 0);
            port.Measure();
            Node.SetPosition(port, new Point(0, height_left_span * (i + 1) - port.DesiredSize.Height / 2));
            //  Do ：设置范围
            Point port_point = new Point(point.X - node.DesiredSize.Width / 2.0 + port.DesiredSize.Width / 2, point.Y - node.DesiredSize.Height / 2.0 + height_left_span * (i + 1));
            port.MeasureBound(port_point);
            NodeLayer.SetPosition(port, port_point);
        }

        //  Do ：更新Port 右
        List<Port> rightPorts = ports.Where(l => l.Dock == Dock.Right)?.ToList();

        double height_right_span = node.DesiredSize.Height / (rightPorts.Count() + 1);

        for (int i = 0; i < rightPorts.Count; i++)
        {
            Port port = rightPorts[i];

            //Rect rect = new Rect(new Point(node.DesiredSize.Width - port.DesiredSize.Width, height_right_span * (i + 1) - port.DesiredSize.Height / 2), port.DesiredSize);

            //port.Arrange(rect);

            port.Measure();

            Node.SetPosition(port, new Point(node.DesiredSize.Width - port.DesiredSize.Width, height_right_span * (i + 1) - port.DesiredSize.Height / 2));

            //  Do ：设置范围
            Point port_point = new Point(point.X + node.DesiredSize.Width / 2.0 - +port.DesiredSize.Width / 2, point.Y - node.DesiredSize.Height / 2.0 + height_right_span * (i + 1));

            port.MeasureBound(port_point);

            NodeLayer.SetPosition(port, port_point);

            //Debug.WriteLine(port.ToString());
        }

        //  Do ：更新Port 上
        List<Port> topPorts = ports.Where(l => l.Dock == Dock.Top)?.ToList();

        double height_top_span = node.DesiredSize.Width / (topPorts.Count() + 1);

        for (int i = 0; i < topPorts.Count; i++)
        {
            Port port = topPorts[i];

            //Rect rect = new Rect(new Point(height_top_span * (i + 1) - port.DesiredSize.Width / 2, 0), port.DesiredSize);

            //port.Arrange(rect);

            port.Measure();

            Node.SetPosition(port, new Point(height_top_span * (i + 1) - port.DesiredSize.Width / 2, 0));


            //  Do ：设置范围
            Point port_point = new Point(point.X - node.DesiredSize.Width / 2.0 + height_top_span * (i + 1), point.Y - node.DesiredSize.Height / 2.0 + port.DesiredSize.Height / 2);

            port.MeasureBound(port_point);

            NodeLayer.SetPosition(port, port_point);

            //Debug.WriteLine(port.ToString());
        }

        //  Do ：更新Port 下
        List<Port> bottomPorts = ports.Where(l => l.Dock == Dock.Bottom)?.ToList();

        double height_bottom_span = node.DesiredSize.Width / (bottomPorts.Count() + 1);

        for (int i = 0; i < bottomPorts.Count; i++)
        {
            Port port = bottomPorts[i];

            //Rect rect = new Rect(new Point(height_bottom_span * (i + 1) - port.DesiredSize.Width / 2, node.DesiredSize.Height - port.DesiredSize.Height), port.DesiredSize);

            //port.Arrange(rect);

            port.Measure();

            Node.SetPosition(port, new Point(height_bottom_span * (i + 1) - port.DesiredSize.Width / 2, node.DesiredSize.Height - port.DesiredSize.Height));

            //  Do ：设置范围
            Point port_point = new Point(point.X - node.DesiredSize.Width / 2.0 + height_bottom_span * (i + 1), point.Y + node.DesiredSize.Height / 2.0 - port.DesiredSize.Height / 2);

            port.MeasureBound(port_point);

            NodeLayer.SetPosition(port, port_point);

            //Debug.WriteLine(port.ToString());
        }
    }

    protected virtual void DoLayoutLink(Node node)
    {

        //System.Diagnostics.Debug.WriteLine("DoLayoutLink");

        ////  Do ：布局Link 
        //foreach (Link link in node.LinksInto)
        //{
        //    this.DoLayoutLink(link);
        //}

        //foreach (Link link in node.LinksOutOf)
        //{
        //    this.DoLayoutLink(link);
        //}

        //foreach (Link link in node.Links)
        //{
        //    this.DoLayoutLink(link);
        //}

        IEnumerable<Link> links = node.GetAllLinks();

        foreach (Link item in links)
        {
            this.DoLayoutLink(item);
        }
    }

    public virtual void DoLayoutLink(Link link)
    {
        if (link.ToNode == null || link.FromNode == null) return;

        Point from = NodeLayer.GetPosition(link.FromNode);
        Point to = NodeLayer.GetPosition(link.ToNode);

        if (link.ToPort != null)
        {
            //to = NodeLayer.GetPosition(link.ToPort);
            Point point = NodeLayer.GetPosition(link.ToPort);
            Thickness thickness = link.ToPort.Margin;
            to = new Point(point.X + (thickness.Left - thickness.Right) / 2, point.Y + (thickness.Top - thickness.Bottom) / 2);
        }

        if (link.FromPort != null)
        {
            //from = NodeLayer.GetPosition(link.FromPort);
            Point point = NodeLayer.GetPosition(link.FromPort);
            Thickness thickness = link.FromPort.Margin;
            from = new Point(point.X + (thickness.Left - thickness.Right) / 2, point.Y + (thickness.Top - thickness.Bottom) / 2);
        }


        //if (this.IsLinkCrossBound == true)
        //{
        //    //  Do ：设置End点交点 
        //    Vector? find_end = Intersects(link.ToPort == null ? link.ToNode.Bound : link.ToPort.Bound, from, to);

        //    if (find_end.HasValue)
        //    {
        //        LinkLayer.SetEnd(link, (Point)find_end.Value);
        //    }

        //    //  Do ：设置起始点交点 
        //    Vector? find_start = Intersects(link.FromPort == null ? link.FromNode.Bound : link.FromPort.Bound, from, to);

        //    if (find_start.HasValue)
        //    {
        //        LinkLayer.SetStart(link, (Point)find_start.Value);
        //    }
        //}
        //else
        //{
        LinkLayer.SetEnd(link, to);
        LinkLayer.SetStart(link, from);
        link.Update();
        //}


        //Debug.WriteLine(link.ToString());
    }

    public virtual void AddNode(params Node[] nodes)
    {
        //  Do ：默认全部刷新
        this.DoLayout(this.Diagram.Nodes.ToArray());
    }
}
