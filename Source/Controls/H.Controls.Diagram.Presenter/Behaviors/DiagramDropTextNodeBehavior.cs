global using H.Controls.Diagram.Bevaviors.DiagramDropBehavior;

namespace H.Controls.Diagram.Presenter.Behaviors;

public class DiagramDropTextNodeBehavior : DiagramDropBehaviorBase
{
    public Type NodeType
    {
        get { return (Type)GetValue(NodeTypeProperty); }
        set { SetValue(NodeTypeProperty, value); }
    }

    public static readonly DependencyProperty NodeTypeProperty =
        DependencyProperty.Register("NodeType", typeof(Type), typeof(DiagramDropTextNodeBehavior), new FrameworkPropertyMetadata(typeof(Node), (d, e) =>
         {
             DiagramDropTextNodeBehavior control = d as DiagramDropTextNodeBehavior;

             if (control == null) return;

             if (e.OldValue is Type o)
             {

             }

             if (e.NewValue is Type n)
             {

             }

         }));

    protected override Node Create(INodeData nodeData)
    {
        IPortableNodeData componentNode = nodeData as IPortableNodeData;
        Node node = Activator.CreateInstance(this.NodeType) as Node;
        foreach (IPortData p in componentNode.PortDatas)
        {
            Port port = Port.Create(node);
            port.Dock = p.Dock;
            //port.Visibility = Visibility.Collapsed;
            port.PortType = p.PortType;
            port.Content = p;
            port.Margin = p.PortMargin;
            node.AddPort(port);
        }

        if (nodeData is ITemplate template)
            template.IsTemplate = false;

        return node;
    }
}
