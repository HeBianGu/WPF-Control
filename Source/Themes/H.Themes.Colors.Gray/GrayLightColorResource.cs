using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Gray;
[Display(Name = "浅灰色（推荐）", GroupName = "强力推荐", Description = "纯色", Order = 10, Prompt = "强力推荐")]
public class GrayLightColorResource : ColorResourceBase
{
    public GrayLightColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Gray;component/Light.xaml")
    };
}
