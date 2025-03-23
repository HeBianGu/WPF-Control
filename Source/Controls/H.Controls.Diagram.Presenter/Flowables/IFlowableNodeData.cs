// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Controls.Diagram.Parts.Base;
using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Mvvm.ViewModels.Base;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace H.Controls.Diagram.Presenter.Flowables;

public interface IFlowableNodeData : INodeData, IMessageable, IFlowablePartData
{
    //Task<IFlowableResult> TryInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram);

    Task<bool?> Start(IFlowableDiagramData diagramData, IFlowableLinkData from = null);
}

