namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;
[Display(Name = "识别连通区域", GroupName = "基础检测", Order = 3)]
public class RenderBlobs : DetectorOpenCVNodeDataBase
{
    private bool _useRectangle = true;
    [Display(Name = "绘制矩形", GroupName = "数据")]
    public bool UseRectangle
    {
        get { return _useRectangle; }
        set
        {
            _useRectangle = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat preMat = from.Mat;
        ConnectedComponents cc = Cv2.ConnectedComponentsEx(preMat);
        if (cc.LabelCount <= 1)
            return Error(null, "没有识别出联通区域");
        Mat labelview = preMat.EmptyClone();
        cc.RenderBlobs(labelview);
        if (UseRectangle)
        {
            foreach (ConnectedComponents.Blob blob in cc.Blobs.Skip(1))
            {
                labelview.Rectangle(blob.Rect, Scalar.Red);
            }
        }
        return this.OK(labelview);
    }
}

