namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Feature;
[Display(Name = "BRISK", GroupName = "特征提取", Order = 0)]
public class BriskFeatureDetector : FeatureOpenCVNodeDataBase
{
    private bool _useRectangle = true;
    [Display(Name = "绘制矩形", GroupName = "数据")]
    public bool UseRectangle
    {
        get { return _useRectangle; }
        set
        {
            _useRectangle = value;
            RaisePropertyChanged();
        }
    }

    private int _threshold = 30;
    [Range(0, 500)]
    [Display(Name = "Threshold", GroupName = "数据")]
    public int Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            RaisePropertyChanged();
        }
    }

    private int _octaves = 3;
    [Range(0, 8)]
    [Display(Name = "Octaves", GroupName = "数据")]
    public int Octaves
    {
        get { return _octaves; }
        set
        {
            _octaves = value;
            RaisePropertyChanged();
        }
    }

    private float _patternScale = 1.0f;
    [Display(Name = "PatternScale", GroupName = "数据")]
    public float PatternScale
    {
        get { return _patternScale; }
        set
        {
            _patternScale = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        string prePath = srcImageNodeData.SrcFilePath;
        Mat gray = new Mat(prePath, ImreadModes.Grayscale);
        Mat dst = new Mat(prePath, ImreadModes.Color);
        using BRISK brisk = BRISK.Create(Threshold, Octaves);
        KeyPoint[] keypoints = brisk.Detect(gray);

        if (keypoints != null)
        {
            Scalar color = new Scalar(0, 255, 0);
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
        return this.OK(dst);
    }
}
