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
public abstract class CircleToCircleMeasureNodeDataBase<T> : FromToMeasureNodeDataBase<T, VisionCircle, VisionCircle> where T : class, IVisionImage
{
    private IExpressionKey _From;
    [GetMethodNameSource(nameof(GetFromNodeDataExpressions))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem))]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "选择输入圆1", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
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
    [Display(Name = "选择输入圆2", GroupName = VisionTabNames.RunParameters, Description = "用来选择测量输入数据")]
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

    private CircleDistanceType _CircleDistanceType;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "测量模式", GroupName = VisionTabNames.RunParameters, Description = "设置圆到圆的距离运行模式")]
    public CircleDistanceType CircleDistanceType
    {
        get { return _CircleDistanceType; }
        set
        {
            _CircleDistanceType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private CircleRelationship _RelationshipResult;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "圆圆关系", GroupName = VisionTabNames.ResultParameters, Description = "计算圆圆关系结果")]
    public CircleRelationship RelationshipResult
    {
        get { return _RelationshipResult; }
        set
        {
            _RelationshipResult = value;
            RaisePropertyChanged();
        }
    }

    protected override IExpressionKey GetFromExpression() => this.From;
    protected override IExpressionKey GetToExpression() => this.To;

    protected override FlowableResult<T> Invoke(T fromImage)
    {

        VisionCircle fromCircle = this.FromValue;
        VisionCircle toCircle = this.ToValue;
        var fromPoint = fromCircle.Point;
        var toPoint = toCircle.Point;
        CircleShape fcircleShape = new CircleShape(fromPoint, fromCircle.Radius) { UseDimension = false };
        this.ResultShapes.Add(fcircleShape);
        CircleShape tcircleShape = new CircleShape(toPoint, toCircle.Radius) { UseDimension = false };
        this.ResultShapes.Add(tcircleShape);
        var tuple = CircleDistanceCalculator.Calculate(fromPoint, fromCircle.Radius, toPoint, toCircle.Radius, this.CircleDistanceType);
        this.RelationshipResult = CircleDistanceCalculator.GetRelationship(fromPoint, fromCircle.Radius, toPoint, toCircle.Radius);
        this.MeasureResult = tuple.distance;
        toPoint = tuple.targetPoint;
        DimensionShape lineShape = new DimensionShape(fromPoint, toPoint);
        if (this.DistanceStroke != null)
            lineShape.Stroke = this.DistanceStroke;
        lineShape.Text = this.GetWorldDistance(lineShape.Length);
        this.ResultShapes.Add(lineShape);

        var resultImage = this.GetExpressionResultImage(fromImage).Clone();
        return this.OK(resultImage as T, lineShape.ToResultPresenter());
    }
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum CircleRelationship
{
    [Display(Name = "无", GroupName = "圆圆关系", Description = "未计算或无法确定两个圆之间的空间关系")]
    None,
    [Display(Name = "相离", GroupName = "圆圆关系", Description = "两个圆彼此分离且圆周没有交点")]
    Separate,           // 相离
    [Display(Name = "外切", GroupName = "圆圆关系", Description = "两个圆在外部仅有一个切点")]
    ExternallyTangent,  // 外切
    [Display(Name = "相交", GroupName = "圆圆关系", Description = "两个圆的圆周存在两个交点")]
    Intersecting,       // 相交
    [Display(Name = "内切", GroupName = "圆圆关系", Description = "一个圆位于另一个圆内且圆周仅有一个切点")]
    InternallyTangent,  // 内切
    [Display(Name = "内含（圆1包含圆2）", GroupName = "圆圆关系", Description = "圆2完全位于圆1内部且圆周不相交")]
    Containing,         // 内含（圆1包含圆2）
    [Display(Name = "内含（圆1被圆2包含）", GroupName = "圆圆关系", Description = "圆1完全位于圆2内部且圆周不相交")]
    Contained           // 内含（圆1被圆2包含）
}


[TypeConverter(typeof(DisplayEnumConverter))]
public enum CircleDistanceType
{
    [Display(Name = "圆心到圆心", GroupName = "测量模式", Description = "计算两个圆心之间的直线距离")]
    CenterToCenter,    // 圆心到圆心
    [Display(Name = "两圆圆周最小距离", GroupName = "测量模式", Description = "计算两个圆周之间的最短距离")]
    Minimum,           // 两圆圆周最小距离
    [Display(Name = "两圆圆周最大距离", GroupName = "测量模式", Description = "计算两个圆周之间的最大距离")]
    Maximum,           // 两圆圆周最大距离
    [Display(Name = "圆1到圆2的最近点", GroupName = "测量模式", Description = "返回圆2圆周上距离圆1最近的点")]
    ClosestPoint,      // 圆1到圆2的最近点
    [Display(Name = "圆1到圆2的最远点", GroupName = "测量模式", Description = "返回圆2圆周上距离圆1最远的点")]
    FarthestPoint      // 圆1到圆2的最远点
}

public static class CircleDistanceCalculator
{
    /// <summary>
    /// 计算两圆之间的距离或目标点
    /// </summary>
    /// <param name="c1">圆1中心</param>
    /// <param name="r1">圆1半径</param>
    /// <param name="c2">圆2中心</param>
    /// <param name="r2">圆2半径</param>
    /// <param name="type">计算类型</param>
    /// <returns>Tuple(距离, 目标点)。目标点可能是圆1或圆2上的点</returns>
    public static (double distance, Point targetPoint) Calculate(Point c1, double r1, Point c2, double r2, CircleDistanceType type)
    {
        Vector centerVector = c2 - c1;
        double centerDistance = centerVector.Length;

        return type switch
        {
            // 圆心到圆心距离
            CircleDistanceType.CenterToCenter => (centerDistance, c2),

            // 两圆圆周最小距离（返回圆2上的最近点）
            CircleDistanceType.Minimum => (
                Math.Max(0, centerDistance - r1 - r2),
                GetPointOnCircle(c1, c2, r2, false)),

            // 两圆圆周最大距离（返回圆2上的最远点）
            CircleDistanceType.Maximum => (
                centerDistance + r1 + r2,
                GetPointOnCircle(c1, c2, r2, true)),

            // 圆1到圆2的最近点（在圆2上）
            CircleDistanceType.ClosestPoint => (
                Math.Max(0, centerDistance - r1 - r2),
                GetPointOnCircle(c1, c2, r2, false)),

            // 圆1到圆2的最远点（在圆2上）
            CircleDistanceType.FarthestPoint => (
                centerDistance + r1 + r2,
                GetPointOnCircle(c1, c2, r2, true)),

            _ => throw new ArgumentException("未知的距离计算类型")
        };
    }

    /// <summary>
    /// 获取圆2上相对于圆1的最近或最远点
    /// </summary>
    private static Point GetPointOnCircle(Point c1, Point c2, double r2, bool farthest)
    {
        if (c1 == c2)
        {
            // 同心圆时返回任意点（如正右方）
            return farthest
                ? new Point(c2.X + r2, c2.Y)
                : new Point(c2.X - r2, c2.Y);
        }

        Vector direction = c2 - c1;
        direction.Normalize();

        return farthest
            ? c2 + direction * r2  // 最远点（沿中心连线向外）
            : c2 - direction * r2; // 最近点（沿中心连线向内）
    }

    /// <summary>
    /// 判断两个圆的位置关系
    /// </summary>
    public static CircleRelationship GetRelationship(Point c1, double r1, Point c2, double r2)
    {
        double d = (c2 - c1).Length;
        double sumR = r1 + r2;
        double diffR = Math.Abs(r1 - r2);

        const double tolerance = 1e-10;

        if (d > sumR + tolerance)
            return CircleRelationship.Separate;

        if (Math.Abs(d - sumR) < tolerance)
            return CircleRelationship.ExternallyTangent;

        if (d > diffR + tolerance)
            return CircleRelationship.Intersecting;

        if (Math.Abs(d - diffR) < tolerance)
            return r1 > r2 ? CircleRelationship.InternallyTangent
                          : CircleRelationship.Contained;

        return r1 > r2 ? CircleRelationship.Containing
                       : CircleRelationship.Contained;
    }
}

