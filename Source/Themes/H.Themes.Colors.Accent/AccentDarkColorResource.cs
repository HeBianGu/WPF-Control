using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Accent;
[Display(Name = "深主题", GroupName = "纯色", Description = "纯色", Order = 50, Prompt = "长期支持")]
public class AccentDarkColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Accent;component/Dark.xaml")
    };
}
