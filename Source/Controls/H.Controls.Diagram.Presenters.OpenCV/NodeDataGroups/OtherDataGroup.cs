using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Other;
namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon("\xE15C")]
[Display(Name = "其他", GroupName = "图像处理的其他算法", Order = 100)]
public class OtherDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IOtherActionNodeData).Assembly.GetInstances<IOtherActionNodeData>().OrderBy(x => x.Order);
    }
}
