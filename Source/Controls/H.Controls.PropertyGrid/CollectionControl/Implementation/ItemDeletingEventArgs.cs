// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Controls.PropertyGrid
{
    public class ItemDeletingEventArgs : CancelRoutedEventArgs
    {
        #region Private Members

        private object _item;

        #endregion

        #region Constructor

        public ItemDeletingEventArgs(RoutedEvent itemDeletingEvent, object itemDeleting)
          : base(itemDeletingEvent)
        {
            _item = itemDeleting;
        }

        #region Property Item

        public object Item
        {
            get
            {
                return _item;
            }
        }

        #endregion

        #endregion
    }
}
