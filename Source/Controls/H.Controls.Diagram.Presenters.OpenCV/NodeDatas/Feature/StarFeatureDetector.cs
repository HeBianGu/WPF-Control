using OpenCvSharp.XFeatures2D;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Feature;
[Display(Name = "STAR", GroupName = "特征提取", Order = 0)]
public class StarFeatureDetector : FeatureOpenCVNodeDataBase
{
    private int _maxSize = 45;
    [Display(Name = "MaxSize", GroupName = "数据")]
    public int MaxSize
    {
        get { return _maxSize; }
        set
        {
            _maxSize = value;
            RaisePropertyChanged();
        }
    }

    private int _responseThreshold = 30;
    [Display(Name = "ResponseThreshold", GroupName = "数据")]
    public int ResponseThreshold
    {
        get { return _responseThreshold; }
        set
        {
            _responseThreshold = value;
            RaisePropertyChanged();
        }
    }

    private int _lineThresholdProjected = 10;
    [Display(Name = "LineThresholdProjected", GroupName = "数据")]
    public int LineThresholdProjected
    {
        get { return _lineThresholdProjected; }
        set
        {
            _lineThresholdProjected = value;
            RaisePropertyChanged();
        }
    }

    private int _lineThresholdBinarized = 8;
    [Display(Name = "LineThresholdBinarized", GroupName = "数据")]
    public int LineThresholdBinarized
    {
        get { return _lineThresholdBinarized; }
        set
        {
            _lineThresholdBinarized = value;
            RaisePropertyChanged();
        }
    }

    private int _suppressNonmaxSize = 5;
    [Display(Name = "SuppressNonmaxSize", GroupName = "数据")]
    public int SuppressNonmaxSize
    {
        get { return _suppressNonmaxSize; }
        set
        {
            _suppressNonmaxSize = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        string path = srcImageNodeData.SrcFilePath;
        Mat dst = new Mat(path, ImreadModes.Color);
        Mat gray = from.Mat;

        StarDetector detector = StarDetector.Create(this.MaxSize, this.ResponseThreshold, this.LineThresholdProjected, this.LineThresholdBinarized, this.SuppressNonmaxSize);
        KeyPoint[] keypoints = detector.Detect(gray);

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
