namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;
[Display(Name = "直线概率检测", GroupName = "基础检测", Order = 1)]
public class HoughLinesP : DetectorOpenCVNodeDataBase
{
    private double _rho = 1.0;
    [Display(Name = "Rho", GroupName = "数据")]
    public double Rho
    {
        get { return _rho; }
        set
        {
            _rho = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private double _theta = Math.PI / 180;
    [Display(Name = "Theta", GroupName = "数据")]
    public double Theta
    {
        get { return _theta; }
        set
        {
            _theta = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private int _threshold = 50;
    [Display(Name = "Threshold", GroupName = "数据")]
    public int Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private double _minLineLength = 50;
    [Display(Name = "MinLineLength", GroupName = "数据")]
    public double MinLineLength
    {
        get { return _minLineLength; }
        set
        {
            _minLineLength = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private double _maxLineGap = 10.0;
    [Display(Name = "MaxLineGap", GroupName = "数据")]
    public double MaxLineGap
    {
        get { return _maxLineGap; }
        set
        {
            _maxLineGap = value;
            DispatcherRaisePropertyChanged();
        }
    }

    protected override IFlowableResult Invoke()
    {
        Mat imgGray = this.PreviourMat;
        Mat imgStd = new Mat(this.SrcFilePath, ImreadModes.Color);
        Mat imgProb = imgStd.Clone();
        LineSegmentPoint[] segProb = Cv2.HoughLinesP(imgGray, Rho, Theta, Threshold, MinLineLength, MaxLineGap);
        foreach (LineSegmentPoint s in segProb)
        {
            imgProb.Line(s.P1, s.P2, Scalar.Red, 3, LineTypes.AntiAlias, 0);
        }
        RefreshMatToView(imgProb);
        return base.Invoke();
    }
}
