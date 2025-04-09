
namespace H.Styles.StyleResources;

public class ConciseStyleExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new ConciseStyleResource().Resource;
    }
}
