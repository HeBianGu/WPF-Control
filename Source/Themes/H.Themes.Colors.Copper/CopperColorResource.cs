using H.Theme.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Copper;
[Display(Name = "深铜色", GroupName = "纯色", Description = "纯色", Order = 100, Prompt = "试验")]
public class CopperColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Copper;component/Dark.xaml")
    };
}
