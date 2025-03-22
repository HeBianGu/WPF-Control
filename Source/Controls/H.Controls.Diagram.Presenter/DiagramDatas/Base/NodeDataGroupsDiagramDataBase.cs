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
