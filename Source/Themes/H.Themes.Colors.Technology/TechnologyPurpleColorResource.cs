using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Technology;

public class TechnologyPurpleColorResource : IColorResource
{
    public string Name => "深空科技紫";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyPurple.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
