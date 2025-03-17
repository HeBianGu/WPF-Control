// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Controls.Diagram.Layers.Base;

public abstract class Layer : Panel
{
    public string Id { get; set; }
    //public bool UseAnimation
    //{
    //    get { return (bool)GetValue(UseAnimationProperty); }
    //    set { SetValue(UseAnimationProperty, value); }
    //}


    //public static readonly DependencyProperty UseAnimationProperty =
    //    DependencyProperty.Register("UseAnimation", typeof(bool), typeof(Layer), new PropertyMetadata(true, (d, e) =>
    //    {
    //        Layer control = d as Layer;

    //        if (control == null) return;

    //        //bool config = e.NewValue as bool;

    //    }));



    //public TimeSpan Duration
    //{
    //    get { return (TimeSpan)GetValue(DurationProperty); }
    //    set { SetValue(DurationProperty, value); }
    //}


    //public static readonly DependencyProperty DurationProperty =
    //    DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(Layer), new PropertyMetadata(TimeSpan.FromMilliseconds(200), (d, e) =>
    //    {
    //        Layer control = d as Layer;

    //        if (control == null) return;

    //        //TimeSpan config = e.NewValue as TimeSpan;

    //    }));


    protected override Size ArrangeOverride(Size arrangeSize)
    {
        //this.UseAnimation = false;

        return arrangeSize;
    }


    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        base.OnMouseDown(e);
    }

}
