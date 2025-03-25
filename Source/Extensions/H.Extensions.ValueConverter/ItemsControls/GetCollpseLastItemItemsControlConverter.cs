// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.ValueConverter.ItemsControls;

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
