// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Handles;
using H.Extensions.Common;
using H.Extensions.TypeConverter;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace H.Controls.ShapeBox.Shapes;
public interface IRectShape : IShape
{
    Rect Rect { get; set; }
}
public class RectShape : TitleShapeBase, IRectShape, IBoundingBoxShape
{
    public RectShape()
    {

    }
    public RectShape(Rect rect)
    {
        this.Rect = rect;
    }

    [Display(Name = "启用交线", GroupName = "样式")]
    public bool UseCross { get; set; } = false;
    [Display(Name = "启用标尺", GroupName = "样式")]
    public bool UseDimension { get; set; } = false;

    [TypeConverter(typeof(Round2RectConverter))]
    [Display(Name = "矩形范围", GroupName = "数据")]
    public Rect Rect { get; set; }

    Rect IBoundingBoxShape.BoundingBox => this.Rect;

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        if (this.Rect.IsZoreOrEmpty())
            return;
        drawingContext.DrawRectangle(fill, pen, Rect);
        this.DrawTitle(view, drawingContext, this.Rect.TopLeft, pen.Brush, 10 / view.Scale, 10 / view.Scale);
        if (this.UseDimension)
        {
            //var topCenter = this.Rect.GetTopCenter();
            //drawingContext.DrawTextAtBottomCenter(this.Rect.Width.ToString("F2"), topCenter, pen.Brush, 10.0 / view.Scale);

            //var rightCenter = this.Rect.GetRightCenter();
            //drawingContext.DrawTextAtLeft(this.Rect.Width.ToString("F2"), rightCenter, pen.Brush, 10.0 / view.Scale);

            this.DrawDimensionLine(view, drawingContext, this.Rect.TopLeft, this.Rect.TopRight, pen);
            this.DrawDimensionLine(view, drawingContext, this.Rect.TopRight, this.Rect.BottomRight, pen);
        }

        if (this.UseCross)
        {
            this.DrawCross(view, drawingContext, this.Rect.TopLeft, pen, 45);
            this.DrawCross(view, drawingContext, this.Rect.BottomRight, pen, 45);
        }
        base.MatrixDrawing(view, drawingContext, pen, fill);
    }

    public override void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
    {
        var r = 10.0 / view.Scale;
        var rect = new Rect(new Point(0, 0), new Point(r * 2, r));
        drawingContext.DrawRectangle(null, new Pen(stroke, strokeThickness), rect);
        this.DrawPoint(view, drawingContext, rect.TopLeft, fill, strokeThickness);
        this.DrawPoint(view, drawingContext, rect.BottomRight, fill, strokeThickness);
    }

    public override void DrawTitle(IView view, DrawingContext drawingContext, Point point, Brush brush, double fontsize = 10, double offset = 5)
    {
        if (string.IsNullOrEmpty(this.Title))
            return;

        var l = 4 / view.Scale;
        point.Offset(l / 2, -l);
        drawingContext.DrawTextAtTopLeft(this.Title, point, this.TitleForeground, fontsize, null, (f, r) =>
        {
            var fill = this.GetTitleBackground(brush);
            var padding = 2 / view.Scale;
            var rect = r.GetPadding(l / 2);
            drawingContext.DrawRoundedRectangle(fill, null, rect, l / 2, l / 2);
        });
    }

    protected override IEnumerable<IHandle> CreateHandles()
    {
        yield return new ActionCircleHandle(x =>
        {
            Vector vector = x - this.Rect.GetCenter();
            this.Rect = new Rect(this.Rect.TopLeft + vector, this.Rect.BottomRight + vector);
        }, this.Rect.GetCenter(), this);
        yield return new ActionHandle(x => this.Rect = new Rect(x, this.Rect.BottomRight), this.Rect.TopLeft, this) { Cursor = Cursors.ScrollAll };
        yield return new ActionHandle(x => this.Rect = new Rect(this.Rect.TopLeft, x), this.Rect.BottomRight, this) { Cursor = Cursors.ScrollAll };
    }

    public override bool Hit(IView view, Point point)
    {
        double l = this.GetStrokeThickness(2.0, view);
        return point.DistanceToRect(this.Rect) < l / view.Scale;
    }
}
