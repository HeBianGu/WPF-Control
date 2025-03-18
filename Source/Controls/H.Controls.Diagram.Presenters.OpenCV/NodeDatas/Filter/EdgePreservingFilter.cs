namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
[Display(Name = "边缘保持器", GroupName = "在平滑图像的同时保留边缘信息的图像处理技术", Order = 30)]
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
            RaisePropertyChanged();
        }
    }

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

    private float _sigmaR = 0.4f;
    [DefaultValue(0.4f)]
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
        Mat normconv = new Mat();
        Cv2.EdgePreservingFilter(src, normconv, this.EdgePreservingMethod, this.SigmaS, this.SigmaR);
        return this.OK(normconv);
    }
}
