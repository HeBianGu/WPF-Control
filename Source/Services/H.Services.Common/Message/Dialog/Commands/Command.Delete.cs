﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public class DeleteCommand : IocMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is FrameworkElement element)
            {
                if (element.DataContext == null)
                    return;
                ItemsControl items = element.GetParent<ItemsControl>();
                if (items.ItemsSource is IList list)
                    list.Remove(element.DataContext);
                else
                    items.Items.Remove(element.DataContext);
            }
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter is FrameworkElement element)
            {
                if (element.DataContext == null)
                    return false;
                ItemsControl items = element.GetParent<ItemsControl>();
                return items != null;
            }
            return false;
        }
    }
}
