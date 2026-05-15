// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Components.Vision.Geometrys;
global using H.Controls.Diagram.Presenter.Expressions;
global using H.Controls.Form.PropertyItem.TextPropertyItems;
global using H.Controls.ShapeBox.Shapes;
global using H.Controls.ShapeBox.Shapes.Base;
using H.Components.Vision.Extensions;

namespace H.Components.Visions.OpenCV.NodeDatas.Detector;
//需要检测实际图像中的有限长度线段
//处理时间要求较高
//需要过滤短小的噪声线段
//大多数实际应用场景
[Icon(FontIcons.LargeErase)]
[Display(Name = "线段查找", GroupName = "查找", Order = 2, Description = "直线概率检测，检测有限长度的线段(x1,y1,x2,y2坐标)")]
public class HoughLinesP : HoughLinesBase, IDetectorGroupableNodeData
{
    private double _rho = 1.0;
    [Range(1.0, 10000.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Display(Name = "距离分辨率（像素）", GroupName = VisionPropertyGroupNames.RunParameters, Description = "距离分辨率（像素），增大可减少计算量但降低精度")]
    public double Rho
    {
        get { return _rho; }
        set
        {
            _rho = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _theta = 180;
    [Range(1.0, 360.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Display(Name = "角度分辨率（角度）", GroupName = VisionPropertyGroupNames.RunParameters, Description = "减小可提高角度精度但增加计算量")]
    public double Theta
    {
        get { return _theta; }
        set
        {
            _theta = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _threshold = 50;
    [Range(1, 100.0)]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Display(Name = "投票阈值", GroupName = VisionPropertyGroupNames.RunParameters, Description = "决定被认定为直线的最小票数,值越大检测到的直线越少但更显著")]
    public int Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _minLineLength = 50;
    [Range(1.0, 10000.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Display(Name = "最小线段长度", GroupName = VisionPropertyGroupNames.RunParameters, Description = "线段的最小长度，小于此值的线段将被拒绝，有助于过滤掉短小的噪声线段")]
    public double MinLineLength
    {
        get { return _minLineLength; }
        set
        {
            _minLineLength = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _maxLineGap = 10.0;
    [Range(0.0, 1000.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Display(Name = "最大允许间隙", GroupName = VisionPropertyGroupNames.RunParameters, Description = "同一直线上点之间的最大允许间隙，如果两点间的间隙小于此值，它们将被视为同一直线的一部分")]
    public double MaxLineGap
    {
        get { return _maxLineGap; }
        set
        {
            _maxLineGap = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private LineSearchType _searchType = LineSearchType.All;
    [DefaultValue(LineSearchType.All)]
    [Display(Name = "线段筛选方式", GroupName = VisionPropertyGroupNames.RunParameters, Description = "All:不过滤；MinLength/MaxLength:按长度阈值过滤；Caliper:按卡尺区域过滤")]
    public LineSearchType SearchType
    {
        get { return _searchType; }
        set
        {
            _searchType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        var resultImage = this.GetExpressionResultImage(fromImage.ToMatImage()).ToMatImage();
        LineSegmentPoint[] lines = Cv2.HoughLinesP(fromImage, Rho, Math.PI / Theta, Threshold, MinLineLength, MaxLineGap);
        lines = this.GetTargetLines(lines).ToArray();

        lines = this.SearchType switch
        {
            LineSearchType.MinLength => lines.OrderBy(x => x.Length()).Take(1).ToArray(),
            LineSearchType.MaxLength => lines.OrderByDescending(x => x.Length()).Take(1).ToArray(),
            LineSearchType.Caliper => ApplyCaliperFilter(lines),
            _ => lines
        };

        if (lines.Length > 0)
            this.ResultVisionLine = lines[0].ToVisionLine();

        var shapes = lines.Select(x => x.ToVisionLine().ToDimensionShape(x => x.Text = this.GetWorldDistance(x.Length)));
        this.ResultShapes = shapes.OfType<IShape>().ToObservable();
        this.MatchingCountResult = lines.Length;
        var resultPresenter = shapes.ToResultPresenter();
        return this.OK(resultImage, resultPresenter, this.MatchingCountResult.ToDetectSuccessMessage());
    }

    private LineSegmentPoint[] ApplyCaliperFilter(LineSegmentPoint[] lines)
    {
        if (this.CaliperShape is not CaliperLineShape caliper)
            return lines;

        return lines.Where(x => caliper.Contains(x.P1.ToPoint(), x.P2.ToPoint())).ToArray();
    }
}

public enum LineSearchType
{
    [Display(Name = "全部", Description = "保留检测到的所有线段")]
    All,
    [Display(Name = "最小半径", Description = "从检测到的线段中选择长度最小的线段")]
    MinLength,
    [Display(Name = "最大半径", Description = "从检测到的线段中选择长度最大的线段")]
    MaxLength,
    [Display(Name = "卡尺筛选", Description = "按卡尺区域筛选线段")]
    Caliper
}

public abstract class HoughLinesBase : OpenCVDetectorNodeDataBase
{
    private VisionLine _ResultVisionLine;
    [Expressionable]
    [Display(Name = "线结果", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public VisionLine ResultVisionLine
    {
        get { return _ResultVisionLine; }
        set
        {
            _ResultVisionLine = value;
            RaisePropertyChanged();
        }
    }

    protected override IShape CreateCaliperShape(Mat fromImage)
    {
        var min = fromImage.Width / 4;
        return new CaliperLineShape()
        {
            From = new System.Windows.Point(min, fromImage.Height / 2),
            To = new System.Windows.Point(min * 3, fromImage.Height / 2)
        };
    }

    private float _targetAngle = -1;
    [Range(-1.0, 360.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Display(Name = "目标角度", GroupName = VisionPropertyGroupNames.RunParameters, Description = "同一直线上点之间的最大允许间隙，如果两点间的间隙小于此值，它们将被视为同一直线的一部分")]
    public float TargetAngle
    {
        get { return _targetAngle; }
        set
        {
            _targetAngle = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _tolerance = 15;
    [Range(1.0, 360.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Display(Name = "角度容差", GroupName = VisionPropertyGroupNames.RunParameters, Description = "同一直线上点之间的最大允许间隙，如果两点间的间隙小于此值，它们将被视为同一直线的一部分")]
    public float Tolerance
    {
        get { return _tolerance; }
        set
        {
            _tolerance = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    public IEnumerable<LineSegmentPoint> GetTargetLines(IEnumerable<LineSegmentPoint> lines)
    {
        if (this.TargetAngle > 0)
            lines = lines.Where(x => x.CalculateAngle().IsAngleNear(this.TargetAngle, this.Tolerance)).ToArray();
        return lines;
    }
}

public static class LineSegmentPointExtension
{
    // 计算两点之间的角度（弧度）
    public static float CalculateAngle(this LineSegmentPoint lineSegment)
    {
        OpenCvSharp.Point p1 = lineSegment.P1;
        OpenCvSharp.Point p2 = lineSegment.P2;
        // 计算差值
        float dx = p2.X - p1.X;
        float dy = p2.Y - p1.Y;

        // 使用Math.Atan2计算角度（弧度）
        double angleRad = Math.Atan2(dy, dx);
        // 转换为角度（如果需要）
        double angleDeg = angleRad * 180 / Math.PI;
        return (float)angleDeg;
    }

    // 判断是否接近水平方向(0°或180°±tolerance)
    public static bool IsHorizontal(this LineSegmentPoint lineSegment, float tolerance = 15)
    {
        float angle = lineSegment.CalculateAngle();
        return IsAngleNear(lineSegment, 0, tolerance) ||
               IsAngleNear(lineSegment, 180, tolerance);
    }

    // 判断是否接近垂直方向(90°或270°±tolerance)
    public static bool IsVertical(this LineSegmentPoint lineSegment, float tolerance = 15)
    {
        float angle = lineSegment.CalculateAngle();
        return IsAngleNear(lineSegment, 90, tolerance) ||
               IsAngleNear(lineSegment, 270, tolerance);
    }

    // 判断是否接近45°对角线方向
    public static bool IsDiagonal45(this LineSegmentPoint lineSegment, float tolerance = 15)
    {
        double angle = lineSegment.CalculateAngle();
        return IsAngleNear(lineSegment, 45, tolerance) ||
               IsAngleNear(lineSegment, 225, tolerance);
    }

    // 判断是否接近135°对角线方向
    public static bool IsDiagonal135(this LineSegmentPoint lineSegment, float tolerance = 15)
    {
        double angle = lineSegment.CalculateAngle();
        return IsAngleNear(lineSegment, 135, tolerance) ||
               IsAngleNear(lineSegment, 315, tolerance);
    }

    // 判断角度是否在targetAngle±tolerance范围内
    public static bool IsAngleNear(this LineSegmentPoint lineSegment, float targetAngle, float tolerance = 15)
    {
        float angle = lineSegment.CalculateAngle();
        return angle.IsAngleNear(targetAngle, tolerance);
    }

    public static bool IsAngleNear(this float angle, float targetAngle, float tolerance = 15)
    {
        // 计算角度差(考虑360°循环)
        float diff = Math.Abs(((angle - targetAngle) + 180) % 360 - 180);
        return diff <= tolerance;
    }
}

