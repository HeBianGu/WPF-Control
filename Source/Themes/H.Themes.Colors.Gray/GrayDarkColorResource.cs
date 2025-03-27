using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Gray;

public class GrayDarkColorResource : IColorResource
{
    public string Name => "深灰色";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Gray;component/Dark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
