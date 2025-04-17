namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Filter)]
[Display(Name = "提取", Description = "提取图片中的信息", Order = 2)]
public class ExtractDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IExtractOpenCVNodeData).Assembly.GetInstances<IExtractOpenCVNodeData>().OrderBy(x => x.Order);
    }
}

