using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Technology;

/// <summary>
/// 芭比曙光粉：明亮而充满活力，如同晨曦中的第一缕阳光，温暖而希望。
/// </summary>
[Display(Name = "芭比曙光粉（长期支持）", GroupName = "清新自然风", Description = "明亮而充满活力，如同晨曦中的第一缕阳光，温暖而希望", Order = 10, Prompt = "长期支持")]
public class TechnologyPinkColorResource : ColorResourceBase
{
    public TechnologyPinkColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyPink.xaml")
    };
}
