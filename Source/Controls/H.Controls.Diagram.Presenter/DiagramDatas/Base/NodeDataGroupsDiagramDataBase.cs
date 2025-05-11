// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class NodeDataGroupsDiagramDataBase : FlowableDiagramDataBase, INodeDataGroupsDiagramData
{
    protected NodeDataGroupsDiagramDataBase()
    {
        ObservableCollection<INodeDataGroup> nodeGroups = this.CreateNodeGroups()?.ToObservable();
        foreach (INodeDataGroup nodeGroup in nodeGroups)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            {
                this.NodeGroups.Add(nodeGroup);
            }));
        }
    }
    private ObservableCollection<INodeDataGroup> _nodeGroups = new ObservableCollection<INodeDataGroup>();
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    public ObservableCollection<INodeDataGroup> NodeGroups
    {
        get { return _nodeGroups; }
        set
        {
            _nodeGroups = value;
            RaisePropertyChanged();
        }
    }

    protected virtual IEnumerable<INodeDataGroup> CreateNodeGroups()
    {
        return this.GetType().Assembly.GetInstances<INodeDataGroup>();
    }

}
