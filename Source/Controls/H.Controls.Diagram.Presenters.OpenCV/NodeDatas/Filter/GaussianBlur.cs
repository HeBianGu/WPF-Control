namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "高斯滤波", GroupName = "对图像进行卷积，能够有效去除噪声并保留图像的整体结构", Order = 1)]
public class GaussianBlur : FilterOpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.KSize = new Size(7, 7);
    }
    private Size _ksize = new Size(7, 7);
    /// <summary>
    /// 高斯核的大小，通常是一个奇数（如 3x3、5x5 等
    /// </summary>
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

    private double _sigmaX;
    [DefaultValue(0.0)]
    [Display(Name = "X方向标准差", GroupName = "数据", Description = "SigmaX 越大，高斯核在水平方向越平坦，平滑效果越强。SigmaX 越小，高斯核在水平方向越尖锐，平滑效果越弱。")]
    public double SigmaX
    {
        get { return _sigmaX; }
        set
        {
            _sigmaX = value;
            RaisePropertyChanged();
        }
    }

    private double _sigmaY;
    [DefaultValue(0.0)]
    [Display(Name = "Y方向标准差", GroupName = "数据", Description = "SigmaY 越大，高斯核在垂直方向越平坦，平滑效果越强。SigmaY 越小，高斯核在垂直方向越尖锐，平滑效果越弱。")]
    public double SigmaY
    {
        get { return _sigmaY; }
        set
        {
            _sigmaY = value;
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
        Cv2.GaussianBlur(preMat, preMat, KSize, SigmaX, SigmaY, BorderType);
        return this.OK(preMat.Clone());
    }
}
