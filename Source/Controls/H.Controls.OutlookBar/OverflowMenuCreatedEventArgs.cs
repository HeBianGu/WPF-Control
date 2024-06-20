using System;
using System.Collections.ObjectModel;

namespace H.Controls.OutlookBar
{
    public class OverflowMenuCreatedEventArgs : EventArgs
    {
        public OverflowMenuCreatedEventArgs(Collection<object> menuItems)
            : base()
        {
            this.MenuItems = menuItems;
        }

        public Collection<object> MenuItems { get; private set; }
    }
}
