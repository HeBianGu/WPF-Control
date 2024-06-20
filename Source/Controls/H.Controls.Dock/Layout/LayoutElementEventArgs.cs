








using System;

namespace H.Controls.Dock.Layout
{
    /// <summary>
    /// Provides an implmentation to raise an event concerning a particular <see cref="LayoutElement"/>.
    /// (eg. This <see cref="LayoutElement"/> has been removed from my childrens collection)
    /// </summary>
    public class LayoutElementEventArgs : EventArgs
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public LayoutElementEventArgs(LayoutElement element)
        {
            this.Element = element;
        }

        /// <summary>Gets the particular <see cref="LayoutElement"/> for which this event has been raised.</summary>
        public LayoutElement Element { get; private set; }
    }
}