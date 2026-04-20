// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.FrameworkElements;

public class FrameworkElementClipCornerRadiusBehavior : Behavior<FrameworkElement>
{
    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FrameworkElementClipCornerRadiusBehavior), new FrameworkPropertyMetadata(default(CornerRadius), (d, e) =>
        {
            FrameworkElementClipCornerRadiusBehavior control = d as FrameworkElementClipCornerRadiusBehavior;

            if (control == null) return;

            if (e.OldValue is CornerRadius o)
            {

            }

            if (e.NewValue is CornerRadius n)
            {

            }
            control.UpdateClip();
        }));

    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.SizeChanged += this.AssociatedObject_SizeChanged;
    }

    private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        this.UpdateClip();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.SizeChanged -= this.AssociatedObject_SizeChanged;
    }

    private void UpdateClip()
    {
        if (this.CornerRadius == default(CornerRadius))
        {
            this.AssociatedObject.Clip = null;
            return;
        }
        Rect rect = new Rect(this.AssociatedObject.RenderSize);
        double radius = this.CornerRadius.TopLeft; // Assuming uniform corner radius
        this.AssociatedObject.Clip = new RectangleGeometry(rect, radius, radius);
    }
}
