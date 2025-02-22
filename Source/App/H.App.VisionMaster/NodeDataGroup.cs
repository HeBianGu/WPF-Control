
using H.Controls.Diagram;
using H.Mvvm;
using System.Collections.ObjectModel;

namespace H.App.VisionMaster;
public interface INodeDataGroup
{
    ObservableCollection<INodeData> NodeDatas { get; set; }
}

[Icon("\xE722")]
public class NodeDataGroup : DisplayBindableBase, INodeDataGroup
{
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

}