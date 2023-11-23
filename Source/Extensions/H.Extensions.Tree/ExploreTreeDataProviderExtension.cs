using System;
using System.Windows.Markup;

namespace H.Extensions.Tree
{
    public class ExploreTreeDataProviderExtension : MarkupExtension
    {
        public string Root { get; set; }
        public bool IsRecursion { get; set; } = false;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var tree = new ExploreTree() { Root = Root };
            var result = TreeDataProvider.Instance.GetTreeNodes(tree, this.IsRecursion);
            return result;
        }
    }
}
