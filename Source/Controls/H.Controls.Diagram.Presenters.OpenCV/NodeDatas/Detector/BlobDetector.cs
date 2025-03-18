using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;
[Display(Name = "Blob检测", GroupName = "基础检测", Description = "用于检测图像中具有相似属性（如颜色、纹理或亮度）的连通区域", Order = 20)]
public class BlobDetector : DetectorOpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        SimpleBlobDetector.Params param = new SimpleBlobDetector.Params();
        CopyFrom(param);
        RefreshData();
    }

    private BlobType _blobType = BlobType.None;
    [Display(Name = "BlobType", GroupName = "数据")]
    public BlobType BlobType
    {
        get { return _blobType; }
        set
        {
            _blobType = value;
            RaisePropertyChanged();
            RefreshData();
        }
    }

    private float _thresholdStep;
    [Display(Name = "ThresholdStep", GroupName = "数据")]
    public float ThresholdStep
    {
        get { return _thresholdStep; }
        set
        {
            _thresholdStep = value;
            RaisePropertyChanged();
        }
    }

    private float _minThreshold;
    [Display(Name = "MinThreshold", GroupName = "数据")]
    public float MinThreshold
    {
        get { return _minThreshold; }
        set
        {
            _minThreshold = value;
            RaisePropertyChanged();
        }
    }

    private float _maxThreshold;
    [Display(Name = "MaxThreshold", GroupName = "数据")]
    public float MaxThreshold
    {
        get { return _maxThreshold; }
        set
        {
            _maxThreshold = value;
            RaisePropertyChanged();
        }
    }

    private uint _minRepeatability;
    [Display(Name = "MinRepeatability", GroupName = "数据")]
    public uint MinRepeatability
    {
        get { return _minRepeatability; }
        set
        {
            _minRepeatability = value;
            RaisePropertyChanged();
        }
    }

    private float _minDistBetweenBlobs;
    [Display(Name = "MinDistBetweenBlobs", GroupName = "数据")]
    public float MinDistBetweenBlobs
    {
        get { return _minDistBetweenBlobs; }
        set
        {
            _minDistBetweenBlobs = value;
            RaisePropertyChanged();
        }
    }

    private bool _filterByColor;
    [Display(Name = "FilterByColor", GroupName = "数据")]
    public bool FilterByColor
    {
        get { return _filterByColor; }
        set
        {
            _filterByColor = value;
            RaisePropertyChanged();
        }
    }

    private byte _blobColor;
    [Display(Name = "BlobColor", GroupName = "数据")]
    public byte BlobColor
    {
        get { return _blobColor; }
        set
        {
            _blobColor = value;
            RaisePropertyChanged();
        }
    }

    private bool _filterByArea;
    [Display(Name = "FilterByArea", GroupName = "数据")]
    public bool FilterByArea
    {
        get { return _filterByArea; }
        set
        {
            _filterByArea = value;
            RaisePropertyChanged();
        }
    }

    private float _minArea;
    [Display(Name = "MinArea", GroupName = "数据")]
    public float MinArea
    {
        get { return _minArea; }
        set
        {
            _minArea = value;
            RaisePropertyChanged();
        }
    }

    private float _maxArea;
    [Display(Name = "MaxArea", GroupName = "数据")]
    public float MaxArea
    {
        get { return _maxArea; }
        set
        {
            _maxArea = value;
            RaisePropertyChanged();
        }
    }

    private bool _filterByCircularity;
    [Display(Name = "FilterByCircularity", GroupName = "数据")]
    public bool FilterByCircularity
    {
        get { return _filterByCircularity; }
        set
        {
            _filterByCircularity = value;
            RaisePropertyChanged();
        }
    }

    private float _minCircularity;
    [Display(Name = "MinCircularity", GroupName = "数据")]
    public float MinCircularity
    {
        get { return _minCircularity; }
        set
        {
            _minCircularity = value;
            RaisePropertyChanged();
        }
    }

    private float _maxCircularity;
    [Display(Name = "MaxCircularity", GroupName = "数据")]
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
    [Display(Name = "FilterByInertia", GroupName = "数据")]
    public bool FilterByInertia
    {
        get { return _filterByInertia; }
        set
        {
            _filterByInertia = value;
            RaisePropertyChanged();
        }
    }

    private float _minInertiaRatio;
    [Display(Name = "MinInertiaRatio", GroupName = "数据")]
    public float MinInertiaRatio
    {
        get { return _minInertiaRatio; }
        set
        {
            _minInertiaRatio = value;
            RaisePropertyChanged();
        }
    }

    private float _maxInertiaRatio;
    [Display(Name = "MaxInertiaRatio", GroupName = "数据")]
    public float MaxInertiaRatio
    {
        get { return _maxInertiaRatio; }
        set
        {
            _maxInertiaRatio = value;
            RaisePropertyChanged();
        }
    }

    private bool _filterByConvexity = true;
    [Display(Name = "FilterByConvexity", GroupName = "数据")]
    public bool FilterByConvexity
    {
        get { return _filterByConvexity; }
        set
        {
            _filterByConvexity = value;
            RaisePropertyChanged();
        }
    }

    private float _minConvexity;
    [Display(Name = "MinConvexity", GroupName = "数据")]
    public float MinConvexity
    {
        get { return _minConvexity; }
        set
        {
            _minConvexity = value;
            RaisePropertyChanged();
        }
    }

    private float _maxConvexity;
    [Display(Name = "MaxConvexity", GroupName = "数据")]
    public float MaxConvexity
    {
        get { return _maxConvexity; }
        set
        {
            _maxConvexity = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat src = from.Mat;
        SimpleBlobDetector.Params param = CopyTo();
        SimpleBlobDetector circleDetector = SimpleBlobDetector.Create(param);
        KeyPoint[] circleKeyPoints = circleDetector.Detect(src);
        Mat detectedCircles = new Mat();
        Cv2.DrawKeypoints(src, circleKeyPoints, detectedCircles, Scalar.HotPink, DrawMatchesFlags.DrawRichKeypoints);
        return this.OK(detectedCircles);
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
}

public enum BlobType
{
    None = 0, Circle = 1, Oval = 2
}
