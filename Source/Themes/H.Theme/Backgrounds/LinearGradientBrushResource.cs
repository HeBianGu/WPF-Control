namespace H.Themes.Backgrounds;

public class LinearGradientBrushResource : IBackgroundResource
{
    public string Name => "LinearGradientBrush";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/Backgrounds/LinearGradientBrush.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
