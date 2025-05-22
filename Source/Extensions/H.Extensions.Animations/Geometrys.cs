// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Transitionable;
global using System.Windows;
using H.Mvvm.ViewModels.Base;

namespace H.Extensions.Animations;

public abstract class TransitionableBase<V> : BindableBase, ITransitionable
{
    //public TimeSpan ShowDuration { get; set; } = TimeSpan.FromMilliseconds(2000);
    //public TimeSpan CloseDuration { get; set; } = TimeSpan.FromMilliseconds(1000);

    public TimeSpan ShowDuration { get; set; } = TimeSpan.FromMilliseconds(200);
    public TimeSpan CloseDuration { get; set; } = TimeSpan.FromMilliseconds(100);
    public V From { get; set; }
    public V To { get; set; }
    public abstract Task Close(DependencyObject visual);
    public abstract Task Show(DependencyObject visual);
}

public abstract class TransitionableBase<T, V> : TransitionableBase<V> where T : DependencyObject
{
    public override async Task Show(DependencyObject visual)
    {
        if (visual is T t)
        {
            await Show(t);
        }
    }

    public override async Task Close(DependencyObject visual)
    {
        if (visual is T t)
        {
            await Close(t);
        }
    }

    public abstract Task Show(T visual);

    public abstract Task Close(T visual);
}

public abstract class ElementTransitionableBase<T> : TransitionableBase<UIElement, T>
{

}

public abstract class ElementTransitionable : ElementTransitionableBase<double>
{

}
