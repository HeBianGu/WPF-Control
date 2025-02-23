namespace H.Controls.Diagram.Extensions.OpenCV.NodeDataGroup;

[Icon("\xE11D")]
[Display(Name = "人脸检测", GroupName = "图像处理的人脸检测", Order = 1)]
public class CascadeClassifierDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(ICascadeClassifierActionNodeData).Assembly.GetInstances<ICascadeClassifierActionNodeData>().OrderBy(x => x.Order);
    }
}
