global using H.Controls.Diagram.Datas;
global using H.Controls.Diagram.Flowables;
using H.Controls.Diagram.Parts;

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class FlowableDiagramDataBase : ZoomableDiagramDataBase, IFlowableDiagramData
{
    protected override IEnumerable<Node> LoadNodes(IEnumerable<INodeData> nodeDatas, IEnumerable<ILinkData> linkDatas)
    {
        DiagramFlowableDataSourceConverter converter = new DiagramFlowableDataSourceConverter(nodeDatas, linkDatas);
        return converter.NodeSource;
    }

    private DiagramFlowableState _state = DiagramFlowableState.None;
    public DiagramFlowableState State
    {
        get { return _state; }
        set
        {
            _state = value;
            RaisePropertyChanged("State");
        }
    }

    private DiagramFlowableMode _flowableMode = DiagramFlowableMode.Node;
    [Display(Name = "运行模式", Order = 0, GroupName = "数据")]
    public DiagramFlowableMode FlowableMode
    {
        get { return _flowableMode; }
        set
        {
            _flowableMode = value;
            RaisePropertyChanged();
        }
    }


    private DiagramFlowableZoomMode _flowableZoomMode;
    [Display(Name = "执行时节点自动缩放", GroupName = "数据")]
    public DiagramFlowableZoomMode FlowableZoomMode
    {
        get { return _flowableZoomMode; }
        set
        {
            _flowableZoomMode = value;
            RaisePropertyChanged();
        }
    }

    private bool _useFlowableSelectToRunning = true;
    public bool UseFlowableSelectToRunning
    {
        get { return _useFlowableSelectToRunning; }
        set
        {
            _useFlowableSelectToRunning = value;
            RaisePropertyChanged();
        }
    }


    protected virtual void OnInvokingPart(Part part)
    {
        if (this.FlowableZoomMode == DiagramFlowableZoomMode.Rect)
            this.ZoomTo(part.Bound);
        else if (this.FlowableZoomMode == DiagramFlowableZoomMode.Center)
        {
            Point point = part.Bound.GetCenter();
            //zoombox.ZoomToCenter(part.Bound.BottomRight);
        }
        if (this.UseFlowableSelectToRunning)
            part.IsSelected = true;
    }

    protected virtual void OnInvokedPart(Part part)
    {
        //if (this.FlowableZoomMode == DiagramFlowableZoomMode.Rect)
        //    this.ZoomTo(part.Bound);
        //else if (this.FlowableZoomMode == DiagramFlowableZoomMode.Center)
        //{
        //    Point point = part.Bound.GetCenter();
        //    //zoombox.ZoomToCenter(part.Bound.BottomRight);
        //}
        //if (this.UseFlowableSelectToRunning)
        //    part.IsSelected = true;
    }

    [Display(Name = "开始", GroupName = "操作", Order = 0)]
    public RelayCommand StartCommand => new RelayCommand(async (s, e) =>
    {
        await this.Start();
    }, (s, e) => this.State.CanStart());


    [Display(Name = "停止", GroupName = "操作", Order = 0)]
    public RelayCommand StopCommand => new RelayCommand((s, e) =>
    {
        this.Stop();
    }, (s, e) => this.State.CanStop());

    [Display(Name = "重置", GroupName = "操作", Order = 0)]
    public RelayCommand ResetCommand => new RelayCommand((s, e) =>
    {
        this.Reset();
    }, (s, e) => this.State.CanReset());


    public RelayCommand StartNodeCommand => new RelayCommand(async (s, e) =>
    {
        Node start = e as Node;
        if (start == null)
            return;
        s.IsBusy = true;
        //if (DiagramAppSetting.Instance.UseAutoShowLog)
        //    DiagramAppSetting.Instance.ShowLog = true;
        this.State = DiagramFlowableState.Running;
        using (new PartInvokable(start, OnInvokingPart, OnInvokedPart))
        {
            bool? b = await InvokeNode(start);
            this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
            IocMessage.Snack?.ShowInfo(b == null ? "用户取消" : b == true ? "运行成功" : "运行失败");
        }
        //Commander.InvalidateRequerySuggested();
        s.IsBusy = false;
        //await Task.Delay(2000).ContinueWith(x =>
        //{
        //    if (DiagramAppSetting.Instance.UseAutoShowLog)
        //        DiagramAppSetting.Instance.ShowLog = false;
        //});
    });

    public virtual async Task<bool?> InvokeNode(Node startNode)
    {
        return await startNode.InvokeNode(this.FlowableMode, OnInvokingPart, OnInvokedPart);
    }

    public virtual async Task<bool?> Start()
    {
        string message = await this.Nodes.Start(this.FlowableMode, x => this.State = x, OnInvokingPart, OnInvokedPart);
        if (!string.IsNullOrEmpty(message))
        {
            IocMessage.Notify?.ShowInfo(message);
            return false;
        }
        return true;
    }

    public virtual void Stop()
    {
        this.Nodes.Stop();
    }

    public virtual void Reset()
    {
        this.Nodes.Reset();
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
