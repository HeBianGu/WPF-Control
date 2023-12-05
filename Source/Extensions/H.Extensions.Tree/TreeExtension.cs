using H.Providers.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace H.Extensions.Tree
{
    public static class TreeExtension
    {
        public static IEnumerable<ITreeNode> GetTreeNodes(this ITree tree, bool isRecursion = true)
        {
            System.Collections.IEnumerable roots = tree.GetChildren(null);
            foreach (object root in roots)
            {
                TreeNodeBase<object> rootNode = new TreeNodeBase<object>(root);
                IEnumerable<TreeNodeBase<object>> childrenNodes = tree.GetTreeNodes(root, isRecursion);
                rootNode.Nodes = new ObservableCollection<TreeNodeBase<object>>(childrenNodes);
                yield return rootNode;
            }
        }

        public static IEnumerable<TreeNodeBase<object>> GetTreeNodes(this ITree tree, object parent, bool isRecursion = true)
        {
            foreach (object item in tree.GetChildren(parent))
            {
                TreeNodeBase<object> rootNode = new TreeNodeBase<object>(item);
                if (isRecursion)
                {
                    IEnumerable<TreeNodeBase<object>> childrenNodes = tree.GetTreeNodes(item);
                    rootNode.Nodes = new ObservableCollection<TreeNodeBase<object>>(childrenNodes);
                }
                yield return rootNode;
            }
        }
    }

}
