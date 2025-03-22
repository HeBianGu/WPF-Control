namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "边缘感知", GroupName = "能够在平滑图像的同时保留边缘信息", Order = 31)]
public class Stylization : FilterOpenCVNodeDataBase
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

    private float _sigmaR = 0.45f;
    [DefaultValue(0.45f)]
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

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat src = from.Mat;
        Mat stylized = new Mat();
        Cv2.Stylization(src, stylized, this.SigmaS, this.SigmaR);
        return this.OK(stylized);
    }
}
