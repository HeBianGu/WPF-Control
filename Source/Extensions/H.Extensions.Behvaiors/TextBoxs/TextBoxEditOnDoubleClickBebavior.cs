// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Xml.Serialization;

namespace H.Extensions.Behvaiors.TextBoxs;

[Obsolete("输入中文有输入法是会自动LostFocus，需要再测试一下")]
public class TextBoxEditOnDoubleClickBebavior : Behavior<TextBox>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.Focusable = false;
        this.AssociatedObject.LostFocus += this.AssociatedObject_LostFocus;
        this.AssociatedObject.MouseDown += this.AssociatedObject_MouseDown;
        this.AssociatedObject.MouseLeave += this.AssociatedObject_MouseLeave;
    }

    private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
    {
        this.AssociatedObject.Focusable = false;
    }

    private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.AssociatedObject.Focusable = true;
    }

    private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
    {
        this.AssociatedObject.Focusable = false;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.LostFocus -= this.AssociatedObject_LostFocus;
        this.AssociatedObject.MouseDown -= this.AssociatedObject_MouseDown;
        this.AssociatedObject.MouseLeave -= this.AssociatedObject_MouseLeave;
    }
}
