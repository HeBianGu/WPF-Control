using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace H.Extensions.Tree
{
    public static class TreeExtension
    {
        public static IEnumerable<ITreeNode> GetTreeNodes(this ITree tree, bool isRecursion = true)
        {
            var roots = tree.GetChildren(null);
            foreach (var root in roots)
            {
                var rootNode = new TreeNodeBase<object>(root);
                var childrenNodes = tree.GetTreeNodes(root, isRecursion);
                rootNode.Nodes = new ObservableCollection<TreeNodeBase<object>>(childrenNodes);
                yield return rootNode;
            }
        }

        public static IEnumerable<TreeNodeBase<object>> GetTreeNodes(this ITree tree, object parent, bool isRecursion = true)
        {
            foreach (var item in tree.GetChildren(parent))
            {
                var rootNode = new TreeNodeBase<object>(item);
                if (isRecursion)
                {
                    var childrenNodes = tree.GetTreeNodes(item);
                    rootNode.Nodes = new ObservableCollection<TreeNodeBase<object>>(childrenNodes);
                }
                yield return rootNode;
            }
        }
    }

}
