global using H.Controls.Diagram.Datas;
global using H.Controls.Diagram.Flowables;
global using H.Extensions.FontIcon;
using System.Text.Json.Serialization;
namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IPartInvokeable
{
    public void OnInvokingPart(IPartData part);
    public void OnInvokedPart(IPartData part);
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

    public virtual void OnInvokingPart(IPartData part)
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

    public virtual void OnInvokedPart(IPartData part)
    {
        //if (this.UseFlowableSelectToRunning)
        //    part.IsSelected = false;
        ////if (this.FlowableZoomMode == DiagramFlowableZoomMode.Rect)
        ////    this.ZoomTo(part.Bound);
        ////else if (this.FlowableZoomMode == DiagramFlowableZoomMode.Center)
        ////{
        ////    Point point = part.Bound.GetCenter();
        ////    //zoombox.ZoomToCenter(part.Bound.BottomRight);
        ////}
        ////if (this.UseFlowableSelectToRunning)
        ////    part.IsSelected = true;
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


    public virtual async Task<bool?> Start()
    {
        var starts = this.GetStartNodeDatas().OfType<IFlowableNodeData>();
        if (starts == null || starts.Count() == 0)
        {
            this.Message = "未找到起始节点";
            IocMessage.Notify?.ShowInfo(this.Message);
            return false;
        }

        if (starts.Count() > 1)
        {
            this.Message = "存在多个起始节点";
            IocMessage.Notify?.ShowInfo(this.Message);
            return false;
        }

        var start = starts.First();
        //this.State = DiagramFlowableState.Running;
        return await this.InvokeState(() => start.Invoke(this));
    }

    protected virtual bool CanStart()
    {
        return this.State.CanStart() && this.Nodes.Count > 0;
    }

    public virtual void Stop()
    {
        this.State = DiagramFlowableState.Canceling;
        this.GotoState(x =>
        {
            if (x.State == FlowableState.Running || x.State == FlowableState.Wait || x.State == FlowableState.Ready)
                return FlowableState.Canceling;
            return null;
        });
    }

    public virtual void Reset()
    {
        this.GotoState(x => FlowableState.Ready);
    }

    public virtual void Wait()
    {
        this.GotoState(x => FlowableState.Wait);
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


    public IEnumerable<IFlowableNodeData> FlowableNodeDatas => this.Datas.NodeDatas.OfType<IFlowableNodeData>();

}
