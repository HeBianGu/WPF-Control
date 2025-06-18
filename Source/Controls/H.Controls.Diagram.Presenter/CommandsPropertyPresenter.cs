// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.Extensions;
using H.Controls.Form;
using H.Presenters.Common;

namespace H.Controls.Diagram.Presenter;

public class CommandsPropertyPresenter : DialogCommandsPresenter<TabFormPresenter>
{
    public IDiagramableNodeData NodeData { get; set; }
    public CommandsPropertyPresenter(IDiagramableNodeData nodeData) : base(new TabFormPresenter(nodeData))
    {
        this.NodeData = nodeData;
    }
}
