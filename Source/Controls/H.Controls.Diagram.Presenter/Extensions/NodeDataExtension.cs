// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Extensions;

public static class NodeDataExtension
{
    public static IEnumerable<ILinkData> GetToLinkDatas(this INodeData node, IDiagramData diagramData)
    {
        return diagramData.LinkDatas.Where(l => l.FromNodeID == node.ID);
    }

    public static IEnumerable<ILinkData> GetFromLinkDatas(this INodeData node, IDiagramData diagramData)
    {
        return diagramData.LinkDatas.Where(l => l.ToNodeID == node.ID);
    }

    public static IEnumerable<INodeData> GetFromNodeDatas(this INodeData node, IDiagramData diagramData)
    {
        if (diagramData == null)
            return Enumerable.Empty<INodeData>();
        return node.GetFromLinkDatas(diagramData).Select(x => x.GetFromNodeData(diagramData));
    }

    public static IEnumerable<INodeData> GetAllFromNodeDatas(this INodeData node, IDiagramData diagramData)
    {
        var froms = node.GetFromNodeDatas(diagramData);
        foreach (var from in froms)
        {
            yield return from;
            var finds = from.GetAllFromNodeDatas(diagramData);
            foreach (var find in finds)
            {
                yield return find;
            }
        }
    }

    public static IEnumerable<INodeData> GetSelectedFromNodeDatas(this INodeData node, IDiagramData diagramData)
    {
        IEnumerable<INodeData> froms = null;
        if (node is ISelectableFromNodeData selectableFromNodeData)
            froms = selectableFromNodeData.GetSelectedFromNodeDatas();
        else
            froms = node.GetFromNodeDatas(diagramData);

        foreach (var from in froms)
        {
            yield return from;
            var finds = from.GetSelectedFromNodeDatas(diagramData);
            foreach (var find in finds)
            {
                yield return find;
            }
        }
    }

    public static IEnumerable<INodeData> GetToNodeDatas(this INodeData node, IDiagramData diagramData)
    {
        return node.GetToLinkDatas(diagramData).Select(x => x.GetToNodeData(diagramData));
    }

    public static IEnumerable<IPortData> GetPortDatas(this INodeData node, IDiagramData diagramData)
    {
        if (node is IPortableNodeData portable)
            return portable.PortDatas;
        return Enumerable.Empty<IPortData>();
    }

    public static IEnumerable<IPortData> GetToNodePortData(this INodeData node, INodeData toNode, IDiagramData diagramData)
    {
        return node.GetToLinkDatas(diagramData).Where(x => x.ToNodeID == toNode.ID).Select(x => x.GetFromPortData(diagramData));
    }

    public static IEnumerable<ILinkData> GetLinks(this INodeData nodeData, IDiagramData diagramData)
    {
        return nodeData.GetFromLinkDatas(diagramData).Concat(nodeData.GetToLinkDatas(diagramData));

    }
}

