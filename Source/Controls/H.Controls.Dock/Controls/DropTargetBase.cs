
using System.ComponentModel;
using System.Windows;

namespace H.Controls.Dock.Controls
{
    /// <summary>Implements a base implementation for the abstract <see cref="DropTarget{T}"/> class.</summary>
    internal abstract class DropTargetBase : DependencyObject
    {
        #region Properties

        #region IsDraggingOver

        /// <summary>IsDraggingOver Attached Dependency Property</summary>
        public static readonly DependencyProperty IsDraggingOverProperty = DependencyProperty.RegisterAttached("IsDraggingOver", typeof(bool), typeof(DropTargetBase),
                new FrameworkPropertyMetadata(false));

        /// <summary>Gets wether the user is dragging a window over the target element.</summary>
        [Bindable(true), Description("Gets wether the user is dragging a window over the target element."), Category("Other")]
        public static bool GetIsDraggingOver(DependencyObject d)
        {
            return (bool)d.GetValue(IsDraggingOverProperty);
        }

        /// <summary>
        /// Sets the IsDraggingOver property.
        /// This dependency property indicates if user is dragging away a window from the target element.
        /// </summary>
        public static void SetIsDraggingOver(DependencyObject d, bool value)
        {
            d.SetValue(IsDraggingOverProperty, value);
        }

        #endregion IsDraggingOver

        #endregion Properties
    }
}