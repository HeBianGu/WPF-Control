


global using H.Extensions.Common;
global using H.Controls.Diagram.Presenter.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace H.Controls.Diagram.Presenters.Workflow.Commands;

public class AddWorkflowNodeCommand : AddNodeCommand
{
    protected override INodeData CreateData(Node node)
    {
        return node.Content as INodeData;
    }

    public override bool CanExecute(object parameter)
    {
        if (parameter is Node node)
        {
            bool r = node.LinksInto.Any(x => x.FromPort?.Dock == x.FromPort?.Dock.GetRevert());
            return !r;
        }
        return true;
    }
}

public class AddWorkflowNodeFromTemplateCommand : AddWorkflowNodeCommand
{
    public override void Execute(object parameter)
    {
        ContentControl content = parameter as ContentControl;
        if (content == null)
            return;

        Button btn = content.GetParent<Button>();
        if (btn == null)
            return;

        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
        {
            if (btn.HorizontalAlignment == HorizontalAlignment.Left)
                this.Dock = Dock.Left;
            if (btn.HorizontalAlignment == HorizontalAlignment.Right)
                this.Dock = Dock.Right;
            if (btn.VerticalAlignment == VerticalAlignment.Top)
                this.Dock = Dock.Top;
            if (btn.VerticalAlignment == VerticalAlignment.Bottom)
                this.Dock = Dock.Bottom;

            Node node = content.GetParent<Node>();
            if (node == null)
                return;

            INodeData nodeData = content.Content as INodeData;
            if (nodeData == null)
                return;

            this.Create(node, nodeData);
        }));

    }

    public override bool CanExecute(object parameter)
    {
        return true;
    }
}
