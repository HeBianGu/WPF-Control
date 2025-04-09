using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Gray;
[Display(Name = "深灰色（推荐）", GroupName = "强力推荐", Description = "纯色", Order = 10, Prompt = "强力推荐")]
public class GrayDarkColorResource : ColorResourceBase
{
    public GrayDarkColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Gray;component/Dark.xaml")
    };
}
