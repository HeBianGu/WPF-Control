using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon("\xE8B8")]
[Display(Name = "特征提取", GroupName = "图像处理的特征提取", Order = 5)]
public class FeatureDetectorDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IFeatureDetectorActionNodeData).Assembly.GetInstances<IFeatureDetectorActionNodeData>().OrderBy(x => x.Order);
    }
}
