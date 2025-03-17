// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Threading.Tasks;

namespace H.Controls.Diagram.Flowables;

public interface IFlowablePortData : IFlowable, IFlowablePartData, IPortData
{
    //IFlowableResult Invoke(IFlowablePartData previors, IFlowablePortData current, IFlowableDiagramData diagram);
    //Task<IFlowableResult> InvokeAsync(IFlowablePartData previors, IFlowablePortData current, IFlowableDiagramData diagram);
    //Task<IFlowableResult> TryInvokeAsync(IFlowablePartData previors, IFlowablePortData current, IFlowableDiagramData diagram);
}
