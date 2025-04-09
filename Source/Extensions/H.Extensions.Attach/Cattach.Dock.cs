// CopyRightTemplate © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Attach;

public static partial class Cattach
{
    public static ControlTemplate GetBottomTemplate(DependencyObject obj)
    {
        return (ControlTemplate)obj.GetValue(BottomTemplateProperty);
    }

    public static void SetBottomTemplate(DependencyObject obj, ControlTemplate value)
    {
        obj.SetValue(BottomTemplateProperty, value);
    }


    public static readonly DependencyProperty BottomTemplateProperty =
        DependencyProperty.RegisterAttached("BottomTemplate", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(default(ControlTemplate), OnBottomTemplateChanged));

    public static void OnBottomTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        ControlTemplate n = (ControlTemplate)e.NewValue;

        ControlTemplate o = (ControlTemplate)e.OldValue;
    }

    public static ControlTemplate GetTopTemplate(DependencyObject obj)
    {
        return (ControlTemplate)obj.GetValue(TopTemplateProperty);
    }

    public static void SetTopTemplate(DependencyObject obj, ControlTemplate value)
    {
        obj.SetValue(TopTemplateProperty, value);
    }


    public static readonly DependencyProperty TopTemplateProperty =
        DependencyProperty.RegisterAttached("TopTemplate", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(default(ControlTemplate), OnTopTemplateChanged));

    public static void OnTopTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        ControlTemplate n = (ControlTemplate)e.NewValue;

        ControlTemplate o = (ControlTemplate)e.OldValue;
    }

    public static ControlTemplate GetLeftTemplate(DependencyObject obj)
    {
        return (ControlTemplate)obj.GetValue(LeftTemplateProperty);
    }

    public static void SetLeftTemplate(DependencyObject obj, ControlTemplate value)
    {
        obj.SetValue(LeftTemplateProperty, value);
    }


    public static readonly DependencyProperty LeftTemplateProperty =
        DependencyProperty.RegisterAttached("LeftTemplate", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(default(ControlTemplate), OnLeftTemplateChanged));

    public static void OnLeftTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        ControlTemplate n = (ControlTemplate)e.NewValue;

        ControlTemplate o = (ControlTemplate)e.OldValue;
    }

    public static ControlTemplate GetRightTemplate(DependencyObject obj)
    {
        return (ControlTemplate)obj.GetValue(RightTemplateProperty);
    }

    public static void SetRightTemplate(DependencyObject obj, ControlTemplate value)
    {
        obj.SetValue(RightTemplateProperty, value);
    }


    public static readonly DependencyProperty RightTemplateProperty =
        DependencyProperty.RegisterAttached("RightTemplate", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(default(ControlTemplate), OnRightTemplateChanged));

    public static void OnRightTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        ControlTemplate n = (ControlTemplate)e.NewValue;

        ControlTemplate o = (ControlTemplate)e.OldValue;
    }


    public static ControlTemplate GetCenterTemplate(DependencyObject obj)
    {
        return (ControlTemplate)obj.GetValue(CenterTemplateProperty);
    }

    public static void SetCenterTemplate(DependencyObject obj, ControlTemplate value)
    {
        obj.SetValue(CenterTemplateProperty, value);
    }


    public static readonly DependencyProperty CenterTemplateProperty =
        DependencyProperty.RegisterAttached("CenterTemplate", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(default(ControlTemplate), OnCenterTemplateChanged));

    public static void OnCenterTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        ControlTemplate n = (ControlTemplate)e.NewValue;

        ControlTemplate o = (ControlTemplate)e.OldValue;
    }

}
