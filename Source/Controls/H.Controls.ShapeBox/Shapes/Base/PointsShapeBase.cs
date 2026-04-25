// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Shapes;

namespace H.Controls.ShapeBox.Shapes.Base;

[TypeConverter(typeof(PointsTypeConverter))]
public class Points : ObservableCollection<Point>
{
    public Points() : base()
    {
    }
    public Points(IEnumerable<Point> collection) : base(collection)
    {
    }

    public override string ToString()
    {
        return string.Join(";", this.Select(p => $"{p.X},{p.Y}"));
    }

    public static Points Parse(string s)
    {
        var points = s.Split(';').Select(x =>
        {
            var xy = x.Split(',');
            return new Point(double.Parse(xy[0]), double.Parse(xy[1]));
        });
        return new Points(points);
    }
}

public class PointsTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
            return true;
        return base.CanConvertFrom(context, sourceType);
    }
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string s)
        {
            var points = s.Split(';').Select(x =>
            {
                var xy = x.Split(',');
                return new Point(double.Parse(xy[0]), double.Parse(xy[1]));
            });
            return new Points(points);
        }
        return base.ConvertFrom(context, culture, value);
    }
}

public interface IPointsShape : IBoundingBoxShape
{
    Points Points { get; set; }
}

public abstract class PointsShapeBase : TitleShapeBase, IPointsShape
{
    protected PointsShapeBase()
    {
        this.Points = new Points();
    }
    protected PointsShapeBase(IEnumerable<Point> points)
    {
        this.Points = new Points(points);
    }
    [Display(Name = "坐标列表", GroupName = "数据", Order = -1)]
    public Points Points { get; set; }
    [Display(Name = "启用交线", GroupName = "样式")]
    public bool UseCross { get; set; } = false;

    public Rect BoundingBox
    {
        get
        {
            if (this.Points.Count == 0)
                return new Rect(0, 0, 0, 0);
            var minx = this.Points.Min(x => x.X);
            var miny = this.Points.Min(x => x.Y);
            return new Rect(minx, miny, this.Points.Max(x => x.X) - minx, this.Points.Max(x => x.Y) - miny);
        }
    }

    public double Area
    {
        get
        {
            return this.Points.ToGeometry(true, true).GetArea();
        }
    }

    public virtual void DrawingCross(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        if (this.Points == null || this.Points.Count == 0)
            return;
        foreach (var item in this.Points)
        {
            this.DrawCross(view, drawingContext, item, pen);
        }
    }

    public override void MatrixDrawing(IView view, DrawingContext dc, Pen pen, Brush fill = null)
    {
        if (this.UseCross)
            this.DrawingCross(view, dc, pen, fill);
        base.MatrixDrawing(view, dc, pen, fill);
    }

    //protected override IEnumerable<IHandle> CreateHandles()
    //{
    //    Matrix matrix = this.GetInvertMatrix();
    //    var normalToPoint = matrix.Transform(this.To);
    //    yield return new ActionHandle(x => this.From = x, this.From);
    //    yield return new ActionHandle(x => this.To = x, normalToPoint);
    //}

    public override bool Hit(IView view, Point point)
    {
        double l = this.GetStrokeThickness(2.0, view);
        return point.DistanceToPoints(this.Points) < l / view.Scale;
    }
}
