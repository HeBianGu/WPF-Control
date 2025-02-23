using H.Mvvm;

namespace HeBianGu.Diagram.OpenCV;

[Icon("\xE722")]
[Display(Name = "基础功能", GroupName = "图像处理的基础功能", Order = 1)]
public class BasicDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IBasicActionNodeData).Assembly.GetInstances<IBasicActionNodeData>().OrderBy(x => x.Order);
    }
}

