// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.GraphSource;

/// <summary>
/// 自定义匹配方式的树结构数据源
/// </summary>
/// <typeparam name="NodeType"></typeparam>
public class TreeGraphSource<NodeType> : IGraphSource
{
    public List<Node> Nodes { get; set; } = new List<Node>();

    private Func<NodeType, NodeType, bool> _childFiter;
    private Predicate<NodeType> _rootFiter;

    public TreeGraphSource(List<NodeType> nodes, Func<NodeType, NodeType, bool> childFiter, Predicate<NodeType> rootFiter)
    {
        _childFiter = childFiter;
        _rootFiter = rootFiter;

        this.Nodes = this.GetSource(nodes);
    }

    protected virtual List<Node> GetSource(List<NodeType> nodes)
    {

        List<Node> treeNodes = new List<Node>();

        if (nodes == null) return treeNodes;

        NodeType obj = nodes.FirstOrDefault(l => _rootFiter(l));

        TreeNode root = new TreeNode() { Content = obj };

        Action<NodeType, Node> action1 = null;

        action1 = (baseType, from) =>
        {
            IEnumerable<NodeType> children = nodes.Where(l => _childFiter(l, baseType));

            foreach (NodeType item in children)
            {
                TreeNode to = new TreeNode() { Content = item };

                Link.Create(from, to);

                treeNodes.Add(to);

                action1(item, to);
            }
        };

        treeNodes.Add(root);

        action1?.Invoke(obj, root);

        return treeNodes;
    }
}
