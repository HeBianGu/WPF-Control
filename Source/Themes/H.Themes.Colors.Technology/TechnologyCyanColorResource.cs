using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Technology;

/// <summary>
/// 电骼兽藏青：深邃而神秘，如同夜晚的星空，引人深思。
/// </summary>
public class TechnologyCyanColorResource : IColorResource
{
    public string Name => "电骼兽藏青";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyCyan.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
