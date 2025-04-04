using H.Theme.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Accent;
[Display(Name = "浅主题", GroupName = "强力推荐", Description = "纯色", Order = 10, Prompt = "强力推荐")]
public class AccentLightColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Accent;component/Light.xaml")
    };
}
