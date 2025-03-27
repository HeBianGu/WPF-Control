using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Purple;

public class PurpleLightColorResource : IColorResource
{
    public string Name => "浅紫色";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Purple;component/Light.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
