namespace H.Themes.Backgrounds;

public class SolidColorBrushResource : IBackgroundResource
{
    public string Name => "纯色（推荐）";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/Backgrounds/Default.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
