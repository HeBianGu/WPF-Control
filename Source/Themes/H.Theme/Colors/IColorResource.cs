using H.Themes;
using System.Windows;

namespace H.Themes.Colors;

public interface IColorResource : IResourceable
{
    string GroupName { get; }
    bool IsDark { get; set; }
}