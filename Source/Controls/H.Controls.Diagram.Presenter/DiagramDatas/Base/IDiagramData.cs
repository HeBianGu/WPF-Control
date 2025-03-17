namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IDiagramData : ICloneable, IDable, INameable, IGroupable
{
    //Part SelectedPart { get; set; }
    //void Clear();
    List<INodeData> NodeDatas { get; }
    List<ILinkData> LinkDatas { get; }
}

public static class DiagramDataExtension
{

    public static IEnumerable<ILinkData> GetToLinkDatas(this INodeData node, IDiagramData diagramData)
    {
        return diagramData.LinkDatas.Where(l => l.FromNodeID == node.ID);
    }

    public static IEnumerable<ILinkData> GetFromLinkDatas(this INodeData node, IDiagramData diagramData)
    {
        return diagramData.LinkDatas.Where(l => l.ToNodeID == node.ID);
    }

    public static INodeData GetFromNodeData(this ILinkData linkData, IDiagramData diagramData)
    {
        return diagramData.NodeDatas.FirstOrDefault(l => l.ID == linkData.FromNodeID);
    }

    public static INodeData GetToNodeData(this ILinkData linkData, IDiagramData diagramData)
    {
        return diagramData.NodeDatas.FirstOrDefault(l => l.ID == linkData.ToNodeID);
    }

    public static IEnumerable<INodeData> GetFromNodeDatas(this INodeData node, IDiagramData diagramData)
    {
        return node.GetFromLinkDatas(diagramData).Select(x => x.GetFromNodeData(diagramData));
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

    public static IEnumerable<ILinkData> GetToLinkDatas(this IPortData port, IDiagramData diagramData)
    {
        return diagramData.LinkDatas.Where(l => l.FromPortID == port.ID);
    }

    public static IEnumerable<ILinkData> GetFromLinkDatas(this IPortData port, IDiagramData diagramData)
    {
        return diagramData.LinkDatas.Where(l => l.FromPortID == port.ID);
    }
    public static IPortData GetFromPortData(this ILinkData linkData, IDiagramData diagramData)
    {
        return diagramData.GetPortDatas().FirstOrDefault(l => l.ID == linkData.FromNodeID);
    }
    public static IPortData GetToPortData(this ILinkData linkData, IDiagramData diagramData)
    {
        return diagramData.GetPortDatas().FirstOrDefault(l => l.ID == linkData.ToPortID);
    }

    public static IEnumerable<INodeData> GetStartNodeDatas(this IDiagramData diagramData)
    {
        return diagramData.NodeDatas.Where(x =>
        {
            var find = x.GetFromNodeDatas(diagramData);
            return find == null || find.Count() == 0;
        });
    }

    public static IEnumerable<INodeData> GetEndNodeDatas(this IDiagramData diagramData)
    {
        return diagramData.NodeDatas.Where(x => x.GetToNodeDatas(diagramData) == null);
    }
    public static IEnumerable<IPortData> GetPortDatas(this IDiagramData diagramData)
    {
        return diagramData.NodeDatas.SelectMany(x => x.GetPortDatas(diagramData));
    }
}

