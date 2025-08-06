﻿// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace H.Controls.Panel.Panel;

/// <summary>
/// 用于加载时延时布局
/// </summary>
public class DelayStackPanel : StackPanel
{

    public DispatcherPriority DispatcherPriority
    {
        get { return (DispatcherPriority)GetValue(DispatcherPriorityProperty); }
        set { SetValue(DispatcherPriorityProperty, value); }
    }


    public static readonly DependencyProperty DispatcherPriorityProperty =
        DependencyProperty.Register("DispatcherPriority", typeof(DispatcherPriority), typeof(DelayStackPanel), new FrameworkPropertyMetadata(DispatcherPriority.Input, (d, e) =>
        {
            DelayStackPanel control = d as DelayStackPanel;

            if (control == null) return;

            if (e.OldValue is DispatcherPriority o)
            {

            }

            if (e.NewValue is DispatcherPriority n)
            {

            }

        }));

    //protected override Size ArrangeOverride(Size arrangeSize)
    //{
    //    try
    //    {
    //        UIElementCollection children = this.InternalChildren;
    //        bool fHorizontal = (this.Orientation == Orientation.Horizontal);
    //        Rect rcChild = new Rect(arrangeSize);
    //        double previousChildSize = 0.0;
    //        this.Dispatcher.DelayInvoke(children, x =>
    //            {
    //                UIElement child = x as UIElement;
    //                if (child == null)
    //                    return;
    //                if (fHorizontal)
    //                {
    //                    rcChild.X += previousChildSize;
    //                    previousChildSize = child.DesiredSize.Width;
    //                    rcChild.Width = previousChildSize;
    //                    rcChild.Height = Math.Max(arrangeSize.Height, child.DesiredSize.Height);
    //                }
    //                else
    //                {
    //                    rcChild.Y += previousChildSize;
    //                    previousChildSize = child.DesiredSize.Height;
    //                    rcChild.Height = previousChildSize;
    //                    rcChild.Width = Math.Max(arrangeSize.Width, child.DesiredSize.Width);
    //                }
    //                child.Arrange(rcChild);
    //            }, null, this.DispatcherPriority);
    //        return arrangeSize;
    //    }
    //    finally
    //    {

    //    }
    //}
    private bool _isRefreshing;
    protected override Size ArrangeOverride(Size arrangeSize)
    {
        if (_isRefreshing)
            return arrangeSize;
        _isRefreshing = true;

        this.Dispatcher.BeginInvoke(this.DispatcherPriority, new Action(() =>
        {
            System.Diagnostics.Debug.WriteLine("DelayInvoke:" + DateTime.Now);
            _isRefreshing = false;
            try
            {
                UIElementCollection children = this.InternalChildren;
                bool fHorizontal = this.Orientation == Orientation.Horizontal;
                Rect rcChild = new Rect(arrangeSize);
                double previousChildSize = 0.0;
                for (int i = 0, count = children.Count; i < count; ++i)
                {
                    UIElement child = children[i];

                    if (child == null) continue;
                    Application.Current.Dispatcher.BeginInvoke(this.DispatcherPriority, new Action(() =>
                    {
                        if (_isRefreshing)
                            return;
                        if (fHorizontal)
                        {
                            rcChild.X += previousChildSize;
                            previousChildSize = child.DesiredSize.Width;
                            rcChild.Width = previousChildSize;
                            rcChild.Height = Math.Max(arrangeSize.Height, child.DesiredSize.Height);
                        }
                        else
                        {
                            rcChild.Y += previousChildSize;
                            previousChildSize = child.DesiredSize.Height;
                            rcChild.Height = previousChildSize;
                            rcChild.Width = Math.Max(arrangeSize.Width, child.DesiredSize.Width);
                        }
                        child.Arrange(rcChild);
                        //System.Diagnostics.Debug.WriteLine("DelayInvokeItem:" + DateTime.Now);

                    }));
                }
            }
            finally
            {

            }
        }));

        return arrangeSize;
    }

    //private bool _isRefreshing;
    //protected override Size ArrangeOverride(Size arrangeSize)
    //{
    //    if (_isRefreshing)
    //        return arrangeSize;
    //    _isRefreshing = true;

    //    this.Dispatcher.BeginInvoke(this.DispatcherPriority, new Action(() =>
    //    {
    //        System.Diagnostics.Debug.WriteLine("DelayInvoke:" + DateTime.Now);
    //        _isRefreshing = false;
    //        base.ArrangeOverride(arrangeSize);
    //    }));
    //    return arrangeSize;
    //}

    //private bool _isRefreshing11;
    //protected override Size MeasureOverride(Size constraint)
    //{
    //    if (_isRefreshing11)
    //        return constraint;
    //    _isRefreshing11 = true;

    //    this.Dispatcher.BeginInvoke(this.DispatcherPriority, new Action(() =>
    //    {
    //        System.Diagnostics.Debug.WriteLine("DelayInvoke:" + DateTime.Now);
    //        _isRefreshing11 = false;
    //        base.MeasureOverride(constraint);
    //    }));
    //    return constraint;
    //}
}
