// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Handles;
using System.Windows.Input;

namespace H.Controls.ShapeBox.State.Adds.Base;
public abstract class AddShapeState<T> : ShapeState<T> where T : IShape
{
    private readonly IShapes _shapes;
    public AddShapeState()
    {

    }

    public AddShapeState(IShapes shapes)
    {
        this._shapes = shapes;
    }

    protected IShapes Shapes => this._shapes;
    public Queue<Point> _clickPoints = new Queue<Point>();
    public override void MouseDown(object sender, MouseButtonEventArgs e)
    {
        base.MouseDown(sender, e);

        if (this.CurrentHitHandle != null)
            return;
        if (e.ChangedButton == MouseButton.Right)
        {
            this.Cancel();
            return;
        }
        if (e.ChangedButton != MouseButton.Left)
            return;
        var p = e.GetPosition(this.GetElementView());
        this._clickPoints.Enqueue(p);
        this.OnClick(this._clickPoints);
    }

    public override void MouseMove(object sender, MouseEventArgs e)
    {
        base.MouseMove(sender, e);
        if (this.CurrentHitHandle != null)
            return;
        if (this._clickPoints == null)
            return;
        var p = e.GetPosition(sender as FrameworkElement);
        this.OnPreviewMouseMove(p);
    }

    protected abstract void OnClick(Queue<Point> points);

    protected abstract void OnPreviewMouseMove(Point p);

    protected virtual void Sumit()
    {
        this.AddShape();
        this.Clear();
        this.ClearStateShape();
    }

    public virtual void Cancel()
    {
        this.Clear();
        this.ClearStateShape();
    }

    protected override void Clear()
    {
        base.Clear();
        this._clickPoints.Clear();
    }

    protected virtual void AddShape()
    {
        if (this._shapes == null)
            return;
        this._shapes.AddShapes(this.Shape);
        this.Shape = this.CreateShape();
    }

    public override IHandle HitHandle(Point point)
    {
        foreach (var handleShape in this.GetShapes().OfType<IHandleShape>())
        {
            var handle = handleShape.HitIHandle(this.View, point);
            if (handle != null)
                return handle;
        }
        return null;
    }

    protected override void OnClickDeleteHandle(IDeleteHandle deleteHandle)
    {
        if (this._shapes == null)
            return;
        base.OnClickDeleteHandle(deleteHandle);
        this.Shapes.DeleteShapes(deleteHandle.Owner);
    }

    protected virtual IEnumerable<IShape> GetShapes()
    {
        if (this._shapes == null)
            return Enumerable.Empty<IShape>();
        return this._shapes.Shapes;
    }
}
