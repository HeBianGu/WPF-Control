using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Platform;

public class AppleColorResource : IColorResource
{
    public string Name => "Apple (IOS)";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/Apple.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
