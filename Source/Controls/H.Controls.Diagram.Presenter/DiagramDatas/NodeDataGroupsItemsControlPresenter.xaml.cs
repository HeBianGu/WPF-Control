using H.Extensions.ValueConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Controls.Diagram.Presenter.DiagramDatas;
public class NodeDataGroupsItemsControlPresenter : DisplayBindableBase
{
    public NodeDataGroupsItemsControlPresenter(IEnumerable<INodeDataGroup> nodeDataGroups)
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

public class GetNodeDataGroupsItemsControlPresenter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable<INodeDataGroup> nodegroups)
            return new NodeDataGroupsItemsControlPresenter(nodegroups);
        return null;
    }
}
