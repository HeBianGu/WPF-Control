// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Controls;

namespace H.Windows.Main;

[TemplatePart(Name = "PART_AdornerBorder")]
public class MainWindow : Window, IMainWindow, IAdornerDialogElement
{
    static MainWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindow), new FrameworkPropertyMetadata(typeof(MainWindow)));
    }

    private UIElement _adornerBorder;
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _adornerBorder = this.Template.FindName("PART_AdornerBorder", this) as UIElement;
    }

    public double CaptionHeight
    {
        get { return (double)GetValue(CaptionHeightProperty); }
        set { SetValue(CaptionHeightProperty, value); }
    }

    public static readonly DependencyProperty CaptionHeightProperty =
        DependencyProperty.Register("CaptionHeight", typeof(double), typeof(MainWindow), new FrameworkPropertyMetadata(45.0, (d, e) =>
        {
            MainWindow control = d as MainWindow;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }

        }));

    public ControlTemplate CaptionTempate
    {
        get { return (ControlTemplate)GetValue(CaptionTempateProperty); }
        set { SetValue(CaptionTempateProperty, value); }
    }

    public static readonly DependencyProperty CaptionTempateProperty =
        DependencyProperty.Register("CaptionTempate", typeof(ControlTemplate), typeof(MainWindow), new FrameworkPropertyMetadata(default(ControlTemplate), (d, e) =>
        {
            MainWindow control = d as MainWindow;

            if (control == null) return;

            if (e.OldValue is ControlTemplate o)
            {

            }

            if (e.NewValue is ControlTemplate n)
            {

            }

        }));

    public ControlTemplate SideTemplate
    {
        get { return (ControlTemplate)GetValue(SideTemplateProperty); }
        set { SetValue(SideTemplateProperty, value); }
    }

    public static readonly DependencyProperty SideTemplateProperty =
        DependencyProperty.Register("SideTemplate", typeof(ControlTemplate), typeof(MainWindow), new FrameworkPropertyMetadata(default(ControlTemplate), (d, e) =>
        {
            MainWindow control = d as MainWindow;

            if (control == null) return;

            if (e.OldValue is ControlTemplate o)
            {

            }

            if (e.NewValue is ControlTemplate n)
            {

            }
        }));

    public UIElement GetElement()
    {
        return this._adornerBorder ?? this.Content as UIElement;
    }

    //protected override void OnStateChanged(EventArgs e)
    //{
    //    base.OnStateChanged(e);
    //    if (this.WindowState == WindowState.Maximized)
    //    {
    //        this.Top = 0;
    //        this.Left = 0;
    //        this.Width = SystemParameters.WorkArea.Width;
    //        this.Height = SystemParameters.WorkArea.Height;
    //    }
    //}
}
