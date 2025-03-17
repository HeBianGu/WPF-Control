// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Threading.Tasks;

namespace H.Controls.Diagram.Flowables;

public interface IFlowableLinkData : IFlowable, ILinkData, IFlowablePartData
{
    //IFlowableResult Invoke(IFlowablePartData previors, IFlowableLinkData current, IFlowableDiagramData diagram);
    //Task<IFlowableResult> InvokeAsync(IFlowablePartData previors, IFlowableLinkData current, IFlowableDiagramData diagram);
    //Task<IFlowableResult> TryInvokeAsync(IFlowablePartData previors, IFlowableLinkData current, IFlowableDiagramData diagram);
    bool IsMatchResult(FlowableResult flowableResult);
}
