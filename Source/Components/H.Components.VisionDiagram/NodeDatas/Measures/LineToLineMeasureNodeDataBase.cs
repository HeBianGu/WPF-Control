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
public abstract class LineToLineMeasureNodeDataBase<T> : FromToMeasureNodeDataBase<T, VisionLine, VisionLine> where T : class, IVisionImage
{
    private IExpressionKey _From;
    [GetMethodNameSource(nameof(GetFromNodeDataExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "选择输入线", GroupName = VisionTabNames.RunParameters, Description = "用来演示如何增加节点表达式参数")]
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
    [Display(Name = "选择输入线", GroupName = VisionTabNames.RunParameters, Description = "用来演示如何增加节点表达式参数")]
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

    private SegmentDistanceType _SegmentDistanceType;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "测量模式", GroupName = VisionTabNames.RunParameters, Description = "设置运行模式")]
    public SegmentDistanceType SegmentDistanceType
    {
        get { return _SegmentDistanceType; }
        set
        {
            _SegmentDistanceType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }


    private SegmentSegmentRelation _SegmentSegmentRelation;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "线线关系", GroupName = VisionTabNames.ResultParameters, Description = "计算点圆关系结果")]
    public SegmentSegmentRelation SegmentSegmentRelation
    {
        get { return _SegmentSegmentRelation; }
        set
        {
            _SegmentSegmentRelation = value;
            RaisePropertyChanged();
        }
    }

    protected override IExpressionKey GetFromExpression() => this.From;
    protected override IExpressionKey GetToExpression() => this.To;

    protected override FlowableResult<T> Invoke(T fromImage)
    {
        VisionLine fromLine = this.FromValue;
        VisionLine toLine = this.ToValue;
        LineShape fcircleShape = new LineShape(fromLine.Start, fromLine.End);
        this.ResultShapes.Add(fcircleShape);
        LineShape tcircleShape = new LineShape(toLine.Start, toLine.End);
        this.ResultShapes.Add(tcircleShape);
        var tuple = SegmentSegmentGeometry.CalculateDistance(fromLine.Start, fromLine.End, toLine.Start, toLine.End);
        this.MeasureResult = tuple.distance;
        this.SegmentSegmentRelation = tuple.relation;
        var fromPoint = tuple.closestOnSeg1;
        var toPoint = tuple.closestOnSeg2;
        DimensionShape lineShape = new DimensionShape(fromPoint, toPoint);
        lineShape.Text = this.GetWorldDistance(lineShape.Length);
        if (this.DistanceStroke != null)
            lineShape.Stroke = this.DistanceStroke;
        this.ResultShapes.Add(lineShape);
        var resultImage = this.GetExpressionResultImage(fromImage).Clone();
        return this.OK(resultImage as T, lineShape.ToAutoResultPresenter("线段间距离"));
    }
}
[TypeConverter(typeof(DisplayEnumConverter))]
public enum SegmentSegmentRelation
{
    [Display(Name = "无", GroupName = "线线关系", Description = "未计算或无法确定两条线段之间的空间关系")]
    None,
    [Display(Name = "两线段相交", GroupName = "线线关系", Description = "两条有限线段存在公共交点")]
    Intersecting,    // 两线段相交
    [Display(Name = "两线段共线", GroupName = "线线关系", Description = "两条线段位于同一条无限直线上")]
    Collinear,      // 两线段共线
    [Display(Name = "两线段平行但不相交", GroupName = "线线关系", Description = "两条线段方向平行且没有公共点")]
    Parallel,       // 两线段平行但不相交
    [Display(Name = "两线段不平行也不相交", GroupName = "线线关系", Description = "两条线段方向不同，但交点位于线段延长线上")]
    Skew            // 两线段不平行也不相交
}
[TypeConverter(typeof(DisplayEnumConverter))]
public enum SegmentDistanceType
{
    [Display(Name = "精确距离", GroupName = "测量模式", Description = "计算两条线段之间的实际最短距离")]
    Exact,          // 精确距离
    [Display(Name = "平方距离（性能优化）", GroupName = "测量模式", Description = "返回未开平方的距离值以减少计算开销")]
    Squared,        // 平方距离（性能优化）
    [Display(Name = "只返回最近点对", GroupName = "测量模式", Description = "仅计算并返回两条线段上的最近点对")]
    ClosestPoints   // 只返回最近点对
}

public static class SegmentSegmentGeometry
{
    /// <summary>
    /// 计算两条线段之间的最短距离及最近点对
    /// </summary>
    /// <returns>(距离, 线段1上的最近点, 线段2上的最近点, 位置关系)</returns>
    public static (double distance, Point closestOnSeg1, Point closestOnSeg2, SegmentSegmentRelation relation)
        CalculateDistance(Point seg1Start, Point seg1End, Point seg2Start, Point seg2End)
    {
        // 首先检查两线段是否相交
        if (DoSegmentsIntersect(seg1Start, seg1End, seg2Start, seg2End))
        {
            Point intersection = FindIntersection(seg1Start, seg1End, seg2Start, seg2End);
            return (0, intersection, intersection, SegmentSegmentRelation.Intersecting);
        }

        // 计算四种可能的端点组合距离
        var (dist1, pt1, relation1) = PointSegmentGeometry.CalculateDistance(seg1Start, seg2Start, seg2End);
        var (dist2, pt2, relation2) = PointSegmentGeometry.CalculateDistance(seg1End, seg2Start, seg2End);
        var (dist3, pt3, relation3) = PointSegmentGeometry.CalculateDistance(seg2Start, seg1Start, seg1End);
        var (dist4, pt4, relation4) = PointSegmentGeometry.CalculateDistance(seg2End, seg1Start, seg1End);

        // 找出最小距离
        double minDist = Math.Min(Math.Min(dist1, dist2), Math.Min(dist3, dist4));

        // 确定最近点对
        Point closestOnSeg1, closestOnSeg2;
        SegmentSegmentRelation relation = SegmentSegmentRelation.Skew;

        if (Math.Abs(dist1 - minDist) < 1e-10)
        {
            closestOnSeg1 = seg1Start;
            closestOnSeg2 = pt1;
        }
        else if (Math.Abs(dist2 - minDist) < 1e-10)
        {
            closestOnSeg1 = seg1End;
            closestOnSeg2 = pt2;
        }
        else if (Math.Abs(dist3 - minDist) < 1e-10)
        {
            closestOnSeg1 = pt3;
            closestOnSeg2 = seg2Start;
        }
        else
        {
            closestOnSeg1 = pt4;
            closestOnSeg2 = seg2End;
        }

        // 检查共线或平行关系
        if (AreSegmentsCollinear(seg1Start, seg1End, seg2Start, seg2End))
            relation = SegmentSegmentRelation.Collinear;
        else if (AreSegmentsParallel(seg1Start, seg1End, seg2Start, seg2End))
        {
            relation = SegmentSegmentRelation.Parallel;
        }

        return (minDist, closestOnSeg1, closestOnSeg2, relation);
    }

    /// <summary>
    /// 判断两条线段是否相交
    /// </summary>
    public static bool DoSegmentsIntersect(Point seg1Start, Point seg1End, Point seg2Start, Point seg2End)
    {
        // 快速排斥实验
        if (Math.Max(seg1Start.X, seg1End.X) < Math.Min(seg2Start.X, seg2End.X) ||
            Math.Max(seg2Start.X, seg2End.X) < Math.Min(seg1Start.X, seg1End.X) ||
            Math.Max(seg1Start.Y, seg1End.Y) < Math.Min(seg2Start.Y, seg2End.Y) ||
            Math.Max(seg2Start.Y, seg2End.Y) < Math.Min(seg1Start.Y, seg1End.Y))
        {
            return false;
        }

        // 跨立实验
        double cross1 = CrossProduct(seg2Start, seg2End, seg1Start);
        double cross2 = CrossProduct(seg2Start, seg2End, seg1End);
        double cross3 = CrossProduct(seg1Start, seg1End, seg2Start);
        double cross4 = CrossProduct(seg1Start, seg1End, seg2End);

        // 允许在端点处相交
        if (Math.Abs(cross1) < 1e-10 || Math.Abs(cross2) < 1e-10 ||
            Math.Abs(cross3) < 1e-10 || Math.Abs(cross4) < 1e-10)
        {
            return true;
        }

        return cross1 * cross2 < 0 && cross3 * cross4 < 0;
    }

    /// <summary>
    /// 计算两条线段的交点（假设已经确定相交）
    /// </summary>
    public static Point FindIntersection(Point seg1Start, Point seg1End, Point seg2Start, Point seg2End)
    {
        Vector dir1 = seg1End - seg1Start;
        Vector dir2 = seg2End - seg2Start;
        Vector diff = seg2Start - seg1Start;

        double crossDirs = CrossProduct(dir1, dir2);
        double crossDiffDir2 = CrossProduct(diff, dir2);

        // 处理平行线段（理论上不应该发生，因为已经检查过相交）
        if (Math.Abs(crossDirs) < 1e-10)
            // 返回任意一个端点作为交点
            return seg1Start;

        double t = crossDiffDir2 / crossDirs;
        return seg1Start + t * dir1;
    }

    /// <summary>
    /// 判断两条线段是否共线
    /// </summary>
    public static bool AreSegmentsCollinear(Point seg1Start, Point seg1End, Point seg2Start, Point seg2End)
    {
        double area1 = CrossProduct(seg1Start, seg1End, seg2Start);
        double area2 = CrossProduct(seg1Start, seg1End, seg2End);
        return Math.Abs(area1) < 1e-10 && Math.Abs(area2) < 1e-10;
    }

    /// <summary>
    /// 判断两条线段是否平行
    /// </summary>
    public static bool AreSegmentsParallel(Point seg1Start, Point seg1End, Point seg2Start, Point seg2End)
    {
        Vector dir1 = seg1End - seg1Start;
        Vector dir2 = seg2End - seg2Start;
        double cross = CrossProduct(dir1, dir2);
        return Math.Abs(cross) < 1e-10;
    }

    private static double CrossProduct(Vector v1, Vector v2)
    {
        return v1.X * v2.Y - v1.Y * v2.X;
    }

    private static double CrossProduct(Point a, Point b, Point c)
    {
        return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
    }
}

