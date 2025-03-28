using H.Styles.Default.StyleResources;

namespace H.Styles.Bootstrap.StyleResources;

public class BootstrapStyleResource : IStyleResource
{
    public string Name => "Bootstrap";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Styles.Bootstrap;component/BootstrapControls.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
