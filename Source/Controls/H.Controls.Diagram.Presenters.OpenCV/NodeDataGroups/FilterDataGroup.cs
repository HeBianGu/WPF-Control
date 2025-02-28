global using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;
using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon("\xE16E")]
[Display(Name = "滤波", GroupName = "对图像进行滤波，降噪，模糊处理", Order = 2)]
public class FilterDataGroup : BasicDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IFilterActionNodeData).Assembly.GetInstances<IFilterActionNodeData>().OrderBy(x => x.Order);
    }
}

