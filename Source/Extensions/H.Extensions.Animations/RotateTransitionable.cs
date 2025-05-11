// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Media;
using System.Windows.Media.Animation;

namespace H.Extensions.Animations;

public class RotateTransitionable : ElementTransitionable
{
    public RotateTransitionable()
    {
        this.From = 360;
        this.To = 360;
    }
    public override async Task Close(UIElement visual)
    {
        DoubleAnimation amination = new DoubleAnimation(this.To, this.CloseDuration);
        await Task.Delay(this.CloseDuration);
        using RenderTransformDisposable renderTransformDisposable = new RenderTransformDisposable(visual);
        RotateTransform transform = new RotateTransform(0);
        visual.RenderTransform = transform;
        visual.RenderTransformOrigin = new Point(0.5, 0.5);
        transform.BeginAnimation(RotateTransform.AngleProperty, amination);
        await Task.Delay(this.CloseDuration);
    }

    public override async Task Show(UIElement visual)
    {
        DoubleAnimation amination = new DoubleAnimation(0, this.ShowDuration);
        using RenderTransformDisposable renderTransformDisposable = new RenderTransformDisposable(visual);
        RotateTransform transform = new RotateTransform(this.From);
        visual.RenderTransform = transform;
        visual.RenderTransformOrigin = new Point(0.5, 0.5);
        transform.BeginAnimation(RotateTransform.AngleProperty, amination);
        await Task.Delay(this.ShowDuration);
    }
}
