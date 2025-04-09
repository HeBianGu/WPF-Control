using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Technology;

/// <summary>
/// 星际无线靛：深邃而广阔，如同无垠的宇宙，充满了未知与探索。
/// </summary>
[Display(Name = "星际无线靛", GroupName = "深色科技风", Description = "深邃而广阔，如同无垠的宇宙，充满了未知与探索", Order = 100, Prompt = "试验")]
public class TechnologyIndigoColorResource : ColorResourceBase
{
    public TechnologyIndigoColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyIndigo.xaml")
    };
}
