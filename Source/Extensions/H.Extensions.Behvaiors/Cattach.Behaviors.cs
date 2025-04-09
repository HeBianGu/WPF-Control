// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Collections.ObjectModel;
using System.Windows;

namespace H.Extensions.Behvaiors;

public static partial class BehvaiorAttach
{
    public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached(
        "Behaviors", typeof(Behaviors), typeof(BehvaiorAttach), new FrameworkPropertyMetadata(new Behaviors(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBehaviorsChanged));

    public static Behaviors GetBehaviors(DependencyObject d)
    {
        return (Behaviors)d.GetValue(BehaviorsProperty);
    }

    public static void SetBehaviors(DependencyObject obj, Behaviors value)
    {
        obj.SetValue(BehaviorsProperty, value);
    }

    private static void OnBehaviorsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        Behaviors o = args.OldValue as Behaviors;

        Behaviors n = args.NewValue as Behaviors;

        BehaviorCollection bc = Interaction.GetBehaviors(sender);

        //  Do ：先删除
        if (o != null)
        {
            foreach (Behavior b in o)
            {
                Behavior behavior = bc.FirstOrDefault(beh => beh.GetType() == b.GetType());

                if (behavior != null)
                    bc.Remove(behavior);
            }
        }

        //  Do ：再更新
        if (n != null)
        {
            foreach (Behavior b in n)
            {
                Behavior behavior = bc.FirstOrDefault(beh => beh.GetType() == b.GetType());

                Behavior instance = Activator.CreateInstance(b.GetType()) as Behavior;

                if (behavior != null)
                {
                    bc.Remove(behavior);
                }


                foreach (System.Reflection.PropertyInfo property in b.GetType().GetProperties())
                {
                    if (property.CanRead && property.CanWrite)
                    {
                        property.SetValue(instance, property.GetValue(b));
                    }
                }

                bc.Add(instance);
            }
        }

    }
}

public class Behaviors : ObservableCollection<Behavior>
{

}
