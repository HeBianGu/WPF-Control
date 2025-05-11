// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Common.Transitionable;

public class IsHitTestVisibleDisposable : IDisposable
{
    private readonly bool _isHitTestVisible;
    private readonly UIElement _element;
    public IsHitTestVisibleDisposable(UIElement element)
    {
        _element = element;
        _isHitTestVisible = _element.IsHitTestVisible;
        _element.IsHitTestVisible = false;
    }
    public void Dispose()
    {
        _element.IsHitTestVisible = _isHitTestVisible;
    }
}
