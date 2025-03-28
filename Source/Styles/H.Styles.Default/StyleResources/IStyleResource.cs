namespace H.Styles.Default.StyleResources;

public interface IStyleResource
{
    string Name { get; }
    ResourceDictionary Resource { get; }
}
