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
using System.Windows.Input;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes;
public interface IRotatedRectShape : IShape
{
    double Angle { get; set; }
    Size Size { get; set; }
    Point Center { get; set; }

}
public class RotatedRectShape : TitleShapeBase, IRotatedRectShape
{
    public RotatedRectShape()
    {

    }
    public RotatedRectShape(Point center, Size size, double angle)
    {
        this.Center = center;
        this.Size = size;
        this.Angle = angle;
    }

    [Display(Name = "启用交线", GroupName = "样式")]
    public bool UseCross { get; set; } = false;

    [Display(Name = "启用角度", GroupName = "样式")]
    public bool UseAngle { get; set; } = true;

    [Display(Name = "启用标尺", GroupName = "样式")]
    public bool UseDimension { get; set; } = false;

    [Display(Name = "中心点", GroupName = "数据")]
    public Point Center { get; set; }

    [Display(Name = "尺寸", GroupName = "数据")]
    public Size Size { get; set; }

    [Display(Name = "旋转角度", GroupName = "数据")]
    public double Angle { get; set; }

    /// <summary>
    /// 获取在应用旋转前的轴对齐矩形
    /// </summary>
    private Rect GetRect()
    {
        return new Rect(new Point(Center.X - Size.Width / 2, Center.Y - Size.Height / 2), Size);
    }

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        if (this.Size.IsEmpty)
            return;

        Rect rect = this.GetRect();
        // 应用旋转变换
        drawingContext.PushTransform(new RotateTransform(this.Angle, this.Center.X, this.Center.Y));

        drawingContext.DrawRectangle(fill, pen, rect);
        this.DrawTitle(view, drawingContext, rect.TopLeft, pen.Brush, 10 / view.Scale, 10 / view.Scale);
        if (this.UseDimension)
        {
            this.DrawDimensionLine(view, drawingContext, rect.TopLeft, rect.TopRight, pen);
            this.DrawDimensionLine(view, drawingContext, rect.TopRight, rect.BottomRight, pen);
            //var topCenter = rect.GetTopCenter();
            //drawingContext.DrawTextAtBottomCenter(this.Size.Width.ToString("F2"), topCenter, pen.Brush, 10.0 / view.Scale);

            //var rightCenter = rect.GetRightCenter();
            //drawingContext.DrawTextAtLeft(this.Size.Height.ToString("F2"), rightCenter, pen.Brush, 10.0 / view.Scale);
        }

        if (this.UseCross)
        {
            this.DrawCross(view, drawingContext, rect.TopLeft, pen, 45);
            this.DrawCross(view, drawingContext, rect.BottomRight, pen, 45);
        }

        if (this.UseAngle)
            drawingContext.DrawArrowPolyLine(rect.GetLeftCenter(), rect.GetRightCenter(), pen.Brush, 2 / view.Scale, 20 / view.Scale);

        // 恢复变换
        drawingContext.Pop();

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
        if (!this.UseTitle)
            return;
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
        var rect = this.GetRect();
        var transform = new RotateTransform(this.Angle, this.Center.X, this.Center.Y);

        // 移动控制点
        yield return new ActionCircleHandle(x =>
        {
            this.Center = x;
        }, this.Center, this);

        // 四个角的缩放控制点
        Point[] corners = { rect.TopLeft, rect.TopRight, rect.BottomRight, rect.BottomLeft };
        for (int i = 0; i < 4; i++)
        {
            int cornerIndex = i;
            yield return new ActionHandle(p =>
            {
                Point transformedPoint = transform.Inverse.Transform(p);
                Point oppositeCorner = corners[(cornerIndex + 2) % 4];
                Vector diagonal = transformedPoint - oppositeCorner;
                Point newCenter = oppositeCorner + diagonal / 2;
                Size newSize = new Size(Math.Abs(diagonal.X), Math.Abs(diagonal.Y));

                // 保持中心点和尺寸同步更新
                this.Center = transform.Transform(newCenter);
                this.Size = newSize;

            }, transform.Transform(corners[cornerIndex]), this)
            { Cursor = cornerIndex % 2 == 0 ? Cursors.SizeNWSE : Cursors.SizeNESW };
        }

        // 旋转控制点
        var rotationHandlePos = new Point(this.Center.X, rect.Top - 20);
        yield return new ActionHandle(x =>
        {
            Vector v = x - this.Center;
            this.Angle = Vector.AngleBetween(new Vector(0, -1), v);
        }, transform.Transform(rotationHandlePos), this)
        { Cursor = Cursors.Hand };
    }

    public override bool Hit(IView view, Point point)
    {
        double l = this.GetStrokeThickness(2.0, view);
        var transform = new RotateTransform(-this.Angle, this.Center.X, this.Center.Y);
        // 将点击点进行反向旋转，然后判断是否在轴对齐的矩形内
        Point transformedPoint = transform.Transform(point);
        return transformedPoint.DistanceToRect(this.GetRect()) < l / view.Scale;
    }

    public Point[] GetPoints()
    {
        var angle = Angle * Math.PI / 180.0;
        var b = (float)Math.Cos(angle) * 0.5f;
        var a = (float)Math.Sin(angle) * 0.5f;

        var pt = new Point[4];
        pt[0].X = Center.X - a * Size.Height - b * Size.Width;
        pt[0].Y = Center.Y + b * Size.Height - a * Size.Width;
        pt[1].X = Center.X + a * Size.Height - b * Size.Width;
        pt[1].Y = Center.Y - b * Size.Height - a * Size.Width;
        pt[2].X = 2 * Center.X - pt[0].X;
        pt[2].Y = 2 * Center.Y - pt[0].Y;
        pt[3].X = 2 * Center.X - pt[1].X;
        pt[3].Y = 2 * Center.Y - pt[1].Y;
        return pt;
    }

    public Rect GetBoundingRect()
    {
        var pt = GetPoints();
        var r = new Rect
        {
            X = (int)Math.Floor(Math.Min(Math.Min(Math.Min(pt[0].X, pt[1].X), pt[2].X), pt[3].X)),
            Y = (int)Math.Floor(Math.Min(Math.Min(Math.Min(pt[0].Y, pt[1].Y), pt[2].Y), pt[3].Y)),
            Width = (int)Math.Ceiling(Math.Max(Math.Max(Math.Max(pt[0].X, pt[1].X), pt[2].X), pt[3].X)),
            Height = (int)Math.Ceiling(Math.Max(Math.Max(Math.Max(pt[0].Y, pt[1].Y), pt[2].Y), pt[3].Y))
        };
        r.Width -= r.X - 1;
        r.Height -= r.Y - 1;
        return r;
    }
}
