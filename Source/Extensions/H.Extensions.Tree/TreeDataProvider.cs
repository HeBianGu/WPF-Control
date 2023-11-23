using H.Providers.Ioc;
using H.Providers.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace H.Extensions.Tree
{
    public class TreeDataProvider : LazyInstance<TreeDataProvider>
    {
        public IEnumerable<ITreeNode> GetTreeNodes(ITree tree, bool isRecursion = true)
        {
            var roots = tree.GetChildren(null);
            foreach (var root in roots)
            {
                var rootNode = new TreeNodeBase<object>(root);
                var childrenNodes = this.GetTreeNodes(root, tree, isRecursion);
                rootNode.Nodes = new ObservableCollection<TreeNodeBase<object>>(childrenNodes);
                yield return rootNode;
            }
        }

        public IEnumerable<TreeNodeBase<object>> GetTreeNodes(object parent, ITree tree, bool isRecursion = true)
        {
            foreach (var item in tree.GetChildren(parent))
            {
                var rootNode = new TreeNodeBase<object>(item);
                if (isRecursion)
                {
                    var childrenNodes = this.GetTreeNodes(item, tree);
                    rootNode.Nodes = new ObservableCollection<TreeNodeBase<object>>(childrenNodes);
                }
                yield return rootNode;
            }
        }
    }
}
