// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.TreeViews;

public class TreeViewSelectNoneOnMouseDownBehavior : Behavior<TreeView>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
    }

    private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Point point = Mouse.GetPosition(this.AssociatedObject);
        var item = this.AssociatedObject.HitTest<TreeViewItem>(point);
        if (item == null)
            this.AssociatedObject.SelectNone();

    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;

    }
}

