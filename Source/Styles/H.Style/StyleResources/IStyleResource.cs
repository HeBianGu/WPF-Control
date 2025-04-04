namespace H.Styles.StyleResources;

public interface IStyleResource
{
    string Name { get; }
    ResourceDictionary Resource { get; }
}
