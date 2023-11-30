








using System.ComponentModel;

namespace H.Controls.Dock.Layout
{
    /// <summary>This interface should be implemented by a classe that supports
    /// - Manipulation of the children of a given parent <see cref="LayoutContainer"/> or
    /// - Manipulation of the children of the <see cref="LayoutRoot"/>.
    /// </summary>
    public interface ILayoutElement : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>Gets the parent <see cref="LayoutContainer"/> for this layout element.</summary>
        ILayoutContainer Parent { get; }

        /// <summary>Gets the <see cref="LayoutRoot"/> for this layout element.</summary>
        ILayoutRoot Root { get; }
    }
}