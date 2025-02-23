namespace H.Controls.Diagram.Extensions.OpenCV.NodeDataGroup;
[Icon("\xE722")]
[Display(Name = "采集", GroupName = "设置输入图像", Order = 0)]
public class ImageDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IImageImportNodeData).Assembly.GetInstances<IImageImportNodeData>().OrderBy(x => x.Order); ;
    }
}