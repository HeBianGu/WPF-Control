// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Extensions;

public static class LinkDataExtension
{
    public static INodeData GetToNodeData(this ILinkData linkData, IDiagramData diagramData)
    {
        return diagramData.NodeDatas.FirstOrDefault(l => l.ID == linkData.ToNodeID);
    }


    public static INodeData GetFromNodeData(this ILinkData linkData, IDiagramData diagramData)
    {
        if (linkData == null)
            return null;
        return diagramData.NodeDatas.FirstOrDefault(l => l.ID == linkData.FromNodeID);
    }

    public static IPortData GetFromPortData(this ILinkData linkData, IDiagramData diagramData)
    {
        return diagramData.GetPortDatas().FirstOrDefault(l => l.ID == linkData.FromPortID);
    }
    public static IPortData GetToPortData(this ILinkData linkData, IDiagramData diagramData)
    {
        return diagramData.GetPortDatas().FirstOrDefault(l => l.ID == linkData.ToPortID);
    }
}

