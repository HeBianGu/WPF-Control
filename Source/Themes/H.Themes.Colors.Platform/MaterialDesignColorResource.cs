using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Platform;

public class MaterialDesignColorResource : IColorResource
{
    public string Name => "Material Design 3 (Google)";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/MaterialDesign.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
