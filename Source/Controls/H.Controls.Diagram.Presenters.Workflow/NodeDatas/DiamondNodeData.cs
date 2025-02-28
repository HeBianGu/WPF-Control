global using H.Controls.Diagram.Presenter.LinkDatas;
global using H.Controls.Diagram.Presenter.PortDatas;
global using H.Controls.Diagram.Flowables;
global using H.Controls.Diagram.Parts;
global using H.Controls.Diagram.Parts.Base;
global using H.Controls.Diagram.Datas;
global using H.Extensions.Geometry;
global using System.ComponentModel.DataAnnotations;
global using System.Threading;
global using System.Windows.Media;

namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;
[Display(Name = "判定", GroupName = "基本流程图形状", Order = 1, Description = "判定")]
public class DiamondNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Diamond;
    }

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        Thread.Sleep(1000);
        int r = this.Random.Next(0, 3);
        return r == 1 ? new FlowableResult<BoolResult>(BoolResult.True) : (IFlowableResult)new FlowableResult<BoolResult>(BoolResult.False);
    }

    public override ILinkData CreateLinkData()
    {
        return new FlowableLinkData<BoolResult>() { FromNodeID = this.ID };
    }

    public override IPortData CreatePortData()
    {
        return new FlowablePortData<BoolResult>(this.ID, PortType.Both);
    }
}
