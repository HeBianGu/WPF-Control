global using H.Controls.Diagram.Parts;
global using H.Common;
global using H.Controls.Diagram.Flowables;

namespace H.Controls.Diagram.Presenter.PortDatas;

public class FlowablePortData : TextPortData, IFlowablePortData
{
    public FlowablePortData()
    {

    }

    public FlowablePortData(string nodeID, PortType portType) : base(nodeID, portType)
    {

    }

    private FlowableState _state = FlowableState.Ready;
    //[XmlIgnore]
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
            RaisePropertyChanged("IsBuzy");
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

    //[XmlIgnore]
    //[Display(Name = "执行")]
    //public DisplayCommand InvokeCommand => new DisplayCommand(async l => await this.TryInvokeAsync(null, null, null));

    public IFlowableResult Invoke(IFlowableDiagramData diagram)
    {
        Thread.Sleep(FlowableDiagramDataSetting.Instance.FlowSleepMillisecondsTimeout);
        return FlowableDiagramDataSetting.Instance.UseMock
            ? this.Random.Next(0, 19) == 1 ? this.Error("模拟仿真一个错误信息") : this.OK("模拟仿真一个成功信息")
            : this.OK("运行成功");
    }

    public virtual async Task<IFlowableResult> InvokeAsync(IFlowableLinkData linkData, IFlowableDiagramData diagram)
    {
        return await Task.Run(() =>
        {
            return this.Invoke(diagram);
        });
    }
    public virtual async Task<IFlowableResult> TryInvokeAsync(IFlowableLinkData linkData, IFlowableDiagramData diagram)
    {
        try
        {
            this.State = FlowableState.Running;
            this.IsBuzy = true;
            using (var stopwatch = new Stopwatchable(this))
            {
                IocLog.Instance?.Info($"正在执行<{this.GetType().Name}>:{this.Text}");
                IFlowableResult result = await InvokeAsync(linkData, diagram);
                IocLog.Instance?.Info(result.State == FlowableResultState.Error ? $"运行错误<{this.GetType().Name}>:{this.Text} {result.Message}" : $"执行完成<{this.GetType().Name}>:{this.Text} {result.Message}");
                this.State = result.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
                return result;
            }
        }
        catch (Exception ex)
        {
            this.State = FlowableState.Error;
            this.Exception = ex;
            this.Message = ex.Message;

            IocLog.Instance?.Info($"执行错误<{this.GetType().Name}>:{this.Text} {this.Message}");
            IocLog.Instance?.Error($"执行错误<{this.GetType().Name}>:{this.Text} {this.Message}");
            IocLog.Instance?.Error(ex);

            return this.Error();
        }
        finally
        {
            this.IsBuzy = false;
        }
    }

    public virtual void Clear()
    {

    }

    public virtual void Dispose()
    {

    }
    public override ILinkData CreateLinkData()
    {
        return new FlowableLinkData()
        {
            FromNodeID = this.NodeID,
            FromPortID = this.ID,
            Text = this.Text ?? this.Name ?? this.Description,
        };
    }

    public override bool CanDrop(Part part, out string message)
    {
        message = null;
        if (part.Content is IPortData port)
        {
            if (port.PortType == PortType.OutPut)
            {
                message = "此节点是输出节点";
                return false;
            }

            if (port.NodeID == this.NodeID)
            {
                message = "不能连接同一个节点";
                return false;
            }
        }

        if (!(part.Content is IFlowable))
        {
            message = "不是Flowable节点";
            return false;
        }
        return true;
    }

    public async Task<bool?> Start(IFlowableDiagramData diagramData)
    {
        if (this.State == FlowableState.Canceling)
            return null;

        IFlowableResult rFrom = this.OK();
        if (diagramData.FlowableMode == DiagramFlowableMode.Port)
        {
            using (new PartDataInvokable(this, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
            {
                rFrom = await this?.TryInvokeAsync(null, diagramData);
                if (rFrom?.State == FlowableResultState.Error)
                    return false;
            }
        }
        var LinkDatas = this.GetToLinkDatas(diagramData).OfType<IFlowableLinkData>();
        foreach (var linkData in LinkDatas)
        {
            var lr = await linkData.Start(diagramData);
            if (lr != true)
                return lr;
        }
        return true;
    }
}

public class FlowablePortData<T> : FlowablePortData where T : Enum
{
    public FlowablePortData()
    {

    }

    public FlowablePortData(string nodeID, PortType portType) : base(nodeID, portType)
    {

    }
    public override ILinkData CreateLinkData()
    {
        return new FlowableLinkData<T>() { FromNodeID = this.NodeID, FromPortID = this.ID };
    }

}
