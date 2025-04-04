namespace H.Theme.Colors;

public class ImageBrushResource : IBackgroundResource
{
    public string Name => "ImageBrush";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/Backgrounds/ImageBrush.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
