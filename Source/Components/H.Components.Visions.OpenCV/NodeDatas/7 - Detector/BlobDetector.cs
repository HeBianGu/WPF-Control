// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.NodeDatas.Detector;
[Icon(FontIcons.LargeErase)]
[Display(Name = "Blob识别", GroupName = "基础识别", Description = "SimpleBlobDetector 是 OpenCV 中一个用于检测图像中斑点的类，特别适合检测圆形或近似圆形的区域", Order = 20)]
public class BlobDetector : OpenCVDetectorNodeDataBase, IDetectorGroupableNodeData
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        SimpleBlobDetector.Params param = new SimpleBlobDetector.Params();
        CopyFrom(param);
        RefreshData();
    }

    private BlobType _blobType = BlobType.None;
    [RefreshFilterOnValueChanged]
    [Display(Name = "形状类型", GroupName = VisionPropertyGroupNames.RunParameters)]
    public BlobType BlobType
    {
        get { return _blobType; }
        set
        {
            _blobType = value;
            RaisePropertyChanged();
            RefreshData();
            this.Invoke();
        }
    }

    private float _thresholdStep;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10.0f)]
    [Display(Name = "阈值步长", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float ThresholdStep
    {
        get { return _thresholdStep; }
        set
        {
            _thresholdStep = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _minThreshold;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10.0f)]
    [Display(Name = "最小阈值", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MinThreshold
    {
        get { return _minThreshold; }
        set
        {
            _minThreshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _maxThreshold;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(220f)]
    [Display(Name = "最大阈值", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MaxThreshold
    {
        get { return _maxThreshold; }
        set
        {
            _maxThreshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private uint _minRepeatability;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10.0f)]
    [Display(Name = "最小重复性", GroupName = VisionPropertyGroupNames.RunParameters)]
    public uint MinRepeatability
    {
        get { return _minRepeatability; }
        set
        {
            _minRepeatability = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _minDistBetweenBlobs;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10.0f)]
    [Display(Name = "最小块间距离", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MinDistBetweenBlobs
    {
        get { return _minDistBetweenBlobs; }
        set
        {
            _minDistBetweenBlobs = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _filterByColor = true;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(true)]
    [Display(Name = "按颜色过滤", GroupName = VisionPropertyGroupNames.RunParameters)]
    public bool FilterByColor
    {
        get { return _filterByColor; }
        set
        {
            _filterByColor = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private byte _blobColor;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10.0f)]
    [Display(Name = "颜色", GroupName = VisionPropertyGroupNames.RunParameters)]
    public byte BlobColor
    {
        get { return _blobColor; }
        set
        {
            _blobColor = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _filterByArea = true;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(true)]
    [Display(Name = "按面积过滤", GroupName = VisionPropertyGroupNames.RunParameters)]
    public bool FilterByArea
    {
        get { return _filterByArea; }
        set
        {
            _filterByArea = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _minArea;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(100f)]
    [Display(Name = "最小面积（像素）", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MinArea
    {
        get { return _minArea; }
        set
        {
            _minArea = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _maxArea;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10000f)]
    [Display(Name = "最大面积（像素）", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MaxArea
    {
        get { return _maxArea; }
        set
        {
            _maxArea = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _filterByCircularity = true;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(true)]
    [Display(Name = "按圆形度过滤", GroupName = VisionPropertyGroupNames.RunParameters)]
    public bool FilterByCircularity
    {
        get { return _filterByCircularity; }
        set
        {
            _filterByCircularity = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _minCircularity;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(0.8f)]
    [Display(Name = "最小圆形度", GroupName = VisionPropertyGroupNames.RunParameters, Description = "最小圆形度（1为完美圆）")]
    public float MinCircularity
    {
        get { return _minCircularity; }
        set
        {
            _minCircularity = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _maxCircularity;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10.0f)]
    [Display(Name = "最大圆形度", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MaxCircularity
    {
        get { return _maxCircularity; }
        set
        {
            _maxCircularity = value;
            RaisePropertyChanged();
        }
    }

    private bool _filterByInertia;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(false)]
    [Display(Name = "惯性过滤", GroupName = VisionPropertyGroupNames.RunParameters)]
    public bool FilterByInertia
    {
        get { return _filterByInertia; }
        set
        {
            _filterByInertia = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _minInertiaRatio;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10.0f)]
    [Display(Name = "最小惯性比", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MinInertiaRatio
    {
        get { return _minInertiaRatio; }
        set
        {
            _minInertiaRatio = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _maxInertiaRatio;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10.0f)]
    [Display(Name = "最大惯性比", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MaxInertiaRatio
    {
        get { return _maxInertiaRatio; }
        set
        {
            _maxInertiaRatio = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _filterByConvexity = true;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(true)]
    [Display(Name = "按凸性过滤", GroupName = VisionPropertyGroupNames.RunParameters)]
    public bool FilterByConvexity
    {
        get { return _filterByConvexity; }
        set
        {
            _filterByConvexity = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _minConvexity;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(0.87f)]
    [Display(Name = "最小凸性", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MinConvexity
    {
        get { return _minConvexity; }
        set
        {
            _minConvexity = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _maxConvexity;
    [BindingVisiblableMethodName(nameof(GetNone))]
    [DefaultValue(10.0f)]
    [Display(Name = "最大凸性", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float MaxConvexity
    {
        get { return _maxConvexity; }
        set
        {
            _maxConvexity = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }
    public bool GetNone()
    {
        return this.BlobType == BlobType.None;
    }

    private void RefreshData()
    {
        if (this.BlobType == BlobType.Circle)
        {
            SimpleBlobDetector.Params p = new SimpleBlobDetector.Params
            {
                MinThreshold = 10,
                MaxThreshold = 230,

                // The area is the number of pixels in the blob.
                FilterByArea = true,
                MinArea = 500,
                MaxArea = 50000,

                // Circularity is a ratio of the area to the perimeter. Polygons with more sides are more circular.
                FilterByCircularity = true,
                MinCircularity = 0.9f,

                // Convexity is the ratio of the area of the blob to the area of its convex hull.
                FilterByConvexity = true,
                MinConvexity = 0.95f,

                // A circle's inertia ratio is 1. A line's is 0. An oval is between 0 and 1.
                FilterByInertia = true,
                MinInertiaRatio = 0.95f
            };

            CopyFrom(p);
        }

        if (this.BlobType == BlobType.Oval)
        {
            SimpleBlobDetector.Params p = new SimpleBlobDetector.Params
            {
                MinThreshold = 10,
                MaxThreshold = 230,
                FilterByArea = true,
                MinArea = 500,
                // The ovals are the smallest blobs in Shapes, so we limit the max area to eliminate the larger blobs.
                MaxArea = 10000,
                FilterByCircularity = true,
                MinCircularity = 0.58f,
                FilterByConvexity = true,
                MinConvexity = 0.96f,
                FilterByInertia = true,
                MinInertiaRatio = 0.1f
            };

            CopyFrom(p);
        }
    }

    private void CopyFrom(SimpleBlobDetector.Params @params)
    {
        System.Reflection.PropertyInfo[] ps = @params.GetType().GetProperties();
        System.Reflection.PropertyInfo[] tps = GetType().GetProperties();

        foreach (System.Reflection.PropertyInfo p in ps)
        {
            System.Reflection.PropertyInfo f = tps.FirstOrDefault(x => x.Name == p.Name && x.PropertyType == p.PropertyType);
            if (f == null)
                continue;
            if (!f.CanWrite)
                continue;
            f.SetValue(this, p.GetValue(@params));
        }
    }

    private SimpleBlobDetector.Params CopyTo()
    {
        SimpleBlobDetector.Params @params = new SimpleBlobDetector.Params();
        System.Reflection.PropertyInfo[] ps = @params.GetType().GetProperties();
        System.Reflection.PropertyInfo[] tps = GetType().GetProperties();

        foreach (System.Reflection.PropertyInfo p in ps)
        {
            System.Reflection.PropertyInfo f = tps.FirstOrDefault(x => x.Name == p.Name && x.PropertyType == p.PropertyType);
            if (f == null)
                continue;
            if (!f.CanRead)
                continue;
            p.SetValue(@params, f.GetValue(this));
        }

        return @params;
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        var resultImage = this.GetExpressionResultImage(fromImage.ToMatImage()).ToMatImage();
        SimpleBlobDetector.Params param = CopyTo();
        SimpleBlobDetector circleDetector = SimpleBlobDetector.Create(param);
        KeyPoint[] circleKeyPoints = circleDetector.Detect(fromImage);

        if (this.CaliperShape is RectShape caliper)
            circleKeyPoints = circleKeyPoints.Where(x => caliper.Rect.Contains(x.Pt.ToPoint().ToPoint())).ToArray();
        //if (roi.HasValue)
        //{
        //    for (int i = 0; i < circleKeyPoints.Length; i++)
        //    {
        //        var pt = circleKeyPoints[i].Pt;
        //        circleKeyPoints[i].Pt = pt.Add(new Point2f(roi.Value.X, roi.Value.Y));
        //    }
        //}
        Mat detectedCircles = new Mat();
        Cv2.DrawKeypoints(resultImage.Mat, circleKeyPoints, detectedCircles, VisionSettings.Instance.OutputColor.ToScalar(), DrawMatchesFlags.DrawRichKeypoints);
        this.MatchingCountResult = circleKeyPoints.Length;
        var resultPresenter = circleKeyPoints.ToResultPresenter();
        return this.OK(detectedCircles, resultPresenter, this.MatchingCountResult.ToDetectSuccessMessage());
    }
}

public enum BlobType
{
    Circle, Oval, None
}
