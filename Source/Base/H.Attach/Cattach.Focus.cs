// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;
using System.Windows.Media;

namespace H.Attach;

public static partial class Cattach
{
    public static readonly DependencyProperty FocusBorderBrushProperty = DependencyProperty.RegisterAttached(
        "FocusBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));
    public static void SetFocusBorderBrush(DependencyObject element, Brush value)
    {
        element.SetValue(FocusBorderBrushProperty, value);
    }
    public static Brush GetFocusBorderBrush(DependencyObject element)
    {
        return (Brush)element.GetValue(FocusBorderBrushProperty);
    }

    public static Thickness GetFocusBorderThickness(DependencyObject obj)
    {
        return (Thickness)obj.GetValue(FocusBorderThicknessProperty);
    }

    public static void SetFocusBorderThickness(DependencyObject obj, Thickness value)
    {
        obj.SetValue(FocusBorderThicknessProperty, value);
    }

    public static readonly DependencyProperty FocusBorderThicknessProperty =
        DependencyProperty.RegisterAttached("FocusBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(new Thickness(1), OnFocusBorderThicknessChanged));

    public static void OnFocusBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Thickness n = (Thickness)e.NewValue;

        Thickness o = (Thickness)e.OldValue;
    }

    public static readonly DependencyProperty FocusBackgroundProperty = DependencyProperty.RegisterAttached(
        "FocusBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

    public static void SetFocusBackground(DependencyObject element, Brush value)
    {
        element.SetValue(FocusBackgroundProperty, value);
    }

    public static Brush GetFocusBackground(DependencyObject element)
    {
        return (Brush)element.GetValue(FocusBackgroundProperty);
    }

    public static readonly DependencyProperty FocusForegroundProperty = DependencyProperty.RegisterAttached(
        "FocusForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

    public static void SetFocusForeground(DependencyObject element, Brush value)
    {
        element.SetValue(FocusForegroundProperty, value);
    }

    public static Brush GetFocusForeground(DependencyObject element)
    {
        return (Brush)element.GetValue(FocusForegroundProperty);
    }
}
