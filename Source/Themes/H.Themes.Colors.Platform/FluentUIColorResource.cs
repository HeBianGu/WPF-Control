using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Platform;

public class FluentUIColorResource : IColorResource
{
    public string Name => "‌Fluent UI (Microsoft)";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/FluentUI.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
