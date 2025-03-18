// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Controls.Diagram.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Controls.Diagram.Parts;

public interface INode
{

}

/// <summary> 节点 </summary>
public partial class Node : FlowablePart, INode
{
    public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Node), "S.Node.Default");

    static Node()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Node), new FrameworkPropertyMetadata(typeof(Node)));
    }



    public string Id => this.GetContent<INodeData>().ID;

    public static readonly DependencyProperty PositionProperty = DependencyProperty.RegisterAttached(
        "Position", typeof(Point), typeof(Node), new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPositionChanged));

    public static Point GetPosition(DependencyObject d)
    {
        return (Point)d.GetValue(PositionProperty);
    }

    public static void SetPosition(DependencyObject obj, Point value)
    {
        obj.SetValue(PositionProperty, value);
    }

    private static void OnPositionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }


    public Point Location
    {
        get { return (Point)GetValue(LocationProperty); }
        set { SetValue(LocationProperty, value); }
    }


    public static readonly DependencyProperty LocationProperty =
        DependencyProperty.Register("Location", typeof(Point), typeof(Node), new FrameworkPropertyMetadata(default(Point)));

    public List<Link> LinksInto { get; set; } = new List<Link>();
    public List<Link> LinksOutOf { get; set; } = new List<Link>();
    public List<Link> ConnectLinks { get; set; } = new List<Link>();
    public static readonly DependencyProperty IsDraggingProperty = DependencyProperty.RegisterAttached(
        "IsDragging", typeof(bool), typeof(Node), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDraggingChanged));

    public static bool GetIsDragging(DependencyObject d)
    {
        return (bool)d.GetValue(IsDraggingProperty);
    }

    public static void SetIsDragging(DependencyObject obj, bool value)
    {
        obj.SetValue(IsDraggingProperty, value);
    }

    private static void OnIsDraggingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }



    protected virtual bool GetVisiblity()
    {
        return true;
    }

    public IEnumerable<Link> GetAllLinks()
    {
        foreach (Link item in this.LinksInto)
        {
            yield return item;
        }
        foreach (Link item in this.LinksOutOf)
        {
            yield return item;
        }
        foreach (Link item in this.ConnectLinks)
        {
            yield return item;
        }
    }

    public override void Delete()
    {
        Link[] links = this.GetAllLinks().ToArray();
        foreach (Link item in links)
        {
            item.Delete();
        }

        this.Clear();

        //  Do ：删除Node数据源
        Diagram diagram = this.GetParent<Diagram>();
        diagram?.RemoveNode(this);
    }

    public override void Clear()
    {
        //  Do ：删除关联Link
        foreach (Link link in this.LinksInto)
        {
            link.FromNode?.LinksOutOf?.Remove(link);
        }

        foreach (Link link in this.LinksOutOf)
        {
            link.ToNode?.LinksInto?.Remove(link);
        }

        foreach (Link link in this.ConnectLinks)
        {
            link.ToNode?.ConnectLinks?.Remove(link);
            link.FromNode?.ConnectLinks?.Remove(link);
        }

        this.LinksOutOf.Clear();
        this.LinksInto.Clear();
        this.ConnectLinks.Clear();

        //this.Ports.Clear();
    }

    public override string ToString()
    {
        return $"{this.GetType().Name} - {this.Id}:{NodeLayer.GetPosition(this)}";
    }


    public List<Node> GetToNodes(Func<Node, bool> filter = null)
    {
        return this.LinksOutOf.Select(k => k.ToNode).Where(filter ?? new Func<Node, bool>(l => true))?.ToList();
    }

    public List<Node> GetFromNodes(Func<Node, bool> filter = null)
    {
        return this.LinksInto.Select(k => k.FromNode).Where(filter ?? new Func<Node, bool>(l => true))?.ToList();
    }

    public IEnumerable<Part> GetAllParts(Func<Part, bool> filter = null)
    {
        yield return this;
        foreach (Port item in this._ports)
        {
            if (filter?.Invoke(item) != false)
                yield return item;
        }
        foreach (Link item in this.LinksOutOf)
        {
            if (filter?.Invoke(item) != false)
                yield return item;
        }
        List<Node> toNodes = this.GetToNodes();
        foreach (Node item in toNodes)
        {
            IEnumerable<Part> parts = item.GetAllParts(filter);
            foreach (Part part in parts)
            {
                yield return part;
            }
        }
    }

    public IEnumerable<Part> GetParts(Func<Part, bool> filter = null)
    {
        yield return this;
        foreach (Port item in this._ports)
        {
            if (filter?.Invoke(item) != false)
                yield return item;
        }
        foreach (Link item in this.LinksOutOf)
        {
            if (filter?.Invoke(item) != false)
                yield return item;
        }
    }

    private List<Port> _ports = new List<Port>();

    public void AddPort(Port port)
    {
        _ports.Add(port);
        this.AddVisualChild(port);
        this.AddLogicalChild(port);
    }

    public void RemovePort(Port port)
    {
        _ports.Remove(port);
        this.RemoveVisualChild(port);
        this.RemoveLogicalChild(port);
    }

    public List<Port> GetPorts(Func<Port, bool> predicate = null)
    {
        return predicate == null ? (this._ports.Where(l => true)?.ToList()) : (this._ports.Where(predicate)?.ToList());
    }

    public void AligmentLayout()
    {
        foreach (Link link in this.LinksInto)
        {
            double spanHeight = ((link.FromNode.ActualHeight + this.ActualHeight) / 2) + 60;
            double spanWidth = ((link.FromNode.ActualWidth + this.ActualWidth) / 2) + 60;
            double x = link.FromNode.Location.X;
            double y = link.FromNode.Location.Y;

            if (link.ToPort == null)
                continue;
            {
                if (link.ToPort.Dock == Dock.Left)
                {
                    x = link.FromNode.Location.X + spanWidth;
                }

                if (link.ToPort.Dock == Dock.Right)
                {
                    x = link.FromNode.Location.X - spanWidth;
                }

                if (link.ToPort.Dock == Dock.Top)
                {
                    y = link.FromNode.Location.Y + spanHeight;
                }

                if (link.ToPort.Dock == Dock.Bottom)
                {
                    y = link.FromNode.Location.Y - spanHeight;
                }
            }

            {
                if (link.FromPort.Dock == Dock.Left)
                {
                    x = link.FromNode.Location.X - spanWidth;
                }

                if (link.FromPort.Dock == Dock.Right)
                {
                    x = link.FromNode.Location.X + spanWidth;
                }

                if (link.FromPort.Dock == Dock.Top)
                {
                    y = link.FromNode.Location.Y - spanHeight;
                }

                if (link.FromPort.Dock == Dock.Bottom)
                {
                    y = link.FromNode.Location.Y + spanHeight;
                }
            }

            if (link.ToPort.Dock == link.FromPort.Dock)
            {
                if (link.ToPort.Dock == Dock.Top || link.ToPort.Dock == Dock.Bottom)
                {
                    x = link.FromNode.Location.X + spanWidth;
                }
                if (link.ToPort.Dock == Dock.Bottom)
                {
                    x = link.FromNode.Location.X + spanWidth;
                }
                if (link.ToPort.Dock == Dock.Left || link.ToPort.Dock == Dock.Right)
                {
                    y = link.FromNode.Location.Y + spanHeight;
                }
                NodeLayer.SetPosition(this, new Point(x, y));
                return;
            }

            Point point = new Point(x, y);
            NodeLayer.SetPosition(this, point);
        }
    }

    public override Part GetPrevious()
    {
        return this.GetFromNodes()?.FirstOrDefault();
    }

    public override Part GetNext()
    {
        return this.GetToNodes()?.FirstOrDefault();
    }

    public INodeData Data => this.GetContent<INodeData>();
}

public partial class Node
{
    protected override void OnRender(DrawingContext drawingContext)
    {
        System.Diagnostics.Debug.WriteLine("Node.OnRender");
        base.OnRender(drawingContext);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
#if DEBUG
        DateTime dateTime = DateTime.Now;
#endif  
        for (int i = 0; i < this.VisualChildrenCount; i++)
        {
            UIElement child = VisualTreeHelper.GetChild(this, i) as UIElement;
            if (child is Port port)
            {
                Point point = Node.GetPosition(port);
                port.Measure(finalSize);
                Rect rect = new Rect(point, port.DesiredSize);
                port.Arrange(rect);
                //child.Arrange(new Rect(finalSize));
            }
            else
            {
                child.Arrange(new Rect(finalSize));
            }
        }
#if DEBUG
        TimeSpan span = DateTime.Now - dateTime;
        System.Diagnostics.Debug.WriteLine("Node.ArrangeOverride：" + span.ToString());
#endif 
        return finalSize;


    }

    protected override int VisualChildrenCount => base.VisualChildrenCount + this._ports.Count;

    protected override Visual GetVisualChild(int index)
    {
        return index >= base.VisualChildrenCount ? this._ports[index - base.VisualChildrenCount] : base.GetVisualChild(index);
    }
}
