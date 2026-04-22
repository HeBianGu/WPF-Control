// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Handles;
using System.Windows.Input;

namespace H.Controls.ShapeBox.State.Base;
public abstract class HandleShapeStateBase : PreviewShapeStateBase
{
    private IHandle _hitHandle;
    private IHandle _mouseOverHandle;
    protected IHandle CurrentHitHandle => this._hitHandle;
    public override void MouseDown(object sender, MouseButtonEventArgs e)
    {
        base.MouseDown(sender, e);
        var position = e.GetPosition(this.GetElementView());
        this._hitHandle = this.HitHandle(position);
        if (this._hitHandle != null)
        {
            e.Handled = true;

            if (this._hitHandle is IMouseClickHandle handle)
                handle.MouseDown(sender, e);
            if (this._hitHandle is IDeleteHandle deleteHandle)
                this.OnClickDeleteHandle(deleteHandle);
        }
    }

    protected override void UpdateCursor(QueryCursorEventArgs e)
    {
        if (this._hitHandle != null)
        {
            e.Cursor = this._hitHandle.Cursor ?? this.Cursor;
            return;
        }
        if (this._mouseOverHandle != null)
        {
            e.Cursor = this._mouseOverHandle.Cursor ?? this.Cursor;
            return;
        }
        base.UpdateCursor(e);
    }

    protected virtual void OnClickDeleteHandle(IDeleteHandle deleteHandle)
    {

    }

    public override void MouseLeave(object sender, MouseEventArgs e)
    {
        base.MouseLeave(sender, e);
        this.ClearHitHande();
    }

    public override void MouseUp(object sender, MouseButtonEventArgs e)
    {
        base.MouseUp(sender, e);
        if (this._hitHandle is IMouseClickHandle handle)
            handle.MouseUp(sender, e);
        this.ClearHitHande();
    }

    public override void MouseMove(object sender, MouseEventArgs e)
    {
        base.MouseMove(sender, e);
        var position = e.GetPosition(this.GetElementView());
        this._mouseOverHandle = this.HitHandle(position);
        if (this._hitHandle == null)
            return;
        if (this._hitHandle is IMoveToHandle handle)
        {
            handle.MoveTo(position);
            this.OnHitHandleMoved();
        }
    }

    protected virtual void OnHitHandleMoved()
    {

    }

    protected virtual void ClearHitHande()
    {
        this._hitHandle = null;
    }

    protected override void Clear()
    {
        base.Clear();
        this.ClearHitHande();
    }
    public virtual IHandle HitHandle(Point point)
    {
        return null;
    }
}
