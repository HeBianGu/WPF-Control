using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Technology;

/// <summary>
/// 星际无线靛：深邃而广阔，如同无垠的宇宙，充满了未知与探索。
/// </summary>
public class TechnologyIndigoColorResource : IColorResource
{
    public string Name => "星际无线靛";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyIndigo.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
