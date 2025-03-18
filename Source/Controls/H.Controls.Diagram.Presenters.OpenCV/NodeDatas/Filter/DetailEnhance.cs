namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "细节增强", GroupName = "细节增强是一种图像处理技术，旨在突出图像中的细节部分（如纹理、边缘、微小特征等），使图像看起来更加清晰和丰富", Order = 20)]
public class DetailEnhance : FilterOpenCVNodeDataBase
{
    private float _sigmaS = 10f;
    [DefaultValue(10f)]
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

    private float _sigmaR = 0.15f;
    [DefaultValue(0.15f)]
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
        Mat detailEnhance = new Mat();
        Cv2.DetailEnhance(src, detailEnhance, this.SigmaS, this.SigmaR);
        return this.OK(detailEnhance);
    }
}
