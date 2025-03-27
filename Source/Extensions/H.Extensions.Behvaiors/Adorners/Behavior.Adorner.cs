// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


#if NET
#endif
using System.Windows;
using System.Windows.Documents;

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



