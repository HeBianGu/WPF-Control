global using H.Common.Commands;

namespace H.Controls.Diagram.Presenter.Commands;

public abstract class AddNodeCommandBase : MarkupCommandBase
{
    public override void Execute(object parameter)
    {
        Node node = parameter as Node;
        if (node == null)
            return;

        INodeData nodeData = this.CreateData(node);
        if (nodeData == null)
            return;

        this.Create(node, nodeData);
    }

    protected abstract INodeData CreateData(Node node);
    protected abstract void Create(Node fromNode, INodeData nodeData);
}
public abstract class AddNodeCommand : AddNodeCommandBase
{
    public Dock Dock { get; set; }
    public double OffSet { get; set; } = 90.0;
    public Type NodeType { get; set; } = typeof(Node);
    public bool IsCreateLink { get; set; } = true;

    protected virtual double GetOffSet(Node fromNode, Node toNode)
    {
        return this.Dock == Dock.Top || this.Dock == Dock.Bottom
            ? this.OffSet + toNode.DesiredSize.Height
            : this.OffSet + toNode.DesiredSize.Width / 2;
    }
    protected override void Create(Node fromNode, INodeData nodeData)
    {
        Diagram diagram = fromNode.GetParent<Diagram>();
        if (diagram == null)
            return;

        IList nodeSource = diagram.Nodes;
        if (nodeSource == null)
            return;

        if (nodeData is ICloneable cloneable)
            nodeData = cloneable.Clone() as INodeData;

        Node toNode = this.CreateNode(nodeData);
        toNode.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        this.OffSet = this.GetOffSet(fromNode, toNode);
        Point location = fromNode.Location;

        if (this.Dock == Dock.Top)
        {
            toNode.Location = new Point(location.X, location.Y - this.OffSet + fromNode.DesiredSize.Height / 2);
            this.CreateLink(diagram, fromNode, toNode);
        }
        if (this.Dock == Dock.Bottom)
        {
            toNode.Location = new Point(location.X, location.Y + this.OffSet - fromNode.DesiredSize.Height / 2);
            this.CreateLink(diagram, fromNode, toNode);
        }
        if (this.Dock == Dock.Left)
        {
            toNode.Location = new Point(location.X - this.OffSet - fromNode.DesiredSize.Width / 2, location.Y);
            this.CreateLink(diagram, fromNode, toNode);
        }
        if (this.Dock == Dock.Right)
        {
            toNode.Location = new Point(location.X + this.OffSet + fromNode.DesiredSize.Width / 2, location.Y);
            this.CreateLink(diagram, fromNode, toNode);
        }
        //nodeSource.Add(toNode);
        //diagram.RefreshData();
        diagram.AddNode(toNode);
    }

    protected virtual Node CreateNode(INodeData nodeData)
    {
        Node node = Activator.CreateInstance(this.NodeType) as Node;
        node.Content = nodeData;

        if (nodeData is IPortableNodeData displayNodeData)
        {
            foreach (IPortData p in displayNodeData.PortDatas)
            {
                Port port = Port.Create(node);
                port.Dock = p.Dock;
                //port.Visibility = Visibility.Collapsed;
                port.PortType = p.PortType;
                port.Content = p;
                port.Margin = p.PortMargin;
                node.AddPort(port);
            }
        }

        if (nodeData is ITemplate template)
            template.IsTemplate = false;

        return node;

    }

    protected virtual void CreateLink(Diagram diagram, Node from, Node to)
    {
        if (!this.IsCreateLink)
            return;
        diagram.LinkNodes(from, to, this.Dock);
    }
}

public class AddNodeFromDataTypeCommand : AddNodeCommand
{
    public Type DataType { get; set; }

    protected override INodeData CreateData(Node node)
    {
        return Activator.CreateInstance(this.DataType) as INodeData;
    }
}
