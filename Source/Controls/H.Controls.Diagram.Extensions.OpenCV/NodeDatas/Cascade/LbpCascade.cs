

namespace H.Controls.Diagram.Extensions.OpenCV;



[Display(Name = "LBP", GroupName = "人脸检测", Order = 0)]
public class LbpCascade : CascadeClassifierActionNodeDataBase
{
    public override IFlowableResult Invoke(Part previors, Node current)
    {
        var src = this._preMat;
        // Load the cascades
        using var lbpCascade = new CascadeClassifier(TextPath.LbpCascade);

        // Detect faces
        Mat lbpResult = DetectFace(lbpCascade, src);
        RefreshMatToView(lbpResult);
        return base.Invoke(previors, current);
    }
}
