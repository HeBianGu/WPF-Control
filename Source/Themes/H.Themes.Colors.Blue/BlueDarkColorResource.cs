using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Blue;
[Display(Name = "深蓝色（长期支持）", GroupName = "纯色", Description = "纯色", Order = 50, Prompt = "长期支持")]
public class BlueDarkColorResource : ColorResourceBase
{
    public BlueDarkColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Blue;component/Dark.xaml")
    };
}
