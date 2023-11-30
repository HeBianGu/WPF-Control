using System.Windows;

namespace H.Themes.Default
{
    public interface IColorResource
    {
        string Name { get; }
        ResourceDictionary Resource { get; }
    }
}
