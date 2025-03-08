namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "边缘感知", GroupName = "滤波/降噪/模糊", Order = 31)]
public class Stylization : FilterActionNodeDataBase
{
    private float _sigmaS = 60f;
    [DefaultValue(60f)]
    [Display(Name = "SigmaS", GroupName = "数据")]
    [Range(0, 200)]
    public float SigmaS
    {
        get { return _sigmaS; }
        set
        {
            _sigmaS = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private float _sigmaR = 0.45f;
    [DefaultValue(0.45f)]
    [Display(Name = "SigmaR", GroupName = "数据")]
    [Range(0, 1.0)]
    public float SigmaR
    {
        get { return _sigmaR; }
        set
        {
            _sigmaR = value;
            DispatcherRaisePropertyChanged();
        }
    }

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        Mat src = this.PreviourMat;
        Mat stylized = new Mat();
        Cv2.Stylization(src, stylized, this.SigmaS, this.SigmaR);
        this.Mat = stylized;
        this.RefreshMatToView();
        return base.Invoke(previors, current);
    }

    protected override IFlowableResult Invoke()
    {
        Mat src = this.PreviourMat;
        Mat stylized = new Mat();
        Cv2.Stylization(src, stylized, this.SigmaS, this.SigmaR);
        this.Mat = stylized;
        this.RefreshMatToView();
        return base.Invoke();
    }
}
