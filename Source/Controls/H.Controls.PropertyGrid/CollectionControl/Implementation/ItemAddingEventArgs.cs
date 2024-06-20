// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Controls.PropertyGrid
{
    public class ItemAddingEventArgs : CancelRoutedEventArgs
    {
        #region Constructor

        public ItemAddingEventArgs(RoutedEvent itemAddingEvent, object itemAdding)
          : base(itemAddingEvent)
        {
            this.Item = itemAdding;
        }

        #endregion

        #region Properties

        #region Item Property

        public object Item
        {
            get;
            set;
        }

        #endregion

        #endregion //Properties
    }
}
