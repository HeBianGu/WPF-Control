// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Controls.ShapeBox.Shapes.Handles;
public abstract class HandleBase : IHandle
{
    public HandleBase(Point postion, IShape shape)
    {
        this.Postion = postion;
        this.Owner = shape;
    }

    public IShape Owner { get; }

    public Point Postion { get; set; }
    public double Length { get; set; } = 6.0;
    public virtual Cursor Cursor { get; set; }

    public virtual void Draw(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        drawingContext.DrawHandle(this.Postion, pen.Brush, pen.Thickness, this.Length / view.Scale);
    }

    public virtual bool HitTestPoint(IView view, Point position)
    {
        var d = 0.5 * this.Length / view.Scale;
        var v = this.Postion - position;
        return Math.Abs(v.X) < d && Math.Abs(v.Y) < d;
    }
}
