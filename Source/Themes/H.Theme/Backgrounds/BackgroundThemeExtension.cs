namespace H.Themes.Backgrounds;

public class BackgroundThemeExtension : MarkupExtension
{
    public BackgroundThemeType Type { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (this.Type == BackgroundThemeType.LinearGradientBrush)
            return new LinearGradientBrushResource().Resource;
        return new SolidColorBrushResource().Resource;
    }
}
