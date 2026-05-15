// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.VisionMaster.ResultPresenter;
using Size = OpenCvSharp.Size;

namespace H.Components.Visions.OpenCV.NodeDatas.Detector;

public enum PointSearchType
{
    [Display(Name = "全部", Description = "保留检测到的所有线段")]
    All,
    [Display(Name = "卡尺筛选", Description = "按卡尺区域筛选线段")]
    Caliper
}

public abstract class PointDetectorNodeDataBase : OpenCVDetectorNodeDataBase, IDetectorGroupableNodeData
{
    private System.Windows.Point _ResultPoint;
    [Expressionable]
    [Display(Name = "角点结果", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "查找到的点数据")]
    public System.Windows.Point ResultPoint
    {
        get { return _ResultPoint; }
        set
        {
            _ResultPoint = value;
            RaisePropertyChanged();
        }
    }

    private PointSearchType _SearchType;
    [DefaultValue(PointSearchType.All)]
    [Display(Name = "角点筛选方式", GroupName = VisionPropertyGroupNames.RunParameters, Description = "All:不过滤；Caliper:按卡尺区域过滤")]
    public PointSearchType SearchType
    {
        get { return _SearchType; }
        set
        {
            _SearchType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }



    protected override IShape CreateCaliperShape(Mat fromImage)
    {
        var min = Math.Min(fromImage.Width, fromImage.Height) / 4;
        return new CaliperPointShape()
        {
            Center = new System.Windows.Point(min * 2, min * 2),
            Radius = min,
            UseHandle = true,
            Stroke = Brushes.LightSkyBlue
        };
    }
}

[Icon(FontIcons.LargeErase)]
[Display(Name = "角点查找(Shi-Tomasi)", GroupName = "查找", Description = "基于GoodFeaturesToTrack(Shi-Tomasi)的角点检测，可配置最大角点数、质量等级、最小距离，并可选亚像素细化", Order = 21)]
public class CornerSubPix : PointDetectorNodeDataBase, IDetectorGroupableNodeData
{
    private int _maxCorners = 500;
    [DefaultValue(500)]
    [Range(1, 1000)]
    [Display(Name = "最大角点数", GroupName = VisionPropertyGroupNames.RunParameters, Description = "返回的最大角点数量")]
    public int MaxCorners
    {
        get { return _maxCorners; }
        set
        {
            _maxCorners = Math.Max(1, value);
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _qualityLevel = 0.03;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [DefaultValue(0.03)]
    [Range(0.003, 1.0)]
    [Display(Name = "质量等级", GroupName = VisionPropertyGroupNames.RunParameters, Description = "角点质量阈值，较大值更严格")]
    public double QualityLevel
    {
        get { return _qualityLevel; }
        set
        {
            _qualityLevel = Math.Max(0.0, Math.Min(1.0, value));
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _minDistance = 10.0;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [DefaultValue(10.0)]
    [Range(0.0, 500.0)]
    [Display(Name = "最小距离", GroupName = VisionPropertyGroupNames.RunParameters, Description = "两个角点之间的最小像素距离")]
    public double MinDistance
    {
        get { return _minDistance; }
        set
        {
            _minDistance = Math.Max(0.0, value);
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _blockSize = 9;
    [DefaultValue(9)]
    [Range(1, 50)]
    [Display(Name = "Block 大小", GroupName = VisionPropertyGroupNames.RunParameters, Description = "用于计算梯度协方差矩阵的邻域大小")]
    public int BlockSize
    {
        get { return _blockSize; }
        set
        {
            _blockSize = Math.Max(1, value);
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _refineSubPix = true;
    [RefreshFilterOnValueChanged]
    [DefaultValue(true)]
    [Display(Name = "亚像素细化", GroupName = VisionPropertyGroupNames.RunParameters, Description = "是否使用 CornerSubPix 对角点位置进行细化")]
    public bool RefineSubPix
    {
        get { return _refineSubPix; }
        set
        {
            _refineSubPix = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _subPixWinSize = 5;
    [BindingVisibleablePropertyName(nameof(RefineSubPix))]
    [DefaultValue(5)]
    [Range(1, 50)]
    [Display(Name = "SubPix 窗口大小", GroupName = VisionPropertyGroupNames.RunParameters, Description = "CornerSubPix 搜索窗口大小的一维长度")]
    public int SubPixWinSize
    {
        get { return _subPixWinSize; }
        set
        {
            _subPixWinSize = Math.Max(1, value);
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        var resultImage = this.GetExpressionResultImage(fromImage.ToMatImage()).ToMatImage();
        Mat gray = new Mat();
        if (fromImage.Channels() == 3 || fromImage.Channels() == 4)
            Cv2.CvtColor(fromImage, gray, ColorConversionCodes.BGR2GRAY);
        else
            gray = fromImage.Clone();

        using Mat gray8 = new Mat();
        gray.ConvertTo(gray8, MatType.CV_8U);
        gray.Dispose();

        Point2f[] corners = Cv2.GoodFeaturesToTrack(
            src: gray8,
            maxCorners: this.MaxCorners,
            qualityLevel: this.QualityLevel,
            minDistance: this.MinDistance,
            mask: null,
            blockSize: this.BlockSize,
            useHarrisDetector: false,
            k: 0.04);

        List<Point2f> pts = corners?.ToList() ?? new List<Point2f>();
        if (this.RefineSubPix && pts.Count > 0)
        {
            TermCriteria criteria = new TermCriteria(CriteriaTypes.Eps | CriteriaTypes.MaxIter, 40, 0.001);
            Cv2.CornerSubPix(gray8, pts, new Size(this.SubPixWinSize, this.SubPixWinSize), new Size(-1, -1), criteria);
        }
        var caliper = this.CaliperShape as CaliperPointShape;
        pts = this.SearchType switch
        {
            PointSearchType.All => pts,
            PointSearchType.Caliper => (caliper != null)
                ? pts.Where(x => caliper.Contains(x.ToPoint().ToPoint())).ToList()
                : pts,
            _ => pts,
        };

        List<IShape> shapes = new List<IShape>();
        foreach (Point2f corner in pts)
        {
            var center = new OpenCvSharp.Point((int)Math.Round(corner.X), (int)Math.Round(corner.Y));
            //Cv2.Circle(colorDst, center, this.PointRadius, new Scalar(0, 255, 0), -1);
            shapes.Add(new PointShape(center.ToPoint()) { Title = this.Name });
        }
        this.MatchingCountResult = pts.Count;
        this.ResultShapes = shapes.ToObservable();
        this.ResultPresenter = shapes.OfType<PointShape>().ToResultPresenter();
        this.ResultPoint = pts.Count > 0 ? pts[0].ToPoint().ToPoint() : default;
        return this.OK(resultImage);
    }

    protected override IShape CreateCaliperShape(Mat fromImage)
    {
        return base.CreateCaliperShape(fromImage);
    }
}


