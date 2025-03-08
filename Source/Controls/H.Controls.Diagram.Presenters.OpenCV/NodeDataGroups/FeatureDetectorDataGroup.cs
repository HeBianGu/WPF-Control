namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon("\xE8B8")]
[Display(Name = "特征提取", GroupName = "图像处理的特征提取", Order = 5)]
public class FeatureDetectorDataGroup : BasicDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IFeatureDetectorOpenCVNodeData).Assembly.GetInstances<IFeatureDetectorOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
