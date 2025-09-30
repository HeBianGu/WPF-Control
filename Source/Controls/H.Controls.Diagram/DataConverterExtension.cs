// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
