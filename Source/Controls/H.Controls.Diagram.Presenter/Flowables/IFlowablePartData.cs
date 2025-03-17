// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Diagram.Flowables;

public interface IFlowablePartData : IPartData, IFlowable
{
    FlowableState State { get; set; }

    Task<IFlowableResult> TryInvokeAsync(IFlowablePartData previors, IFlowableDiagramData diagram);
}
