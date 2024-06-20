using H.Controls.Dock.Controls;
using System;

namespace H.Controls.Dock
{
    public sealed class LayoutFloatingWindowControlCreatedEventArgs : EventArgs
    {
        public LayoutFloatingWindowControlCreatedEventArgs(LayoutFloatingWindowControl layoutFloatingWindowControl)
        {
            this.LayoutFloatingWindowControl = layoutFloatingWindowControl;
        }

        public LayoutFloatingWindowControl LayoutFloatingWindowControl { get; }
    }

}
