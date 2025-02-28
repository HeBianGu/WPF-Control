namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class NodeGroupsDiagramDataBase : FlowableDiagramDataBase, INodeGroupsDiagramData
{
    protected NodeGroupsDiagramDataBase()
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

        //IEnumerable<Type> types = this.GetType().Assembly.GetTypes().Where(x => typeof(INodeData).IsAssignableFrom(x)).Where(x => !x.IsAbstract);
        //types = types.Where(x => x.GetCustomAttributes<NodeTypeAttribute>(true).Any(t => t.DiagramType == this.GetType()));
        //List<NodeData> datas = new List<NodeData>();
        //foreach (Type item in types)
        //{
        //    NodeData data = Activator.CreateInstance(item) as NodeData;
        //    DisplayAttribute type = item.GetCustomAttribute<DisplayAttribute>();
        //    data.Name = type?.Name;
        //    data.GroupName = type?.GroupName;
        //    data.Description = type?.Description;
        //    //data.Order = type?.Order ?? 0;
        //    int? od = type.GetOrder();
        //    if (od.HasValue)
        //        this.Order = od.Value;
        //    datas.Add(data);
        //}
        //IEnumerable<IGrouping<string, NodeData>> groups = datas.OrderBy(x => x.Order).GroupBy(x => x.GroupName);
        //return groups.Select(x => new NodeGroup(x.ToList()) { Name = x.Key, Columns = x.ToList()?.FirstOrDefault()?.Columns ?? 4 }); ;
    }

}
