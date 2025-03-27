using System.Windows;

namespace H.Themes.Default.Colors;

public interface IColorResource
{
    string Name { get; }
    ResourceDictionary Resource { get; }
}
