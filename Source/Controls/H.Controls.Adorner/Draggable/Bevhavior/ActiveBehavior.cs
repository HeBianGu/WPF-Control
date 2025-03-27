// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace H.Controls.Adorner.Draggable.Bevhavior;


//  ToDo：目前不好用 后面测试
public abstract class ActiveBehavior<B, T> : Behavior<T> where T : DependencyObject where B : Behavior, new()
{
    public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(B),
            new PropertyMetadata(default(bool), OnIsActiveChanged));

    public static bool GetIsActive(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsActiveProperty);
    }

    public static void SetIsActive(DependencyObject obj, bool value)
    {
        obj.SetValue(IsActiveProperty, value);
    }

    private static void OnIsActiveChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
    {
        BehaviorCollection bc = Interaction.GetBehaviors(dpo);

        if (Convert.ToBoolean(e.NewValue))
            bc.Add(new B());
        else
        {
            Behavior behavior = bc.FirstOrDefault(beh => beh.GetType() == typeof(B));
            if (behavior != null)
                bc.Remove(behavior);
        }
    }
}



