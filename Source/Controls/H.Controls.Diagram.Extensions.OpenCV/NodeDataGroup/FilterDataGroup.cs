namespace H.Controls.Diagram.Extensions.OpenCV.NodeDataGroup;

[Icon("\xE16E")]
[Display(Name = "滤波", GroupName = "对图像进行滤波，降噪，模糊处理", Order = 2)]
public class FilterDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IFilterActionNodeData).Assembly.GetInstances<IFilterActionNodeData>().OrderBy(x => x.Order); ;
    }
}

