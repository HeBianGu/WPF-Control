namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "素描", GroupName = "滤波/降噪/模糊", Order = 0)]
public class PencilSketch : FeatureDetectorActionNodeDataBase
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


    private float _sigmaR = 0.07f;
    [DefaultValue(0.07f)]
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

    private float _shadeFactor = 0.01f;
    [DefaultValue(0.01f)]
    [Display(Name = "SigmaR", GroupName = "数据")]
    [Range(0, 0.01)]
    public float ShadeFactor
    {
        get { return _shadeFactor; }
        set
        {
            _shadeFactor = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private PencilOutType _pencilOutType;
    [DefaultValue(PencilOutType.Src)]
    [Display(Name = "OutType", GroupName = "数据")]
    public PencilOutType PencilOutType
    {
        get { return _pencilOutType; }
        set
        {
            _pencilOutType = value;
            DispatcherRaisePropertyChanged();
        }
    }



    protected override IFlowableResult Refresh()
    {
        Mat src = this._preMat;
        //  Do ：输出8位的单通道图像
        Mat pencil1 = new Mat();
        //  Do ：输出与源图像相同大小与类型的图像
        Mat pencil2 = new Mat();
        Cv2.PencilSketch(src, pencil1, pencil2, this.SigmaS, this.SigmaR, this.ShadeFactor);
        this.Mat = this.PencilOutType == PencilOutType.Src ? pencil2 : pencil1;
        this.RefreshMatToView();
        return base.Refresh();
    }
}

public enum PencilOutType
{
    Src, Channel8
}
