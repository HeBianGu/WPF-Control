using System.Windows.Markup;

namespace H.Extensions.FontIcon;

public class GetFontIconsExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return FontIcons.GetValues().OrderBy(x => x.Item1);
    }
}
