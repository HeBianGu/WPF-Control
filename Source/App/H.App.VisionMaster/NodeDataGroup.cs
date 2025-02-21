
using H.Controls.Diagram;
using H.Mvvm;
using System.Collections.ObjectModel;

namespace H.App.VisionMaster;
public interface INodeDataGroup
{
    ObservableCollection<INodeData> NodeDatas { get; set; }
}

public class NodeDataGroup : DisplayBindableBase, INodeDataGroup
{
    public string Icon { get; set; }
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