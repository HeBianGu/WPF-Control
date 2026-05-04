// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Handles;

namespace H.Controls.ShapeBox.Shapes.Base;
public interface IHandleShape : IShape
{
    bool UseHandle { get; set; }

    IHandle HitIHandle(IView view, Point position);
}

public abstract class HandleShapeBase : SelectableShapeBase, IHandleShape
{
    //protected HandleShapeBase()
    //{
    //    this.Handles = this.CreateHandles().ToList();
    //}
    public bool UseHandle { get; set; }

    public override void MatrixDrawing(IView view, DrawingContext dc, Pen pen, Brush fill = null)
    {
        if (this.CanHandle(view))
            this.DrawHandle(view, dc, pen, fill);
    }
    public virtual void DrawHandle(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        foreach (var item in this.CreateHandles())
        {
            item.Draw(view, drawingContext, pen, fill);
        }
    }
    protected virtual bool CanHandle(IView view)
    {
        if (!this.UseHandle)
            return false;
        if (this is IBoundingBoxShape boundingBox)
        {
            if (boundingBox.BoundingBox.Width.ToView(view) < this.GetHandleViewSize() 
                && boundingBox.BoundingBox.Height.ToView(view) < this.GetHandleViewSize())
                return false;
        }
        return true;
    }

    protected virtual double GetHandleViewSize()
    {
        return 50;
    }


    //public List<IHandle> Handles { get; set; }

    protected virtual IEnumerable<IHandle> CreateHandles()
    {
        return Enumerable.Empty<IHandle>();
    }

    public virtual IHandle HitIHandle(IView view, Point position)
    {
        if (this.CanHandle(view) == false)
            return null;
        position = this.GetInvertMatrix().Transform(position);
        foreach (var handle in this.CreateHandles())
        {
            if (handle.HitTestPoint(view, position))
                return handle;
        }
        return null;
    }
}
