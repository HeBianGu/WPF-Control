using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Copper;

public class CopperDarkColorResource : IColorResource
{
    public string Name => "深铜色";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Copper;component/Dark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
