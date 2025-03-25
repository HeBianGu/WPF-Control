using System.Windows.Markup;

namespace H.Extensions.Tree;

public class ClassTypeTreeExtension : MarkupExtension
{
    public Type Type { get; set; }
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new ClassTypeTree(this.Type);
    }
}
