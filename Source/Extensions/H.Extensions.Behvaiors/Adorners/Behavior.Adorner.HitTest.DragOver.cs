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

public class DragOverHitTestAdornerBehavior : HitTestAdornerBehavior
{
    public Type AdornerDropErrorType
    {
        get { return (Type)GetValue(AdornerDropErrorTypeProperty); }
        set { SetValue(AdornerDropErrorTypeProperty, value); }
    }

    public static readonly DependencyProperty AdornerDropErrorTypeProperty =
        DependencyProperty.Register("AdornerDropErrorType", typeof(Type), typeof(DragOverHitTestAdornerBehavior), new FrameworkPropertyMetadata(default(Type), (d, e) =>
        {
            DragOverHitTestAdornerBehavior control = d as DragOverHitTestAdornerBehavior;

            if (control == null) return;

            if (e.OldValue is Type o)
            {

            }

            if (e.NewValue is Type n)
            {

            }

        }));

    protected override void OnAttached()
    {
        this.AssociatedObject.DragOver += AssociatedObject_DragOver;
        this.AssociatedObject.Drop += AssociatedObject_Drop;
        //AssociatedObject.DragEnter += AssociatedObject_DragEnter;
        //AssociatedObject.DragLeave += AssociatedObject_DragLeave;
    }

    public static bool GetIsPreviewing(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsPreviewingProperty);
    }

    public static void SetIsPreviewing(DependencyObject obj, bool value)
    {
        obj.SetValue(IsPreviewingProperty, value);
    }

    public static readonly DependencyProperty IsPreviewingProperty =
        DependencyProperty.RegisterAttached("IsPreviewing", typeof(bool), typeof(DragOverHitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsPreviewingChanged));

    public static void OnIsPreviewingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }

    private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
    {
        //this.Clear();
    }

    private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
    {

    }

    protected virtual void Drop(object sender, DragEventArgs e)
    {
        if (_preVisualHitElement.GetContent() is IHitTestElementDrop drag)
        {
            if (drag.CanDrop(_preVisualHitElement, e))
                drag.Drop(_preVisualHitElement, e);
        }
        else if (this.AssociatedObject is IHitTestElementDrop drop)
        {
            if (drop.CanDrop(_preVisualHitElement, e))
                drop.Drop(_preVisualHitElement, e);
        }
    }

    private void AssociatedObject_Drop(object sender, DragEventArgs e)
    {
        Drop(sender, e);
        Clear();
    }

    private void AssociatedObject_DragOver(object sender, DragEventArgs e)
    {
        if (this.AdornerType == null)
            return;
        if (this.AdornerVisual == null)
            this.AdornerVisual = this.AssociatedObject;

        Point point = e.GetPosition(this.AssociatedObject);
        UIElement visualHit = this.AssociatedObject.HitTest<UIElement>(point, x =>
        {
            if (GetIsHitTest(x) == false)
                return false;
            if (_preVisualHitElement.GetContent() is IHitTestElementDrop drop)
                return drop.IsHitTest(x, e);
            return true;
        });
        if (visualHit == null)
        {
            Clear();
            if (this.AssociatedObject != _preVisualHitElement)
            {
                DragEnter(this.AssociatedObject, e);
                DragLeave(_preVisualHitElement, e);
            }
            _preVisualHitElement = this.AssociatedObject;
        }
        else
        {
            if (visualHit != _preVisualHitElement)
            {
                Clear();
                DragLeave(_preVisualHitElement, e);
                DragEnter(visualHit, e);
            }
            AddAdorner(visualHit);
            _preVisualHitElement = visualHit;
        }

        if (_preVisualHitElement.GetContent() is IHitTestElementDrag drag)
            drag.DragOver(_preVisualHitElement, e);
    }

    protected virtual void DragEnter(UIElement element, DragEventArgs e)
    {
        if (element.GetContent() is IHitTestElementDrag newDrag)
            newDrag.DragEnter(element, e);
    }

    protected virtual void DragLeave(UIElement element, DragEventArgs e)
    {
        if (_preVisualHitElement.GetContent() is IHitTestElementDrag oldDrag)
            oldDrag.DragLeave(_preVisualHitElement, e);
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.DragOver -= AssociatedObject_DragOver;
        this.AssociatedObject.Drop -= AssociatedObject_Drop;
        //AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
        //AssociatedObject.DragLeave -= AssociatedObject_DragLeave;
        Clear();
    }

    protected override bool CheckAdorner(UIElement elment)
    {
        return elment.GetAdorner(x => x.GetType() == this.AdornerType) == null;
    }

    protected override Adorner GetAdorner(UIElement elment)
    {
        Adorner adorner = Activator.CreateInstance(this.AdornerType, elment) as Adorner;
        object data = elment.GetDataContext();
        if (data is IHitTestElementDrag drag)
        {
            if (drag.CanDrop(elment, null))
                adorner = drag.GetDragAdorner(elment);
            else
                adorner = Activator.CreateInstance(this.AdornerDropErrorType, elment) as Adorner;
        }

        if (data is IGetDropAdorner gdrag)
            return gdrag.GetDropAdorner(elment);
        return adorner;
    }
}

