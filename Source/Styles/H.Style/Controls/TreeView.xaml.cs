// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;
using System.Windows.Data;

namespace H.Styles.Controls;

public class TreeViewKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Default");
}

public class TreeViewItemKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(TreeViewItemKeys), "S.TreeViewItem.Default");
}


public class GetFalseToHiddenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? Visibility.Visible : Visibility.Hidden;
        return Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}