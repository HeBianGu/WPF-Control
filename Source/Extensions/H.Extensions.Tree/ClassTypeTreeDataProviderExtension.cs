using System;
using System.Windows.Markup;

namespace H.Extensions.Tree
{
    public class ClassTypeTreeDataProviderExtension : MarkupExtension
    {
        public Type Type { get; set; }
        public bool IsRecursion { get; set; } = false;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var tree = new ClassTypeTree(this.Type);
            var result = TreeDataProvider.Instance.GetTreeNodes(tree,this.IsRecursion);
            return result;
        }
    }
}
