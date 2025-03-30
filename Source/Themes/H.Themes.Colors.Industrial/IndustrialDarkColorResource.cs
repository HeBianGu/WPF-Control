using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Industrial;

public class IndustrialDarkColorResource : IColorResource
{
    public string Name => "暗黑工业风";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/Dark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
