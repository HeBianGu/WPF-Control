namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "反转图片", GroupName = "基础函数", Description = "会改变整个图像的像素数量和分辨率", Order = 3)]
public class Flip : BasicOpenCVNodeDataBase
{
    private FlipMode _flipMode;
    [Display(Name = "旋转方式", GroupName = "数据")]
    public FlipMode FlipMode
    {
        get { return _flipMode; }
        set
        {
            _flipMode = value;
            RaisePropertyChanged();
        }
    }


    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat result = new Mat();
        Cv2.Flip(from.Mat, result, this.FlipMode);
        return this.OK(result);
    }
}
