using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Technology;
[Display(Name = "琥珀终端风", GroupName = "深色科技风", Description = "纯色", Order = 100, Prompt = "试验")]
public class AmberTerminalDarkColorResource : ColorResourceBase
{
    public AmberTerminalDarkColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/AmberTerminalDark.xaml")
    };
}
