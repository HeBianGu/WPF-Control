// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Controls.Diagram.Parts.Base;
using H.Mvvm.ViewModels.Base;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace H.Controls.Diagram.Flowables;

public interface IFlowableNodeData : INodeData, IMessageable, IFlowablePartData
{
    Task<IFlowableResult> TryInvokeAsync(IFlowablePortData previors, IFlowableDiagramData diagram);

    Task<bool?> Start(IFlowableDiagramData diagramData, IFlowablePortData from = null);

    //Task<bool?> InvokeNodes(IFlowableDiagramData diagramData, IFlowableNodeData from = null);

    //Task<bool?> InvokeLinks(IFlowableDiagramData diagramData, IFlowablePartData from);

    //Task<bool?> InvokePorts(IFlowableDiagramData diagramData, IFlowablePartData from);
}
