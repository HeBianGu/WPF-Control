namespace H.Styles.StyleResources;

public class NoneStyleResource : IStyleResource
{
    public string Name => "默认样式";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Style;component/NoneControls.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
