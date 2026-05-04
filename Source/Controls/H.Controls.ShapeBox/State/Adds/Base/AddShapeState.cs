// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Handles;
using H.Extensions.Mvvm.Commands;

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

    //protected virtual bool CanSumit()
    //{
    //    return true;
    //}

    //public virtual bool CanCancel()
    //{
    //    return true;
    //}


    //[Icon(FontIcons.Accept)]
    //[Display(Name = "提交", GroupName = "右键菜单", Description = "提交当前操作")]
    //public DisplayCommand SumitCommand => new DisplayCommand(x =>
    //{
    //    this.Sumit();
    //}, x => this.CanSumit());

    //[Icon(FontIcons.Back)]
    //[Display(Name = "取消", GroupName = "右键菜单", Description = "取消当前操作")]
    //public DisplayCommand CancelCommand => new DisplayCommand(x =>
    //{
    //    this.Cancel();
    //}, x => this.CanCancel());

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

    protected override IEnumerable<IShape> GetShapes()
    {
        if (this._shapes == null)
            return Enumerable.Empty<IShape>();
        return this._shapes.Shapes;
    }
}
