// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Controls.Diagram.Presenter.Extensions;
using H.Controls.Diagram.Presenter.LinkDatas;
using H.Controls.Diagram.Presenter.PortDatas;
using System.Windows.Media.Animation;

namespace H.Controls.Diagram.Presenter.NodeDatas;

public class FlowableNodeData : ShowPropertyViewNodeDataBase, IFlowableNodeData
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

    private FlowableInvokeMode _invokeMode = FlowableInvokeMode.Serial;
    [Display(Name = "流程执行方式", GroupName = "流程控制", Description = "设置流程执行后续节点方式，串行或者并行")]
    public FlowableInvokeMode InvokeMode
    {
        get { return _invokeMode; }
        set
        {
            _invokeMode = value;
            RaisePropertyChanged();
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

    public virtual IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        Thread.Sleep(FlowableDiagramDataSetting.Instance.FlowSleepMillisecondsTimeout);
        return FlowableDiagramDataSetting.Instance.UseMock
            ? this.Random.Next(0, 19) == 1 ? this.Error("模拟仿真一个错误信息") : this.OK("模拟仿真一个成功信息")
            : this.OK("运行成功");
    }

    protected virtual async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData current)
    {
        return await Task.FromResult(this.OK());
    }

    public virtual async Task<IFlowableResult> InvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        var r = await this.BeforeInvokeAsync(previors, diagram);
        if (r.State != FlowableResultState.OK)
            return r;
        return await Task.Run(() =>
        {
            return this.Invoke(previors, diagram);
        });
    }

    protected virtual bool CanInvoke(IFlowableLinkData previors, IFlowableDiagramData current)
    {
        return true;
    }

    public virtual async Task<IFlowableResult> TryInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        if (!this.CanInvoke(previors, diagram))
        {
            this.State = FlowableState.Break;
            this.Message = "不满足执行条件阻止流程";
            return FlowableResult.Break;
        }

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
                if (result.State == FlowableResultState.Break)
                {
                    this.State = FlowableState.Break;
                    this.Message = "不满足执行条件阻止流程";
                    return FlowableResult.Break;
                }
                else
                {
                    if (this.UseInfoLogger)
                        IocLog.Instance?.Info(result.State == FlowableResultState.Error ? $"运行错误<{this.GetType().Name}>:{this.Text} {result.Message}" : $"执行完成<{this.GetType().Name}>:{this.Text} {result.Message}");
                    this.State = result.ToState();
                    return result;
                }
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

    public override IFlowableLinkData CreateLinkData()
    {
        return new FlowableLinkData() { FromNodeID = this.ID };
    }

    public override IFlowablePortData CreatePortData()
    {
        return new FlowablePortData(this.ID, PortType.Both);
    }
    public virtual void Dispose()
    {

    }

    protected T GetFromNodeData<T>(IDiagramData diagramData, IFlowableLinkData from = null) where T : INodeData
    {
        if (from == null)
            return default;

        if (from.GetFromNodeData(diagramData) is T t)
            return t;
        return default(T);
    }

    protected T GetStartFromNodeData<T>(IDiagramData diagramData) where T : INodeData
    {
        return diagramData.GetStartNodeDatas().OfType<T>().FirstOrDefault();
    }

    public virtual async Task<bool?> Start(IFlowableDiagramData diagramData, IFlowableLinkData from = null)
    {
        if (this.State == FlowableState.Canceling)
            return null;
        IFlowableResult nresult;
        using (new PartDataInvokable(this, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
        {
            nresult = await this.TryInvokeAsync(from, diagramData) as FlowableResult;
            if (nresult == null)
                return null;
            if (nresult.State == FlowableResultState.Break)
                return true;
            if (nresult.State == FlowableResultState.Error)
                return false;
        }
        var toports = this.GetFlowablePortDatas(diagramData).ToList();
        if (this.InvokeMode == FlowableInvokeMode.Serial)
        {
            foreach (var portData in toports)
            {
                var lr = await portData.Item1.Start(diagramData, this, portData.Item2);
                if (lr != true)
                    return lr;
            }
            return true;
        }
        else
        {
            var tasks = toports.Select(x => x.Item1.Start(diagramData, this, x.Item2));
            var all = await Task.WhenAll(tasks.ToArray());
            return all.All(x => x != false);
        }


    }

    protected virtual IEnumerable<Tuple<IFlowablePortData, Predicate<IFlowableLinkData>>> GetFlowablePortDatas(IFlowableDiagramData diagramData)
    {
        var toLinks = this.GetToLinkDatas(diagramData).OfType<IFlowableLinkData>();
        return toLinks.Select(x => x.GetFromPortData(diagramData)).Distinct().OfType<IFlowablePortData>().Select(x => new Tuple<IFlowablePortData, Predicate<IFlowableLinkData>>(x, null));
    }

}

public class StartFlowableNodeData : FlowableNodeData
{
    public StartFlowableNodeData()
    {
        this.UseStart = true;
    }
}
