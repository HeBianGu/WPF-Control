global using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

namespace H.Controls.Diagram.Presenters.OpenCV;
[Display(Name = "OpenCV基础", GroupName = "机器视觉", Order = 0)]
public class OpenCVDiagramData : NodeDataGroupsDiagramDataBase
{
    protected override IEnumerable<INodeDataGroup> CreateNodeGroups()
    {
        return typeof(IBasicDataGroup).GetInstances<IBasicDataGroup>().OrderBy(x => x.Order);
    }
}


[Display(Name = "OpenCV检测", GroupName = "机器视觉", Order = 0)]
public class OpenCVDetector : FlowableDiagramDataBase
{
    
}


[Display(Name = "OpenCV视频处理", GroupName = "机器视觉", Order = 0)]
public class OpenCVVideo : FlowableDiagramDataBase
{

}
