using H.Common;
using H.Controls.Diagram.Datas;
using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Controls.Diagram.Presenter.LinkDatas;

public class FlowableLinkData : TextLinkData, IFlowableLinkData
{
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
    [System.Text.Json.Serialization.JsonIgnore]

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

    private bool _useInfoLogger = true;
    [System.Text.Json.Serialization.JsonIgnore]

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
    //[XmlIgnore]
    //[Display(Name = "执行")]
    //public RelayCommand InvokeCommand => new RelayCommand(async l => await this.TryInvokeAsync(null, null));

    public IFlowableResult Invoke(IFlowableDiagramData diagram)
    {
        Thread.Sleep(FlowableDiagramDataSetting.Instance.FlowSleepMillisecondsTimeout);
        return FlowableDiagramDataSetting.Instance.UseMock
            ? this.Random.Next(0, 19) == 1 ? this.Error("模拟仿真一个错误信息") : this.OK("模拟仿真一个成功信息")
            : this.OK("运行成功");
    }

    public virtual async Task<IFlowableResult> InvokeAsync(IFlowableDiagramData diagram)
    {
        return await Task.Run(() =>
        {
            return this.Invoke(diagram);
        });
    }
    public virtual async Task<IFlowableResult> TryInvokeAsync(IFlowableDiagramData diagram)
    {
        try
        {
            this.State = FlowableState.Running;
            this.IsBuzy = true;
            using (var stopwatch = new Stopwatchable(this))
            {
                IocLog.Instance?.Info($"正在执行<{this.GetType().Name}>:{this.Text}");
                IFlowableResult result = await InvokeAsync(diagram);
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

    /// <summary>
    /// 匹配运行时判定指向的哪个流程
    /// </summary>
    /// <param name="flowableResult"></param>
    /// <returns></returns>
    public virtual bool IsMatchResult(IFlowableResult flowableResult)
    {
        return true;
    }

    public async Task<bool?> Start(IFlowableDiagramData diagramData)
    {
        if (diagramData.FlowableMode != DiagramFlowableMode.Node)
        {    //  Do ：Links
            if (this.State == FlowableState.Canceling)
                return null;
            this.State = FlowableState.Running;
            using (new PartDataInvokable(this, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
            {
                IFlowableResult r = await this?.TryInvokeAsync(diagramData);
                this.State = r?.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
                if (r?.State == FlowableResultState.Error)
                    return false;
            }
        }

        //  Do ：To Ports
        IFlowablePortData tPort = this.GetToPortData(diagramData) as IFlowablePortData;
        if (tPort != null && diagramData.FlowableMode == DiagramFlowableMode.Port)
        {
            if (this.State == FlowableState.Canceling)
                return null;
            using (new PartDataInvokable(tPort, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
            {
                IFlowableResult rTo = await tPort?.TryInvokeAsync(this, diagramData);
                if (rTo?.State == FlowableResultState.Error)
                    return false;
            }
        }
        var tNodeData = this.GetToNodeData(diagramData) as IFlowableNodeData;
        //  Do ：递归执行ToNode
        bool? b = await tNodeData?.Start(diagramData, this);
        if (b != true)
            return b;
        return true;
    }
}

public class FlowableLinkData<T> : FlowableLinkData where T : Enum
{
    public FlowableLinkData()
    {
        this.RefreshText();
    }
    private T _nodeResult;
    [Display(Name = "节点结果", GroupName = "常用")]
    public T NodeResult
    {
        get { return _nodeResult; }
        set
        {
            _nodeResult = value;
            RaisePropertyChanged();
            this.RefreshText();
        }
    }

    private void RefreshText()
    {
        DisplayEnumConverter display = new DisplayEnumConverter(typeof(T));
        this.Text = display.ConvertTo(this.NodeResult, typeof(string))?.ToString();
    }
    public override bool IsMatchResult(IFlowableResult flowableResult)
    {
        if (flowableResult is FlowableResult<T> result)
            //  Do ：结果类型相同，并且参数值相同才可以执行
            return this.NodeResult.Equals(result.Value);
        return false;
    }
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum BoolResult
{
    [Display(Name = "是")]
    True,
    [Display(Name = "否")]
    False
}
