using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Solid;

public class OracleDarkColorResource : IColorResource
{
    public string Name => "OracleDark";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/OracleDark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
