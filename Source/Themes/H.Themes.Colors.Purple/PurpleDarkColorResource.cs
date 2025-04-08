using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Purple;
[Display(Name = "深紫色（长期支持）", GroupName = "纯色", Description = "纯色", Order = 100, Prompt = "长期支持")]
public class PurpleDarkColorResource : ColorResourceBase
{
    public PurpleDarkColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Purple;component/Dark.xaml")
    };
}
