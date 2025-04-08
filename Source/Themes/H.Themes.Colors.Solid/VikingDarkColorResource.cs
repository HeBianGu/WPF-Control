using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Solid;
[Display(Name = "Viking Dark", GroupName = "纯色", Description = "纯色", Order = 90, Prompt = "试验")]
public class VikingDarkColorResource : ColorResourceBase
{
    public VikingDarkColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/VikingDark.xaml")
    };
}
