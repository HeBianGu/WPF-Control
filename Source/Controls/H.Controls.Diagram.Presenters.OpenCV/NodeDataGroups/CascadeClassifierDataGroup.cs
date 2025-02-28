global using H.Controls.Diagram.Presenter.DiagramDatas;
global using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Cascade;
global using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon("\xE11D")]
[Display(Name = "人脸检测", GroupName = "图像处理的人脸检测", Order = 1)]
public class CascadeClassifierDataGroup : BasicDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(ICascadeClassifierActionNodeData).Assembly.GetInstances<ICascadeClassifierActionNodeData>().OrderBy(x => x.Order);
    }
}
