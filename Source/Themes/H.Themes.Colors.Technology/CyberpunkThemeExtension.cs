using System.Windows.Markup;

namespace H.Themes.Colors.Technology;

public class CyberpunkThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new CyberpunkColorResource().Resource;
    }
}
