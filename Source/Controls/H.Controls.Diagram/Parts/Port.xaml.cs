// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Controls.Diagram.Parts;

public partial class Port : FlowablePart
{
    public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Port), "S.Port.Default");

    static Port()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Port), new FrameworkPropertyMetadata(typeof(Port)));
    }

    public string Id => this.GetContent<IPortData>().ID;

    public static Port Create(Node parent)
    {
        Port port = new Port();
        port.ParentNode = parent;
        //parent.Ports.Add(port);
        return port;
    }

    public Dock Dock
    {
        get { return (Dock)GetValue(DockProperty); }
        set { SetValue(DockProperty, value); }
    }


    public static readonly DependencyProperty DockProperty =
        DependencyProperty.Register("Dock", typeof(Dock), typeof(Port), new PropertyMetadata(default(Dock), (d, e) =>
        {
            Port control = d as Port;

            if (control == null) return;

            //Dock config = e.NewValue as Dock;

        }));

    public Node ParentNode { get; set; }

    public PortType PortType { get; set; }


    public override void Delete()
    {
        base.Delete();

        this.ParentNode.LinksInto.Where(l => l.ToPort == this).ToList().ForEach(l => l.Delete());

        this.ParentNode.LinksOutOf.Where(l => l.FromPort == this).ToList().ForEach(l => l.Delete());

        this.Clear();
    }

    public IEnumerable<Link> GetLinkInto()
    {
        return this.ParentNode.LinksInto.Where(l => l.ToPort == this);
    }

    public IEnumerable<Link> GetLinksOutOf()
    {
        return this.ParentNode.LinksOutOf.Where(l => l.FromPort == this);
    }

    public IEnumerable<Link> GetConnectLinks()
    {
        return this.ParentNode.ConnectLinks.Where(l => l.ToPort == this || l.FromPort == this);
    }

    public override void Clear()
    {
        //  Do ：删除Link
        this.ParentNode.LinksInto.RemoveAll(l => l.ToPort == this);

        this.ParentNode.LinksOutOf.RemoveAll(l => l.FromPort == this);

        //  Do ：删除Node
        this.ParentNode.RemovePort(this);

        //this.ParentNode.RefreshData();

        this.ParentNode = null;
    }

    public override string ToString()
    {
        return $"{this.GetType().Name} - {this.Id}:{NodeLayer.GetPosition(this)}  {Node.GetPosition(this)}";
    }

    public Point ChangedPoint(Point point, double span = 50)
    {
        if (this.Dock == Dock.Left)
            return new Point(point.X - span, point.Y);
        else if (this.Dock == Dock.Top)
        {
            return new Point(point.X, point.Y - span);
        }
        else
        {
            return this.Dock == Dock.Right ? new Point(point.X + span, point.Y) : new Point(point.X, point.Y + span);
        }
    }

    protected override Size ArrangeOverride(Size arrangeSize)
    {
        System.Diagnostics.Debug.WriteLine("Port.ArrangeOverride");
        return base.ArrangeOverride(arrangeSize);
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        System.Diagnostics.Debug.WriteLine("Port.OnRender");
        base.OnRender(drawingContext);
    }

    public override Part GetPrevious()
    {
        List<Port> ports = this.ParentNode.GetPorts();
        int index = ports.IndexOf(this);
        return index == 0 ? ports[ports.Count - 1] : ports[index - 1];
    }

    public override Part GetNext()
    {
        List<Port> ports = this.ParentNode.GetPorts();
        int index = ports.IndexOf(this);
        return index == ports.Count - 1 ? ports[0] : ports[index + 1];
    }

    public IPortData Data => this.GetContent<IPortData>();
}
