using H.Mvvm.ViewModels.Tree;
using System.Windows.Markup;

namespace H.Extensions.Tree;

public class ClassTypeTreeDataProviderExtension : MarkupExtension
{
    public Type Type { get; set; }
    public bool IsRecursion { get; set; } = false;
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        ClassTypeTree tree = new ClassTypeTree(this.Type);
        System.Collections.Generic.IEnumerable<ITreeNode> result = tree.GetTreeNodes(this.IsRecursion);
        return result;
    }
}
