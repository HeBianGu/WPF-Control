using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Technology;

/// <summary>
/// 芭比曙光粉：明亮而充满活力，如同晨曦中的第一缕阳光，温暖而希望。
/// </summary>
public class TechnologyPinkColorResource : IColorResource
{
    public string Name => "芭比曙光粉";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyPink.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
