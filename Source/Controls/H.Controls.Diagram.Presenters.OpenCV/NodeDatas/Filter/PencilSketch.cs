namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "素描", GroupName = "素描效果通常强调图像的边缘和轮廓，同时减少或去除颜色和纹理信息", Order = 3)]
public class PencilSketch : FilterOpenCVNodeDataBase
{
    private float _sigmaS = 60f;
    [DefaultValue(60f)]
    [Display(Name = "空间标准差", GroupName = "数据", Description = "较大的 SigmaS 会使滤波核覆盖更广的区域，平滑效果更明显；较小的 SigmaS 则限制滤波核的作用范围，保留更多细节")]
    [Range(0, 200)]
    public float SigmaS
    {
        get { return _sigmaS; }
        set
        {
            _sigmaS = value;
            RaisePropertyChanged();
        }
    }

    private float _sigmaR = 0.07f;
    [DefaultValue(0.07f)]
    [Display(Name = "范围标准差", GroupName = "数据", Description = "较大的 SigmaR 允许像素值差异较大的像素参与平滑，平滑效果更强；较小的 SigmaR 则更注重保留边缘，避免平滑边缘区域")]
    [Range(0, 1.0)]
    public float SigmaR
    {
        get { return _sigmaR; }
        set
        {
            _sigmaR = value;
            RaisePropertyChanged();
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
            RaisePropertyChanged();
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
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat src = from.Mat;
        //  Do ：输出8位的单通道图像
        Mat pencil1 = new Mat();
        //  Do ：输出与源图像相同大小与类型的图像
        Mat pencil2 = new Mat();
        Cv2.PencilSketch(src, pencil1, pencil2, this.SigmaS, this.SigmaR, this.ShadeFactor);
        return this.OK(this.PencilOutType == PencilOutType.Src ? pencil2 : pencil1);
    }
}

public enum PencilOutType
{
    Src, Channel8
}
