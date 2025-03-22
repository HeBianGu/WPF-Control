namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Cascade;

[Display(Name = "LBP", GroupName = "人脸检测", Order = 0)]
public class LbpCascade : CascadeClassifierOpenCVNodeDataBase
{
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat src = from.Mat;
        // Load the cascades
        using CascadeClassifier lbpCascade = new CascadeClassifier(TextPath.LbpCascade);

        // Detect faces
        Mat lbpResult = DetectFace(lbpCascade, src);
        return this.OK(lbpResult);
    }
}
