global using H.Controls.Diagram.Datas;
global using H.Controls.Diagram.Flowables;
global using H.Extensions.FontIcon;
using System.Text.Json.Serialization;
namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IPartInvokeable
{
    public void OnInvokingPart(Part part);
    public void OnInvokedPart(Part part);
}

public abstract class FlowableDiagramDataBase : ZoomableDiagramDataBase, IFlowableDiagramData, IPartInvokeable
{
    protected override IEnumerable<Node> LoadToNodes(IEnumerable<INodeData> nodeDatas, IEnumerable<ILinkData> linkDatas)
    {
        var converter = new DiagramFlowableDataSourceConverter(nodeDatas, linkDatas);
        return converter.NodeSource;
    }

    private DiagramFlowableState _state = DiagramFlowableState.None;
    [JsonIgnore]
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

    public virtual void OnInvokingPart(Part part)
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

    public virtual void OnInvokedPart(Part part)
    {
        if (this.UseFlowableSelectToRunning)
            part.IsSelected = false;
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
    [Icon(FontIcons.Replay)]
    [Display(Name = "开始", GroupName = "操作", Order = 0)]
    public DisplayCommand StartCommand => new DisplayCommand(async e =>
    {
        await this.Start();
    }, e => this.CanStart());

    [Icon(FontIcons.Location)]
    [Display(Name = "停止", GroupName = "操作", Order = 0)]
    public DisplayCommand StopCommand => new DisplayCommand(e =>
    {
        this.Stop();
    }, e => this.State.CanStop());

    [Icon(FontIcons.Refresh)]
    [Display(Name = "重置", GroupName = "操作", Order = 0)]
    public DisplayCommand ResetCommand => new DisplayCommand(e =>
    {
        this.Reset();
    }, e => this.State.CanReset());

    //public RelayCommand StartNodeCommand => new RelayCommand(async (s, e) =>
    //{
    //    Node start = e as Node;
    //    if (start == null)
    //        return;
    //    s.IsBusy = true;
    //    //if (DiagramAppSetting.Instance.UseAutoShowLog)
    //    //    DiagramAppSetting.Instance.ShowLog = true;
    //    this.State = DiagramFlowableState.Running;
    //    using (new PartInvokable(start, OnInvokingPart, OnInvokedPart))
    //    {
    //        await this.InvokeState(() => InvokeNode(start));
    //        //bool? b = await InvokeNode(start);
    //        //this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
    //        //IocMessage.Snack?.ShowInfo(b == null ? "用户取消" : b == true ? "运行成功" : "运行失败");
    //    }
    //    //Commander.InvalidateRequerySuggested();
    //    s.IsBusy = false;
    //    //await Task.Delay(2000).ContinueWith(x =>
    //    //{
    //    //    if (DiagramAppSetting.Instance.UseAutoShowLog)
    //    //        DiagramAppSetting.Instance.ShowLog = false;
    //    //});
    //});

    //protected async Task<bool?> InvokeState(Func<Task<bool?>> action)
    //{
    //    this.State = DiagramFlowableState.Running;
    //    this.Message = "正在运行";
    //    var b = await action?.Invoke();
    //    this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
    //    var message = b == null ? "用户取消" : b == true ? "运行成功" : "运行失败";
    //    IocMessage.Snack?.ShowInfo(message);
    //    H.Mvvm.Commands.InvalidateRequerySuggested();
    //    this.Message = message;
    //    return b;
    //}

    protected virtual async Task<bool?> InvokeNode(Node startNode)
    {
        return await this.InvokeState(() => startNode.InvokeNode(this.FlowableMode, OnInvokingPart, OnInvokedPart));
        //this.State = DiagramFlowableState.Running;
        //var b = await startNode.InvokeNode(this.FlowableMode, OnInvokingPart, OnInvokedPart);
        //this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
        //IocMessage.Snack?.ShowInfo(b == null ? "用户取消" : b == true ? "运行成功" : "运行失败");
        //return b;
    }

    public virtual async Task<bool?> Start()
    {
        Node node = this.Nodes.GetStartNode(out string message);
        if (node == null)
        {
            this.Message = message;
            IocMessage.Notify?.ShowInfo(message);
            return false;
        }
        //this.State = DiagramFlowableState.Running;
        return await this.InvokeState(() => node.InvokeNode(this.FlowableMode, OnInvokingPart, OnInvokedPart));

        //var b = await node.Start(this.FlowableMode, OnInvokingPart, OnInvokedPart);
        //this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
        //this.Message = message;
        //H.Mvvm.Commands.InvalidateRequerySuggested();
        //if (!string.IsNullOrEmpty(message))
        //{
        //    IocMessage.Notify?.ShowInfo(message);
        //    return false;
        //}
        //return true;
    }

    protected virtual bool CanStart()
    {
        return this.State.CanStart() && this.Nodes.Count > 0;
    }

    public virtual void Stop()
    {
        this.State = DiagramFlowableState.Canceling;
        this.Nodes.Stop();
    }

    public virtual void Reset()
    {
        this.Nodes.Reset();
    }

    public virtual void Wait()
    {
        this.Nodes.Wait();
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
