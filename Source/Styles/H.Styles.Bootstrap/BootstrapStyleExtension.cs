using System.Windows.Markup;

namespace H.Styles.Bootstrap;

public class BootstrapStyleExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Styles.Bootstrap;component/BootstrapControls.xaml")
        };
    }
}
