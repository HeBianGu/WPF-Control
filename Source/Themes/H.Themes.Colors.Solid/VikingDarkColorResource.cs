using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Solid;

public class VikingDarkColorResource : IColorResource
{
    public string Name => "Viking Dark";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/VikingDark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
