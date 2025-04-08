using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Platform;
[Display(Name = "‌Fluent UI (Microsoft)", GroupName = "系统平台", Description = "纯色", Order = 100, Prompt = "试验")]
public class FluentUIColorResource : ColorResourceBase
{
    public FluentUIColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/FluentUI.xaml")
    };
}
