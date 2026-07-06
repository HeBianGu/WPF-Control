// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.ShapeBox.Shapes;

namespace H.Components.VisionDiagram.NodeDatas.Measures;
public abstract class LineToLineAngleMeasureNodeDataBase<T> : FromToMeasureNodeDataBase<T, VisionLine, VisionLine>
    where T : class, IVisionImage
{

    private IExpressionKey _From;
    [GetMethodNameSource(nameof(GetFromNodeDataExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "选择输入线1", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
    public IExpressionKey From
    {
        get { return _From; }
        set
        {
            _From = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private IExpressionKey _To;
    [GetMethodNameSource(nameof(GetToNodeDataExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "选择输入线2", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
    public IExpressionKey To
    {
        get { return _To; }
        set
        {
            _To = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private LineAngleType _LineAngleType = LineAngleType.Smallest;

    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "角度模式", GroupName = VisionTabNames.RunParameters, Description = "设置两条线的夹角计算模式")]
    public LineAngleType LineAngleType
    {
        get { return _LineAngleType; }
        set
        {
            _LineAngleType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _Angle;

    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "夹角(°)", GroupName = VisionTabNames.ResultParameters, Description = "两条线段的夹角（单位：度）")]
    public double Angle
    {
        get { return _Angle; }
        set
        {
            _Angle = value;
            RaisePropertyChanged();
        }
    }

    private LineLineRelation _LineLineRelation;

    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "线线关系", GroupName = VisionTabNames.ResultParameters, Description = "计算两线段的关系结果")]
    public LineLineRelation LineLineRelation
    {
        get { return _LineLineRelation; }
        set
        {
            _LineLineRelation = value;
            RaisePropertyChanged();
        }
    }

    protected override IExpressionKey GetFromExpression() => this.From;
    protected override IExpressionKey GetToExpression() => this.To;
    protected override FlowableResult<T> Invoke(T fromImage)
    {
        VisionLine fromLine = this.FromValue;
        VisionLine toLine = this.ToValue;

        this.ResultShapes.Add(new LineShape(fromLine.Start, fromLine.End));
        this.ResultShapes.Add(new LineShape(toLine.Start, toLine.End));

        var result = LineLineAngleGeometry.CalculateAngle(fromLine.Start, fromLine.End, toLine.Start, toLine.End, this.LineAngleType);

        this.Angle = result.angleDegrees;
        this.MeasureResult = result.angleDegrees;
        this.LineLineRelation = result.relation;

        // 顶点：优先取线段交点；若线段不相交则取延长线交点（平行/共线则回退为两线段中点的中点）
        Point vertex = SegmentSegmentGeometry.DoSegmentsIntersect(fromLine.Start, fromLine.End, toLine.Start, toLine.End)
            ? SegmentSegmentGeometry.FindIntersection(fromLine.Start, fromLine.End, toLine.Start, toLine.End)
            : LineLineAngleGeometry.FindInfiniteLineIntersectionOrFallback(fromLine.Start, fromLine.End, toLine.Start, toLine.End);

        AngleShape angleShape = new AngleShape(vertex, vertex + result.dir1, vertex + result.dir2);
        angleShape.Text = $"{this.Angle:0.###}°";
        angleShape.Stroke = this.DistanceStroke;
        this.ResultShapes.Add(angleShape);
        var resultImage = this.GetExpressionResultImage(fromImage).Clone();
        return this.OK(resultImage as T, angleShape.ToAutoResultPresenter("线线夹角"));
    }
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum LineAngleType
{
    [Display(Name = "最小夹角(0~90)")]
    Smallest,

    [Display(Name = "有向夹角(0~180)")]
    Directed
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum LineLineRelation
{
    [Display(Name = "无")]
    None,

    [Display(Name = "平行")]
    Parallel,

    [Display(Name = "共线")]
    Collinear,

    [Display(Name = "相交")]
    Intersecting
}

public static class LineLineAngleGeometry
{
    public static (double angleDegrees, LineLineRelation relation, Point vertex, Vector dir1, Vector dir2) CalculateAngle(
        Point seg1Start,
        Point seg1End,
        Point seg2Start,
        Point seg2End,
        LineAngleType angleType)
    {
        Vector d1 = seg1End - seg1Start;
        Vector d2 = seg2End - seg2Start;

        if (d1.LengthSquared < 1e-10 || d2.LengthSquared < 1e-10)
            return (0, LineLineRelation.None, seg1Start, d1, d2);

        d1.Normalize();
        d2.Normalize();

        LineLineRelation relation = LineLineRelation.None;

        if (SegmentSegmentGeometry.AreSegmentsCollinear(seg1Start, seg1End, seg2Start, seg2End))
            relation = LineLineRelation.Collinear;
        else if (SegmentSegmentGeometry.AreSegmentsParallel(seg1Start, seg1End, seg2Start, seg2End))
        {
            relation = LineLineRelation.Parallel;
        }
        else if (SegmentSegmentGeometry.DoSegmentsIntersect(seg1Start, seg1End, seg2Start, seg2End))
        {
            relation = LineLineRelation.Intersecting;
        }

        double dot = Vector.Multiply(d1, d2);
        dot = Math.Max(-1, Math.Min(1, dot));

        double angleRad = Math.Acos(dot);
        double angleDeg = angleRad * 180.0 / Math.PI;

        if (angleType == LineAngleType.Smallest)
        {
            if (angleDeg > 90)
                angleDeg = 180 - angleDeg;
        }

        // 这里的 vertex 不再用于绘制顶点（绘制顶点在 Invoke 里统一计算），仅保持返回值兼容
        Point vertex = seg1Start;

        return (angleDeg, relation, vertex, d1, d2);
    }

    /// <summary>
    /// 计算两条“延长直线”的交点；平行/几乎平行时回退为两线段中点的中点。
    /// </summary>
    public static Point FindInfiniteLineIntersectionOrFallback(Point a1, Point a2, Point b1, Point b2)
    {
        Vector r = a2 - a1;
        Vector s = b2 - b1;

        double rxs = Cross(r, s);
        if (Math.Abs(rxs) < 1e-10)
        {
            Point c1 = new Point((a1.X + a2.X) / 2.0, (a1.Y + a2.Y) / 2.0);
            Point c2 = new Point((b1.X + b2.X) / 2.0, (b1.Y + b2.Y) / 2.0);
            return new Point((c1.X + c2.X) / 2.0, (c1.Y + c2.Y) / 2.0);
        }

        Vector qp = b1 - a1;
        double t = Cross(qp, s) / rxs; // a1 + t*r
        return a1 + t * r;
    }

    private static double Cross(Vector v1, Vector v2) => v1.X * v2.Y - v1.Y * v2.X;
}