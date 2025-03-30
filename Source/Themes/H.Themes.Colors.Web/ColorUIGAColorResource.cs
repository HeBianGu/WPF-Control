using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Web;

public class ColorUIGAColorResource : IColorResource
{
    public string Name => "‌‌ColorUI-GA";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/‌ColorUI-GA.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
