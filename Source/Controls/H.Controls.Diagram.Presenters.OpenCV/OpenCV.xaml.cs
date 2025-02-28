using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

namespace H.Controls.Diagram.Presenters.OpenCV;
[Display(Name = "OpenCVSharp基础", GroupName = "机器视觉", Order = 0)]
public class OpenCVDiagramData : NodeGroupsDiagramDataBase
{
    protected override IEnumerable<INodeDataGroup> CreateNodeGroups()
    {
        return typeof(IBasicDataGroup).GetInstances<IBasicDataGroup>().OrderBy(x => x.Order);
    }
}


[Display(Name = "OpenCVSharp检测", GroupName = "机器视觉", Order = 0)]
public class OpenCVDetector : FlowableDiagramDataBase
{
    
}


[Display(Name = "OpenCVSharp视频处理", GroupName = "机器视觉", Order = 0)]
public class OpenCVVideo : FlowableDiagramDataBase
{

}
