// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Handles;
using H.Extensions.Common;

namespace H.Controls.ShapeBox.Shapes;


public interface ICircleShape : IBoundingBoxShape, ITitleShape
{
    Point Center { get; set; }
    double Radius { get; set; }
}

public class CircleShape : TitleShapeBase, ICircleShape
{
    public CircleShape()
    {
        this.UseTitle = false;
    }
    public CircleShape(Point center, double radius)
    {
        this.Center = center;
        this.Radius = radius;
        this.UseTitle = false;
    }

    [Display(Name = "中心坐标", GroupName = "数据")]
    public Point Center { get; set; }
    [Display(Name = "半径", GroupName = "数据")]
    public double Radius { get; set; }
    [Display(Name = "启用交线", GroupName = "样式")]
    public bool UseCross { get; set; } = false;
    [Display(Name = "启用标尺", GroupName = "样式")]
    public bool UseDimension { get; set; } = true;
    public string DimensionText { get; set; }
    public Rect BoundingBox => this.Center.ToRect(this.Radius);

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        if (this.Radius <= 0)
            return;
        drawingContext.DrawEllipse(fill, pen, this.Center, this.Radius, this.Radius);

        if (this.UseCross)
            this.DrawCross(view, drawingContext, this.Center, pen);
        if (this.UseDimension)
        {
            this.DrawDimensionLine(view, drawingContext, this.Center, this.Center + new Vector(this.Radius, 0), pen, PointStyleStype.Arrow, this.DimensionText);
        }
        else
        {
            if (this.UseCross)
                this.DrawCross(view, drawingContext, this.Center, pen);
        }
        if (this.UseTitle && !string.IsNullOrEmpty(this.Title))
            this.DrawTitle(view, drawingContext, pen, fill);
        base.MatrixDrawing(view, drawingContext, pen, fill);
    }

    public override void DrawTitle(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        drawingContext.DrawTextAtCenter(this.Title, this.Center, this.TitleForeground, 15 / view.Scale, this.TitleBackground);
    }


    protected override IEnumerable<IHandle> CreateHandles()
    {
        yield return new ActionHandle(x => this.Center = x, this.Center, this);
        yield return new ActionHandle(x => this.Radius = (x - this.Center).Length, new Point(this.Center.X + this.Radius, this.Center.Y), this);
    }

    public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
    {
        var r = 10.0 / view.Scale;
        var center = new Point(r, r);
        drawingContext.DrawEllipse(null, new Pen(stroke, strokeThickness), center, r, r);
        this.DrawPoint(view, drawingContext, center, fill, strokeThickness);
        this.DrawPoint(view, drawingContext, center + new Vector(r, 0), fill, strokeThickness);
    }

    public override bool Hit(IView view, Point point)
    {
        double l = this.GetStrokeThickness(2.0, view);
        return point.DistanceToCircle(this.Center, this.Radius) < l / view.Scale;
    }
}
