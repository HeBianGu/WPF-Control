using System.Windows;

namespace H.Themes.Default.Colors;
[Display(Name = "深色", GroupName = "纯色", Description = "纯色", Order = 10, Prompt = "长期支持")]
public class DefaultColorResource : ColorResourceBase
{
    public string Name => "默认";
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Default;component/Colors/Default.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
