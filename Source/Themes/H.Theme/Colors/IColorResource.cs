using System.Windows;

namespace H.Theme.Colors;

public interface IColorResource : IResourceable
{
    string GroupName { get; }
}