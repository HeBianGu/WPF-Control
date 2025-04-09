using System.Windows;

namespace H.Themes.Colors;

[Display(Name = "深色（推荐）", GroupName = "强力推荐", Description = "纯色", Order = 10, Prompt = "强力推荐")]
public class DarkColorResource : ColorResourceBase
{
    public DarkColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/Colors/Dark.xaml")
    };
}
