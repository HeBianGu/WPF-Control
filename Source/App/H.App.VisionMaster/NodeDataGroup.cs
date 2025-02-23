
using H.Controls.Diagram;
using H.Controls.Diagram.Extension;
using H.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

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

public class ImageImportNodeData : ActionNodeDataBase
{
    public string ImagePath { get; set; }
    public string ImageName { get; set; }
    public string ImageDescription { get; set; }

    protected override IEnumerable<IPortData> CreatePortDatas()
    {
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Bottom;
            port.PortType = PortType.OutPut;
            yield return port;
        }
    }
}

public class ActionNodeData : ActionNodeDataBase
{

}

public abstract class ActionNodeDataBase : LineCardNodeData
{
    protected override IEnumerable<IPortData> CreatePortDatas()
    {
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Top;
            port.PortType = PortType.Input;
            yield return port;
        }
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Bottom;
            port.PortType = PortType.OutPut;
            yield return port;
        }


    }
}