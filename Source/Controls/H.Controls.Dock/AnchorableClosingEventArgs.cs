








using H.Controls.Dock.Layout;
using System.ComponentModel;

namespace H.Controls.Dock
{
    /// <summary>
    /// Implements a Cancelable event that can be raised to ask the client application whether closing this anchorable
    /// and removing its content (viewmodel) is OK or not.
    /// </summary>
    public class AnchorableClosingEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Class constructor from the anchorable layout model.
        /// </summary>
        /// <param name="document"></param>
        public AnchorableClosingEventArgs(LayoutAnchorable anchorable)
        {
            this.Anchorable = anchorable;
        }

        /// <summary>
        /// Gets the model of the anchorable that is about to be closed.
        /// </summary>
        public LayoutAnchorable Anchorable { get; private set; }
    }
}