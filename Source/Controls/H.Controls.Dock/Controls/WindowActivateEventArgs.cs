
using System;

namespace H.Controls.Dock.Controls
{
    internal class WindowActivateEventArgs : EventArgs
    {
        public WindowActivateEventArgs(IntPtr hwndActivating)
        {
            this.HwndActivating = hwndActivating;
        }

        public IntPtr HwndActivating { get; private set; }
    }
}