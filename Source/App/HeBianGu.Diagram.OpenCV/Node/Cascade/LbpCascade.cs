// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




using H.Controls.Diagram;
using OpenCvSharp;

namespace HeBianGu.Diagram.OpenCV
{
    
    
    [Display(Name = "LBP", GroupName = "人脸检测", Order = 0)]
    public class LbpCascade : CascadeBase
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
}
