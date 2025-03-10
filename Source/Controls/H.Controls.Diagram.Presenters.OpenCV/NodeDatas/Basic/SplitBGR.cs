namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "通道分割", GroupName = "基础函数", Order = 30)]
public class SplitBGR : BasicOpenCVNodeDataBase
{
    private SplitSelectType _splitSelectType;
    [DefaultValue(SplitSelectType.B)]
    [Display(Name = "选择通道", GroupName = "数据")]
    public SplitSelectType SplitSelectType
    {
        get { return _splitSelectType; }
        set
        {
            _splitSelectType = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private bool _useMarge = true;
    [DefaultValue(true)]
    [Display(Name = "启用合并", GroupName = "数据")]
    public bool UseMarge
    {
        get { return _useMarge; }
        set
        {
            _useMarge = value;
            DispatcherRaisePropertyChanged();
        }
    }

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        //using var src = new Mat(ImagePath.Lenna, ImreadModes.Color);
        Mat src = this.PreviourMat;
        Cv2.Split(src, out Mat[] mats);

        if (this.UseMarge)
        {
            Mat zero = new Mat(mats[0].Size(), MatType.CV_8UC1, new Scalar(0));
            if (this.SplitSelectType == SplitSelectType.B)
            {
                Mat sum = new Mat();
                Cv2.Merge(new Mat[] { mats[0], zero, zero }, sum);//(b,0,0)图像
                this.Mat = sum;
            }
            if (this.SplitSelectType == SplitSelectType.G)
            {
                Mat sum = new Mat();
                Cv2.Merge(new Mat[] { zero, mats[1], zero }, sum);//(0,g,0)图像
                this.Mat = sum;
            }
            if (this.SplitSelectType == SplitSelectType.R)
            {
                Mat sum = new Mat();
                Cv2.Merge(new Mat[] { zero, zero, mats[2] }, sum);//(0,0,r)图像
                this.Mat = sum;
            }
        }
        else
        {
            this.Mat = mats[(int)this.SplitSelectType];
        }

        this.UpdateMatToView();
        return base.Invoke(previors, current);
    }

    protected override IFlowableResult Invoke()
    {
        Mat src = this.PreviourMat;
        Cv2.Split(src, out Mat[] mats);

        if (this.UseMarge)
        {
            Mat zero = new Mat(mats[0].Size(), MatType.CV_8UC1, new Scalar(0));
            if (this.SplitSelectType == SplitSelectType.B)
            {
                Mat sum = new Mat();
                Cv2.Merge(new Mat[] { mats[0], zero, zero }, sum);//(b,0,0)图像
                this.Mat = sum;
            }
            if (this.SplitSelectType == SplitSelectType.G)
            {
                Mat sum = new Mat();
                Cv2.Merge(new Mat[] { zero, mats[1], zero }, sum);//(0,g,0)图像
                this.Mat = sum;
            }
            if (this.SplitSelectType == SplitSelectType.R)
            {
                Mat sum = new Mat();
                Cv2.Merge(new Mat[] { zero, zero, mats[2] }, sum);//(0,0,r)图像
                this.Mat = sum;
            }
        }
        else
        {
            this.Mat = mats[(int)this.SplitSelectType];
        }

        this.UpdateMatToView();
        return base.Invoke();
    }
}

public enum SplitSelectType
{
    B = 0, G, R
}
