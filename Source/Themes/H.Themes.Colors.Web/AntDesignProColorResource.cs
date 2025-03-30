using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Web;

public class AntDesignProColorResource : IColorResource
{
    public string Name => "An tDesign Pro";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/AntDesignPro.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
