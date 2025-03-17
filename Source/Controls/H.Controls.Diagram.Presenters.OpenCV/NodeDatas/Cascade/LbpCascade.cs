namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Cascade;

[Display(Name = "LBP", GroupName = "人脸检测", Order = 0)]
public class LbpCascade : CascadeClassifierOpenCVNodeDataBase
{
    public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData current)
    {
        Mat src = this.PreviourMat;
        // Load the cascades
        using CascadeClassifier lbpCascade = new CascadeClassifier(TextPath.LbpCascade);

        // Detect faces
        Mat lbpResult = DetectFace(lbpCascade, src);
        this.Mat = lbpResult;
        UpdateMatToView(lbpResult);
        return base.Invoke(previors, current);
    }
}
