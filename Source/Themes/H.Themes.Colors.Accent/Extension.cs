using System.Windows.Markup;

namespace H.Themes.Colors.Accent;

public class AccentLightThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new AccentLightColorResource().Resource;
    }
}

public class AccentDarkThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new AccentDarkColorResource().Resource;
    }
}
