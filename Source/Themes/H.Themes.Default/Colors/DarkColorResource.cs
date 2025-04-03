using System.Windows;

namespace H.Themes.Default.Colors;

[Display(Name = "深色", GroupName = "强力推荐", Description = "纯色", Order = 10, Prompt = "强力推荐")]
public class DarkColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Default;component/Colors/Dark.xaml")
    };
}
