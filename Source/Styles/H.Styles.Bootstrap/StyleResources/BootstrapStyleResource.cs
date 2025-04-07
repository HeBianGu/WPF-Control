using H.Styles.StyleResources;

namespace H.Styles.Bootstrap.StyleResources;

public class BootstrapStyleResource : IStyleResource
{
    public string Name => "Bootstrap(开发中)";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Styles.Bootstrap;component/BootstrapControls.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
