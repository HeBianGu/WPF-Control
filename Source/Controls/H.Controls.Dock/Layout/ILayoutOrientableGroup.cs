








using System.Windows.Controls;

namespace H.Controls.Dock.Layout
{
    /// <summary>Interface definition for a <see cref="ILayoutGroup"/> that supports a <see cref="System.Windows.Controls.Orientation"/> property.</summary>
    public interface ILayoutOrientableGroup : ILayoutGroup
    {
        /// <summary>Gets/sets the <see cref="System.Windows.Controls.Orientation"/> of the <see cref="ILayoutGroup"/>.</summary>
        Orientation Orientation { get; set; }
    }
}