namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;
[Icon(FontIcons.Photo2)]
[Display(Name = "图片数据源", Description = "设置输入图像", Order = 0)]
public class ImageDataGroup : BasicDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IOpenCVImageNodeData).Assembly.GetInstances<IOpenCVImageNodeData>().OrderBy(x => x.Order); ;
    }
}
