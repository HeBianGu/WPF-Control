// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Diagram.Presenter.Flowables;

public interface IFlowableResult
{
    string Message { get; }
    FlowableResultState State { get; set; }
}
