using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Web;

public class LayUIColorResource : IColorResource
{
    public string Name => "‌LayUI";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/LayUI.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
