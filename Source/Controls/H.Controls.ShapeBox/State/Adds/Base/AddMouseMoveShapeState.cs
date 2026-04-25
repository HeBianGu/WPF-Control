// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Controls.ShapeBox.State.Adds.Base;

public abstract class AddMouseMoveShapeState<T> : AddShapeState<T> where T : IShape
{
    public AddMouseMoveShapeState()
    {

    }

    public AddMouseMoveShapeState(IShapes shapes) : base(shapes)
    {

    }

    public override void MouseDown(object sender, MouseButtonEventArgs e)
    {
        base.MouseDown(sender, e);
    }

    public override void MouseUp(object sender, MouseButtonEventArgs e)
    {
        base.MouseUp(sender, e);
        this.Sumit();
    }

    protected override void Sumit()
    {
        if (this._clickPoints.Count == 0)
            return;
        base.Sumit();
    }

    public override void MouseMove(object sender, MouseEventArgs e)
    {
        base.MouseMove(sender, e);
        if (e.LeftButton == MouseButtonState.Released)
            return;
        var p = e.GetPosition(this.GetElementView());
        this._clickPoints.Enqueue(p);
    }
}
