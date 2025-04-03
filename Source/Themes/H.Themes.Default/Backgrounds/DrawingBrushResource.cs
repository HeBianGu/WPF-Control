namespace H.Themes.Default.Colors;

public class DrawingBrushResource : IBackgroundResource
{
    public string Name => "DrawingBrush";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Default;component/Backgrounds/DrawingBrush.xaml")
    };

    public string GroupName => "默认";

    public override string ToString()
    {
        return this.Name;
    }
}
