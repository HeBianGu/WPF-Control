namespace H.Themes.Backgrounds;

public class ImageBrushResource : IBackgroundResource
{
    public string Name => "图片画刷（开发中）";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/Backgrounds/ImageBrush.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
