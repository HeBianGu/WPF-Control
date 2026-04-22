// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.Shapes.Handles;

public abstract class MoveToHandleBase : HandleBase, IMoveToHandle
{
    public MoveToHandleBase(Point postion, IShape shape) : base(postion, shape)
    {

    }

    public abstract void MoveTo(Point point);
}
