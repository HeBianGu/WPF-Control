using H.Common.Interfaces;

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IDiagramData : ICloneable, IDable, INameable, IGroupable
{
    //Part SelectedPart { get; set; }
    //void Clear();
    IList<INodeData> NodeDatas { get; }
    IList<ILinkData> LinkDatas { get; }
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
        if (linkData == null)
            return null;
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

    public static IEnumerable<INodeData> GetFromNodeDatas(this IPortData portData, IDiagramData diagramData)
    {
        return portData.GetFromLinkDatas(diagramData).Select(x => x.GetFromNodeData(diagramData));
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
        return diagramData.GetPortDatas().FirstOrDefault(l => l.ID == linkData.FromPortID);
    }
    public static IPortData GetToPortData(this ILinkData linkData, IDiagramData diagramData)
    {
        return diagramData.GetPortDatas().FirstOrDefault(l => l.ID == linkData.ToPortID);
    }

    public static IEnumerable<INodeData> GetStartNodeDatas(this IDiagramData diagramData)
    {
        return diagramData.NodeDatas.Where(x =>
        {
            var finds = x.GetFromNodeDatas(diagramData);
            if (x is IPortableNodeData portable && portable.PortDatas.Count(x => x.PortType == PortType.Input) > 0)
                return false;
            return finds == null || finds.Count() == 0;
        });
    }

    public static T TryGetStartNodeData<T>(this IDiagramData diagramData, out string message) where T : INodeData
    {
        var starts = diagramData.GetStartNodeDatas().OfType<T>();
        if (starts == null || starts.Count() == 0)
        {
            message = "未找到起始节点";
            return default;
        }

        if (starts.Count() > 1)
        {
            message = "存在多个起始节点";
            return default;
        }
        message = null;
        return starts.First();
    }

    public static IEnumerable<INodeData> GetEndNodeDatas(this IDiagramData diagramData)
    {
        return diagramData.NodeDatas.Where(x => x.GetToNodeDatas(diagramData) == null);
    }
    public static IEnumerable<IPortData> GetPortDatas(this IDiagramData diagramData)
    {
        return diagramData.NodeDatas.SelectMany(x => x.GetPortDatas(diagramData));
    }

    public static IEnumerable<ILinkData> GetLinks(this INodeData nodeData, IDiagramData diagramData)
    {
        return nodeData.GetFromLinkDatas(diagramData).Concat(nodeData.GetToLinkDatas(diagramData));

    }
    public static void DeleteNodeData(this IDiagramData diagramData, INodeData nodeData)
    {
        var links = nodeData.GetLinks(diagramData);
        foreach (var item in links)
        {
            diagramData.LinkDatas.Remove(item);
        }
        diagramData.NodeDatas.Remove(nodeData);
    }

    public static void DeletePartData(this IDiagramData diagramData, IPartData partData)
    {
        if (partData is INodeData nodeData)
            diagramData.DeleteNodeData(nodeData);
        if (partData is ILinkData linkData)
            diagramData.LinkDatas.Remove(linkData);
        if (partData is IPortData portData)
            return;
    }
}

