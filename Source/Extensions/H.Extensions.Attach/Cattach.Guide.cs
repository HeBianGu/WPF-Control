// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Extensions.Attach;

/// <summary>
/// 新手向导
/// </summary>
public static partial class Cattach
{
    public static bool GetUseGuide(DependencyObject obj)
    {
        return (bool)obj.GetValue(UseGuideProperty);
    }

    public static void SetUseGuide(DependencyObject obj, bool value)
    {
        obj.SetValue(UseGuideProperty, value);
    }


    public static readonly DependencyProperty UseGuideProperty =
        DependencyProperty.RegisterAttached("UseGuide", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnUseGuideChanged));

    public static void OnUseGuideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;
        bool n = (bool)e.NewValue;
        bool o = (bool)e.OldValue;
    }

    public static object GetGuideTitle(DependencyObject obj)
    {
        return obj.GetValue(GuideTitleProperty);
    }

    public static void SetGuideTitle(DependencyObject obj, object value)
    {
        obj.SetValue(GuideTitleProperty, value);
    }


    public static readonly DependencyProperty GuideTitleProperty =
        DependencyProperty.RegisterAttached("GuideTitle", typeof(object), typeof(Cattach), new PropertyMetadata(null, OnGuideTitleChanged));

    public static void OnGuideTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        object n = e.NewValue;

        object o = e.OldValue;
    }


    public static string GetGuideParentTitle(DependencyObject obj)
    {
        return (string)obj.GetValue(GuideParentTitleProperty);
    }

    public static void SetGuideParentTitle(DependencyObject obj, string value)
    {
        obj.SetValue(GuideParentTitleProperty, value);
    }


    public static readonly DependencyProperty GuideParentTitleProperty =
        DependencyProperty.RegisterAttached("GuideParentTitle", typeof(string), typeof(Cattach), new PropertyMetadata(null, OnGuideParentTitleChanged));

    public static void OnGuideParentTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }


    public static object GetGuideData(DependencyObject obj)
    {
        return obj.GetValue(GuideDataProperty);
    }

    public static void SetGuideData(DependencyObject obj, object value)
    {
        obj.SetValue(GuideDataProperty, value);
    }


    public static readonly DependencyProperty GuideDataProperty =
        DependencyProperty.RegisterAttached("GuideData", typeof(object), typeof(Cattach), new PropertyMetadata(null, OnGuideDataChanged));

    public static void OnGuideDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;
        object n = e.NewValue;
        object o = e.OldValue;
    }

    public static DataTemplate GetGuideDataTemplate(DependencyObject obj)
    {
        return (DataTemplate)obj.GetValue(GuideDataTemplateProperty);
    }

    public static void SetGuideDataTemplate(DependencyObject obj, DataTemplate value)
    {
        obj.SetValue(GuideDataTemplateProperty, value);
    }

    public static readonly DependencyProperty GuideDataTemplateProperty =
        DependencyProperty.RegisterAttached("GuideDataTemplate", typeof(DataTemplate), typeof(Cattach), new PropertyMetadata(null, OnGuideDataTemplateChanged));

    public static void OnGuideDataTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        DataTemplate n = (DataTemplate)e.NewValue;

        DataTemplate o = (DataTemplate)e.OldValue;
    }


    public static bool GetIsGuide(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsGuideProperty);
    }

    public static void SetIsGuide(DependencyObject obj, bool value)
    {
        obj.SetValue(IsGuideProperty, value);
    }


    public static readonly DependencyProperty IsGuideProperty =
        DependencyProperty.RegisterAttached("IsGuide", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsGuideChanged));

    public static void OnIsGuideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        bool n = (bool)e.NewValue;
        bool o = (bool)e.OldValue;
    }


    public static bool GetGuideUseClick(DependencyObject obj)
    {
        return (bool)obj.GetValue(GuideUseClickProperty);
    }

    public static void SetGuideUseClick(DependencyObject obj, bool value)
    {
        obj.SetValue(GuideUseClickProperty, value);
    }


    public static readonly DependencyProperty GuideUseClickProperty =
        DependencyProperty.RegisterAttached("GuideUseClick", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnGuideUseClickChanged));

    public static void OnGuideUseClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }



    public static bool GetIsGuideAdonerElement(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsGuideAdonerElementProperty);
    }

    public static void SetIsGuideAdonerElement(DependencyObject obj, bool value)
    {
        obj.SetValue(IsGuideAdonerElementProperty, value);
    }

    public static readonly DependencyProperty IsGuideAdonerElementProperty =
        DependencyProperty.RegisterAttached("IsGuideAdonerElement", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnIsGuideAdonerElementChanged));

    static public void OnIsGuideAdonerElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }


    public static string GetGuideIcon(DependencyObject obj)
    {
        return (string)obj.GetValue(GuideIconProperty);
    }

    public static void SetGuideIcon(DependencyObject obj, string value)
    {
        obj.SetValue(GuideIconProperty, value);
    }

    public static readonly DependencyProperty GuideIconProperty =
        DependencyProperty.RegisterAttached("GuideIcon", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnGuideIconChanged));

    static public void OnGuideIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }


    public static string GetGuideAssemblyVersion(DependencyObject obj)
    {
        return (string)obj.GetValue(GuideAssemblyVersionProperty);
    }

    public static void SetGuideAssemblyVersion(DependencyObject obj, string value)
    {
        obj.SetValue(GuideAssemblyVersionProperty, value);
    }
    public static readonly DependencyProperty GuideAssemblyVersionProperty =
        DependencyProperty.RegisterAttached("GuideAssemblyVersion", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnGuideAssemblyVersionChanged));

    static public void OnGuideAssemblyVersionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }

}
