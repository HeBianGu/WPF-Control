using System.Windows.Markup;

namespace H.Themes.Colors.Gray;

public class GrayDarkThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new GrayDarkColorResource().Resource;
    }
}

public class GrayLightThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new GrayLightColorResource().Resource;
    }
}
