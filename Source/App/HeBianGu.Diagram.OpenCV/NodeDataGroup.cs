using H.Mvvm;
using System.Collections.ObjectModel;

namespace HeBianGu.Diagram.OpenCV;
public interface INodeDataGroup : IDisplayBindable
{
    ObservableCollection<INodeData> NodeDatas { get; set; }
}

[Icon("\xE722")]
public abstract class NodeDataGroupBase : DisplayBindableBase, INodeDataGroup
{
    public NodeDataGroupBase()
    {
        this.NodeDatas = this.CreateNodeDatas().ToObservable();
    }
    private ObservableCollection<INodeData> _nodeDatas = new ObservableCollection<INodeData>();
    public ObservableCollection<INodeData> NodeDatas
    {
        get { return _nodeDatas; }
        set
        {
            _nodeDatas = value;
            RaisePropertyChanged();
        }
    }

    protected abstract IEnumerable<INodeData> CreateNodeDatas();
}
