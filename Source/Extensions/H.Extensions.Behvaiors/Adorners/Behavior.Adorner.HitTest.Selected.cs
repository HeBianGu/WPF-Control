// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

#if NET
#endif
using H.Common.Interfaces;
using H.Extensions.Common;

namespace H.Extensions.Behvaiors.Adorners;

public class SelectedHitTestAdornerBehavior : HitTestAdornerBehavior
{
    public ObservableCollection<object> Parameters { get; } = new ObservableCollection<object>();
    public UIElement SelectedElement
    {
        get { return (UIElement)GetValue(SelectedElementProperty); }
        set { SetValue(SelectedElementProperty, value); }
    }

    public static readonly DependencyProperty SelectedElementProperty =
        DependencyProperty.Register("SelectedElement", typeof(UIElement), typeof(SelectedHitTestAdornerBehavior), new FrameworkPropertyMetadata(default(UIElement), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
        {
            SelectedHitTestAdornerBehavior control = d as SelectedHitTestAdornerBehavior;
            if (control == null)
                return;
            if (e.OldValue is UIElement o)
            {
                SetIsSelected(o, false);
            }
            control.Clear();
            if (e.NewValue is UIElement n)
            {
                SetIsSelected(n, true);
                control.AddAdorner(n);
                control.OnSelectedChanged();
            }
            control._preVisualHitElement = e.NewValue as UIElement;
            control.OnSelectdElementChanged();
        }));



    protected virtual void OnSelectdElementChanged()
    {

    }

    public static bool GetIsSelected(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsSelectedProperty);
    }

    public static void SetIsSelected(DependencyObject obj, bool value)
    {
        obj.SetValue(IsSelectedProperty, value);
    }

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(SelectedHitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsSelectedChanged));

    static public void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        bool n = (bool)e.NewValue;
        bool o = (bool)e.OldValue;
    }

    protected override void OnAttached()
    {
        this.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown), true);
    }
    protected override void OnDetaching()
    {
        this.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown));
        this.Clear();
    }

    private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (this.AdornerType == null)
            return;
        if (this.AdornerVisual == null)
            this.AdornerVisual = this.AssociatedObject;
        Point point = e.GetPosition(this.AssociatedObject);
        this.SelectedElement = this.HitElement(point);
    }

    protected virtual UIElement HitElement(Point point)
    {
        return this.AssociatedObject.HitTest<UIElement>(point, x => GetIsHitTest(x));
    }

    public static readonly RoutedEvent SelectedChanged =
        EventManager.RegisterRoutedEvent(
            "SelectedChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(SelectedHitTestAdornerBehavior));

    public static void AddSelectedChangedHandler(DependencyObject d, RoutedEventHandler handler)
    {
        if (d is UIElement uiElement)
        {
            uiElement.AddHandler(SelectedChanged, handler);
        }
    }

    public static void RemoveSelectedChangedHandler(DependencyObject d, RoutedEventHandler handler)
    {
        if (d is UIElement uiElement)
        {
            uiElement.RemoveHandler(SelectedChanged, handler);
        }
    }

    public void OnSelectedChanged()
    {
        RoutedEventArgs args = new RoutedEventArgs(SelectedChanged, this.AssociatedObject);
        this.AssociatedObject.RaiseEvent(args);
    }
}

