// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;
using System.Windows.Media.Animation;

namespace H.Extensions.StoryBoard;

public class DoubleStoryboard : StoryboardEngineBase<double>
{
    public static DoubleStoryboard Create(double from, double to, double second, string property)
    {
        return new DoubleStoryboard(from, to, second, property);
    }

    public static void StartAll(UIElement element, params DoubleStoryboard[] engines)
    {
        foreach (DoubleStoryboard item in engines)
        {
            item.Start(element);
        }
    }

    public DoubleStoryboard(double from, double to, double second, string property) : base(from, to, second, property)
    {

    }

    protected bool CanAnimation(UIElement element)
    {
        if (element is FrameworkElement framework)
        {
            return element.IsVisible && framework.IsLoaded;
        }
        return element.IsVisible;
    }

    public override StoryboardEngineBase Start(UIElement element, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
    {
        if (CanAnimation(element) == false)
        {
            Completed?.Invoke(element);
            return this;
        }
        DoubleAnimation animation = new DoubleAnimation(this.FromValue, this.ToValue, this.Duration);
        this.Storyboard.Children.Add(animation);
        Storyboard.SetTarget(animation, element);
        Storyboard.SetTargetProperty(animation, this.PropertyPath);
        if (this.CompletedEvent != null)
        {
            this.Storyboard.Completed += this.CompletedEvent;
        }
        if (Completed != null)
        {
            this.Storyboard.Completed += (l, k) =>
            {
                Completed?.Invoke(element);
            };
        }
        Timeline.SetDesiredFrameRate(this.Storyboard, StoryboardSetting.DesiredFrameRate);
        init?.Invoke(this);
        animation.EasingFunction = this.Easing;
        this.Storyboard.Begin();
        return this;
    }
    public override StoryboardEngineBase Stop()
    {
        this.Storyboard.Stop();
        return this;
    }
}
