using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Cyberpunk;

public class CyberpunkColorResource : IColorResource
{
    public string Name => "深色科技风";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Cyberpunk;component/Dark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
