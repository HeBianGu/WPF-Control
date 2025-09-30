// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

#if NET
#endif
namespace H.Extensions.Behvaiors.Adorners;

public interface IHitTestElementDrag : IHitTestElementDrop, IGetDragAdorner
{
    void DragEnter(UIElement element, DragEventArgs e);
    void DragLeave(UIElement element, DragEventArgs e);
    void DragOver(UIElement element, DragEventArgs e);
}

public interface IGetDropAdorner
{
    Adorner GetDropAdorner(UIElement element);
    void RemoveDropAdorner(UIElement element);
}

public interface IGetDragAdorner
{
    Adorner GetDragAdorner(UIElement element);
    void RemoveDragAdorner(UIElement element);
}

public interface IHitTestElementDrop : IGetDropAdorner
{
    bool CanDrop(UIElement element, DragEventArgs e);
    void Drop(UIElement element, DragEventArgs e);
    bool IsHitTest(UIElement element, DragEventArgs e);
}

