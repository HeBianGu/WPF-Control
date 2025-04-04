
namespace H.Styles.StyleResources;

public class ConciseStyleResource : IStyleResource
{
    public string Name => "简洁样式（推荐）";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Style;component/ConciseControls.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
