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
public abstract class LineToCircleMeasureNodeDataBase<T> : FromToMeasureNodeDataBase<T, VisionLine, VisionCircle> where T : class, IVisionImage
{
    private IExpressionKey _From;
    [GetMethodNameSource(nameof(GetFromNodeDataExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "选择输入线", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
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
    [Display(Name = "选择输入圆", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
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

    private SegmentCircleDistanceType _SegmentCircleDistanceType;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "测量模式", GroupName = VisionTabNames.RunParameters, Description = "设置圆到圆的距离运行模式")]
    public SegmentCircleDistanceType SegmentCircleDistanceType
    {
        get { return _SegmentCircleDistanceType; }
        set
        {
            _SegmentCircleDistanceType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private SegmentCircleRelation _Relationship;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "圆圆关系", GroupName = VisionTabNames.ResultParameters, Description = "计算圆圆关系结果")]
    public SegmentCircleRelation Relationship
    {
        get { return _Relationship; }
        set
        {
            _Relationship = value;
            RaisePropertyChanged();
        }
    }

    protected override IExpressionKey GetFromExpression() => this.From;
    protected override IExpressionKey GetToExpression() => this.To;

    protected override FlowableResult<T> Invoke(T fromImage)
    {
        VisionLine visionLine = this.FromValue;
        VisionCircle visionCircle = this.ToValue;
        this.ResultShapes.Add(visionLine.ToLineShape());
        this.ResultShapes.Add(visionCircle.ToCircleShape());
        var tuple = SegmentCircleGeometry.CalculateRelationship(visionLine.Start, visionLine.End, visionCircle.Point, visionCircle.Radius);
        this.MeasureResult = tuple.distance;
        this.Relationship = tuple.relation;
        var fromPoint = visionCircle.Point;
        var toPoint = tuple.farthestPoint;
        if (this.SegmentCircleDistanceType == SegmentCircleDistanceType.ClosestPoint)
            toPoint = tuple.closestPoint;
        if (this.SegmentCircleDistanceType == SegmentCircleDistanceType.FarthestPoint)
            toPoint = tuple.farthestPoint;
        if (this.SegmentCircleDistanceType == SegmentCircleDistanceType.Maximum)
            toPoint = tuple.farthestPoint;
        if (this.SegmentCircleDistanceType == SegmentCircleDistanceType.Minimum)
            toPoint = tuple.closestPoint;

        DimensionShape lineShape = new DimensionShape(fromPoint, toPoint);
        if (tuple.intersections.Count == 1)
            this.ResultShapes.Add(new PointShape(tuple.intersections[0]));
        if (tuple.intersections.Count == 2)
            this.ResultShapes.Add(new LineShape(tuple.intersections[0], tuple.intersections[1]));

        lineShape.Text = this.GetWorldDistance(lineShape.Length);
        if (this.DistanceStroke != null)
            lineShape.Stroke = this.DistanceStroke;
        this.ResultShapes.Add(lineShape);

        var resultImage = this.GetExpressionResultImage(fromImage).Clone();
        return this.OK(resultImage as T, lineShape.ToResultPresenter());
    }
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum SegmentCircleRelation
{
    [Display(Name = "无")]
    None,
    [Display(Name = "无交点")]
    NoIntersection,   // 无交点
    [Display(Name = "相切（一个交点）")]
    Tangent,          // 相切（一个交点）
    [Display(Name = "相交（两个交点）")]
    Secant,           // 相交（两个交点）
    [Display(Name = "线段完全在圆内")]
    SegmentInside,    // 线段完全在圆内
    [Display(Name = "线段端点与圆相交（特殊相切情况）")]
    PointIntersection // 线段端点与圆相交（特殊相切情况）
}
[TypeConverter(typeof(DisplayEnumConverter))]
public enum SegmentCircleDistanceType
{
    [Display(Name = "最小距离")]
    Minimum,          // 最小距离
    [Display(Name = "最大距离")]
    Maximum,          // 最大距离
    [Display(Name = "最近点")]
    ClosestPoint,     // 最近点
    [Display(Name = "最远点")]
    FarthestPoint     // 最远点
}

public static class SegmentCircleGeometry
{
    /// <summary>
    /// 计算线段与圆的几何关系及距离信息
    /// </summary>
    public static (SegmentCircleRelation relation,
                  double distance,
                  Point closestPoint,
                  Point farthestPoint,
                  List<Point> intersections)
        CalculateRelationship(Point segStart, Point segEnd, Point circleCenter, double radius)
    {
        Vector segmentVec = segEnd - segStart;
        Vector centerToStart = circleCenter - segStart;

        double a = segmentVec.LengthSquared;
        double b = 2 * Vector.Multiply(centerToStart, segmentVec);
        double c = centerToStart.LengthSquared - radius * radius;

        double discriminant = b * b - 4 * a * c;
        List<Point> intersections = new List<Point>();
        SegmentCircleRelation relation;

        // 处理线段长度为0的情况（点线段）
        if (a < 1e-10)
        {
            double pointDist = (segStart - circleCenter).Length;
            relation = Math.Abs(pointDist - radius) < 1e-10 ?
                SegmentCircleRelation.PointIntersection :
                pointDist < radius ? SegmentCircleRelation.SegmentInside : SegmentCircleRelation.NoIntersection;

            return (relation,
                    pointDist - radius,
                    segStart,
                    segStart,
                    relation == SegmentCircleRelation.PointIntersection ?
                        new List<Point> { segStart } :
                        new List<Point>());
        }

        if (discriminant < -1e-10) // 无实数解
            relation = SegmentCircleRelation.NoIntersection;
        else if (Math.Abs(discriminant) < 1e-10) // 一个解（相切）
        {
            relation = SegmentCircleRelation.Tangent;
            double t = -b / (2 * a);
            if (t >= 0 && t <= 1)
                intersections.Add(segStart + t * segmentVec);
        }
        else // 两个解
        {
            double sqrtDiscriminant = Math.Sqrt(discriminant);
            double t1 = (-b - sqrtDiscriminant) / (2 * a);
            double t2 = (-b + sqrtDiscriminant) / (2 * a);

            bool t1Valid = t1 >= 0 && t1 <= 1;
            bool t2Valid = t2 >= 0 && t2 <= 1;

            if (t1Valid) intersections.Add(segStart + t1 * segmentVec);
            if (t2Valid) intersections.Add(segStart + t2 * segmentVec);

            relation = intersections.Count switch
            {
                0 => SegmentCircleRelation.NoIntersection,
                1 => SegmentCircleRelation.Tangent,
                2 => SegmentCircleRelation.Secant,
                _ => SegmentCircleRelation.NoIntersection
            };
        }

        // 检查线段是否完全在圆内
        bool startInside = (segStart - circleCenter).Length <= radius;
        bool endInside = (segEnd - circleCenter).Length <= radius;
        if (startInside && endInside && relation == SegmentCircleRelation.NoIntersection)
            relation = SegmentCircleRelation.SegmentInside;

        // 计算最近点和最远点
        (Point closest, double minDist) = GetClosestPointOnSegment(segStart, segEnd, circleCenter);
        Point farthest = (segStart - circleCenter).Length > (segEnd - circleCenter).Length ? segStart : segEnd;
        double signedDistance = minDist - radius;

        return (relation, signedDistance, closest, farthest, intersections);
    }

    private static (Point point, double distance) GetClosestPointOnSegment(Point segStart, Point segEnd, Point point)
    {
        Vector segment = segEnd - segStart;
        Vector pointVec = point - segStart;

        double t = Vector.Multiply(pointVec, segment) / segment.LengthSquared;
        t = Math.Max(0, Math.Min(1, t));

        Point closestPoint = segStart + t * segment;
        return (closestPoint, (closestPoint - point).Length);
    }

    /// <summary>
    /// 获取线段与圆的交点（精确计算）
    /// </summary>
    public static List<Point> GetIntersectionPoints(Point segStart, Point segEnd, Point circleCenter, double radius)
    {
        var result = CalculateRelationship(segStart, segEnd, circleCenter, radius);
        return result.intersections;
    }

    /// <summary>
    /// 判断线段是否与圆相交（包括相切）
    /// </summary>
    public static bool DoesIntersect(Point segStart, Point segEnd, Point circleCenter, double radius)
    {
        var relation = CalculateRelationship(segStart, segEnd, circleCenter, radius).relation;
        return relation != SegmentCircleRelation.NoIntersection &&
               relation != SegmentCircleRelation.SegmentInside;
    }
}

