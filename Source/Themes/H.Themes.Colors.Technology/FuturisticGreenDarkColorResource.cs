using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Technology;

public class FuturisticGreenDarkColorResource : IColorResource
{
    public string Name => "未来主义绿";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/FuturisticGreenDark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
