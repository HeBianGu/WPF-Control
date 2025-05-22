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

public class WindowKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(WindowKeys), "S.Window.Default");

    [Obsolete("用FontIconButton替换")]
    public static ComponentResourceKey Button => new ComponentResourceKey(typeof(WindowKeys), "S.Window.Button");
    public static ComponentResourceKey FontIconButton => new ComponentResourceKey(typeof(WindowKeys), "S.Window.FontIconButton");

    public static ComponentResourceKey WindowChrome => new ComponentResourceKey(typeof(WindowKeys), "S.Window.WindowChrome");

}

public class GetNullToCollapsedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Visibility.Collapsed;
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
