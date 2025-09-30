// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;

namespace H.Extensions.Behvaiors.ItemsControls;

public abstract class ButtonBehaviorBase : Behavior<Button>
{
    protected override void OnAttached()
    {
        this.AssociatedObject.Click += AssociatedObject_Click;
    }

    private void AssociatedObject_Click(object sender, RoutedEventArgs e)
    {
        OnClick();
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.Click -= AssociatedObject_Click;
    }

    protected abstract void OnClick();

    protected ItemsControl ItemsControl => this.AssociatedObject.GetParent<ItemsControl>();
    protected IList ItemsSource => this.ItemsControl?.ItemsSource as IList;
    protected object Item => this.AssociatedObject.DataContext;
}