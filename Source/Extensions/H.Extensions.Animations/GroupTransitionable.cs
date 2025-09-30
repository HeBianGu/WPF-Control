// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Animations;

public class GroupTransitionable : ITransitionable
{
    public GroupTransitionable(params ITransitionable[] transitionables)
    {
        this._transitionables = transitionables;
    }
    private readonly ITransitionable[] _transitionables;

    public async Task Close(DependencyObject visual)
    {
        foreach (ITransitionable transitionable in this._transitionables)
        {
            await transitionable.Close(visual);
        }
    }

    public async Task Show(DependencyObject visual)
    {
        foreach (ITransitionable transitionable in this._transitionables)
        {
            await transitionable.Show(visual);
        }
    }
}
