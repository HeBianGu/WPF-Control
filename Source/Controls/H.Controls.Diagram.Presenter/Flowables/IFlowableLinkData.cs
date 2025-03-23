// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Threading.Tasks;

namespace H.Controls.Diagram.Presenter.Flowables;

public interface IFlowableLinkData : ILinkData, IFlowablePartData
{
    Task<bool?> Start(IFlowableDiagramData diagramData);
}
