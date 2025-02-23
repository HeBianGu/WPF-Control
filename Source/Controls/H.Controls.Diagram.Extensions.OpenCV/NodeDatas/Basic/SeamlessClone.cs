namespace H.Controls.Diagram.Extensions.OpenCV.NodeDatas.Basic;
[Display(Name = "无缝融合", GroupName = "基础函数", Description = "将一幅图像中的指定目标复制后粘贴到另一幅图像中，并自然的融合", Order = 0)]
public class SeamlessClone : BasicActionNodeDataBase
{
    public SeamlessClone()
    {
        this.DetectFilePath = this.GetDataPath(ImagePath.Girl);
        this.FilePath = this.DetectFilePath;
    }

    protected override void OpenFilePath(string name)
    {
        this.DetectFilePath = name;
    }

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

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Point = new Point(260, 270);
    }

    private Point _point = new Point(260, 270);
    [Display(Name = "Point", GroupName = "数据")]
    public Point Point
    {
        get { return _point; }
        set
        {
            _point = value;
            RaisePropertyChanged();
        }
    }

    private SeamlessCloneMethods _method = SeamlessCloneMethods.NormalClone;
    [DefaultValue(SeamlessCloneMethods.NormalClone)]
    [Display(Name = "Method", GroupName = "数据")]
    public SeamlessCloneMethods Method
    {
        get { return _method; }
        set
        {
            _method = value;
            RaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node current)
    //{
    //    var path = this.DetectFilePath;
    //    Mat src = new Mat(path, ImreadModes.Color);
    //    Mat dst = this.GetFromMat(current);
    //    Mat src0 = src.Resize(dst.Size(), 0, 0, InterpolationFlags.Lanczos4);
    //    Mat mask = Mat.Zeros(src0.Size(), MatType.CV_8UC3);
    //    mask.Circle(200, 200, 100, Scalar.White, -1);
    //    Mat blend = new Mat();
    //    Cv2.SeamlessClone(src0, dst, mask, this.Point, blend, this.Method);
    //    this.Mat = blend;
    //    this.RefreshMatToView();
    //    return base.Invoke(previors, current);
    //}

    protected override IFlowableResult Refresh()
    {
        var path = this.DetectFilePath;
        Mat src = new Mat(path, ImreadModes.Color);
        Mat dst = this._preMat;
        Mat src0 = src.Resize(dst.Size(), 0, 0, InterpolationFlags.Lanczos4);
        Mat mask = Mat.Zeros(src0.Size(), MatType.CV_8UC3);
        mask.Circle(200, 200, 100, Scalar.White, -1);
        Mat blend = new Mat();
        Cv2.SeamlessClone(src0, dst, mask, this.Point, blend, this.Method);
        this.Mat = blend;
        this.RefreshMatToView();
        return base.Refresh();
    }
}
