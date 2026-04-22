// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Handles;

namespace H.Controls.ShapeBox.Shapes.Base;
public abstract class FromToShapeBase : PreviewShapeBase, IFromToShape
{
    public FromToShapeBase()
    {

    }
    public FromToShapeBase(Point from, Point to)
    {
        this.From = from;
        this.To = to;
    }
    [Display(Name = "起始坐标", GroupName = "数据", Order = -1)]
    public Point From { get; set; }
    [Display(Name = "终止坐标", GroupName = "数据", Order = -1)]
    public Point To { get; set; }

    public double Angle => this.CalculateAngle(this.From.X, this.From.Y, this.To.X, this.To.Y);

    public double Length => (this.From - this.To).Length;

    public Point Center => this.From + (this.To - this.From) / 2;

    protected Matrix GetRotateAtFromMatrix()
    {
        Matrix matrix = new Matrix();
        matrix.RotateAt(-this.Angle, this.From.X, this.From.Y);
        return matrix;
    }

    protected override Matrix GetMatrix()
    {
        Matrix matrix = new Matrix();
        matrix.RotateAt(this.Angle, this.From.X, this.From.Y);
        return matrix;
    }

    protected override IEnumerable<IHandle> CreateHandles()
    {
        Matrix matrix = this.GetInvertMatrix();
        var normalToPoint = matrix.Transform(this.To);
        yield return new ActionHandle(x => this.From = x, this.From, this);
        yield return new ActionHandle(x => this.To = x, normalToPoint, this);
    }

    //public override IHandle HitIHandle(IView view, Point position)
    //{
    //    Matrix matrix = this.GetRotateAtFromMatrix();
    //    return base.HitIHandle(view, matrix.Transform(position));
    //}

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        if (this.From == this.To)
            return;
        Matrix matrix = this.GetInvertMatrix();
        var normalToPoint = matrix.Transform(this.To);
        this.MatrixDrawing(view, drawingContext, normalToPoint, pen, fill);
        base.MatrixDrawing(view, drawingContext, pen, fill);
    }
    public abstract void MatrixDrawing(IView view, DrawingContext drawingContext, Point normalToPoint, Pen pen, Brush fill = null);

    public override bool Hit(IView view, Point point)
    {
        double l = this.GetStrokeThickness(2.0, view);
        return point.DistanceToSegment(this.From, this.To) < l / view.Scale;
    }
}
