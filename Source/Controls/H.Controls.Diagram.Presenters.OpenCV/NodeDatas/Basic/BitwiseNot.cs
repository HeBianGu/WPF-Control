global using H.Controls.Diagram.Flowables;
global using H.Controls.Diagram.Parts;
global using H.Controls.Diagram.Parts.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "反转黑白", GroupName = "基础函数", Description = "二指图片的效果反转既黑色变白色，白色变黑色", Order = 20)]
public class BitwiseNot : BasicOpenCVNodeDataBase
{
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        if (from.Mat == null)
            return this.Error(null, "数据源为空");
        Mat src = from.Mat;
        Cv2.BitwiseNot(src, src);
        return this.OK(src);
    }
}
