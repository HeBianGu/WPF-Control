// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Controls.ShapeBox.Shapes.Handles;
public class ActionHandle : MoveToHandleBase
{
    private Action<Point> _moveToAction;
    public ActionHandle(Action<Point> moveToAction, Point postion, IShape shape) : base(postion, shape)
    {
        this._moveToAction = moveToAction;
        this.Cursor = Cursors.Hand;
    }

    public override void MoveTo(Point point)
    {
        this._moveToAction?.Invoke(point);
    }
}
