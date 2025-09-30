// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Media.Animation;

namespace H.Extensions.Animations;

public class OpacityTransitionable : ElementTransitionable
{
    public OpacityTransitionable()
    {
        this.From = 0;
        this.To = 0;
    }

    public override async Task Close(UIElement visual)
    {
        DoubleAnimation amination = new DoubleAnimation(this.To, this.CloseDuration);
        visual.BeginAnimation(UIElement.OpacityProperty, amination);
        await Task.Delay(this.CloseDuration);
    }

    public override async Task Show(UIElement visual)
    {
        DoubleAnimation amination = new DoubleAnimation(this.From, 1, this.ShowDuration);
        visual.BeginAnimation(UIElement.OpacityProperty, amination);
        await Task.Delay(this.CloseDuration);
    }
}
