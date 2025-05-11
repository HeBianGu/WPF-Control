// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;

//需要检测实际图像中的有限长度线段
//处理时间要求较高
//需要过滤短小的噪声线段
//大多数实际应用场景
[Display(Name = "直线概率检测", GroupName = "基础检测", Order = 2, Description = "检测有限长度的线段(x1,y1,x2,y2坐标)")]
public class HoughLinesP : HoughLinesPBase
{
    private double _rho = 1.0;
    [Display(Name = "距离分辨率（像素）", GroupName = "数据", Description = "距离分辨率（像素），增大可减少计算量但降低精度")]
    public double Rho
    {
        get { return _rho; }
        set
        {
            _rho = value;
            RaisePropertyChanged();
        }
    }

    private double _theta = 180;
    [Display(Name = "角度分辨率（角度）", GroupName = "数据", Description = "减小可提高角度精度但增加计算量")]
    public double Theta
    {
        get { return _theta; }
        set
        {
            _theta = value;
            RaisePropertyChanged();
        }
    }

    private int _threshold = 50;
    [Display(Name = "投票阈值", GroupName = "数据", Description = "决定被认定为直线的最小票数,值越大检测到的直线越少但更显著")]
    public int Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            RaisePropertyChanged();
        }
    }

    private double _minLineLength = 50;
    [Display(Name = "最小线段长度", GroupName = "数据", Description = "线段的最小长度，小于此值的线段将被拒绝，有助于过滤掉短小的噪声线段")]
    public double MinLineLength
    {
        get { return _minLineLength; }
        set
        {
            _minLineLength = value;
            RaisePropertyChanged();
        }
    }

    private double _maxLineGap = 10.0;
    [Display(Name = "最大允许间隙", GroupName = "数据", Description = "同一直线上点之间的最大允许间隙，如果两点间的间隙小于此值，它们将被视为同一直线的一部分")]
    public double MaxLineGap
    {
        get { return _maxLineGap; }
        set
        {
            _maxLineGap = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat imgGray = from.Mat;
        Mat imgStd = new Mat(srcImageNodeData.SrcFilePath, ImreadModes.Color);
        Mat imgProb = imgStd.Clone();
        LineSegmentPoint[] lines = Cv2.HoughLinesP(imgGray, Rho, Math.PI / Theta, Threshold, MinLineLength, MaxLineGap);
        lines = this.GetTargetLines(lines).ToArray();
        foreach (LineSegmentPoint s in lines)
        {
            imgProb.Line(s.P1, s.P2, this.OutputColor.ToScalar(), this.OutPutThickness, LineTypes.AntiAlias, 0);
        }

        return this.OK(imgProb);
    }
}

public abstract class HoughLinesPBase : DetectorOpenCVNodeDataBase
{
    private float _targetAngle = -1;
    [Display(Name = "目标角度", GroupName = "数据", Description = "同一直线上点之间的最大允许间隙，如果两点间的间隙小于此值，它们将被视为同一直线的一部分")]
    public float TargetAngle
    {
        get { return _targetAngle; }
        set
        {
            _targetAngle = value;
            RaisePropertyChanged();
        }
    }

    private float _tolerance = 15;
    [Display(Name = "角度容差", GroupName = "数据", Description = "同一直线上点之间的最大允许间隙，如果两点间的间隙小于此值，它们将被视为同一直线的一部分")]
    public float Tolerance
    {
        get { return _tolerance; }
        set
        {
            _tolerance = value;
            RaisePropertyChanged();
        }
    }

    public IEnumerable<LineSegmentPoint> GetTargetLines(IEnumerable<LineSegmentPoint> lines)
    {
        if (this.TargetAngle > 0)
            lines = lines.Where(x => x.CalculateAngle().IsAngleNear(this.TargetAngle, this.Tolerance)).ToArray();
        return lines;
    }

    //public IEnumerable<LineSegmentPoint> GetSobelHorizontalLines(Mat imgGray, IEnumerable<LineSegmentPoint> lines)
    //{
    //    // 使用Sobel算子获取梯度方向
    //    Mat dx = new Mat(), dy = new Mat();
    //    Cv2.Sobel(imgGray, dx, MatType.CV_32F, 1, 0);
    //    Cv2.Sobel(imgGray, dy, MatType.CV_32F, 0, 1);

    //    // 计算梯度方向(角度)
    //    Mat angles = new Mat();
    //    Cv2.Phase(dx, dy, angles, angleInDegrees: true);

    //    var horizontalLines = new List<LineSegmentPoint>();
    //    var verticalLines = new List<LineSegmentPoint>();
    //    foreach (var line in lines)
    //    {
    //        // 获取直线中点附近的梯度方向
    //        Point center = new Point((line.P1.X + line.P2.X) / 2, (line.P1.Y + line.P2.Y) / 2);
    //        float angle = angles.At<float>(center.Y, center.X);

    //        if (line.IsAngleNear(angle, 15))
    //            yield return line;

    //        //// 水平线检测 (角度接近0或180度)
    //        //if (Math.Abs(angle) < 5 || Math.Abs(angle) > 165)
    //        //{
    //        //    horizontalLines.Add(line);
    //        //}
    //        //// 垂直线检测 (角度接近90或-90度)
    //        //else if (Math.Abs(angle) > 85 && Math.Abs(angle) < 95)
    //        //{
    //        //    verticalLines.Add(line);
    //        //}
    //    }

    //    //foreach (LineSegmentPoint s in horizontalLines)
    //    //{
    //    //    imgProb.Line(s.P1, s.P2, Colors.Green.ToScalar(), this.OutPutThickness, LineTypes.AntiAlias, 0);
    //    //}

    //    //foreach (LineSegmentPoint s in verticalLines)
    //    //{
    //    //    imgProb.Line(s.P1, s.P2, Colors.Orange.ToScalar(), this.OutPutThickness, LineTypes.AntiAlias, 0);
    //    //}
    //}

    //public IEnumerable<LineSegmentPoint> GetSobelHorizontalLines(Mat imgGray, IEnumerable<LineSegmentPoint> lines)
    //{
    //    // 使用Sobel算子获取梯度方向
    //    Mat dx = new Mat(), dy = new Mat();
    //    Cv2.Sobel(imgGray, dx, MatType.CV_32F, 1, 0);
    //    Cv2.Sobel(imgGray, dy, MatType.CV_32F, 0, 1);

    //    // 计算梯度方向(角度)
    //    Mat angles = new Mat();
    //    Cv2.Phase(dx, dy, angles, angleInDegrees: true);

    //    var horizontalLines = new List<LineSegmentPoint>();
    //    var verticalLines = new List<LineSegmentPoint>();
    //    foreach (var line in lines)
    //    {
    //        // 获取直线中点附近的梯度方向
    //        Point center = new Point((line.P1.X + line.P2.X) / 2, (line.P1.Y + line.P2.Y) / 2);
    //        float angle = angles.At<float>(center.Y, center.X);
    //        if (angle.IsAngleNear(0.0f, 15.0f))
    //            yield return line;

    //        //// 水平线检测 (角度接近0或180度)
    //        //if (Math.Abs(angle) < 5 || Math.Abs(angle) > 165)
    //        //{
    //        //    horizontalLines.Add(line);
    //        //}
    //        //// 垂直线检测 (角度接近90或-90度)
    //        //else if (Math.Abs(angle) > 85 && Math.Abs(angle) < 95)
    //        //{
    //        //    verticalLines.Add(line);
    //        //}
    //    }

    //    //foreach (LineSegmentPoint s in horizontalLines)
    //    //{
    //    //    imgProb.Line(s.P1, s.P2, Colors.Green.ToScalar(), this.OutPutThickness, LineTypes.AntiAlias, 0);
    //    //}

    //    //foreach (LineSegmentPoint s in verticalLines)
    //    //{
    //    //    imgProb.Line(s.P1, s.P2, Colors.Orange.ToScalar(), this.OutPutThickness, LineTypes.AntiAlias, 0);
    //    //}
    //}
}

public static class LineSegmentPointExtension
{
    // 计算两点之间的角度（弧度）
    public static float CalculateAngle(this LineSegmentPoint lineSegment)
    {
        Point p1 = lineSegment.P1;
        Point p2 = lineSegment.P2;
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

