namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.FitPage)]
[Display(Name = "特征提取", Description = "特征提取是计算机视觉和图像处理中的核心步骤，它的主要作用是将原始数据（如图像）转换为更能代表问题本质的特征表示，从而显著提高后续处理的效果和效率。", Order = 5)]
public class FeatureDetectorDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IFeatureDetectorOpenCVNodeData).Assembly.GetInstances<IFeatureDetectorOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
