namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "感兴趣区域", GroupName = "基础函数", Description = "会改变整个图像的像素数量和分辨率", Order = 3)]
public class ROI : BasicOpenCVNodeDataBase
{
    private System.Windows.Int32Rect _rect = new System.Windows.Int32Rect(0, 0, 320, 320);
    [Display(Name = "范围", GroupName = "数据")]
    public System.Windows.Int32Rect Rect
    {
        get { return _rect; }
        set
        {
            _rect = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        if (!this.Rect.HasArea)
            return this.Error(from.Mat, "Rect范围参数不正确:" + this.Rect.ToString());
        // 选择中心区域作为ROI
        Mat roiImage = new Mat(from.Mat, this.Rect.ToCVRect());
        return this.OK(roiImage);
    }
}
