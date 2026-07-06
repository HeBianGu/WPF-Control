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
public abstract class PointToCircleMeasureNodeDataBase<T> : FromToMeasureNodeDataBase<T, Point, VisionCircle> where T : class, IVisionImage
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

    private PointCircleDistanceType _PointCircleDistanceType;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "测量模式", GroupName = VisionTabNames.RunParameters, Description = "设置点到圆的距离运行模式")]
    public PointCircleDistanceType PointCircleDistanceType
    {
        get { return _PointCircleDistanceType; }
        set
        {
            _PointCircleDistanceType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }


    private PointCircleRelation _PointCircleRelation;
    [ReadOnly(true)]
    [Expressionable]
    [Tab(VisionTabNames.ResultParameters)]
    [Display(Name = "点圆关系", GroupName = VisionTabNames.ResultParameters, Description = "计算点圆关系结果")]
    public PointCircleRelation PointCircleRelation
    {
        get { return _PointCircleRelation; }
        set
        {
            _PointCircleRelation = value;
            RaisePropertyChanged();
        }
    }

    protected override IExpressionKey GetFromExpression() => this.From;
    protected override IExpressionKey GetToExpression() => this.To;

    protected override FlowableResult<T> Invoke(T fromImage)
    {
        var fromPoint = this.FromValue;
        var circle = this.ToValue;
        var toPoint = circle.Point;
        this.ResultShapes.Add(new PointShape(fromPoint));
        this.ResultShapes.Add(circle.ToCircleShape(x => x.DimensionText = this.GetWorldDistance(x.Radius)));
        var tuple = PointCircleGeometry.CalculateDistance(fromPoint, circle.Point, circle.Radius, this.PointCircleDistanceType);
        this.MeasureResult = tuple.distance;
        toPoint = tuple.targetPoint;
        this.PointCircleRelation = PointCircleGeometry.GetRelation(fromPoint, circle.Point, circle.Radius);
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
public enum PointCircleRelation
{
    [Display(Name = "无")]
    None,
    [Display(Name = "点在圆内（不包括圆周）")]
    Inside,
    [Display(Name = "点在圆周上")]
    OnCircumference,
    [Display(Name = "点在圆外")]
    Outside
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum PointCircleDistanceType
{
    [Display(Name = "圆心到点的距离")]
    CenterToPoint,    // 圆心到点的距离
    [Display(Name = "点到圆周的最短距离（带符号）")]
    BoundaryToPoint,   // 点到圆周的最短距离（带符号）
    [Display(Name = "点到圆周的绝对距离")]
    AbsoluteBoundary,  // 点到圆周的绝对距离
    [Display(Name = "返回圆周上最近点")]
    ClosestPoint       // 返回圆周上最近点
}

public static class PointCircleGeometry
{
    /// <summary>
    /// 计算点与圆的几何关系
    /// </summary>
    public static PointCircleRelation GetRelation(Point point, Point center, double radius)
    {
        double distanceSquared = (point - center).LengthSquared;
        double radiusSquared = radius * radius;

        if (Math.Abs(distanceSquared - radiusSquared) < 1e-10)
            return PointCircleRelation.OnCircumference;

        return distanceSquared < radiusSquared ?
            PointCircleRelation.Inside :
            PointCircleRelation.Outside;
    }

    /// <summary>
    /// 计算点与圆的各种距离
    /// </summary>
    /// <returns>Tuple(距离值, 目标点)</returns>
    public static (double distance, Point targetPoint) CalculateDistance(
        Point point, Point center, double radius, PointCircleDistanceType type)
    {
        Vector vector = point - center;
        double centerDistance = vector.Length;

        return type switch
        {
            // 圆心到点的距离
            PointCircleDistanceType.CenterToPoint =>
                (centerDistance, center),

            // 点到圆周的带符号距离（内负外正）
            PointCircleDistanceType.BoundaryToPoint =>
                (centerDistance - radius, GetClosestPoint(point, center, radius)),

            // 点到圆周的绝对距离
            PointCircleDistanceType.AbsoluteBoundary =>
                (Math.Abs(centerDistance - radius), GetClosestPoint(point, center, radius)),

            // 返回圆周上最近点
            PointCircleDistanceType.ClosestPoint =>
                (centerDistance - radius, GetClosestPoint(point, center, radius)),

            _ => throw new ArgumentException("未知的距离计算类型")
        };
    }

    /// <summary>
    /// 获取圆周上离给定点最近的点
    /// </summary>
    private static Point GetClosestPoint(Point point, Point center, double radius)
    {
        if (center == point)
            // 点在圆心位置时，返回圆周上任意点（如正右方）
            return new Point(center.X + radius, center.Y);

        Vector direction = point - center;
        direction.Normalize();
        return center + direction * radius;
    }

    /// <summary>
    /// 获取圆周上离给定点最远的点
    /// </summary>
    public static Point GetFarthestPoint(Point point, Point center, double radius)
    {
        if (center == point)
            // 点在圆心位置时，最远点仍是圆周上任意点
            return new Point(center.X + radius, center.Y);

        Vector direction = center - point; // 反向向量
        direction.Normalize();
        return center + direction * radius;
    }
}

