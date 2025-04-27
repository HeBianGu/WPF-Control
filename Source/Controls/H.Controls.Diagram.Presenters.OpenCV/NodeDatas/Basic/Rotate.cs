namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "旋转图片", GroupName = "基础函数", Description = "会改变整个图像的像素数量和分辨率", Order = 3)]
public class Rotate : BasicOpenCVNodeDataBase
{
    private RotateFlags _rotateFlags;
    [Display(Name = "旋转方式", GroupName = "数据")]
    public RotateFlags RotateFlags
    {
        get { return _rotateFlags; }
        set
        {
            _rotateFlags = value;
            RaisePropertyChanged();
        }
    }


    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat result = new Mat();
        Cv2.Rotate(from.Mat, result, this.RotateFlags);
        return this.OK(result);
    }
}
