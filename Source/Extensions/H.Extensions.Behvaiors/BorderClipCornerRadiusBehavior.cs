// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors;

public class BorderClipCornerRadiusBehavior : Behavior<Border>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.SizeChanged += this.AssociatedObject_SizeChanged;
        var descriptor = DependencyPropertyDescriptor.FromProperty(Border.CornerRadiusProperty, typeof(Border));
        descriptor.AddValueChanged(this.AssociatedObject, DependencyProperty_ValueChanged);
    }

    public void DependencyProperty_ValueChanged(object sender, EventArgs e)
    {
        this.UpdateClip();
    }

    private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        this.UpdateClip();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.SizeChanged -= this.AssociatedObject_SizeChanged;
        var descriptor = DependencyPropertyDescriptor.FromProperty(Border.CornerRadiusProperty, typeof(Border));
        descriptor.RemoveValueChanged(this.AssociatedObject, DependencyProperty_ValueChanged);
    }
    private void UpdateClip()
    {
        if (this.AssociatedObject.CornerRadius == default)
        {
            this.AssociatedObject.Clip = null;
            return;
        }
        Rect rect = new Rect(this.AssociatedObject.RenderSize);
        double radius = this.AssociatedObject.CornerRadius.TopLeft; // Assuming uniform corner radius
        this.AssociatedObject.Clip = new RectangleGeometry(rect, radius, radius);
    }
}
