namespace H.Controls.Diagram.Presenter.DiagramDatas;

public abstract class FlowableDiagramDataBase : DiagramDataBase, IFlowableDiagramData
{
    protected override IEnumerable<Node> LoadNodes(IEnumerable<INodeData> nodeDatas, IEnumerable<ILinkData> linkDatas)
    {
        DiagramFlowableDataSourceConverter converter = new DiagramFlowableDataSourceConverter(nodeDatas, linkDatas);
        return converter.NodeSource;
    }

    private DiagramFlowableState _state = DiagramFlowableState.None;
    /// <summary> 说明  </summary>
    public DiagramFlowableState State
    {
        get { return _state; }
        set
        {
            _state = value;
            RaisePropertyChanged("State");
        }
    }

    private DiagramFlowableMode _startMode = DiagramFlowableMode.Port;
    [Display(Name = "运行模式", Order = 0, GroupName = "数据")]
    public DiagramFlowableMode StartMode
    {
        get { return _startMode; }
        set
        {
            _startMode = value;
            RaisePropertyChanged();
        }
    }

    [Display(Name = "开始", GroupName = "操作", Order = 0)]
    public RelayCommand StartCommand => new RelayCommand(async (s, e) =>
    {
        Node start = this.GetStartNode();
        if (start == null)
            return;
        s.IsBusy = true;
        if (DiagramAppSetting.Instance.UseAutoShowLog)
            DiagramAppSetting.Instance.ShowLog = true;
        this.State = DiagramFlowableState.Running;
        bool? b = await Start(start);
        this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
        IocMessage.Snack?.ShowInfo(b == null ? "用户取消" : b == true ? "运行成功" : "运行失败");
        //Commander.InvalidateRequerySuggested();
        s.IsBusy = false;
        await Task.Delay(2000).ContinueWith(x =>
         {
             //AppSetting.Instance.ShowLog = false;
             //WindowMessageViewPresenter.Instance.IsVisible = false;

         });
    }, (s, e) => this.State != DiagramFlowableState.Running && this.State != DiagramFlowableState.Canceling);


    [Display(Name = "停止", GroupName = "操作", Order = 0)]
    public RelayCommand StopCommand => new RelayCommand((s, e) =>
    {
        IEnumerable<Part> parts = this.Nodes.SelectMany(x => x.GetParts());
        foreach (Part part in parts)
        {
            IFlowable data = part.GetContent<IFlowable>();
            {
                if (data != null)
                {
                    if (data.State == FlowableState.Running || data.State == FlowableState.Wait)
                        data.State = FlowableState.Canceling;
                }
            }
            if (part.State == FlowableState.Running || part.State == FlowableState.Wait)
                part.State = FlowableState.Canceling;
        }
    }, (s, e) => this.State == DiagramFlowableState.Running && this.State != DiagramFlowableState.Canceling);

    [Display(Name = "重置", GroupName = "操作", Order = 0)]
    public RelayCommand CancelCommand => new RelayCommand((s, e) =>
    {
        IEnumerable<Part> parts = this.Nodes.SelectMany(x => x.GetParts());
        foreach (Part part in parts)
        {
            part.State = FlowableState.Ready;
            if (part.Content is IFlowable flowable)
                flowable.State = FlowableState.Ready;
        }
        this.State = DiagramFlowableState.None;
    }, (s, e) => this.State != DiagramFlowableState.None && this.State != DiagramFlowableState.Canceling);


    public RelayCommand StartNodeCommand => new RelayCommand(async (s, e) =>
    {
        Node start = e as Node;
        if (start == null)
            return;
        s.IsBusy = true;
        if (DiagramAppSetting.Instance.UseAutoShowLog)
            DiagramAppSetting.Instance.ShowLog = true;
        this.State = DiagramFlowableState.Running;
        bool? b = await Start(start);
        this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
        IocMessage.Snack?.ShowInfo(b == null ? "用户取消" : b == true ? "运行成功" : "运行失败");
        //Commander.InvalidateRequerySuggested();
        s.IsBusy = false;
        await Task.Delay(2000).ContinueWith(x =>
        {
            if (DiagramAppSetting.Instance.UseAutoShowLog)
                DiagramAppSetting.Instance.ShowLog = false;
        });
    });

    protected virtual Node GetStartNode()
    {
        IEnumerable<Part> parts = this.Nodes.SelectMany(x => x.GetParts());
        foreach (Part part in parts)
        {
            part.State = FlowableState.Wait;
            if (part.Content is IFlowable flowable)
                flowable.State = FlowableState.Wait;
        }
        IEnumerable<Node> starts = this.Nodes.Where(x => x.GetFromNodes().Count == 0 && x.GetContent<FlowableNodeData>()?.UseStart == true);

        if (starts == null || starts.Count() == 0)
        {
            IocMessage.Snack?.ShowInfo("未找到起始节点,请添加UseStart节点");
            return null;
        }

        if (starts.Count() > 1)
        {
            IocMessage.Snack?.ShowInfo("查询到多个节点");
            return null;
        }

        return starts.FirstOrDefault();
    }

    public virtual async Task<bool?> Start(Node startNode)
    {
        Action<Part> builder = x =>
        {
            if (DiagramAppSetting.Instance.UseAutoLocator)
                this.LocateCenter(x);
            if (DiagramAppSetting.Instance.UseAutoSelect)
                x.IsSelected = true;
            if (DiagramAppSetting.Instance.UseAutoScaleTo)
                this.LocateRect(x);
        };
        return this.StartMode == DiagramFlowableMode.Link
            ? await startNode.StartLink(builder)
            : this.StartMode == DiagramFlowableMode.Port ? await startNode.StartPort(builder) : await startNode.StartNode(builder);
    }

    public async Task<bool?> Start()
    {
        Node start = this.GetStartNode();
        if (start == null)
        {
            this.Message = "没有找到开始节点";
            return false;
        }
        bool? b = await Start(start);
        this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
        return b;
    }

    private string _message;
    [Browsable(false)]
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged();
        }
    }

}
