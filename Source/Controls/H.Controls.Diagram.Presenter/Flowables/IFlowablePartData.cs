// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Controls.Diagram.Flowables;

namespace H.Controls.Diagram.Presenter.Flowables;

public interface IFlowablePartData : IPartData, IFlowable
{
    FlowableState State { get; set; }
}
