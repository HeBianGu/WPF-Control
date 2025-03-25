// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Extensions.Attach;

public static partial class Cattach
{

    public static bool? GetIsChecked(DependencyObject obj)
    {
        return (bool?)obj.GetValue(IsCheckedProperty);
    }

    public static void SetIsChecked(DependencyObject obj, bool? value)
    {
        obj.SetValue(IsCheckedProperty, value);
    }


    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.RegisterAttached("IsChecked", typeof(bool?), typeof(Cattach), new PropertyMetadata(default(bool?), OnIsCheckedChanged));

    public static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        bool? n = (bool?)e.NewValue;

        bool? o = (bool?)e.OldValue;
    }


    public static string GetCheckedGeometry(DependencyObject obj)
    {
        return (string)obj.GetValue(CheckedGeometryProperty);
    }

    public static void SetCheckedGeometry(DependencyObject obj, string value)
    {
        obj.SetValue(CheckedGeometryProperty, value);
    }


    public static readonly DependencyProperty CheckedGeometryProperty =
        DependencyProperty.RegisterAttached("CheckedGeometry", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnCheckedGeometryChanged));

    public static void OnCheckedGeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }


    public static string GetUnCheckedGeometry(DependencyObject obj)
    {
        return (string)obj.GetValue(UnCheckedGeometryProperty);
    }

    public static void SetUnCheckedGeometry(DependencyObject obj, string value)
    {
        obj.SetValue(UnCheckedGeometryProperty, value);
    }


    public static readonly DependencyProperty UnCheckedGeometryProperty =
        DependencyProperty.RegisterAttached("UnCheckedGeometry", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnUnCheckedGeometryChanged));

    public static void OnUnCheckedGeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }


    public static object GetCheckedContent(DependencyObject obj)
    {
        return (object)obj.GetValue(CheckedContentProperty);
    }

    public static void SetCheckedContent(DependencyObject obj, object value)
    {
        obj.SetValue(CheckedContentProperty, value);
    }

    public static readonly DependencyProperty CheckedContentProperty =
        DependencyProperty.RegisterAttached("CheckedContent", typeof(object), typeof(Cattach), new PropertyMetadata(default(object), OnCheckedContentChanged));

    static public void OnCheckedContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        object n = (object)e.NewValue;

        object o = (object)e.OldValue;
    }


    public static object GetUnCheckedContent(DependencyObject obj)
    {
        return (object)obj.GetValue(UnCheckedContentProperty);
    }

    public static void SetUnCheckedContent(DependencyObject obj, object value)
    {
        obj.SetValue(UnCheckedContentProperty, value);
    }

    public static readonly DependencyProperty UnCheckedContentProperty =
        DependencyProperty.RegisterAttached("UnCheckedContent", typeof(object), typeof(Cattach), new PropertyMetadata(default(object), OnUnCheckedContentChanged));

    static public void OnUnCheckedContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        object n = (object)e.NewValue;

        object o = (object)e.OldValue;
    }


    public static string GetCheckedText(DependencyObject obj)
    {
        return (string)obj.GetValue(CheckedTextProperty);
    }

    public static void SetCheckedText(DependencyObject obj, string value)
    {
        obj.SetValue(CheckedTextProperty, value);
    }


    public static readonly DependencyProperty CheckedTextProperty =
        DependencyProperty.RegisterAttached("CheckedText", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnCheckedTextChanged));

    public static void OnCheckedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }


    public static string GetUncheckedText(DependencyObject obj)
    {
        return (string)obj.GetValue(UncheckedTextProperty);
    }

    public static void SetUncheckedText(DependencyObject obj, string value)
    {
        obj.SetValue(UncheckedTextProperty, value);
    }


    public static readonly DependencyProperty UncheckedTextProperty =
        DependencyProperty.RegisterAttached("UncheckedText", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnUncheckedTextChanged));

    public static void OnUncheckedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }

    public static Brush GetCheckedForeground(DependencyObject obj)
    {
        return (Brush)obj.GetValue(CheckedForegroundProperty);
    }

    public static void SetCheckedForeground(DependencyObject obj, Brush value)
    {
        obj.SetValue(CheckedForegroundProperty, value);
    }


    public static readonly DependencyProperty CheckedForegroundProperty =
        DependencyProperty.RegisterAttached("CheckedForeground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnCheckedForegroundChanged));

    public static void OnCheckedForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }

    public static Brush GetUncheckForeground(DependencyObject obj)
    {
        return (Brush)obj.GetValue(UncheckForegroundProperty);
    }

    public static void SetUncheckForeground(DependencyObject obj, Brush value)
    {
        obj.SetValue(UncheckForegroundProperty, value);
    }


    public static readonly DependencyProperty UncheckForegroundProperty =
        DependencyProperty.RegisterAttached("UncheckForeground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnUncheckForegroundChanged));

    public static void OnUncheckForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }
    public static Brush GetCheckedBackground(DependencyObject obj)
    {
        return (Brush)obj.GetValue(CheckedBackgroundProperty);
    }

    public static void SetCheckedBackground(DependencyObject obj, Brush value)
    {
        obj.SetValue(CheckedBackgroundProperty, value);
    }


    public static readonly DependencyProperty CheckedBackgroundProperty =
        DependencyProperty.RegisterAttached("CheckedBackground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnCheckedBackgroundChanged));

    public static void OnCheckedBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }


    public static Brush GetUncheckedBackground(DependencyObject obj)
    {
        return (Brush)obj.GetValue(UncheckedBackgroundProperty);
    }

    public static void SetUncheckedBackground(DependencyObject obj, Brush value)
    {
        obj.SetValue(UncheckedBackgroundProperty, value);
    }


    public static readonly DependencyProperty UncheckedBackgroundProperty =
        DependencyProperty.RegisterAttached("UncheckedBackground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnUncheckedBackgroundChanged));

    public static void OnUncheckedBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }

    public static Brush GetCheckedBorderBrush(DependencyObject obj)
    {
        return (Brush)obj.GetValue(CheckedBorderBrushProperty);
    }

    public static void SetCheckedBorderBrush(DependencyObject obj, Brush value)
    {
        obj.SetValue(CheckedBorderBrushProperty, value);
    }


    public static readonly DependencyProperty CheckedBorderBrushProperty =
        DependencyProperty.RegisterAttached("CheckedBorderBrush", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnCheckedBorderBrushChanged));

    public static void OnCheckedBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }


    public static Brush GetUnCheckedBorderBrush(DependencyObject obj)
    {
        return (Brush)obj.GetValue(UnCheckedBorderBrushProperty);
    }

    public static void SetUnCheckedBorderBrush(DependencyObject obj, Brush value)
    {
        obj.SetValue(UnCheckedBorderBrushProperty, value);
    }


    public static readonly DependencyProperty UnCheckedBorderBrushProperty =
        DependencyProperty.RegisterAttached("UnCheckedBorderBrush", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnUnCheckedBorderBrushChanged));

    public static void OnUnCheckedBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }

    public static Thickness GetCheckedBorderThickness(DependencyObject obj)
    {
        return (Thickness)obj.GetValue(CheckedBorderThicknessProperty);
    }

    public static void SetCheckedBorderThickness(DependencyObject obj, Thickness value)
    {
        obj.SetValue(CheckedBorderThicknessProperty, value);
    }


    public static readonly DependencyProperty CheckedBorderThicknessProperty =
        DependencyProperty.RegisterAttached("CheckedBorderThickness", typeof(Thickness), typeof(Cattach), new PropertyMetadata(default(Thickness), OnCheckedBorderThicknessChanged));

    public static void OnCheckedBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Thickness n = (Thickness)e.NewValue;

        Thickness o = (Thickness)e.OldValue;
    }


    public static Thickness GetUnCheckedBorderThickness(DependencyObject obj)
    {
        return (Thickness)obj.GetValue(UnCheckedBorderThicknessProperty);
    }

    public static void SetUnCheckedBorderThickness(DependencyObject obj, Thickness value)
    {
        obj.SetValue(UnCheckedBorderThicknessProperty, value);
    }


    public static readonly DependencyProperty UnCheckedBorderThicknessProperty =
        DependencyProperty.RegisterAttached("UnCheckedBorderThickness", typeof(Thickness), typeof(Cattach), new PropertyMetadata(default(Thickness), OnUnCheckedBorderThicknessChanged));

    public static void OnUnCheckedBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Thickness n = (Thickness)e.NewValue;

        Thickness o = (Thickness)e.OldValue;
    }

    public static double GetCheckedOpacity(DependencyObject obj)
    {
        return (double)obj.GetValue(CheckedOpacityProperty);
    }

    public static void SetCheckedOpacity(DependencyObject obj, double value)
    {
        obj.SetValue(CheckedOpacityProperty, value);
    }


    public static readonly DependencyProperty CheckedOpacityProperty =
        DependencyProperty.RegisterAttached("CheckedOpacity", typeof(double), typeof(Cattach), new PropertyMetadata(1.0, OnCheckedOpacityChanged));

    public static void OnCheckedOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        double n = (double)e.NewValue;

        double o = (double)e.OldValue;
    }


    public static double GetUncheckedOpacity(DependencyObject obj)
    {
        return (double)obj.GetValue(UncheckedOpacityProperty);
    }

    public static void SetUncheckedOpacity(DependencyObject obj, double value)
    {
        obj.SetValue(UncheckedOpacityProperty, value);
    }


    public static readonly DependencyProperty UncheckedOpacityProperty =
        DependencyProperty.RegisterAttached("UncheckedOpacity", typeof(double), typeof(Cattach), new PropertyMetadata(1.0, OnUncheckedOpacityChanged));

    public static void OnUncheckedOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        double n = (double)e.NewValue;

        double o = (double)e.OldValue;
    }
}
