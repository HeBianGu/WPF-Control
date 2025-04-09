using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Copper;
[Display(Name = "深铜色（推荐）", GroupName = "强力推荐", Description = "纯色", Order = 100, Prompt = "强力推荐")]
public class CopperColorResource : ColorResourceBase
{
    public CopperColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Copper;component/Dark.xaml")
    };
}
