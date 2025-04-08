using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Technology;

/// <summary>
/// 电骼兽藏青：深邃而神秘，如同夜晚的星空，引人深思。
/// </summary>
[Display(Name = "电骼兽藏青", GroupName = "深色科技风", Description = "深邃而神秘，如同夜晚的星空，引人深思", Order = 100, Prompt = "试验")]
public class TechnologyCyanColorResource : ColorResourceBase
{
    public TechnologyCyanColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyCyan.xaml")
    };
}
