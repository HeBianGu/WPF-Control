using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Technology;

/// <summary>
/// SOIOR：清新而自然，如同春天的气息，充满了生机与活力。
/// </summary>
public class TechnologySOIORColorResource : IColorResource
{
    public string Name => "春日生机";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologySOIOR.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
