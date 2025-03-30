using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Web;

public class BootstrapColorResource : IColorResource
{
    public string Name => "‌Bootstrap";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/Bootstrap.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
