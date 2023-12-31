﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace H.Extensions.StoryBoard
{
    public abstract class StoryboardEngineBase : IDisposable
    {
        public Storyboard Storyboard { get; set; } = StoryboardFactory.Create();
        public EventHandler CompletedEvent { get; set; }
        public EasingFunctionBase Easing { get; set; } = EasingFunctionFactroy.PowerEase;
        public PropertyPath PropertyPath { get; set; }
        public Duration Duration { get; set; }
        public void Dispose()
        {
            Storyboard.Completed -= CompletedEvent;
        }

        public abstract StoryboardEngineBase Start(UIElement element, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null);
        public abstract StoryboardEngineBase Stop();
        public StoryboardEngineBase(double second, string property)
        {
            PropertyPath = new PropertyPath(property);
            Duration = new Duration(TimeSpan.FromSeconds(second));
        }
    }

    public abstract class StoryboardEngineBase<T> : StoryboardEngineBase
    {
        public StoryboardEngineBase(T from, T to, double second, string property) : base(second, property)
        {
            FromValue = from;
            ToValue = to;
        }
        public T FromValue { get; set; }
        public T ToValue { get; set; }

    }
}
