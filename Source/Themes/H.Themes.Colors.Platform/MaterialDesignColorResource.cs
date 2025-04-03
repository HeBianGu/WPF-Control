using H.Themes.Default.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Platform;
[Display(Name = "Material Design 3 (Google)", GroupName = "系统平台", Description = "纯色", Order = 100, Prompt = "试用")]
public class MaterialDesignColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/MaterialDesign.xaml")
    };
}
