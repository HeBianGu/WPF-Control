// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Controls.Adorner.Adorner.Base;

public abstract class AdornerBase : System.Windows.Documents.Adorner
{
    public AdornerBase(UIElement adornedElement) : base(adornedElement)
    {

    }

    public static Pen GetPen(DependencyObject obj)
    {
        return (Pen)obj.GetValue(PenProperty);
    }

    public static void SetPen(DependencyObject obj, Pen value)
    {
        obj.SetValue(PenProperty, value);
    }


    public static readonly DependencyProperty PenProperty =
        DependencyProperty.RegisterAttached("Pen", typeof(Pen), typeof(AdornerBase), new PropertyMetadata(new Pen(Brushes.DeepSkyBlue, 1), OnPenChanged));

    public static void OnPenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Pen n = (Pen)e.NewValue;

        Pen o = (Pen)e.OldValue;
    }


    public static Brush GetFill(DependencyObject obj)
    {
        return (Brush)obj.GetValue(FillProperty);
    }

    public static void SetFill(DependencyObject obj, Brush value)
    {
        obj.SetValue(FillProperty, value);
    }


    public static readonly DependencyProperty FillProperty =
        DependencyProperty.RegisterAttached("Fill", typeof(Brush), typeof(AdornerBase), new PropertyMetadata(default(Brush), OnFillChanged));

    public static void OnFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Brush n = (Brush)e.NewValue;

        Brush o = (Brush)e.OldValue;
    }




}
