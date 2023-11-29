using System;
using System.Windows.Markup;

namespace H.Extensions.Tree.Demo
{
    public class ExploreTreeDataProviderExtension : MarkupExtension
    {
        public string Root { get; set; }
        public bool IsRecursion { get; set; } = false;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var tree = new ExploreTree() { Root = Root };
            var result = tree.GetTreeNodes(tree, IsRecursion);
            return result;
        }
    }
}
