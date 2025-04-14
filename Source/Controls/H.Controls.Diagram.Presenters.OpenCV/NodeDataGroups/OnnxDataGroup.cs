namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Favicon2)]
[Display(Name = "Onnx目标检测", Description = "应用Onnx模型对目标检测", Order = 50)]
public class OnnxDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IOnnxOpenCVNodeData).Assembly.GetInstances<IOnnxOpenCVNodeData>().OrderBy(x => x.Order);
    }
}