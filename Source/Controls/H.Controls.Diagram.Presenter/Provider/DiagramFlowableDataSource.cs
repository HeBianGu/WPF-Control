// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Provider;

public class FlowableDiagramDataSource : DiagramDataSource
{
    public FlowableDiagramDataSource(List<Node> nodeSource) : base(nodeSource)
    {
    }

    public FlowableDiagramDataSource(IEnumerable<INodeData> nodes, IEnumerable<ILinkData> links) : base(nodes, links)
    {

    }

    protected override ILinkData CreateLinkData()
    {
        return new FlowableLinkData();
    }

}
