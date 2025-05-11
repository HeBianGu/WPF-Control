// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.ContextMenus;

public class MouseOverContextMenuBehavior : Behavior<FrameworkElement>
{
    protected override void OnAttached()
    {
        this.AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
    }

    private void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
    {
        this.AssociatedObject.Dispatcher.BeginInvoke(() =>
        {
            this.AssociatedObject.ContextMenu.PlacementTarget = this.AssociatedObject;
            this.AssociatedObject.ContextMenu.IsOpen = true;
        });
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
    }
}
