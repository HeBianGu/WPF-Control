using OpenCvSharp.XFeatures2D;

namespace H.Controls.Diagram.Extensions.OpenCV;
[Display(Name = "FREAK", GroupName = "特征提取", Order = 0)]
public class FreakFeatureDetector : FeatureDetectorActionNodeDataBase
{
    private int _nFeatures = 1000;
    [Display(Name = "nFeatures", GroupName = "数据")]
    public int nFeatures
    {
        get { return _nFeatures; }
        set
        {
            _nFeatures = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private float _scaleFactor = 1.2f;
    [Display(Name = "ScaleFactor", GroupName = "数据")]
    public float ScaleFactor
    {
        get { return _scaleFactor; }
        set
        {
            _scaleFactor = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _nLevels = 8;
    [Display(Name = "nLevels", GroupName = "数据")]
    public int nLevels
    {
        get { return _nLevels; }
        set
        {
            _nLevels = value;
            DispatcherRaisePropertyChanged();
        }
    }
    private int _edgeThreshold = 31;
    [Display(Name = "EdgeThreshold", GroupName = "数据")]
    public int EdgeThreshold
    {
        get { return _edgeThreshold; }
        set
        {
            _edgeThreshold = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _firstLevel = 0;
    [Display(Name = "FirstLevel", GroupName = "数据")]
    public int FirstLevel
    {
        get { return _firstLevel; }
        set
        {
            _firstLevel = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _wtaK = 2;
    [Display(Name = "WtaK", GroupName = "数据")]
    public int WtaK
    {
        get { return _wtaK; }
        set
        {
            _wtaK = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private ORBScoreType _scoreType = ORBScoreType.Harris;
    [Display(Name = "ScoreType", GroupName = "数据")]
    public ORBScoreType ScoreType
    {
        get { return _scoreType; }
        set
        {
            _scoreType = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private int _patchSize = 31;
    [Display(Name = "PatchSize", GroupName = "数据")]
    public int PatchSize
    {
        get { return _patchSize; }
        set
        {
            _patchSize = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _fastThreshold = 20;
    [Display(Name = "FastThreshold", GroupName = "数据")]
    public int FastThreshold
    {
        get { return _fastThreshold; }
        set
        {
            _fastThreshold = value;
            DispatcherRaisePropertyChanged();
        }
    }



    private bool _orientationNormalized = true;
    [Display(Name = "OrientationNormalized", GroupName = "数据")]
    public bool OrientationNormalized
    {
        get { return _orientationNormalized; }
        set
        {
            _orientationNormalized = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private bool _scaleNormalized = true;
    [Display(Name = "ScaleNormalized", GroupName = "数据")]
    public bool ScaleNormalized
    {
        get { return _scaleNormalized; }
        set
        {
            _scaleNormalized = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private float _patternScale = 22.0f;
    [Display(Name = "PatternScale", GroupName = "数据")]
    public float PatternScale
    {
        get { return _patternScale; }
        set
        {
            _patternScale = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _nOctaves = 4;
    [Display(Name = "nOctaves", GroupName = "数据")]
    public int nOctaves
    {
        get { return _nOctaves; }
        set
        {
            _nOctaves = value;
            DispatcherRaisePropertyChanged();
        }
    }



    protected override IFlowableResult Refresh()
    {
        var filePath = this._srcFilePath;
        using var gray = new Mat(filePath, ImreadModes.Grayscale);
        using var dst = new Mat(filePath, ImreadModes.Color);

        // ORB
        using var orb = ORB.Create(this.nFeatures, this.ScaleFactor, this.nLevels, this.EdgeThreshold, this.FirstLevel, this.WtaK, this.ScoreType, this.PatchSize, this.FastThreshold);
        KeyPoint[] keypoints = orb.Detect(gray);

        // FREAK
        using var freak = FREAK.Create(this.OrientationNormalized, this.ScaleNormalized, this.PatternScale, this.nOctaves);
        Mat freakDescriptors = new Mat();
        freak.Compute(gray, ref keypoints, freakDescriptors);

        if (keypoints != null)
        {
            var color = new Scalar(0, 255, 0);
            foreach (KeyPoint kpt in keypoints)
            {
                float r = kpt.Size / 2;
                Cv2.Circle(dst, (Point)kpt.Pt, (int)r, color);
                Cv2.Line(dst,
                    (Point)new Point2f(kpt.Pt.X + r, kpt.Pt.Y + r),
                    (Point)new Point2f(kpt.Pt.X - r, kpt.Pt.Y - r),
                    color);
                Cv2.Line(dst,
                    (Point)new Point2f(kpt.Pt.X - r, kpt.Pt.Y + r),
                    (Point)new Point2f(kpt.Pt.X + r, kpt.Pt.Y - r),
                    color);
            }
        }
        RefreshMatToView(dst);
        return base.Refresh();
    }
}
