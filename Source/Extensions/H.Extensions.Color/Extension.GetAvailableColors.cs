using System.Windows.Markup;

namespace H.Extensions.Color;

public class GetAvailableColorsExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return ColorFactory.CreateAvailableColors();
    }
}
