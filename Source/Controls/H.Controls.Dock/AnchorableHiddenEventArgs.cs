








using H.Controls.Dock.Layout;
using System;

namespace H.Controls.Dock
{
    /// <summary>
    /// Implements an event that can be raised to inform the client application about an
    /// anchorable that been hidden.
    /// </summary>
    public class AnchorableHiddenEventArgs : EventArgs
    {
        /// <summary>
        /// Class constructor from the anchorables layout model.
        /// </summary>
        /// <param name="document"></param>
        public AnchorableHiddenEventArgs(LayoutAnchorable anchorable)
        {
            this.Anchorable = anchorable;
        }

        /// <summary>
        /// Gets the model of the anchorable that has been hidden.
        /// </summary>
        public LayoutAnchorable Anchorable { get; private set; }
    }
}