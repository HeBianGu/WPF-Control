global using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Morphology;
using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Tablet)]
[Display(Name = "形态", GroupName = "对图像进行腐蚀、膨胀、开运算和闭运算", Order = 4)]
public class MorphologyDataGroup : BasicDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IMorphologyOpenCVNodeData).Assembly.GetInstances<IMorphologyOpenCVNodeData>().OrderBy(x => x.Order); ;
    }
}

