// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Extensions;

public static class DiagramableNodeDataExtension
{
    public static IEnumerable<INodeData> GetFromNodeDatas(this IDiagramableNodeData nodeData)
    {
        return nodeData.GetFromNodeDatas(nodeData.DiagramData);
    }

    public static IEnumerable<INodeData> GetAllFromNodeDatas(this IDiagramableNodeData nodeData)
    {
        return nodeData.GetAllFromNodeDatas(nodeData.DiagramData).Distinct();
    }

    public static IEnumerable<T> GetAllFromNodeDatas<T>(this IDiagramableNodeData nodeData)
    {
        return nodeData.GetAllFromNodeDatas().OfType<T>();
    }

    public static IEnumerable<INodeData> GetToNodeDatas(this IDiagramableNodeData nodeData)
    {
        return nodeData.GetToNodeDatas(nodeData.DiagramData);
    }
}

