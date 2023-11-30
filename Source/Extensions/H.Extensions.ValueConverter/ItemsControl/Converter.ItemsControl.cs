// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace H.Extensions.ValueConverter
{
    public class GetIsFirstItemInItemsControlConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject item = (DependencyObject)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);

            foreach (var current in ic.Items)
            {
                var cItem = ic.ItemContainerGenerator.ContainerFromItem(current) as UIElement;
                if (cItem == item)
                {
                    return true;
                }
                if (cItem.Visibility == Visibility.Visible)
                    return false;
            }

            return false;

            //return ic.ItemContainerGenerator.IndexFromContainer(item) == 0;
        }
    }

    public class GetCollpseLastItemItemsControlConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject item = (DependencyObject)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            bool r = ic.ItemContainerGenerator.IndexFromContainer(item)
                    == ic.Items.Count - 1;
            return r ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class GetIsLastItemInItemsControlConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject item = (DependencyObject)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            if (ic == null)
                return false;
            return ic.ItemContainerGenerator.IndexFromContainer(item)
                    == ic.Items.Count - 1;
        }
    }
}
