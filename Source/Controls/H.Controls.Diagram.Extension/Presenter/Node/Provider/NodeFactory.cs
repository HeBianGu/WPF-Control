

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace H.Controls.Diagram.Extension
{
    public static class NodeFactory
    {
        public static IEnumerable<INodeData> LoadGeometryNodeDatas(params Type[] type)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(TextNodeData).IsAssignableFrom(t)).Where(x => !x.IsAbstract);

            foreach (var item in types)
            {
                var result = Activator.CreateInstance(item) as TextNodeData;
                if (type == null)
                    yield return result;
                var nodeTypes = item.GetCustomAttributes<NodeTypeAttribute>();
                var nodeType = nodeTypes?.FirstOrDefault(l => type.Any(x => x == l.DiagramType));
                if (nodeType == null)
                    continue;
                var displayer = item.GetCustomAttribute<DisplayAttribute>();
                result.Name = displayer?.Name;
                result.Description = displayer?.Description;
                result.GroupName = displayer?.GroupName;
                yield return result;
            }
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

        public static IEnumerable<NodeGroup> GetNodeGroups(this IEnumerable<NodeData> nodeDatas)
        {
            var groups = nodeDatas.GroupBy(l => l.GroupName);
            foreach (var group in groups)
            {
                NodeGroup nodeGroup = new NodeGroup();
                nodeGroup.Name = group.Key;
                nodeGroup.AddRange(group);
                yield return nodeGroup;
            }
        }

        public static IEnumerable<NodeGroup> GetNodeGroups(params Type[] type)
        {
            var groups1 = LoadGeometryNodeDatas(type).OfType<TextNodeData>().GetNodeGroups();

            var groups2 = LoadIconNodeDatas(type).OfType<TextNodeData>().Take(10).GetNodeGroups();

            return groups1.Union(groups2);
        }

        //public static IEnumerable<NodeGroup> GetSymbolNodeGroups(params Type[] type)
        //{
        //    return LoadIconNodeDatas().OfType<SymbolNodeData>().Take(20).GetNodeGroups();
        //}

    }
}
