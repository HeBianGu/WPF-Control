// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using Microsoft.Xaml.Behaviors;
#if NET
#endif 
using System.Windows;
using System.Windows.Media;

namespace H.Extensions.Behvaiors.Adorners;

public abstract class AdornerBehaviorBase : Behavior<FrameworkElement>
{
    public Type AdornerType
    {
        get { return (Type)GetValue(AdornerTypeProperty); }
        set { SetValue(AdornerTypeProperty, value); }
    }

    public static readonly DependencyProperty AdornerTypeProperty =
        DependencyProperty.Register("AdornerType", typeof(Type), typeof(AdornerBehaviorBase), new PropertyMetadata(default(Type), (d, e) =>
        {
            AdornerBehaviorBase control = d as AdornerBehaviorBase;

            if (control == null) return;

            Type config = e.NewValue as Type;

        }));


    public Visual AdornerVisual
    {
        get { return (Visual)GetValue(AdornerVisualProperty); }
        set { SetValue(AdornerVisualProperty, value); }
    }


    public static readonly DependencyProperty AdornerVisualProperty =
        DependencyProperty.Register("AdornerVisual", typeof(Visual), typeof(AdornerBehaviorBase), new FrameworkPropertyMetadata(default(Visual), (d, e) =>
        {
            AdornerBehaviorBase control = d as AdornerBehaviorBase;

            if (control == null) return;

            if (e.OldValue is Visual o)
            {

            }

            if (e.NewValue is Visual n)
            {

            }
        }));

    public bool IsUse
    {
        get { return (bool)GetValue(IsUseProperty); }
        set { SetValue(IsUseProperty, value); }
    }


    public static readonly DependencyProperty IsUseProperty =
        DependencyProperty.Register("IsUse", typeof(bool), typeof(AdornerBehaviorBase), new FrameworkPropertyMetadata(true, (d, e) =>
        {
            AdornerBehaviorBase control = d as AdornerBehaviorBase;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }
        }));


    public bool IsHitTestVisible
    {
        get { return (bool)GetValue(IsHitTestVisibleProperty); }
        set { SetValue(IsHitTestVisibleProperty, value); }
    }


    public static readonly DependencyProperty IsHitTestVisibleProperty =
        DependencyProperty.Register("IsHitTestVisible", typeof(bool), typeof(AdornerBehaviorBase), new FrameworkPropertyMetadata(default(bool), (d, e) =>
        {
            AdornerBehaviorBase control = d as AdornerBehaviorBase;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }

        }));


}



