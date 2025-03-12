namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "基础滤波", GroupName = "滤波/降噪/模糊", Order = 0)]
public class Blur : FilterOpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.KSize = new Size(8, 8);
    }
    private Size _ksize = new Size(8, 8);
    [Display(Name = "KSize", GroupName = "数据")]
    public Size KSize
    {
        get { return _ksize; }
        set
        {
            _ksize = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private Point? _anchor;
    [DefaultValue(null)]
    [Display(Name = "Anchor", GroupName = "数据")]
    public Point? Anchor
    {
        get { return _anchor; }
        set
        {
            _anchor = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private BorderTypes _borderType = BorderTypes.Default;
    [DefaultValue(BorderTypes.Default)]
    [Display(Name = "BorderType", GroupName = "数据")]
    public BorderTypes BorderType
    {
        get { return _borderType; }
        set
        {
            _borderType = value;
            DispatcherRaisePropertyChanged();
        }
    }

    protected override IFlowableResult Invoke()
    {
        Mat preMat = this.PreviourMat;
        Cv2.Blur(preMat, preMat, KSize, Anchor, BorderType);
        Mat = preMat;
        UpdateMatToView();
        return base.Invoke();
    }
}
