namespace H.Themes.Default.Colors;

public class LinearGradientBrushResource : IBackgroundResource
{
    public string Name => "LinearGradientBrush";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Default;component/Backgrounds/LinearGradientBrush.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
