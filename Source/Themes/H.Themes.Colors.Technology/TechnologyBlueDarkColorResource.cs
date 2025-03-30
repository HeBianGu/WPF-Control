using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Technology;

public class TechnologyBlueDarkColorResource : IColorResource
{
    public string Name => "深空科技蓝";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/Dark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
