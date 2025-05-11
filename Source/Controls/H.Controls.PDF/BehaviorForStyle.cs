// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace H.Controls.PDF
{
    /// <summary>
    /// Wrapper to allow a behavior to be included in a style definition
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <typeparam name="TBehavior"></typeparam>
    public class BehaviorForStyle<TTarget, TBehavior> : Behavior<TTarget>
        where TTarget : DependencyObject
        where TBehavior : BehaviorForStyle<TTarget, TBehavior>, new()
    {
        public static readonly DependencyProperty IsEnabledForStyleProperty =
            DependencyProperty.RegisterAttached("IsEnabledForStyle",
                typeof(bool),
                typeof(BehaviorForStyle<TTarget, TBehavior>),
                new FrameworkPropertyMetadata(false, OnIsEnabledForStyleChanged));

        public bool IsEnabledForStyle
        {
            get => (bool)GetValue(IsEnabledForStyleProperty);
            set => SetValue(IsEnabledForStyleProperty, value);
        }

        private static void OnIsEnabledForStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is bool))
                return;

            var newValue = (bool)e.NewValue;

            var behaviors = Interaction.GetBehaviors(d);
            var existingBehavior = behaviors.FirstOrDefault(b => b.GetType() == typeof(TBehavior)) as TBehavior;

            if (!newValue && existingBehavior != null)
                behaviors.Remove(existingBehavior);
            else if (newValue && existingBehavior == null)
                behaviors.Add(new TBehavior());
        }
    }
}