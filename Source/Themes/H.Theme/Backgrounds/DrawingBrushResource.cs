namespace H.Themes.Backgrounds;

public class DrawingBrushResource : IBackgroundResource
{
    public string Name => "画刷（开发中）";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/Backgrounds/DrawingBrush.xaml")
    };

    public string GroupName => "默认";

    public override string ToString()
    {
        return this.Name;
    }
}
