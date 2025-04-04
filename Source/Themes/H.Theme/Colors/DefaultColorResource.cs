using System.Windows;

namespace H.Themes.Colors;
[Display(Name = "默认（长期支持）", GroupName = "纯色", Description = "纯色", Order = 10, Prompt = "长期支持")]
public class DefaultColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/Colors/Default.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
