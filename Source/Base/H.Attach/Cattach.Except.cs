// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;

namespace H.Attach;

public static partial class Cattach
{
    public static bool GetIsExceptSelf(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsExceptSelfProperty);
    }

    public static void SetIsExceptSelf(DependencyObject obj, bool value)
    {
        obj.SetValue(IsExceptSelfProperty, value);
    }

    public static readonly DependencyProperty IsExceptSelfProperty =
        DependencyProperty.RegisterAttached("IsExceptSelf", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnIsExceptSelfChanged));

    public static void OnIsExceptSelfChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }

    public static bool GetIsExcepChildren(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsExcepChildrenProperty);
    }

    public static void SetIsExcepChildren(DependencyObject obj, bool value)
    {
        obj.SetValue(IsExcepChildrenProperty, value);
    }

    public static readonly DependencyProperty IsExcepChildrenProperty =
        DependencyProperty.RegisterAttached("IsExcepChildren", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnIsExcepChildrenChanged));

    public static void OnIsExcepChildrenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }

}
