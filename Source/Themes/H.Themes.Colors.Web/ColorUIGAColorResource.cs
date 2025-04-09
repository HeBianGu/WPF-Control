using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Web;
[Display(Name = "ColorUI-GA", GroupName = "网站前端风", Description = "纯色", Order = 100, Prompt = "试验")]
public class ColorUIGAColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/‌ColorUI-GA.xaml")
    };
}
