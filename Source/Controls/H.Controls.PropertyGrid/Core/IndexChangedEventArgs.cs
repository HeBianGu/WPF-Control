// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;

namespace H.Controls.PropertyGrid
{
    public class IndexChangedEventArgs : PropertyChangedEventArgs<int>
    {
        #region Constructors

        public IndexChangedEventArgs(RoutedEvent routedEvent, int oldIndex, int newIndex)
          : base(routedEvent, oldIndex, newIndex)
        {
        }

        #endregion

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((IndexChangedEventHandler)genericHandler)(genericTarget, this);
        }
    }
}
