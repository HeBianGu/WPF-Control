// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Diagram.Presenter.Flowables;

public enum FlowableState
{
    Ready = 0,
    Wait,
    Running,
    Success,
    Error,
    Canceling,
    Canceled,
    Stopped,
    Pause
}
