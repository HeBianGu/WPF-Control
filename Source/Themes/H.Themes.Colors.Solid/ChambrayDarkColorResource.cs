using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Solid;

public class ChambrayDarkColorResource : IColorResource
{
    public string Name => "Chambray Dark";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/ChambrayDark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
