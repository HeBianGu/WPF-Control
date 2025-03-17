namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class TreeDigramDataBase : NodeDataGroupsDiagramDataBase, ITreeDigramData
{
    private TreeNodeBase<Part> _root = new TreeNodeBase<Part>(null);
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    public TreeNodeBase<Part> Root
    {
        get { return _root; }
        set
        {
            _root = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand SelectedTreeNodeChanged => new RelayCommand(e =>
    {
        if (e is TreeNodeBase<Part> project)
            project.Model.IsSelected = true;
    });

    protected override void OnItemsChanged()
    {
        base.OnItemsChanged();
        this.RefreshRoot();
    }

    private static bool _isRefreshRooting;
    public virtual void RefreshRoot()
    {
        if (_isRefreshRooting)
            return;
        _isRefreshRooting = true;
        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
        {
            _isRefreshRooting = false;
            this.Root.Nodes.Clear();
            //foreach (Node note in this.Nodes)
            //{
            //    NodeTreeNode nd = new NodeTreeNode(note);

            //    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            //    {
            //        this.Root.AddNode(nd);
            //    }));

            //    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            //    {
            //        foreach (Port port in note.GetPorts())
            //        {
            //            PortTreeNode pd = new PortTreeNode(port);
            //            nd.AddNode(pd);
            //            pd.RefreshSelected();
            //        }
            //        nd.RefreshSelected();
            //    }));

            //    foreach (Link link in note.LinksOutOf)
            //    {
            //        LinkTreeNode ld = new LinkTreeNode(link);
            //        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            //        {
            //            this.Root.AddNode(ld);
            //            ld.RefreshSelected();
            //        }));
            //    }
            //}
        }));
    }

    private bool _isSelectedPartRefreshing;
    protected override void OnSelectedPartChanged()
    {
        base.OnSelectedPartChanged();
        //if (this.SelectedPart == null)
        //    return;
        ////if (!this.SelectedPart.IsSelected)
        ////    return; 
        //if (_isSelectedPartRefreshing)
        //    return;
        //_isSelectedPartRefreshing = true;

        //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
        //{
        //    _isSelectedPartRefreshing = false;
        //    TreeNodeBase<Part> find = this.Root.FindAll(x => x.Model == this.SelectedPart)?.FirstOrDefault();
        //    if (find == null)
        //        return;
        //    find.IsSelected = true;
        //    if (find.Parent != null)
        //        find.Parent.IsExpanded = true;
        //}));
    }
}
