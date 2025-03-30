using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Technology;

public class AmberTerminalDarkColorResource : IColorResource
{
    public string Name => "琥珀终端风";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/AmberTerminalDark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
