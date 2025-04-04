using System.Windows;

namespace H.Theme.Colors;

[Display(Name = "浅色", GroupName = "强力推荐", Description = "纯色", Order = 10, Prompt = "强力推荐")]
public class LightColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/Colors/Light.xaml")
    };
}
