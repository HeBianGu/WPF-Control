namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Favicon2)]
[Display(Name = "Yolov目标检测", Description = "应用Yolov模型对目标检测", Order = 50)]
public class YolovCaptureDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IYolovOpenCVNodeData).Assembly.GetInstances<IYolovOpenCVNodeData>().OrderBy(x => x.Order);
    }
}