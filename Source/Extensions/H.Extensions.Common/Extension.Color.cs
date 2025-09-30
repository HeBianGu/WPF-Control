// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows;
using System.Windows.Media;

namespace H.Extensions.Common;

public static class ColorExtenstion
{
    public static SolidColorBrush ToSolid(this string hex, Action<SolidColorBrush> action = null)
    {
        var color = (Color)ColorConverter.ConvertFromString(hex);
        var brush = new SolidColorBrush(color);
        action?.Invoke(brush);
        return brush;
    }
    public static SolidColorBrush ToFreezeSolid(this string hex, Action<SolidColorBrush> action = null)
    {
        return hex.ToSolid(x =>
         {
             if (x.CanFreeze) x.Freeze();
             action?.Invoke(x);
         });
    }

    public static SolidColorBrush ToSolid(this Color color, Action<SolidColorBrush> action = null)
    {
        var brush = new SolidColorBrush(color);
        action?.Invoke(brush);
        return brush;
    }
}
