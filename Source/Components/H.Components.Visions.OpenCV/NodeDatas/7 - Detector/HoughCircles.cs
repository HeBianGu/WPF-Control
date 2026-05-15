// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.NodeDatas.Detector;
[Icon(FontIcons.LargeErase)]
[Display(Name = "圆查找", GroupName = "查找", Order = 1, Description = "用于检测图像中圆形的函数")]
public class HoughCircles : OpenCVDetectorNodeDataBase, IDetectorGroupableNodeData
{
    private HoughModes _HoughModes = HoughModes.Gradient;
    [DefaultValue(HoughModes.GradientAlt)]
    [Display(Name = "类型", GroupName = VisionPropertyGroupNames.RunParameters, Description = "类型")]
    public HoughModes HoughModes
    {
        get { return _HoughModes; }
        set
        {
            _HoughModes = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _dp = 1.0;
    [DefaultValue(1.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0.01, 100.0)]
    [Display(Name = "累加器反比", GroupName = VisionPropertyGroupNames.RunParameters, Description = "累加器分辨率与图像分辨率的反比 (推荐值: 1)")]
    public double dp
    {
        get { return _dp; }
        set
        {
            _dp = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _minDist;
    [DefaultValue(50.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(1, 10000.0)]
    [Display(Name = "圆间最小距离", GroupName = VisionPropertyGroupNames.RunParameters, Description = " 检测到的圆心之间的最小距离 (根据图像大小调整)")]
    public double minDist
    {
        get { return _minDist; }
        set
        {
            _minDist = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _param1;
    [DefaultValue(100.0)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0.1, 200.0)]
    [Display(Name = "边缘检测阈值", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用于边缘检测的高阈值 (通常设为100-200)")]
    public double param1
    {
        get { return _param1; }
        set
        {
            _param1 = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _param2;
    [DefaultValue(0.1)]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0.1, 100.0)]
    [Display(Name = "累加器阈值", GroupName = VisionPropertyGroupNames.RunParameters, Description = "累加器阈值，值越小检测到的假圆越多 (通常设为20-50)")]
    public double param2
    {
        get { return _param2; }
        set
        {
            _param2 = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _minRadius;
    [DefaultValue(0)]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(0, 10000)]
    [Display(Name = "最小半径", GroupName = VisionPropertyGroupNames.RunParameters, Description = "要检测的圆的最小半径 (0表示不限制)")]
    public int minRadius
    {
        get { return _minRadius; }
        set
        {
            _minRadius = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _maxRadius;
    [DefaultValue(0)]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(0, 10000)]
    [Display(Name = "最大半径", GroupName = VisionPropertyGroupNames.RunParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public int maxRadius
    {
        get { return _maxRadius; }
        set
        {
            _maxRadius = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private CircleSearchType _CircleType;
    [DefaultValue(CircleSearchType.All)]
    [Display(Name = "圆筛选方式", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用于从检测到的多个圆中筛选输出结果（全部/最小半径/最大半径/卡尺筛选）")]
    public CircleSearchType CircleType
    {
        get { return _CircleType; }
        set
        {
            _CircleType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private VisionCircle _ResultVisionCircle = VisionCircle.Empty;
    [Expressionable]
    [Display(Name = "圆结果", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "要检测的圆的最大半径 (0表示不限制)")]
    public VisionCircle ResultVisionCircle
    {
        get { return _ResultVisionCircle; }
        set
        {
            _ResultVisionCircle = value;
            RaisePropertyChanged();
        }
    }

    private DetectDisplayMode _DetectDisplayMode = DetectDisplayMode.Dimension;
    [DefaultValue(DetectDisplayMode.Dimension)]
    [Display(Name = "绘制结果方式", GroupName = VisionPropertyGroupNames.DisplayParameters)]
    public DetectDisplayMode DetectDisplayMode
    {
        get { return _DetectDisplayMode; }
        set
        {
            _DetectDisplayMode = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }
    protected override IShape CreateCaliperShape(Mat fromImage)
    {
        var min = Math.Min(fromImage.Width, fromImage.Height) / 4;
        return new CaliperCircleShape()
        {
            Center = new System.Windows.Point(min * 2, min * 2),
            FromRadius = min,
            ToRadius = min + min * 0.5,
        };
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat gray = fromImage;
        CircleSegment[] circles = Cv2.HoughCircles(gray, this.HoughModes, this.dp, this.minDist, this.param1, this.param2, this.minRadius, this.maxRadius);
        var resultImage = this.GetExpressionResultImage(fromImage.ToMatImage()).ToMatImage();
        var color = VisionSettings.Instance.OutputColor.ToScalar();

        var caliper = this.CaliperShape as CaliperCircleShape;
        circles = this.CircleType switch
        {
            CircleSearchType.All => circles,
            CircleSearchType.MinRadius => circles.OrderBy(x => x.Radius).Take(1).ToArray(),
            CircleSearchType.MaxRadius => circles.OrderByDescending(x => x.Radius).Take(1).ToArray(),
            CircleSearchType.Caliper => (caliper != null)
                ? circles.Where(x => caliper.Contains(x.Center.ToPoint().ToPoint(), x.Radius)).ToArray()
                : circles,
            _ => circles,
        };

        var shapes = circles.Select(x =>
        {
            var r = new CircleShape(x.Center.ToPoint().ToPoint(), x.Radius) { Title = this.Name };
            if (this.GetScalerNodeData() is IScalerNodeData scaler)
                r.DimensionText = scaler.GetWorldDistance(x.Radius);
            return r;
        });

        if (this.DetectDisplayMode == DetectDisplayMode.Dimension)
        {
            this.ResultShapes = shapes.OfType<IShape>().ToObservable();
        }
        else if (this.DetectDisplayMode == DetectDisplayMode.Default)
        {
            this.ResultShapes = shapes.OfType<IShape>().ToObservable();
        }
        else
        {
            foreach (var circle in shapes)
            {
                // 绘制圆心
                Cv2.Circle(resultImage.Mat, circle.Center.ToCVPoint(), resultImage.Mat.ToThickness(), color, -1);
                // 绘制圆轮廓
                Cv2.Circle(resultImage.Mat, circle.Center.ToCVPoint(), (int)circle.Radius, color, resultImage.Mat.ToThickness());
            }
        }
        this.ResultImages = shapes.Select(x => x.BoundingBox.ToCVRect()).ToResultImages(resultImage.Mat).ToList();
        this.FirstResultImage = this.ResultImages.FirstOrDefault()?.Image;
        this.MatchingCountResult = circles.Count();
        var first = circles.FirstOrDefault();
        this.ResultVisionCircle = new VisionCircle(first.Center.ToPoint().ToPoint(), first.Radius);
        IResultPresenter resultPresenter = shapes.ToResultPresenter();
        return this.OK(resultImage, resultPresenter, this.MatchingCountResult.ToDetectSuccessMessage());
    }
}

public enum CircleSearchType
{
    [Display(Name = "全部", Description = "保留检测到的所有圆")]
    All = 0,
    [Display(Name = "最小半径", Description = "从检测到的圆中选择半径最小的圆")]
    MinRadius = 1,
    [Display(Name = "最大半径", Description = "从检测到的圆中选择半径最大的圆")]
    MaxRadius = 2,
    [Display(Name = "卡尺筛选", Description = "按卡尺区域筛选圆")]
    Caliper = 3,
}