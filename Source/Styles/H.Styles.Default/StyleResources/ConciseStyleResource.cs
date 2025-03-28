namespace H.Styles.Default.StyleResources;

public class ConciseStyleResource : IStyleResource
{
    public string Name => "简洁样式";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Styles.Default;component/ConciseControls.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
