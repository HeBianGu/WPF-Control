// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Controls.ShapeBox.Shapes.Handles;
public abstract class MouseClickHandle : HandleBase, IMouseClickHandle
{
    public MouseClickHandle(Point postion, IShape shape) : base(postion, shape)
    {

    }
    public virtual void MouseDown(object sender, MouseButtonEventArgs e)
    {

    }
    public virtual void MouseUp(object sender, MouseButtonEventArgs e)
    {

    }
}
