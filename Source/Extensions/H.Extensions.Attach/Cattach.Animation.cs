// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace H.Extensions.Attach;

public static partial class Cattach
{

    public static double GetFromDouble(DependencyObject obj)
    {
        return (double)obj.GetValue(FromDoubleProperty);
    }

    public static void SetFromDouble(DependencyObject obj, double value)
    {
        obj.SetValue(FromDoubleProperty, value);
    }


    public static readonly DependencyProperty FromDoubleProperty =
        DependencyProperty.RegisterAttached("FromDouble", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnFromDoubleChanged));

    public static void OnFromDoubleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        double n = (double)e.NewValue;

        double o = (double)e.OldValue;
    }


    public static double GetToDouble(DependencyObject obj)
    {
        return (double)obj.GetValue(ToDoubleProperty);
    }

    public static void SetToDouble(DependencyObject obj, double value)
    {
        obj.SetValue(ToDoubleProperty, value);
    }


    public static readonly DependencyProperty ToDoubleProperty =
        DependencyProperty.RegisterAttached("ToDouble", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnToDoubleChanged));

    public static void OnToDoubleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        double n = (double)e.NewValue;

        double o = (double)e.OldValue;
    }





    public static Color GetToBackgroundColor(DependencyObject obj)
    {
        return (Color)obj.GetValue(ToBackgroundColorProperty);
    }

    public static void SetToBackgroundColor(DependencyObject obj, Color value)
    {
        obj.SetValue(ToBackgroundColorProperty, value);
    }
    public static readonly DependencyProperty ToBackgroundColorProperty =
        DependencyProperty.RegisterAttached("ToBackgroundColor", typeof(Color), typeof(Cattach), new PropertyMetadata(default(Color), OnToBackgroundColorChanged));

    static public void OnToBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        Control control = d as Control;
        Color n = (Color)e.NewValue;
        Color o = (Color)e.OldValue;
        //var colorAnimation = new ColorAnimation
        //{
        //    To = n,
        //    Duration = TimeSpan.FromSeconds(0.2)
        //};
        //if (control.Background is SolidColorBrush solid)
        //{
        //    if (solid.IsFrozen)
        //    {
        //        solid = solid.Clone();
        //        control.Background = solid;
        //    }
        //}
        //else
        //{
        //    solid = new SolidColorBrush();
        //    control.Background = solid;
        //}
        ////solid.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        //Storyboard storyboard = new Storyboard();
        //storyboard.Children.Add(new DoubleAnimation()
        //{
        //    To = 0,
        //    Duration = TimeSpan.FromSeconds(1)
        //});
        //storyboard.Children.Add(new DoubleAnimation()
        //{
        //    BeginTime = TimeSpan.FromSeconds(1),
        //    To = 1,
        //    Duration = TimeSpan.FromSeconds(0.5)
        //});
        //Storyboard.SetTarget(storyboard, control);
        //Storyboard.SetTargetProperty(control, new PropertyPath(FrameworkElement.OpacityProperty));


        var storyboard = new Storyboard();
        var opacityAnimation = new DoubleAnimationUsingKeyFrames
        {
            Duration = TimeSpan.FromSeconds(1) // 完整循环时间
        };

        // 添加关键帧
        opacityAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, KeyTime.FromPercent(0.0)));   // 开始: 1.0
        opacityAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0.0, KeyTime.FromPercent(0.5)));  // 中间: 0.0
        opacityAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, KeyTime.FromPercent(1.0)));  // 结束: 1.0

        storyboard.Children.Add(opacityAnimation);
        Storyboard.SetTarget(opacityAnimation, control);
        Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(UIElement.OpacityProperty));
        storyboard.Begin(control);
    }


}
