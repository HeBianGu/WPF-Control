using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Web;

public class WeUIColorResource : IColorResource
{
    public string Name => "‌WeUI";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/WeUI.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
