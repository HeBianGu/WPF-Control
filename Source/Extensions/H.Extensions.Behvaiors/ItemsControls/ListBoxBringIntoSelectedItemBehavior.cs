// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.ItemsControls;

public class ListBoxBringIntoSelectedItemBehavior : Behavior<ListBox>
{
    protected override void OnAttached()
    {
        this.AssociatedObject.SelectionChanged += this.AssociatedObject_SelectionChanged;
        this.AssociatedObject.Loaded += this.AssociatedObject_Loaded;
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        this.AssociatedObject.ScrollIntoView(this.AssociatedObject.SelectedItem);
    }

    private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        this.AssociatedObject.ScrollIntoView(this.AssociatedObject.SelectedItem);
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
    }
}
