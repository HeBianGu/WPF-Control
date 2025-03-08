using H.Extensions.ValueConverter;
using System.Globalization;

namespace H.Controls.Diagram.Presenter.DiagramDatas;
public class NodeDataGroupsContextMenuPresenter : DisplayBindableBase
{
    public NodeDataGroupsContextMenuPresenter(IEnumerable<INodeDataGroup> nodeDataGroups)
    {
        this._nodeDataGroups = nodeDataGroups;
    }

    private IEnumerable<INodeDataGroup> _nodeDataGroups;
    public IEnumerable<INodeDataGroup> NodeDataGroups
    {
        get { return _nodeDataGroups; }
        set
        {
            _nodeDataGroups = value;
            RaisePropertyChanged();
        }
    }
}

public class GetNodeDataGroupsContextMenuPresenter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable<INodeDataGroup> nodegroups)
            return new NodeDataGroupsContextMenuPresenter(nodegroups);
        return null;
    }
}
