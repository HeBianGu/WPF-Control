using System.Windows;

namespace H.Themes.Default.Colors;

public interface IColorResource : IResourceable
{
    string GroupName { get; }
}