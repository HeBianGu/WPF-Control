// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Extensions.Attach;

public static partial class Cattach
{
    public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(object), typeof(Cattach), new FrameworkPropertyMetadata(false, Attach));
    public static void SetAttach(DependencyObject dp, object value)
    {
        dp.SetValue(AttachProperty, value);
    }
    public static object GetAttach(DependencyObject dp)
    {
        return (object)dp.GetValue(AttachProperty);
    }

    public static readonly DependencyProperty AttachControlTemplateProperty = DependencyProperty.RegisterAttached(
        "AttachControlTemplate", typeof(ControlTemplate), typeof(Cattach), new FrameworkPropertyMetadata(null));

    public static ControlTemplate GetAttachControlTemplate(DependencyObject d)
    {
        return (ControlTemplate)d.GetValue(AttachControlTemplateProperty);
    }

    public static void SetAttachControlTemplate(DependencyObject obj, ControlTemplate value)
    {
        obj.SetValue(AttachControlTemplateProperty, value);
    }


    public static readonly DependencyProperty AttachTemplateProperty = DependencyProperty.RegisterAttached(
        "AttachTemplate", typeof(DataTemplate), typeof(Cattach), new FrameworkPropertyMetadata(default(DataTemplate), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static DataTemplate GetAttachTemplate(DependencyObject d)
    {
        return (DataTemplate)d.GetValue(AttachTemplateProperty);
    }

    public static void SetAttachTemplate(DependencyObject obj, DataTemplate value)
    {
        obj.SetValue(AttachTemplateProperty, value);
    }

    public static Brush GetAttachForeground(DependencyObject obj)
    {
        return (Brush)obj.GetValue(AttachForegroundProperty);
    }

    public static void SetAttachForeground(DependencyObject obj, Brush value)
    {
        obj.SetValue(AttachForegroundProperty, value);
    }

    public static readonly DependencyProperty AttachForegroundProperty =
        DependencyProperty.RegisterAttached("AttachForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnAttachForegroundChanged));

    public static void OnAttachForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }


    public static Brush GetAttachBackground(DependencyObject obj)
    {
        return (Brush)obj.GetValue(AttachBackgroundProperty);
    }

    public static void SetAttachBackground(DependencyObject obj, Brush value)
    {
        obj.SetValue(AttachBackgroundProperty, value);
    }

    public static readonly DependencyProperty AttachBackgroundProperty =
        DependencyProperty.RegisterAttached("AttachBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnAttachBackgroundChanged));

    public static void OnAttachBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }


    public static Brush GetAttachBorderBrush(DependencyObject obj)
    {
        return (Brush)obj.GetValue(AttachBorderBrushProperty);
    }

    public static void SetAttachBorderBrush(DependencyObject obj, Brush value)
    {
        obj.SetValue(AttachBorderBrushProperty, value);
    }

    public static readonly DependencyProperty AttachBorderBrushProperty =
        DependencyProperty.RegisterAttached("AttachBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnAttachBorderBrushChanged));

    public static void OnAttachBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }
    [TypeConverter(typeof(LengthConverter))]
    public static double GetAttachWidth(DependencyObject obj)
    {
        return (double)obj.GetValue(AttachWidthProperty);
    }
    [TypeConverter(typeof(LengthConverter))]
    public static void SetAttachWidth(DependencyObject obj, double value)
    {
        obj.SetValue(AttachWidthProperty, value);
    }

    public static readonly DependencyProperty AttachWidthProperty =
        DependencyProperty.RegisterAttached("AttachWidth", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits, OnAttachWidthChanged));

    public static void OnAttachWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        double n = (double)e.NewValue;

        double o = (double)e.OldValue;
    }


    public static Thickness GetAttachBorderThickness(DependencyObject obj)
    {
        return (Thickness)obj.GetValue(AttachBorderThicknessProperty);
    }

    public static void SetAttachBorderThickness(DependencyObject obj, Thickness value)
    {
        obj.SetValue(AttachBorderThicknessProperty, value);
    }

    public static readonly DependencyProperty AttachBorderThicknessProperty =
        DependencyProperty.RegisterAttached("AttachBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits, OnAttachBorderThicknessChanged));

    public static void OnAttachBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Thickness n = (Thickness)e.NewValue;

        Thickness o = (Thickness)e.OldValue;
    }


    public static HorizontalAlignment GetAttachHorizontalAlignment(DependencyObject obj)
    {
        return (HorizontalAlignment)obj.GetValue(AttachHorizontalAlignmentProperty);
    }

    public static void SetAttachHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
    {
        obj.SetValue(AttachHorizontalAlignmentProperty, value);
    }


    public static readonly DependencyProperty AttachHorizontalAlignmentProperty =
        DependencyProperty.RegisterAttached("AttachHorizontalAlignment", typeof(HorizontalAlignment), typeof(Cattach), new FrameworkPropertyMetadata(default(HorizontalAlignment), FrameworkPropertyMetadataOptions.Inherits, OnAttachHorizontalAlignmentChanged));

    public static void OnAttachHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        HorizontalAlignment n = (HorizontalAlignment)e.NewValue;

        HorizontalAlignment o = (HorizontalAlignment)e.OldValue;
    }

    public static void SetAttachCornerRaius(DependencyObject obj, CornerRadius value)
    {
        obj.SetValue(AttachCornerRaiusProperty, value);
    }
    public static readonly DependencyProperty AttachCornerRaiusProperty =
        DependencyProperty.RegisterAttached("AttachCornerRaius", typeof(CornerRadius), typeof(Cattach), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits, OnAttachCornerRaiusChanged));

    public static void OnAttachCornerRaiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        CornerRadius n = (CornerRadius)e.NewValue;

        CornerRadius o = (CornerRadius)e.OldValue;
    }

    public static VerticalAlignment GetAttachVerticalAlignment(DependencyObject obj)
    {
        return (VerticalAlignment)obj.GetValue(AttachVerticalAlignmentProperty);
    }

    public static void SetAttachVerticalAlignment(DependencyObject obj, VerticalAlignment value)
    {
        obj.SetValue(AttachVerticalAlignmentProperty, value);
    }

    public static readonly DependencyProperty AttachVerticalAlignmentProperty =
        DependencyProperty.RegisterAttached("AttachVerticalAlignment", typeof(VerticalAlignment), typeof(Cattach), new FrameworkPropertyMetadata(default(VerticalAlignment), FrameworkPropertyMetadataOptions.Inherits, OnAttachVerticalAlignmentChanged));

    public static void OnAttachVerticalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        VerticalAlignment n = (VerticalAlignment)e.NewValue;

        VerticalAlignment o = (VerticalAlignment)e.OldValue;
    }


    public static Thickness GetAttachMargin(DependencyObject obj)
    {
        return (Thickness)obj.GetValue(AttachMarginProperty);
    }

    public static void SetAttachMargin(DependencyObject obj, Thickness value)
    {
        obj.SetValue(AttachMarginProperty, value);
    }

    public static readonly DependencyProperty AttachMarginProperty =
        DependencyProperty.RegisterAttached("AttachMargin", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits, OnAttachMarginChanged));

    public static void OnAttachMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Thickness n = (Thickness)e.NewValue;

        Thickness o = (Thickness)e.OldValue;
    }

    [TypeConverter(typeof(LengthConverter))]
    public static double GetAttachHeight(DependencyObject obj)
    {
        return (double)obj.GetValue(AttachHeightProperty);
    }
    [TypeConverter(typeof(LengthConverter))]
    public static void SetAttachHeight(DependencyObject obj, double value)
    {
        obj.SetValue(AttachHeightProperty, value);
    }

    public static readonly DependencyProperty AttachHeightProperty =
        DependencyProperty.RegisterAttached("AttachHeight", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits, OnAttachHeightChanged));

    public static void OnAttachHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        double n = (double)e.NewValue;

        double o = (double)e.OldValue;
    }


    public static Dock GetAttachDock(DependencyObject obj)
    {
        return (Dock)obj.GetValue(AttachDockProperty);
    }

    public static void SetAttachDock(DependencyObject obj, Dock value)
    {
        obj.SetValue(AttachDockProperty, value);
    }


    public static readonly DependencyProperty AttachDockProperty =
        DependencyProperty.RegisterAttached("AttachDock", typeof(Dock), typeof(Cattach), new PropertyMetadata(default(Dock), OnAttachDockChanged));

    public static void OnAttachDockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Dock n = (Dock)e.NewValue;

        Dock o = (Dock)e.OldValue;
    }

}
