// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Threading.Tasks;

namespace H.Common.Transitionable;

public static class TransitionHostableExtensions
{
    public static async Task Show(this ITransitionHostable host, UIElement visual)
    {
        visual.Visibility = Visibility.Visible;
        if (host.Transitionable == null)
            return;
        IsHitTestVisibleDisposable disposable = new IsHitTestVisibleDisposable(visual);
        await host.Transitionable.Show(visual);
        disposable.Dispose();
    }

    public static async Task Close(this ITransitionHostable host, UIElement visual)
    {
        if (host.Transitionable == null)
            return;
        IsHitTestVisibleDisposable disposable = new IsHitTestVisibleDisposable(visual);
        await host.Transitionable.Close(visual);
        disposable.Dispose();
        visual.Visibility = Visibility.Collapsed;
    }
}
