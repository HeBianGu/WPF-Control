// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Controls.Diagram.Parts.Base;
using H.Mvvm.ViewModels.Base;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace H.Controls.Diagram.Flowables;

public interface IFlowableNodeData : INodeData, IFlowable, IMessageable, IFlowablePartData
{
    //public bool UseStart { get; set; }
    //IFlowableResult Invoke(IFlowablePartData previors, IFlowableNodeData current, IFlowableDiagramData diagram);
    //Task<IFlowableResult> InvokeAsync(IFlowablePartData previors, IFlowableNodeData current, IFlowableDiagramData diagram);
    //Task<IFlowableResult> TryInvokeAsync(IFlowablePartData previors, IFlowableNodeData current, IFlowableDiagramData diagram);
}

public interface IFlowablePartData : IPartData, IFlowable
{
    FlowableState State { get; set; }

    Task<IFlowableResult> TryInvokeAsync(IFlowablePartData previors, IFlowableDiagramData diagram);
}
