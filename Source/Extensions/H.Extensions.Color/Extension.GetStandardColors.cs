using System.Windows.Markup;

namespace H.Extensions.Color;

public class GetStandardColorsExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return ColorFactory.CreateStandardColors();
    }
}
