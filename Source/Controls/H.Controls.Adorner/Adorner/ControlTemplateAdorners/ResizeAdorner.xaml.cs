// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Adorner.Adorner.ControlTemplateAdorners;

public class ResizeAdorner : ControlTemplateAdorner
{
    public static ComponentResourceKey TemplateDefaultKey => new ComponentResourceKey(typeof(ResizeAdorner), "S.ResizeAdorner.Template.Default");

    static ResizeAdorner()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeAdorner), new FrameworkPropertyMetadata(typeof(ResizeAdorner)));
    }

    protected virtual ControlTemplate CreateDefaultTemplate()
    {
        return Application.Current.FindResource(TemplateDefaultKey) as ControlTemplate;
    }

    private Thumb thumb;
    public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
    {
        if (this._contentControl.Template == null)
            this._contentControl.Template = this.CreateDefaultTemplate();

        this._contentControl.ApplyTemplate();

        var handleThumbStyle = ResizeAdorner.GetHanldeThumbStyle(adornedElement);
        var moveThumbStyle = ResizeAdorner.GetMoveThumbStyle(adornedElement);

        {
            Thumb thumb = this._contentControl.Template.FindName("PART_Move", this._contentControl) as Thumb;
            if (moveThumbStyle != null)
                thumb.Style = moveThumbStyle;
            if (thumb != null)
                thumb.DragDelta += (s, e) =>
                {
                    FrameworkElement element = this.AdornedElement as FrameworkElement;
                    if (element == null)
                        return;
                    this.DragMoveHorizontal(e.HorizontalChange);
                    this.DragMoveVertical(e.VerticalChange);
                    //  Do ：触发附加事件
                    ResizeAdorner.RaiseResizedEvent(element);
                };
        }


        {
            Thumb thumb = this._contentControl.Template.FindName("PART_LeftTop", this._contentControl) as Thumb;
            if (thumb != null)
                thumb.DragDelta += Thumb_DragDelta;
            if (handleThumbStyle != null)
                thumb.Style = handleThumbStyle;
        }
        {
            Thumb thumb = this._contentControl.Template.FindName("PART_LeftCenter", this._contentControl) as Thumb;
            if (thumb != null)
                thumb.DragDelta += Thumb_DragDelta;
            if (handleThumbStyle != null)
                thumb.Style = handleThumbStyle;
        }
        {
            Thumb thumb = this._contentControl.Template.FindName("PART_LeftBottom", this._contentControl) as Thumb;
            if (thumb != null)
                thumb.DragDelta += Thumb_DragDelta;
            if (handleThumbStyle != null)
                thumb.Style = handleThumbStyle;
        }
        {
            Thumb thumb = this._contentControl.Template.FindName("PART_RightTop", this._contentControl) as Thumb;
            if (thumb != null)
                thumb.DragDelta += Thumb_DragDelta;
            if (handleThumbStyle != null)
                thumb.Style = handleThumbStyle;
        }
        {
            Thumb thumb = this._contentControl.Template.FindName("PART_RightCenter", this._contentControl) as Thumb;
            if (thumb != null)
                thumb.DragDelta += Thumb_DragDelta;
            if (handleThumbStyle != null)
                thumb.Style = handleThumbStyle;
        }
        {
            Thumb thumb = this._contentControl.Template.FindName("PART_RightBottom", this._contentControl) as Thumb;
            if (thumb != null)
                thumb.DragDelta += Thumb_DragDelta;
            if (handleThumbStyle != null)
                thumb.Style = handleThumbStyle;
        }

        {
            Thumb thumb = this._contentControl.Template.FindName("PART_RightTop", this._contentControl) as Thumb;
            if (thumb != null)
                thumb.DragDelta += Thumb_DragDelta;
            if (handleThumbStyle != null)
                thumb.Style = handleThumbStyle;
        }
        {
            Thumb thumb = this._contentControl.Template.FindName("PART_CenterTop", this._contentControl) as Thumb;
            if (thumb != null)
                thumb.DragDelta += Thumb_DragDelta;
            if (handleThumbStyle != null)
                thumb.Style = handleThumbStyle;
        }
        {
            Thumb thumb = this._contentControl.Template.FindName("PART_CenterBottom", this._contentControl) as Thumb;
            if (thumb != null)
                thumb.DragDelta += Thumb_DragDelta;
            if (handleThumbStyle != null)
                thumb.Style = handleThumbStyle;
        }
    }

    private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        Thumb thumb = (Thumb)sender;

        double ElementMiniSize = thumb.DesiredSize.Width;

        FrameworkElement element = this.AdornedElement as FrameworkElement;
        if (element == null) return;
        Resize(element);

        switch (thumb.VerticalAlignment)
        {
            case VerticalAlignment.Bottom:
                if (element.Height + e.VerticalChange > ElementMiniSize)
                    this.SetHeight(e.VerticalChange);
                break;
            case VerticalAlignment.Top:
                if (element.Height - e.VerticalChange > ElementMiniSize)
                {
                    this.SetHeight(-e.VerticalChange);
                    this.SetTop(e.VerticalChange);
                }
                break;
        }
        switch (thumb.HorizontalAlignment)
        {
            case HorizontalAlignment.Left:
                if (element.Width - e.HorizontalChange > ElementMiniSize)
                {
                    this.SetLeft(e.HorizontalChange);
                    this.SetWidth(-e.HorizontalChange);
                }
                break;
            case HorizontalAlignment.Right:
                if (element.Width + e.HorizontalChange > ElementMiniSize)
                    this.SetWidth(e.HorizontalChange);
                break;
        }
        e.Handled = true;

        //  Do ：触发附加事件
        ResizeAdorner.RaiseResizedEvent(element);
    }

    protected virtual void SetLeft(double change)
    {
        if (Canvas.GetLeft(this.AdornedElement) == double.NaN)
            Canvas.SetLeft(this.AdornedElement, 0);
        Canvas.SetLeft(this.AdornedElement, Math.Max(Canvas.GetLeft(this.AdornedElement) + change, 0));
    }
    protected virtual void SetTop(double change)
    {
        if (Canvas.GetTop(this.AdornedElement) == double.NaN)
            Canvas.SetTop(this.AdornedElement, 0);
        Canvas.SetTop(this.AdornedElement, Math.Max(Canvas.GetTop(this.AdornedElement) + change, 0));
    }

    protected virtual void DragMoveHorizontal(double change)
    {
        Canvas canvas = this.AdornedElement.GetParent<Canvas>();
        if (canvas == null) return;

        if (Canvas.GetLeft(this.AdornedElement) + change < 0)
        {
            Canvas.SetLeft(this.AdornedElement, 0);
            return;
        }
        if (Canvas.GetLeft(this.AdornedElement) + this.AdornedElement.RenderSize.Width + change > canvas.RenderSize.Width)
        {
            Canvas.SetLeft(this.AdornedElement, canvas.RenderSize.Width - this.AdornedElement.RenderSize.Width);
            return;
        }

        Canvas.SetLeft(this.AdornedElement, Canvas.GetLeft(this.AdornedElement) + change);
    }
    protected virtual void DragMoveVertical(double change)
    {
        Canvas canvas = this.AdornedElement.GetParent<Canvas>();

        if (canvas == null) return;
        if (Canvas.GetTop(this.AdornedElement) + change < 0)
        {
            Canvas.SetTop(this.AdornedElement, 0);
            return;
        }
        if (Canvas.GetTop(this.AdornedElement) + this.AdornedElement.RenderSize.Height + change > canvas.RenderSize.Height)
        {
            Canvas.SetTop(this.AdornedElement, canvas.RenderSize.Height - this.AdornedElement.RenderSize.Height);
            return;
        }

        Canvas.SetTop(this.AdornedElement, Canvas.GetTop(this.AdornedElement) + change);
    }

    public double MinValue { get; set; } = 10;
    protected virtual void SetHeight(double change)
    {
        FrameworkElement element = this.AdornedElement as FrameworkElement;
        element.Height = Math.Max(this.MinValue, element.Height + change);
    }
    protected virtual void SetWidth(double change)
    {
        FrameworkElement element = this.AdornedElement as FrameworkElement;
        element.Width = Math.Max(this.MinValue, element.Width + change);
    }

    private void Resize(FrameworkElement fElement)
    {
        if (double.IsNaN(fElement.Width))
            fElement.Width = fElement.RenderSize.Width;
        if (double.IsNaN(fElement.Height))
            fElement.Height = fElement.RenderSize.Height;

    }

    protected override Size MeasureOverride(Size constraint)
    {
        base.MeasureOverride(constraint);
        return this.AdornedElement.DesiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        base.ArrangeOverride(finalSize);
        this._contentControl.Arrange(new Rect(new Point(0, 0), this.AdornedElement.DesiredSize));
        return finalSize;
    }

    public static readonly RoutedEvent ResizedEvent =
        EventManager.RegisterRoutedEvent(
            "Resized",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(ResizeAdorner));

    public static void AddResizedHandler(DependencyObject d, RoutedEventHandler handler)
    {
        if (d is UIElement uiElement)
        {
            uiElement.AddHandler(ResizedEvent, handler);
        }
    }

    public static void RemoveResizedHandler(DependencyObject d, RoutedEventHandler handler)
    {
        if (d is UIElement uiElement)
        {
            uiElement.RemoveHandler(ResizedEvent, handler);
        }
    }
    public static void RaiseResizedEvent(UIElement source)
    {
        source.RaiseEvent(new RoutedEventArgs(ResizedEvent, source));
    }



    public static Style GetHanldeThumbStyle(DependencyObject obj)
    {
        return (Style)obj.GetValue(HanldeThumbStyleProperty);
    }

    public static void SetHanldeThumbStyle(DependencyObject obj, Style value)
    {
        obj.SetValue(HanldeThumbStyleProperty, value);
    }

    /// <summary> 应用窗体关闭和显示 </summary>
    public static readonly DependencyProperty HanldeThumbStyleProperty =
        DependencyProperty.RegisterAttached("HanldeThumbStyle", typeof(Style), typeof(ResizeAdorner), new PropertyMetadata(default(Style), OnHanldeThumbStyleChanged));

    static public void OnHanldeThumbStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        Style n = (Style)e.NewValue;

        Style o = (Style)e.OldValue;
    }

    public static Style GetMoveThumbStyle(DependencyObject obj)
    {
        return (Style)obj.GetValue(MoveThumbStyleProperty);
    }

    public static void SetMoveThumbStyle(DependencyObject obj, Style value)
    {
        obj.SetValue(MoveThumbStyleProperty, value);
    }

    /// <summary> 应用窗体关闭和显示 </summary>
    public static readonly DependencyProperty MoveThumbStyleProperty =
        DependencyProperty.RegisterAttached("MoveThumbStyle", typeof(Style), typeof(ResizeAdorner), new PropertyMetadata(default(Style), OnMoveThumbStyleChanged));

    static public void OnMoveThumbStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        Style n = (Style)e.NewValue;

        Style o = (Style)e.OldValue;
    }

}
