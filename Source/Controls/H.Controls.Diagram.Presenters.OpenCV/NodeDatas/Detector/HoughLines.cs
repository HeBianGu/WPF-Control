namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;
[Display(Name = "标准直线检测", GroupName = "基础检测", Order = 0)]
public class HoughLines : DetectorActionNodeDataBase
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


    private double _srn = 0;
    [Display(Name = "Srn", GroupName = "数据")]
    public double Srn
    {
        get { return _srn; }
        set
        {
            _srn = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private double _stn = 0;
    [Display(Name = "Stn", GroupName = "数据")]
    public double Stn
    {
        get { return _stn; }
        set
        {
            _stn = value;
            DispatcherRaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node current)
    //{
    //    var imgGray = GetFromMat(current);
    //    var imgStd = new Mat(GetFromFilePath(current), ImreadModes.Color);
    //    LineSegmentPolar[] segStd = Cv2.HoughLines(imgGray, Rho, Theta, Threshold, Srn, Stn);
    //    int limit = Math.Min(segStd.Length, 10);
    //    for (int i = 0; i < limit; i++)
    //    {
    //        float rho = segStd[i].Rho;
    //        float theta = segStd[i].Theta;
    //        double a = Math.Cos(theta);
    //        double b = Math.Sin(theta);
    //        double x0 = a * rho;
    //        double y0 = b * rho;
    //        Point pt1 = new Point { X = (int)Math.Round(x0 + 1000 * -b), Y = (int)Math.Round(y0 + 1000 * a) };
    //        Point pt2 = new Point { X = (int)Math.Round(x0 - 1000 * -b), Y = (int)Math.Round(y0 - 1000 * a) };
    //        imgStd.Line(pt1, pt2, Scalar.Red, 3, LineTypes.AntiAlias, 0);
    //    }

    //    RefreshMatToView(imgStd);
    //    return base.Invoke(previors, current);
    //}

    protected override IFlowableResult Refresh()
    {
        Mat imgGray = this._preMat;
        Mat imgStd = new Mat(this._srcFilePath, ImreadModes.Color);
        LineSegmentPolar[] segStd = Cv2.HoughLines(imgGray, Rho, Theta, Threshold, Srn, Stn);
        int limit = Math.Min(segStd.Length, 10);
        for (int i = 0; i < limit; i++)
        {
            float rho = segStd[i].Rho;
            float theta = segStd[i].Theta;
            double a = Math.Cos(theta);
            double b = Math.Sin(theta);
            double x0 = a * rho;
            double y0 = b * rho;
            Point pt1 = new Point { X = (int)Math.Round(x0 + 1000 * -b), Y = (int)Math.Round(y0 + 1000 * a) };
            Point pt2 = new Point { X = (int)Math.Round(x0 - 1000 * -b), Y = (int)Math.Round(y0 - 1000 * a) };
            imgStd.Line(pt1, pt2, Scalar.Red, 3, LineTypes.AntiAlias, 0);
        }

        RefreshMatToView(imgStd);
        return base.Refresh();
    }
}
