using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Industrial;
[Display(Name = "暗黑工业风", GroupName = "生产工业风", Description = "纯色", Order = 100, Prompt = "试验")]
public class IndustrialDarkColorResource : ColorResourceBase
{
    public IndustrialDarkColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/Dark.xaml")
    };
}
