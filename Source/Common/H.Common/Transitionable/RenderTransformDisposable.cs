// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System;
global using System.Windows;
global using System.Windows.Media;

namespace H.Common.Transitionable;

public class RenderTransformDisposable : IDisposable
{
    private readonly Transform _renderTransform;
    private readonly Point _renderTransformOrigin;
    private readonly UIElement _element;
    public RenderTransformDisposable(UIElement element)
    {
        _element = element;
        _renderTransform = element.RenderTransform;
        _renderTransformOrigin = element.RenderTransformOrigin;
    }
    public void Dispose()
    {
        _element.RenderTransform = _renderTransform;
        _element.RenderTransformOrigin = _renderTransformOrigin;
    }
}
