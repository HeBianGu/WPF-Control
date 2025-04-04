namespace H.Themes.Backgrounds;

public class RadialGradientBrushResource : IBackgroundResource
{
    public string Name => "中心渐变（开发中）";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/Backgrounds/RadialGradientBrush.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
