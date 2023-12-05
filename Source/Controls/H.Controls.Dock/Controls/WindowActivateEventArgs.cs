
using System;

namespace H.Controls.Dock.Controls
{
    internal class WindowActivateEventArgs : EventArgs
    {
        public WindowActivateEventArgs(IntPtr hwndActivating)
        {
            HwndActivating = hwndActivating;
        }

        public IntPtr HwndActivating { get; private set; }
    }
}