namespace H.Themes.Default.Colors;

public class RadialGradientBrushResource : IBackgroundResource
{
    public string Name => "RadialGradientBrush";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Default;component/Backgrounds/RadialGradientBrush.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
