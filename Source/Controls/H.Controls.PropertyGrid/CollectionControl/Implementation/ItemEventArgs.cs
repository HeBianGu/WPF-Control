// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Controls.PropertyGrid
{
    public class ItemEventArgs : RoutedEventArgs
    {
        #region Protected Members

        private object _item;

        #endregion

        #region Constructor

        internal ItemEventArgs(RoutedEvent routedEvent, object newItem)
          : base(routedEvent)
        {
            _item = newItem;
        }

        #endregion

        #region Property Item

        public object Item
        {
            get
            {
                return _item;
            }
        }

        #endregion
    }
}
