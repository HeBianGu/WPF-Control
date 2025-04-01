namespace H.Themes.Default.Colors;

public class SolidColorBrushResource : IBackgroundResource
{
    public string Name => "SolidColorBrush";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Default;component/Backgrounds/Default.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
