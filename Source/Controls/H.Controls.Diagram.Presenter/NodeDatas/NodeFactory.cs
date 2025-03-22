namespace H.Controls.Diagram.Presenter.NodeDatas;

public static class NodeFactory
{
    public static IEnumerable<INodeData> LoadGeometryNodeDatas(params Type[] type)
    {
        foreach (Type item in type)
        {
            foreach (IGeometryNodeData n in item.GetInstances<IGeometryNodeData>())
            {
                yield return n;
            }
        }

        //IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(TextNodeData).IsAssignableFrom(t)).Where(x => !x.IsAbstract);

        //foreach (Type item in types)
        //{
        //    TextNodeData result = Activator.CreateInstance(item) as TextNodeData;
        //    if (type == null)
        //        yield return result;
        //    IEnumerable<NodeTypeAttribute> nodeTypes = item.GetCustomAttributes<NodeTypeAttribute>();
        //    NodeTypeAttribute nodeType = nodeTypes?.FirstOrDefault(l => type.Any(x => x == l.DiagramType));
        //    if (nodeType == null)
        //        continue;
        //    DisplayAttribute displayer = item.GetCustomAttribute<DisplayAttribute>();
        //    result.Name = displayer?.Name;
        //    result.Description = displayer?.Description;
        //    result.GroupName = displayer?.GroupName;
        //    yield return result;
        //}
    }

    public static IEnumerable<INodeData> LoadIconNodeDatas(params Type[] type)
    {
        //var fields = typeof(IconAll).GetFields();
        //foreach (var field in fields)
        //{
        //    var v = field.GetValue(null);
        //    yield return new SymbolNodeData() { Icon = v?.ToString(), GroupName = "全部符号" };
        //}
        return null;
    }

    public static IEnumerable<NodeDataGroup> GetNodeGroups(this IEnumerable<NodeData> nodeDatas)
    {
        IEnumerable<IGrouping<string, NodeData>> groups = nodeDatas.GroupBy(l => l.GroupName);
        foreach (IGrouping<string, NodeData> group in groups)
        {
            NodeDataGroup nodeGroup = new NodeDataGroup();
            nodeGroup.Name = group.Key;
            nodeGroup.NodeDatas.AddRange(group);
            yield return nodeGroup;
        }
    }

    public static IEnumerable<NodeDataGroup> GetNodeGroups(params Type[] type)
    {
        IEnumerable<NodeDataGroup> groups1 = LoadGeometryNodeDatas(type).OfType<TextNodeData>().GetNodeGroups();

        IEnumerable<NodeDataGroup> groups2 = LoadIconNodeDatas(type).OfType<TextNodeData>().Take(10).GetNodeGroups();

        return groups1.Union(groups2);
    }

    //public static IEnumerable<NodeGroup> GetSymbolNodeGroups(params Type[] type)
    //{
    //    return LoadIconNodeDatas().OfType<SymbolNodeData>().Take(20).GetNodeGroups();
    //}

}
