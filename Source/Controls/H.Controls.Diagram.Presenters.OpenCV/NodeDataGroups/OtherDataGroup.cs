namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.More)]
[Display(Name = "其他", Description = "图像处理的其他算法", Order = 100)]
public class OtherDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IOtherOpenCVNodeData).Assembly.GetInstances<IOtherOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
