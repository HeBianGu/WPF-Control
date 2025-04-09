namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;
[Icon(FontIcons.Photo)]
[Display(Name = "基础检测", Description = "图像处理的基础检测", Order = 3)]
public class DetectorDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IDetectorOpenCVNodeData).Assembly.GetInstances<IDetectorOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
