using System.Windows.Markup;

namespace H.Themes.Colors.Futurism;

public class FuturismThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new FuturismColorResource().Resource;
    }
}
