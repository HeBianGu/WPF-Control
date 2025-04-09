// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Extensions.Attach;

public static partial class Cattach
{
    public static readonly DependencyProperty IconHorizontalAlignmentProperty = DependencyProperty.RegisterAttached(
"IconHorizontalAlignment", typeof(HorizontalAlignment), typeof(Cattach), new FrameworkPropertyMetadata(default(HorizontalAlignment), FrameworkPropertyMetadataOptions.Inherits, OnIconHorizontalAlignmentChanged));

    public static HorizontalAlignment GetIconHorizontalAlignment(DependencyObject d)
    {
        return (HorizontalAlignment)d.GetValue(IconHorizontalAlignmentProperty);
    }

    public static void SetIconHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
    {
        obj.SetValue(IconHorizontalAlignmentProperty, value);
    }

    private static void OnIconHorizontalAlignmentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }

    public static readonly DependencyProperty IconVerticalAlignmentProperty = DependencyProperty.RegisterAttached(
        "IconVerticalAlignment", typeof(VerticalAlignment), typeof(Cattach), new FrameworkPropertyMetadata(VerticalAlignment.Center, FrameworkPropertyMetadataOptions.Inherits, OnIconVerticalAlignmentChanged));

    public static VerticalAlignment GetIconVerticalAlignment(DependencyObject d)
    {
        return (VerticalAlignment)d.GetValue(IconVerticalAlignmentProperty);
    }

    public static void SetIconVerticalAlignment(DependencyObject obj, VerticalAlignment value)
    {
        obj.SetValue(IconVerticalAlignmentProperty, value);
    }

    private static void OnIconVerticalAlignmentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }


    public static double GetIconOpacity(DependencyObject obj)
    {
        return (double)obj.GetValue(IconOpacityProperty);
    }

    public static void SetIconOpacity(DependencyObject obj, double value)
    {
        obj.SetValue(IconOpacityProperty, value);
    }


    public static readonly DependencyProperty IconOpacityProperty =
        DependencyProperty.RegisterAttached("IconOpacity", typeof(double), typeof(Cattach), new PropertyMetadata(1.0, OnIconOpacityChanged));

    public static void OnIconOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        double n = (double)e.NewValue;

        double o = (double)e.OldValue;
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
        "Icon", typeof(object), typeof(Cattach), new FrameworkPropertyMetadata(null));

    public static object GetIcon(DependencyObject d)
    {
        return (object)d.GetValue(IconProperty);
    }

    public static void SetIcon(DependencyObject obj, object value)
    {
        obj.SetValue(IconProperty, value);
    }


    public static readonly DependencyProperty IconSizeProperty = DependencyProperty.RegisterAttached(
        "IconSize", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(12D));

    public static double GetIconSize(DependencyObject d)
    {
        return (double)d.GetValue(IconSizeProperty);
    }

    public static void SetIconSize(DependencyObject obj, double value)
    {
        obj.SetValue(IconSizeProperty, value);
    }

    public static readonly DependencyProperty IconMarginProperty = DependencyProperty.RegisterAttached(
        "IconMargin", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(null));

    public static Thickness GetIconMargin(DependencyObject d)
    {
        return (Thickness)d.GetValue(IconMarginProperty);
    }

    public static void SetIconMargin(DependencyObject obj, Thickness value)
    {
        obj.SetValue(IconMarginProperty, value);
    }


    public static Brush GetIconForeground(DependencyObject obj)
    {
        return (Brush)obj.GetValue(IconForegroundProperty);
    }

    public static void SetIconForeground(DependencyObject obj, Brush value)
    {
        obj.SetValue(IconForegroundProperty, value);
    }


    public static readonly DependencyProperty IconForegroundProperty =
        DependencyProperty.RegisterAttached("IconForeground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnIconForegroundChanged));

    public static void OnIconForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }

}
