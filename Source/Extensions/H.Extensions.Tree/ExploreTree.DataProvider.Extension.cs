using H.Mvvm.ViewModels.Tree;
using System.Windows.Markup;

namespace H.Extensions.Tree;

public class ExploreTreeDataProviderExtension : MarkupExtension
{
    public string Root { get; set; }
    public bool IsRecursion { get; set; } = false;
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        ExploreTree tree = new ExploreTree()
        {
            Root = this.Root
        };
        System.Collections.Generic.IEnumerable<ITreeNode> result = tree.GetTreeNodes(this.IsRecursion);
        return result;
    }
}
