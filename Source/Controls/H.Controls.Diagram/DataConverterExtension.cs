// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Linq;

namespace H.Controls.Diagram;

public static class DataConverterExtension
{
    public static Tuple<IEnumerable<INodeData>, IEnumerable<ILinkData>> SaveToDatas(this IEnumerable<Node> nodes)
    {
        DiagramDataSource converter = new DiagramDataSource(nodes.ToList());
        IEnumerable<ILinkData> linkDatas = converter.GetLinkDatas();
        IEnumerable<INodeData> nodeDatas = converter.GetNodeDatas();
        return Tuple.Create(nodeDatas, linkDatas);
    }

    public static IEnumerable<Node> LoadToNodes(this IEnumerable<INodeData> nodeDatas, IEnumerable<ILinkData> linkDatas)
    {
        DiagramDataSource converter = new DiagramDataSource(nodeDatas, linkDatas);
        return converter.Nodes;
    }
}
