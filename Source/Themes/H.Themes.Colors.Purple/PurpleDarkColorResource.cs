using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Purple;

public class PurpleDarkColorResource : IColorResource
{
    public string Name => "深紫色";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Purple;component/Dark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
