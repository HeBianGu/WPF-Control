namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Feature;
[Display(Name = "MSER", GroupName = "特征提取", Order = 0)]
public class MserFeatureDetector : FeatureOpenCVNodeDataBase
{
    private int _delta = 5;
    [Display(Name = "Delta", GroupName = "数据")]
    public int Delta
    {
        get { return _delta; }
        set
        {
            _delta = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _minArea = 60;
    [Display(Name = "MinArea", GroupName = "数据")]
    public int MinArea
    {
        get { return _minArea; }
        set
        {
            _minArea = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _maxArea = 14400;
    [Display(Name = "MaxArea", GroupName = "数据")]
    public int MaxArea
    {
        get { return _maxArea; }
        set
        {
            _maxArea = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private double _maxVariation = 0.25;
    [Display(Name = "MaxVariation", GroupName = "数据")]
    public double MaxVariation
    {
        get { return _maxVariation; }
        set
        {
            _maxVariation = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private double _minDiversity = 0.2;
    [Display(Name = "MinDiversity", GroupName = "数据")]
    public double MinDiversity
    {
        get { return _minDiversity; }
        set
        {
            _minDiversity = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _maxEvolution = 200;
    [Display(Name = "MaxEvolution", GroupName = "数据")]
    public int MaxEvolution
    {
        get { return _maxEvolution; }
        set
        {
            _maxEvolution = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private double _areaThreshold = 1.01;
    [Display(Name = "AreaThreshold", GroupName = "数据")]
    public double AreaThreshold
    {
        get { return _areaThreshold; }
        set
        {
            _areaThreshold = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private double _minMargin = 0.003;
    [Display(Name = "MinMargin", GroupName = "数据")]
    public double MinMargin
    {
        get { return _minMargin; }
        set
        {
            _minMargin = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _edgeBlurSize = 5;
    [Display(Name = "EdgeBlurSize", GroupName = "数据")]
    public int EdgeBlurSize
    {
        get { return _edgeBlurSize; }
        set
        {
            _edgeBlurSize = value;
            DispatcherRaisePropertyChanged();
        }
    }

    protected override IFlowableResult Invoke()
    {
        Mat gray = this.PreviourMat;
        Mat dst = new Mat(this.SrcFilePath, ImreadModes.Color);
        MSER mser = MSER.Create(this.Delta, this.MinArea, this.MaxArea, this.MaxVariation, this.MinDiversity, this.MaxEvolution, this.AreaThreshold, this.MinMargin, this.EdgeBlurSize);
        mser.DetectRegions(gray, out Point[][] contours, out _);
        foreach (Point[] pts in contours)
        {
            Scalar color = Scalar.RandomColor();
            foreach (Point p in pts)
            {
                dst.Circle(p, 1, color);
            }
        }
        this.Mat = dst;
        UpdateMatToView();
        return base.Invoke();
    }
}
