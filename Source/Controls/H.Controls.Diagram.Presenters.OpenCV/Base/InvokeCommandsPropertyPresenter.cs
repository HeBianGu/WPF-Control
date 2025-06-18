// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter;
using H.Controls.Diagram.Presenter.Extensions;
using H.Controls.Diagram.Presenter.NodeDatas.Base;
using H.Extensions.Mvvm.Commands;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public class InvokeCommandsPropertyPresenter : CommandsPropertyPresenter
{
    public InvokeCommandsPropertyPresenter(IDiagramableNodeData nodeData) : base(nodeData)
    {
    }

    [Icon(FontIcons.Delete)]
    [Display(Name = "执行", GroupName = "操作")]
    public DisplayCommand InvokeCommand => new DisplayCommand(async x =>
    {
        var srcs = this.NodeData.GetSelectedFromNodeDatas(this.NodeData.DiagramData).OfType<ISrcImageNodeData>();
        if (this.NodeData.DiagramData is IFlowableDiagramData flowableDiagramData)
            await flowableDiagramData.Start();
    });
}

