using System.Windows.Markup;

namespace H.Styles;

public class ConciseStyleExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Style;component/ConciseControls.xaml")
        };
    }
}
