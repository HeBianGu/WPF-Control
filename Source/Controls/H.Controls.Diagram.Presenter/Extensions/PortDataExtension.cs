// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Extensions;

public static class PortDataExtension
{
    public static IEnumerable<INodeData> GetFromNodeDatas(this IPortData portData, IDiagramData diagramData)
    {
        return portData.GetFromLinkDatas(diagramData).Select(x => x.GetFromNodeData(diagramData));
    }

    public static IEnumerable<ILinkData> GetToLinkDatas(this IPortData port, IDiagramData diagramData)
    {
        return diagramData.LinkDatas.Where(l => l.FromPortID == port.ID);
    }

    public static IEnumerable<ILinkData> GetFromLinkDatas(this IPortData port, IDiagramData diagramData)
    {
        return diagramData.LinkDatas.Where(l => l.FromPortID == port.ID);
    }
}

