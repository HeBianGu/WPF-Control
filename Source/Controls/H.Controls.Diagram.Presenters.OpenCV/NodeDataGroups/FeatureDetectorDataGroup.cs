namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.FitPage)]
[Display(Name = "特征提取", Description = "图像处理的特征提取", Order = 5)]
public class FeatureDetectorDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IFeatureDetectorOpenCVNodeData).Assembly.GetInstances<IFeatureDetectorOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
