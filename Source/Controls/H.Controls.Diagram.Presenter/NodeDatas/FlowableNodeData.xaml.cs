global using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Controls.Diagram.Presenter.NodeDatas.Base;
using H.Extensions.FontIcon;
using System.Windows.Documents;

namespace H.Controls.Diagram.Presenter.NodeDatas;

public class FlowableNodeData : TextNodeData, IFlowableNodeData
{
    private FlowableState _state = FlowableState.Ready;
    [Browsable(false)]
    public FlowableState State
    {
        get { return _state; }
        set
        {
            _state = value;
            RaisePropertyChanged("State");
        }
    }

    private bool _useAnimation = true;
    [XmlIgnore]
    [Browsable(false)]
    public bool UseAnimation
    {
        get { return _useAnimation; }
        set
        {
            _useAnimation = value;
            RaisePropertyChanged();
        }
    }

    private bool _useStart;
    [XmlIgnore]
    [Browsable(false)]
    public bool UseStart
    {
        get { return _useStart; }
        set
        {
            _useStart = value;
            RaisePropertyChanged();
        }
    }

    private TimeSpan _timeSpan;
    public TimeSpan TimeSpan
    {
        get { return _timeSpan; }
        set
        {
            _timeSpan = value;
            RaisePropertyChanged();
        }
    }

    private bool _useInvoke;
    [XmlIgnore]
    [Browsable(false)]
    public bool UseInvoke
    {
        get { return _useInvoke; }
        set
        {
            _useInvoke = value;
            RaisePropertyChanged();
        }
    }

    private string _message;
    //[XmlIgnore]
    [Browsable(false)]
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged("Message");
        }
    }

    private bool _isBuzy;
    [XmlIgnore]
    [Browsable(false)]
    public bool IsBuzy
    {
        get { return _isBuzy; }
        set
        {
            _isBuzy = value;
            RaisePropertyChanged();
        }
    }

    private bool _useInfoLogger = true;
    [XmlIgnore]
    [Browsable(false)]
    public bool UseInfoLogger
    {
        get { return _useInfoLogger; }
        set
        {
            _useInfoLogger = value;
            RaisePropertyChanged();
        }
    }

    private Exception _exception;
    /// <summary> 说明  </summary>
    [Browsable(false)]
    [XmlIgnore]
    public Exception Exception
    {
        get { return _exception; }
        set
        {
            _exception = value;
            RaisePropertyChanged("Exception");
        }
    }
    [XmlIgnore]
    [Browsable(false)]
    protected Random Random { get; } = new Random();

    protected virtual IFlowableResult OK(string message = "运行成功")
    {
        this.Message = message;
        return new FlowableResult(message) { State = FlowableResultState.OK };
    }

    protected virtual IFlowableResult Error(string message = "运行错误")
    {
        this.Message = message;
        return new FlowableResult(message) { State = FlowableResultState.Error };
    }

    //[Browsable(false)]
    //[Icon(FontIcons.Play)]
    //[Display(Name = "执行")]
    //public DisplayCommand InvokeCommand => new DisplayCommand(async l =>
    //{
    //    if (l is Node node)
    //        await this.TryInvokeAsync(null, node);
    //}, x => x is Node);

    public virtual IFlowableResult Invoke(IFlowablePortData previors, IFlowableDiagramData diagram)
    {
        Thread.Sleep(DiagramAppSetting.Instance.FlowSleepMillisecondsTimeout);
        return DiagramAppSetting.Instance.UseMock
            ? this.Random.Next(0, 19) == 1 ? this.Error("模拟仿真一个错误信息") : this.OK("模拟仿真一个成功信息")
            : this.OK("运行成功");
    }

    public virtual async Task<IFlowableResult> InvokeAsync(IFlowablePortData previors, IFlowableDiagramData diagram)
    {
        return await Task.Run(() =>
        {
            return this.Invoke(previors, diagram);
        });
    }
    public virtual async Task<IFlowableResult> TryInvokeAsync(IFlowablePortData previors, IFlowableDiagramData diagram)
    {
        try
        {
            this.Clear();
            this.State = FlowableState.Running;
            this.IsBuzy = true;
            using (var stopwatch = new Stopwatchable(this))
            {
                if (this.UseInfoLogger)
                    IocLog.Instance?.Info($"正在执行<{this.GetType().Name}>:{this.Text}");
                IFlowableResult result = await InvokeAsync(previors, diagram);
                if (this.UseInfoLogger)
                    IocLog.Instance?.Info(result.State == FlowableResultState.Error ? $"运行错误<{this.GetType().Name}>:{this.Text} {result.Message}" : $"执行完成<{this.GetType().Name}>:{this.Text} {result.Message}");
                this.State = result.ToState();
                return result;
            }
        }
        catch (Exception ex)
        {
            this.State = FlowableState.Error;
            this.Exception = ex;
            this.Message = ex.Message;
            IocLog.Instance?.Error($"执行错误<{this.GetType().Name}>:{this.Name} {this.Message}");
            IocLog.Instance?.Error(ex);
            return this.Error(ex.Message);
        }
        finally
        {
            this.IsBuzy = false;
        }
    }

    public virtual void Clear()
    {
    }

    public override ILinkData CreateLinkData()
    {
        return new FlowableLinkData() { FromNodeID = this.ID };
    }

    public override IPortData CreatePortData()
    {
        return new FlowablePortData(this.ID, PortType.Both);
    }
    public virtual void Dispose()
    {

    }

    //protected T GetFromData<T>(Node current)
    //{
    //    Node from = current.GetFromNodes().FirstOrDefault();
    //    return from == null ? default : from.GetContent<T>();
    //}
    //protected async Task<IFlowableResult?> OnInvokeCurrentNode(IFlowableDiagramData diagramData, IFlowablePortData from)
    //{
    //    if (this.State == FlowableState.Canceling)
    //        return null;
    //    using (new PartDataInvokable(this, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
    //    {
    //        return await this.TryInvokeAsync(from, diagramData) as FlowableResult;
    //        //if (result == null || result.State == FlowableResultState.Error)
    //        //    return result;
    //    }
    //}
    public async Task<bool?> Start(IFlowableDiagramData diagramData, IFlowablePortData from = null)
    {
        if (this.State == FlowableState.Canceling)
            return null;

        IFlowableResult nresult;
        using (new PartDataInvokable(this, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
        {
            nresult = await this.TryInvokeAsync(from, diagramData) as FlowableResult;
            if (nresult.State == FlowableResultState.Error)
                return false;
        }

        var toLinks = this.GetToLinkDatas(diagramData).OfType<IFlowableLinkData>().Where(x => x.IsMatchResult(nresult));
        foreach (var linkData in toLinks)
        {
            if (linkData.State == FlowableState.Canceling)
                return null;
            //  Do ：From Ports
            IFlowablePortData fPort = linkData.GetFromPortData(diagramData) as IFlowablePortData;
            using (new PartDataInvokable(fPort, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
            {
                IFlowableResult rFrom = await fPort?.TryInvokeAsync(diagramData);
                if (rFrom?.State == FlowableResultState.Error)
                    return false;
            }

            //  Do ：Links
            if (linkData.State == FlowableState.Canceling)
                return null;
            linkData.State = FlowableState.Running;
            using (new PartDataInvokable(linkData, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
            {
                IFlowableResult r = await linkData?.TryInvokeAsync(fPort, diagramData);
                linkData.State = r?.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
                if (r?.State == FlowableResultState.Error)
                    return false;
            }
            if (linkData.State == FlowableState.Canceling)
                return null;
            //  Do ：To Ports
            IFlowablePortData tPort = linkData.GetFromPortData(diagramData) as IFlowablePortData;
            using (new PartDataInvokable(tPort, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
            {
                IFlowableResult rTo = await tPort?.TryInvokeAsync(diagramData);
                if (rTo?.State == FlowableResultState.Error)
                    return false;
            }

            var tNodeData = linkData.GetToNodeData(diagramData) as IFlowableNodeData;
            //  Do ：递归执行ToNode
            bool? b = await tNodeData?.Start(diagramData, tPort);
            if (b != true)
                return b;
        }
        return true;
    }
}

public class StartFlowableNodeData : FlowableNodeData
{
    public StartFlowableNodeData()
    {
        this.UseStart = true;
    }
}
