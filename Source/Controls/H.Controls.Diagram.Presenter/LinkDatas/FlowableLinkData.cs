namespace H.Controls.Diagram.Presenter.LinkDatas;

public class FlowableLinkData : TextLinkData, IFlowableLink
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
    [XmlIgnore]
    [Display(Name = "执行")]
    public RelayCommand InvokeCommand => new RelayCommand(async l => await this.TryInvokeAsync(null, null));

    public IFlowableResult Invoke(Part previors, Link current)
    {
        Thread.Sleep(DiagramAppSetting.Instance.FlowSleepMillisecondsTimeout);
        return DiagramAppSetting.Instance.UseMock
            ? this.Random.Next(0, 19) == 1 ? this.Error("模拟仿真一个错误信息") : this.OK("模拟仿真一个成功信息")
            : this.OK("运行成功");
    }

    public virtual async Task<IFlowableResult> InvokeAsync(Part previors, Link current)
    {
        return await Task.Run(() =>
        {
            return this.Invoke(previors, current);
        });
    }
    public virtual async Task<IFlowableResult> TryInvokeAsync(Part previors, Link current)
    {
        try
        {
            this.State = FlowableState.Running;
            this.IsBuzy = true;
            IocLog.Instance?.Info($"正在执行<{this.GetType().Name}>:{this.Text}");
            IFlowableResult result = await InvokeAsync(previors, current);
            IocLog.Instance?.Info(result.State == FlowableResultState.Error ? $"运行错误<{this.GetType().Name}>:{this.Text} {result.Message}" : $"执行完成<{this.GetType().Name}>:{this.Text} {result.Message}");
            this.State = result.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
            return result;
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
    public virtual bool IsMatchResult(FlowableResult flowableResult)
    {
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
    public override bool IsMatchResult(FlowableResult flowableResult)
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
