global using H.Controls.Diagram.Presenter.DiagramDatas;
global using H.Mvvm.Attributes;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.EmojiTabPeople)]
[Display(Name = "人脸检测", GroupName = "图像处理的人脸检测", Order = 1)]
public class CascadeClassifierDataGroup : BasicDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(ICascadeClassifierOpenCVNodeData).Assembly.GetInstances<ICascadeClassifierOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
