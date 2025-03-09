namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "细节增强", GroupName = "滤波/降噪/模糊", Order = 20)]
public class DetailEnhance : FilterOpenCVNodeDataBase
{
    private float _sigmaS = 10f;
    [DefaultValue(10f)]
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

    private float _sigmaR = 0.15f;
    [DefaultValue(0.15f)]
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

    protected override IFlowableResult Invoke()
    {
        Mat src = this.PreviourMat;
        Mat detailEnhance = new Mat();
        Cv2.DetailEnhance(src, detailEnhance, this.SigmaS, this.SigmaR);
        this.Mat = detailEnhance;
        this.RefreshMatToView();
        return base.Invoke();
    }
}
