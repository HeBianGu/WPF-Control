// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.Win32;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace H.Attach;

public static partial class Cattach
{
    public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
        "Watermark", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(""));

    public static string GetWatermark(DependencyObject d)
    {
        return (string)d.GetValue(WatermarkProperty);
    }

    public static void SetWatermark(DependencyObject obj, string value)
    {
        obj.SetValue(WatermarkProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
        "CornerRadius", typeof(CornerRadius), typeof(Cattach), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits));

    public static CornerRadius GetCornerRadius(DependencyObject d)
    {
        return (CornerRadius)d.GetValue(CornerRadiusProperty);
    }

    public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
    {
        obj.SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty PasswordProperty =
      DependencyProperty.RegisterAttached("Password",
      typeof(string), typeof(Cattach),
      new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

    private static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(Cattach));
    public static string GetPassword(DependencyObject dp)
    {
        return (string)dp.GetValue(PasswordProperty);
    }
    public static void SetPassword(DependencyObject dp, string value)
    {
        dp.SetValue(PasswordProperty, value);
    }
    private static bool GetIsUpdating(DependencyObject dp)
    {
        return (bool)dp.GetValue(IsUpdatingProperty);
    }
    private static void SetIsUpdating(DependencyObject dp, bool value)
    {
        dp.SetValue(IsUpdatingProperty, value);
    }
    private static void OnPasswordPropertyChanged(DependencyObject sender,
        DependencyPropertyChangedEventArgs e)
    {
        PasswordBox passwordBox = sender as PasswordBox;
        passwordBox.PasswordChanged -= PasswordChanged;
        if (!GetIsUpdating(passwordBox))
        {
            passwordBox.Password = (string)e.NewValue;
        }
        passwordBox.PasswordChanged += PasswordChanged;
    }
    private static void Attach(DependencyObject sender,
        DependencyPropertyChangedEventArgs e)
    {
        PasswordBox passwordBox = sender as PasswordBox;
        if (passwordBox == null)
            return;
        if ((bool)e.OldValue)
        {
            passwordBox.PasswordChanged -= PasswordChanged;
        }
        if ((bool)e.NewValue)
        {
            passwordBox.PasswordChanged += PasswordChanged;
        }
    }
    private static void PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordBox passwordBox = sender as PasswordBox;
        SetIsUpdating(passwordBox, true);
        SetPassword(passwordBox, passwordBox.Password);
        SetIsUpdating(passwordBox, false);
    }

    public static IList GetSelectedItems(DependencyObject obj)
    {
        return (IList)obj.GetValue(SelectedItemsProperty);
    }

    public static void SetSelectedItems(DependencyObject obj, IList value)
    {
        obj.SetValue(SelectedItemsProperty, value);
    }

    public static readonly DependencyProperty SelectedItemsProperty =

        DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(Cattach), new FrameworkPropertyMetadata(OnSelectedItemsChanged));

    public static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ListBox listBox = d as ListBox;

        if ((listBox != null) && (listBox.SelectionMode == SelectionMode.Multiple))
        {
            if (e.OldValue != null)
            {
                listBox.SelectionChanged -= OnlistBoxSelectionChanged;
            }

            IList collection = e.NewValue as IList;

            listBox.SelectedItems.Clear();

            if (collection != null)
            {
                foreach (object item in collection)
                {
                    listBox.SelectedItems.Add(item);
                }
                listBox.SelectionChanged += OnlistBoxSelectionChanged;
            }
        }
    }

    private static void OnlistBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        IList dataSource = GetSelectedItems(sender as DependencyObject);

        //添加用户选中的当前项.
        foreach (object item in e.AddedItems)
        {
            dataSource.Add(item);
        }

        //删除用户取消选中的当前项
        foreach (object item in e.RemovedItems)
        {
            dataSource.Remove(item);
        }
    }
}

public static partial class Cattach
{
    public static bool GetIsBuzy(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsBuzyProperty);
    }

    public static void SetIsBuzy(DependencyObject obj, bool value)
    {
        obj.SetValue(IsBuzyProperty, value);
    }

    public static readonly DependencyProperty IsBuzyProperty =
        DependencyProperty.RegisterAttached("IsBuzy", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false));

    public static string GetBuzyText(DependencyObject obj)
    {
        return (string)obj.GetValue(BuzyTextProperty);
    }

    public static void SetBuzyText(DependencyObject obj, string value)
    {
        obj.SetValue(BuzyTextProperty, value);
    }

    public static readonly DependencyProperty BuzyTextProperty =
        DependencyProperty.RegisterAttached("BuzyText", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata("请等待"));

    public static Geometry GetPath(DependencyObject obj)
    {
        return (Geometry)obj.GetValue(PathProperty);
    }

    public static void SetPath(DependencyObject obj, Geometry value)
    {
        obj.SetValue(PathProperty, value);
    }

    public static readonly DependencyProperty PathProperty =
        DependencyProperty.RegisterAttached("Path", typeof(Geometry), typeof(Cattach), new FrameworkPropertyMetadata(default(Geometry)));

}

public static partial class Cattach
{
    public static readonly DependencyProperty IsDragEnterProperty = DependencyProperty.RegisterAttached(
        "IsDragEnter", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDragEnterChanged));

    public static bool GetIsDragEnter(DependencyObject d)
    {
        return (bool)d.GetValue(IsDragEnterProperty);
    }

    public static void SetIsDragEnter(DependencyObject obj, bool value)
    {
        obj.SetValue(IsDragEnterProperty, value);
    }

    private static void OnIsDragEnterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {

    }

}

public static partial class Cattach
{
    /// <summary>
    /// 通过绑定方式设置资源
    /// </summary>
    public static readonly DependencyProperty DynamicResourcesProperty =
        DependencyProperty.RegisterAttached(
            "DynamicResources",
            typeof(ResourceDictionary),
            typeof(Cattach),
            new PropertyMetadata(null, OnDynamicResourcesChanged));

    public static void SetDynamicResources(DependencyObject obj, ResourceDictionary value) =>
        obj.SetValue(DynamicResourcesProperty, value);

    public static ResourceDictionary GetDynamicResources(DependencyObject obj) =>
        (ResourceDictionary)obj.GetValue(DynamicResourcesProperty);

    private static void OnDynamicResourcesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement element && e.NewValue is ResourceDictionary newResources)
        {
            element.Resources = newResources; 
        }
    }
}
