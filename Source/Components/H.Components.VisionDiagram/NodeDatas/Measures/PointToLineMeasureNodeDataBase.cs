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
public abstract class PointToLineMeasureNodeDataBase<T> : FromToMeasureNodeDataBase<T, Point, VisionLine> where T : class, IVisionImage
{
    private IExpressionKey _From;
    [GetMethodNameSource(nameof(GetFromNodeDataExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "选择输入点", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
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
    [Display(Name = "选择输入线段", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
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

    private PointSegmentDistanceType _PointSegmentDistanceType;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "测量模式", GroupName = VisionTabNames.RunParameters, Description = "设置测量模式")]
    public PointSegmentDistanceType PointSegmentDistanceType
    {
        get { return _PointSegmentDistanceType; }
        set
        {
            _PointSegmentDistanceType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }


    private PointSegmentRelation _PointSegmentRelation;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "点线关系", GroupName = VisionTabNames.ResultParameters, Description = "计算点线关系结果")]
    public PointSegmentRelation PointSegmentRelation
    {
        get { return _PointSegmentRelation; }
        set
        {
            _PointSegmentRelation = value;
            RaisePropertyChanged();
        }
    }

    protected override IExpressionKey GetFromExpression() => this.From;
    protected override IExpressionKey GetToExpression() => this.To;
    protected override FlowableResult<T> Invoke(T fromImage)
    {
        Point fromPoint = this.FromValue;
        VisionLine toLine = this.ToValue;
        this.ResultShapes.Add(new PointShape(fromPoint));
        this.ResultShapes.Add(toLine.ToLineShape());
        var tuple = PointSegmentGeometry.CalculateDistance(fromPoint, toLine.Start, toLine.End);
        this.MeasureResult = tuple.distance;
        var toPoint = tuple.closestPoint;
        this.PointSegmentRelation = tuple.relation;
        DimensionShape lineShape = new DimensionShape(fromPoint, toPoint);
        lineShape.Text = this.GetWorldDistance(lineShape.Length);
        if (this.DistanceStroke != null)
            lineShape.Stroke = this.DistanceStroke;
        this.ResultShapes.Add(lineShape);
        var resultImage = this.GetExpressionResultImage(fromImage).Clone();
        return this.OK(resultImage as T, lineShape.ToResultPresenter());
    }
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum PointSegmentRelation
{
    [Display(Name = "无", GroupName = "点线关系", Description = "未计算或无法确定点与线段之间的空间关系")]
    None,
    [Display(Name = "点在线段上", GroupName = "点线关系", Description = "目标点位于线段起点与终点之间")]
    OnSegment,      // 点在线段上
    [Display(Name = "点在直线的延长线上但不在线段内", GroupName = "点线关系", Description = "目标点与线段共线，但位于线段范围之外")]
    OnExtendedLine, // 点在直线的延长线上但不在线段内
    [Display(Name = "点在直线左侧（从起点看向终点）", GroupName = "点线关系", Description = "从线段起点朝终点观察时，目标点位于左侧")]
    LeftSide,       // 点在直线左侧（从起点看向终点）
    [Display(Name = "点在直线右侧（从起点看向终点）", GroupName = "点线关系", Description = "从线段起点朝终点观察时，目标点位于右侧")]
    RightSide       // 点在直线右侧（从起点看向终点）
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum PointSegmentDistanceType
{
    [Display(Name = "精确距离（默认）", GroupName = "测量模式", Description = "计算目标点到有限线段的实际最短距离")]
    Exact,          // 精确距离（默认）
    [Display(Name = "平方距离（避免开方运算）", GroupName = "测量模式", Description = "返回未开平方的距离值以减少计算开销")]
    Squared,        // 平方距离（避免开方运算）
    [Display(Name = "只返回最近点坐标", GroupName = "测量模式", Description = "仅返回线段上距离目标点最近的坐标")]
    ClosestPoint    // 只返回最近点坐标
}

public static class PointSegmentGeometry
{
    /// <summary>
    /// 计算点到线段的最短距离及最近点
    /// </summary>
    /// <returns>(距离, 最近点, 位置关系)</returns>
    public static (double distance, Point closestPoint, PointSegmentRelation relation) CalculateDistance(Point point, Point segmentStart, Point segmentEnd)
    {
        Vector segment = segmentEnd - segmentStart;
        Vector pointVector = point - segmentStart;

        double segmentLengthSquared = segment.LengthSquared;

        // 处理线段长度为0的情况（起点终点重合）
        if (segmentLengthSquared < 1e-10)
        {
            double distance1 = (point - segmentStart).Length;
            return (distance1, segmentStart, PointSegmentRelation.OnSegment);
        }

        // 计算投影比例 [0,1]表示在线段上
        double t = Vector.Multiply(pointVector, segment) / segmentLengthSquared;

        Point closestPoint;
        PointSegmentRelation relation;

        if (t < 0) // 线段起点之前
        {
            closestPoint = segmentStart;
            relation = PointSegmentRelation.OnExtendedLine;
        }
        else if (t > 1) // 线段终点之后
        {
            closestPoint = segmentEnd;
            relation = PointSegmentRelation.OnExtendedLine;
        }
        else // 在线段上
        {
            closestPoint = segmentStart + t * segment;
            relation = PointSegmentRelation.OnSegment;
        }

        // 计算左右关系（基于2D叉积）
        double crossProduct = (segmentEnd.X - segmentStart.X) * (point.Y - segmentStart.Y)
                           - (segmentEnd.Y - segmentStart.Y) * (point.X - segmentStart.X);

        if (relation == PointSegmentRelation.OnExtendedLine)
            relation = crossProduct > 0 ? PointSegmentRelation.LeftSide : PointSegmentRelation.RightSide;

        double distance = (point - closestPoint).Length;
        return (distance, closestPoint, relation);
    }

    /// <summary>
    /// 计算点到线段的平方距离（优化性能）
    /// </summary>
    public static double CalculateSquaredDistance(Point point, Point segmentStart, Point segmentEnd)
    {
        var result = CalculateDistance(point, segmentStart, segmentEnd);
        return result.distance * result.distance;
    }

    /// <summary>
    /// 判断点是否在线段上（包括端点）
    /// </summary>
    public static bool IsPointOnSegment(Point point, Point segmentStart, Point segmentEnd, double tolerance = 1e-6)
    {
        // 首先检查是否在直线上
        if (Math.Abs((segmentEnd.X - segmentStart.X) * (point.Y - segmentStart.Y) -
                   (segmentEnd.Y - segmentStart.Y) * (point.X - segmentStart.X)) > tolerance)
            return false;

        // 然后检查是否在线段范围内
        double minX = Math.Min(segmentStart.X, segmentEnd.X) - tolerance;
        double maxX = Math.Max(segmentStart.X, segmentEnd.X) + tolerance;
        double minY = Math.Min(segmentStart.Y, segmentEnd.Y) - tolerance;
        double maxY = Math.Max(segmentStart.Y, segmentEnd.Y) + tolerance;

        return point.X >= minX && point.X <= maxX &&
               point.Y >= minY && point.Y <= maxY;
    }
}

