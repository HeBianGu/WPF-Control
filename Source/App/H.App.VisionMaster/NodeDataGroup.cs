
using H.Controls.Diagram;
using H.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace H.App.VisionMaster;
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
