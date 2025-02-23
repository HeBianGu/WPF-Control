namespace H.Controls.Diagram.Extensions.OpenCV.NodeDataGroup;

[Icon("\xE71E")]
[Display(Name = "基础检测", GroupName = "图像处理的基础检测", Order = 3)]
public class DetectorDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IDetectorActionNodeData).Assembly.GetInstances<IDetectorActionNodeData>().OrderBy(x => x.Order);
    }
}
