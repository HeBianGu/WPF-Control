// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Diagram.Presenter;

public static class DataConverterExtension
{
    public static Tuple<IEnumerable<INodeData>, IEnumerable<ILinkData>> SaveToDatas(this IEnumerable<Node> nodes)
    {
        DiagramDataSourceConverter converter = new DiagramDataSourceConverter(nodes.ToList());
        IEnumerable<ILinkData> linkDatas = converter.GetLinkType();
        IEnumerable<INodeData> nodeDatas = converter.GetNodeType();
        return Tuple.Create(nodeDatas, linkDatas);
    }

    public static IEnumerable<Node> LoadToNodes(this IEnumerable<INodeData> nodeDatas, IEnumerable<ILinkData> linkDatas)
    {
        DiagramDataSourceConverter converter = new DiagramDataSourceConverter(nodeDatas, linkDatas);
        return converter.NodeSource;
    }
}
