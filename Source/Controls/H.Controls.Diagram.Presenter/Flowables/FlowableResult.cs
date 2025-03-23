// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Diagram.Presenter.Flowables;

public class FlowableResult : IFlowableResult
{
    public FlowableResult(string message)
    {
        this.Message = message;
    }
    public string Message { get; }

    public FlowableResultState State { get; set; }
}

public class FlowableResult<T> : FlowableResult
{
    public FlowableResult(T t) : base(null)
    {
        this.State = FlowableResultState.OK;
        this.Value = t;
    }
    public FlowableResult(T t, string message) : base(message)
    {
        this.State = FlowableResultState.OK;
        this.Value = t;
    }
    public T Value { get; set; }
}
