using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Technology;

/// <summary>
/// SOIOR：清新而自然，如同春天的气息，充满了生机与活力。
/// </summary>
[Display(Name = "春日生机（推荐）", GroupName = "强力推荐", Description = "清新而自然，如同春天的气息，充满了生机与活力", Order = 20, Prompt = "强力推荐")]
public class TechnologySOIORColorResource : ColorResourceBase
{
    public TechnologySOIORColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologySOIOR.xaml")
    };
}
