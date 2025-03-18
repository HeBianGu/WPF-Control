namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Feature;
[Display(Name = "FAST", GroupName = "特征提取", Order = 0)]
public class FastFeatureDetector : FeatureOpenCVNodeDataBase
{
    private bool _nonmaxSupression = true;
    [Display(Name = "NonmaxSupression", GroupName = "数据")]
    public bool NonmaxSupression
    {
        get { return _nonmaxSupression; }
        set
        {
            _nonmaxSupression = value;
            RaisePropertyChanged();
        }
    }

    private int _threshold;
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

    //public override IFlowableResult Invoke(Part previors, Node diagram)
    //{
    //    var imgSrc = GetFromMat(diagram);
    //    //using Mat imgSrc = new Mat(ImagePath.Lenna, ImreadModes.Color);
    //    Mat imgGray = new Mat();
    //    Mat imgDst = imgSrc.Clone();
    //    Cv2.CvtColor(imgSrc, imgGray, ColorConversionCodes.BGR2GRAY, 0);
    //    KeyPoint[] keypoints = Cv2.FAST(imgGray, 50, true);
    //    foreach (KeyPoint kp in keypoints)
    //    {
    //        imgDst.Circle((Point)kp.Pt, 3, Scalar.Red, -1, LineTypes.AntiAlias, 0);
    //    }
    //    UpdateMatToView(imgDst);
    //    return base.Invoke(previors, diagram);
    //}

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat imgSrc = from.Mat;
        //using Mat imgSrc = new Mat(ImagePath.Lenna, ImreadModes.Color);
        using Mat imgGray = new Mat();
        Mat imgDst = imgSrc.Clone();
        Cv2.CvtColor(imgSrc, imgGray, ColorConversionCodes.BGR2GRAY, 0);
        KeyPoint[] keypoints = Cv2.FAST(imgGray, 50, true);
        foreach (KeyPoint kp in keypoints)
        {
            imgDst.Circle((Point)kp.Pt, 3, Scalar.Red, -1, LineTypes.AntiAlias, 0);
        }
        return this.OK(imgDst);
    }
}
