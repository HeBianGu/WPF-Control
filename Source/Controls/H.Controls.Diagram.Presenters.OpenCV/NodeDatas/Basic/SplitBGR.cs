namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "通道分割", GroupName = "基础函数", Description = "分割BGR通道", Order = 30)]
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
            RaisePropertyChanged();
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
            RaisePropertyChanged();
        }
    }
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        //using var src = new Mat(ImagePath.Lenna, ImreadModes.Color);
        Mat src = from.Mat;
        Cv2.Split(src, out Mat[] mats);

        if (this.UseMarge)
        {
            using Mat zero = new Mat(mats[0].Size(), MatType.CV_8UC1, new Scalar(0));
            if (this.SplitSelectType == SplitSelectType.B)
            {
                Mat sum = new Mat();
                Cv2.Merge(new Mat[] { mats[0], zero, zero }, sum);//(b,0,0)图像
                return this.OK(sum);
            }
            if (this.SplitSelectType == SplitSelectType.G)
            {
                Mat sum = new Mat();
                Cv2.Merge(new Mat[] { zero, mats[1], zero }, sum);//(0,g,0)图像
                return this.OK(sum);
            }
            if (this.SplitSelectType == SplitSelectType.R)
            {
                Mat sum = new Mat();
                Cv2.Merge(new Mat[] { zero, zero, mats[2] }, sum);//(0,0,r)图像
                return this.OK(sum);
            }
        }
        else
        {
            return this.OK(mats[(int)this.SplitSelectType]);
        }

        return this.Error(null);
    }
}

public enum SplitSelectType
{
    B = 0, G, R
}
