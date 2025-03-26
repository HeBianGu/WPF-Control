using System.Windows.Markup;

namespace H.Styles.Default;


public class LogoExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return LogoDataProvider.Logo;
    }
}
