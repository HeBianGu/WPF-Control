﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Attach;

public static partial class Cattach
{

    public static bool GetUseSearch(DependencyObject obj)
    {
        return (bool)obj.GetValue(UseSearchProperty);
    }

    public static void SetUseSearch(DependencyObject obj, bool value)
    {
        obj.SetValue(UseSearchProperty, value);
    }


    public static readonly DependencyProperty UseSearchProperty =
        DependencyProperty.RegisterAttached("UseSearch", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, OnUseSearchChanged));

    public static void OnUseSearchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //Cattach control = d as Cattach;

        //bool n = e.NewValue as bool;

        //bool o = e.OldValue as bool;
    }

    [TypeConverter(typeof(LengthConverter))]
    public static double GetSearchWidth(DependencyObject obj)
    {
        return (double)obj.GetValue(SearchWidthProperty);
    }
    [TypeConverter(typeof(LengthConverter))]
    public static void SetSearchWidth(DependencyObject obj, double value)
    {
        obj.SetValue(SearchWidthProperty, value);
    }


    public static readonly DependencyProperty SearchWidthProperty =
        DependencyProperty.RegisterAttached("SearchWidth", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(default(double), OnSearchWidthChanged));

    public static void OnSearchWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //Cattach control = d as Cattach;

        //double n = e.NewValue as double;

        //double o = e.OldValue as double;
    }


    public static Dock GetSearchDock(DependencyObject obj)
    {
        return (Dock)obj.GetValue(SearchDockProperty);
    }

    public static void SetSearchDock(DependencyObject obj, Dock value)
    {
        obj.SetValue(SearchDockProperty, value);
    }


    public static readonly DependencyProperty SearchDockProperty =
        DependencyProperty.RegisterAttached("SearchDock", typeof(Dock), typeof(Cattach), new FrameworkPropertyMetadata(Dock.Right, OnSearchDockChanged));

    public static void OnSearchDockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Dock n = (Dock)e.NewValue;

        Dock o = (Dock)e.OldValue;
    }


    public static bool GetSearchUseHistory(DependencyObject obj)
    {
        return (bool)obj.GetValue(SearchUseHistoryProperty);
    }

    public static void SetSearchUseHistory(DependencyObject obj, bool value)
    {
        obj.SetValue(SearchUseHistoryProperty, value);
    }


    public static readonly DependencyProperty SearchUseHistoryProperty =
        DependencyProperty.RegisterAttached("SearchUseHistory", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, OnSearchUseHistoryChanged));

    public static void OnSearchUseHistoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }


    public static double GetSearchHeight(DependencyObject obj)
    {
        return (double)obj.GetValue(SearchHeightProperty);
    }

    public static void SetSearchHeight(DependencyObject obj, double value)
    {
        obj.SetValue(SearchHeightProperty, value);
    }


    public static readonly DependencyProperty SearchHeightProperty =
        DependencyProperty.RegisterAttached("SearchHeight", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnSearchHeightChanged));

    public static void OnSearchHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        double n = (double)e.NewValue;

        double o = (double)e.OldValue;
    }


    public static VerticalAlignment GetSearchVerticalAlignment(DependencyObject obj)
    {
        return (VerticalAlignment)obj.GetValue(SearchVerticalAlignmentProperty);
    }

    public static void SetSearchVerticalAlignment(DependencyObject obj, VerticalAlignment value)
    {
        obj.SetValue(SearchVerticalAlignmentProperty, value);
    }


    public static readonly DependencyProperty SearchVerticalAlignmentProperty =
        DependencyProperty.RegisterAttached("SearchVerticalAlignment", typeof(VerticalAlignment), typeof(Cattach), new PropertyMetadata(default(VerticalAlignment), OnSearchVerticalAlignmentChanged));

    public static void OnSearchVerticalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        VerticalAlignment n = (VerticalAlignment)e.NewValue;

        VerticalAlignment o = (VerticalAlignment)e.OldValue;
    }


    public static HorizontalAlignment GetSearchHorizontalAlignment(DependencyObject obj)
    {
        return (HorizontalAlignment)obj.GetValue(SearchHorizontalAlignmentProperty);
    }

    public static void SetSearchHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
    {
        obj.SetValue(SearchHorizontalAlignmentProperty, value);
    }


    public static readonly DependencyProperty SearchHorizontalAlignmentProperty =
        DependencyProperty.RegisterAttached("SearchHorizontalAlignment", typeof(HorizontalAlignment), typeof(Cattach), new PropertyMetadata(default(HorizontalAlignment), OnSearchHorizontalAlignmentChanged));

    public static void OnSearchHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        HorizontalAlignment n = (HorizontalAlignment)e.NewValue;

        HorizontalAlignment o = (HorizontalAlignment)e.OldValue;
    }


    public static Thickness GetSearchMargin(DependencyObject obj)
    {
        return (Thickness)obj.GetValue(SearchMarginProperty);
    }

    public static void SetSearchMargin(DependencyObject obj, Thickness value)
    {
        obj.SetValue(SearchMarginProperty, value);
    }


    public static readonly DependencyProperty SearchMarginProperty =
        DependencyProperty.RegisterAttached("SearchMargin", typeof(Thickness), typeof(Cattach), new PropertyMetadata(default(Thickness), OnSearchMarginChanged));

    public static void OnSearchMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Thickness n = (Thickness)e.NewValue;

        Thickness o = (Thickness)e.OldValue;
    }


    public static string GetSearchText(DependencyObject obj)
    {
        return (string)obj.GetValue(SearchTextProperty);
    }

    public static void SetSearchText(DependencyObject obj, string value)
    {
        obj.SetValue(SearchTextProperty, value);
    }


    public static readonly DependencyProperty SearchTextProperty =
        DependencyProperty.RegisterAttached("SearchText", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnSearchTextChanged));

    public static void OnSearchTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }


}
