using System.Windows.Markup;

namespace H.Themes.Colors.Cyberpunk;

public class CyberpunkThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new CyberpunkColorResource().Resource;
    }
}
