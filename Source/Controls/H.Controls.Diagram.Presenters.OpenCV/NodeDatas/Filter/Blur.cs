namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "基础滤波", GroupName = "用于图像的平滑、去噪、边缘检测等任务", Order = 0)]
public class Blur : FilterOpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.KSize = new Size(8, 8);
    }
    private Size _ksize = new Size(8, 8);
    [Display(Name = "核大小", GroupName = "数据", Description = "核的大小决定了滤波的范围。核越大，平滑效果越强，但计算量也会增加")]
    public Size KSize
    {
        get { return _ksize; }
        set
        {
            _ksize = value;
            RaisePropertyChanged();
        }
    }

    private Point? _anchor;
    [DefaultValue(null)]
    [Display(Name = "锚点", GroupName = "数据", Description = "内核的锚点，表示内核的参考点。null 表示使用内核的中心作为锚点")]
    public Point? Anchor
    {
        get { return _anchor; }
        set
        {
            _anchor = value;
            RaisePropertyChanged();
        }
    }

    private BorderTypes _borderType = BorderTypes.Default;
    [DefaultValue(BorderTypes.Default)]
    [Display(Name = "边界处理", GroupName = "数据", Description = "由于滤波核在边界无法完全覆盖图像，需要指定如何填充边界外的像素")]
    public BorderTypes BorderType
    {
        get { return _borderType; }
        set
        {
            _borderType = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat preMat = from.Mat;
        Cv2.Blur(preMat, preMat, KSize, Anchor, BorderType);
        return this.OK(preMat.Clone());
    }
}
