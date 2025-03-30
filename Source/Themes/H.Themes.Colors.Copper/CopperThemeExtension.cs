using System.Windows.Markup;

namespace H.Themes.Colors.Copper;

public class CopperThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new CopperColorResource().Resource;
    }
}
