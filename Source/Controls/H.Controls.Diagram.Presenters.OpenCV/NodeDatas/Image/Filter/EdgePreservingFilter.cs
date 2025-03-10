namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "边缘保持器", GroupName = "滤波/降噪/模糊", Order = 30)]
public class EdgePreservingFilter : FilterOpenCVNodeDataBase
{
    private EdgePreservingMethods _edgePreservingMethods = EdgePreservingMethods.RecursFilter;
    [DefaultValue(EdgePreservingMethods.RecursFilter)]
    [Display(Name = "Method", GroupName = "数据")]
    public EdgePreservingMethods EdgePreservingMethod
    {
        get { return _edgePreservingMethods; }
        set
        {
            _edgePreservingMethods = value;
            DispatcherRaisePropertyChanged();
        }
    }

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

    private float _sigmaR = 0.4f;
    [DefaultValue(0.4f)]
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
        Mat normconv = new Mat();
        Cv2.EdgePreservingFilter(src, normconv, this.EdgePreservingMethod, this.SigmaS, this.SigmaR);
        this.Mat = normconv;
        this.UpdateMatToView();
        return base.Invoke();
    }
}
