global using H.Controls.Diagram.Flowables;
global using H.Controls.Diagram.Parts;
global using H.Controls.Diagram.Parts.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "反转黑白", GroupName = "基础函数", Description = "二指图片的效果反转既黑色变白色，白色变黑色", Order = 20)]
public class BitwiseNot : BasicOpenCVNodeDataBase
{
    public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData current)
    {
        if (this.PreviourMat == null)
            return this.Error("数据源为空");
        Mat src = this.GetFromData(current, previors).Mat;
        Cv2.BitwiseNot(src, src);
        this.Mat = src;
        this.UpdateMatToView();
        return base.Invoke(previors, current);
    }
}
