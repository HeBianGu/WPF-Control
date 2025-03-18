using OpenCvSharp.Features2D;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Feature;
[Display(Name = "SIFT", GroupName = "特征提取", Order = 0)]
public class SiftFeatureDetector : FeatureOpenCVNodeDataBase
{
    public SiftFeatureDetector()
    {
        this.DetectFilePath = this.GetDataPath(ImagePath.Match1);
    }

    //protected override void OpenFilePath(string name)
    //{
    //    this.DetectFilePath = name;
    //}

    private string _detectFilePath;
    [Display(Name = "检测图片路径", GroupName = "数据")]
    public string DetectFilePath
    {
        get { return _detectFilePath; }
        set
        {
            _detectFilePath = value;
            RaisePropertyChanged();
        }
    }

    private MatcherType _matcherType;
    [Display(Name = "MatcherType", GroupName = "数据")]
    public MatcherType MatcherType
    {
        get { return _matcherType; }
        set
        {
            _matcherType = value;
            RaisePropertyChanged();
        }
    }

    private int _nFeatures = 0;
    [Display(Name = "nFeatures", GroupName = "数据")]
    public int nFeatures
    {
        get { return _nFeatures; }
        set
        {
            _nFeatures = value;
            RaisePropertyChanged();
        }
    }

    private int _nOctaveLayers = 3;
    [Display(Name = "nOctaveLayers", GroupName = "数据")]
    public int nOctaveLayers
    {
        get { return _nOctaveLayers; }
        set
        {
            _nOctaveLayers = value;
            RaisePropertyChanged();
        }
    }

    private double _contrastThreshold = 0.04;
    [Display(Name = "ContrastThreshold", GroupName = "数据")]
    public double ContrastThreshold
    {
        get { return _contrastThreshold; }
        set
        {
            _contrastThreshold = value;
            RaisePropertyChanged();
        }
    }

    private double _edgeThreshold = 10;
    [Display(Name = "EdgeThreshold", GroupName = "数据")]
    public double EdgeThreshold
    {
        get { return _edgeThreshold; }
        set
        {
            _edgeThreshold = value;
            RaisePropertyChanged();
        }
    }

    private double _sigma = 1.6;
    [Display(Name = "Sigma", GroupName = "数据")]
    public double Sigma
    {
        get { return _sigma; }
        set
        {
            _sigma = value;
            RaisePropertyChanged();
        }
    }
    private NormTypes _normTypes = NormTypes.L2;
    [Display(Name = "NormType", GroupName = "数据")]
    public NormTypes NormType
    {
        get { return _normTypes; }
        set
        {
            _normTypes = value;
            RaisePropertyChanged();
        }
    }

    private bool _crossCheck = false;
    [Display(Name = "CrossCheck", GroupName = "数据")]
    public bool CrossCheck
    {
        get { return _crossCheck; }
        set
        {
            _crossCheck = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat src1 = new Mat(this.DetectFilePath, ImreadModes.Color);
        Mat src2 = from.Mat;
        Mat gray1 = new Mat();
        Mat gray2 = new Mat();
        Cv2.CvtColor(src1, gray1, ColorConversionCodes.BGR2GRAY);
        Cv2.CvtColor(src2, gray2, ColorConversionCodes.BGR2GRAY);

        SIFT sift = SIFT.Create(this.nFeatures, this.nOctaveLayers, this.ContrastThreshold, this.EdgeThreshold, this.Sigma);

        // Detect the keypoints and generate their descriptors using SIFT
        Mat<float> descriptors1 = new Mat<float>();
        Mat<float> descriptors2 = new Mat<float>();
        sift.DetectAndCompute(gray1, null, out KeyPoint[] keypoints1, descriptors1);
        sift.DetectAndCompute(gray2, null, out KeyPoint[] keypoints2, descriptors2);

        // Match descriptor vectors
        if (this.MatcherType == MatcherType.BFMatcher)
        {
            BFMatcher bfMatcher = new BFMatcher(this.NormType, this.CrossCheck);
            DMatch[] bfMatches = bfMatcher.Match(descriptors1, descriptors2);
            // Draw matches
            Mat bfView = new Mat();
            Cv2.DrawMatches(gray1, keypoints1, gray2, keypoints2, bfMatches, bfView);
            this.Mat = bfView;
            this.OK(bfView);
        }
        if (this.MatcherType == MatcherType.FlannBasedMatcher)
        {
            FlannBasedMatcher flannMatcher = new FlannBasedMatcher();
            DMatch[] flannMatches = flannMatcher.Match(descriptors1, descriptors2);
            // Draw matches
            Mat flannView = new Mat();
            Cv2.DrawMatches(gray1, keypoints1, gray2, keypoints2, flannMatches, flannView);
            this.Mat = flannView;
            this.OK(flannView);
        }
        return this.Error(null);
    }
}

public enum MatcherType
{
    BFMatcher = 0, FlannBasedMatcher
}
