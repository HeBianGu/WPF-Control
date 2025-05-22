// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.ValueConverter.ItemsControls;

public class GetIsFirstItemInItemsControlConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DependencyObject item = (DependencyObject)value;
        ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
        foreach (object current in ic.Items)
        {
            UIElement cItem = ic.ItemContainerGenerator.ContainerFromItem(current) as UIElement;
            if (cItem == item)
                return true;
            if (cItem.Visibility == Visibility.Visible)
                return false;
        }
        return false;
    }
}
