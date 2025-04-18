namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;
[Display(Name = "标准直线检测", GroupName = "基础检测", Order = 1)]
public class HoughLines : DetectorOpenCVNodeDataBase
{
    private double _rho = 1.0;
    [DefaultValue(1.0)]
    [Display(Name = "距离分辨率（像素）", GroupName = "数据",Description = "距离分辨率（像素），增大可减少计算量但降低精度")]
    public double Rho
    {
        get { return _rho; }
        set
        {
            _rho = value;
            RaisePropertyChanged();
        }
    }

    private double _theta = Math.PI / 180;
    [Display(Name = "角度分辨率（弧度）", GroupName = "数据",Description = "减小可提高角度精度但增加计算量")]
    public double Theta
    {
        get { return _theta; }
        set
        {
            _theta = value;
            RaisePropertyChanged();
        }
    }

    private int _threshold = 100;
    [Range(100,200)]
    [Display(Name = "投票阈值", GroupName = "数据",Description = "决定被认定为直线的最小票数,值越大检测到的直线越少但更显著")]
    public int Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            RaisePropertyChanged();
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
            RaisePropertyChanged();
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
            RaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node diagram)
    //{
    //    var imgGray = GetFromMat(diagram);
    //    var imgStd = new Mat(GetFromFilePath(diagram), ImreadModes.Color);
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

    //    UpdateMatToView(imgStd);
    //    return base.Invoke(previors, diagram);
    //}

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat imgGray = from.Mat;
        //Mat imgStd = new Mat(this.SrcFilePath, ImreadModes.Color);
        LineSegmentPolar[] segStd = Cv2.HoughLines(imgGray, Rho, Theta, Threshold, Srn, Stn);
        Mat imgStd = this.GetPrviewMat(srcImageNodeData, from, imgGray);
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
            imgStd.Line(pt1, pt2, this.OutputColor.ToScalar(), this.OutPutThickness, LineTypes.AntiAlias, 0);
        }

        return this.OK(imgStd);
    }
}
