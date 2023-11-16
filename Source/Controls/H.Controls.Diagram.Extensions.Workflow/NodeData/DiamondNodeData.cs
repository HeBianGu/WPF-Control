



using H.Controls.Diagram.Extension;
using H.Extensions.Geometry;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Windows.Media;

namespace H.Controls.Diagram.Extensions.Workflow
{
    [Display(Name = "判定", GroupName = "基本流程图形状", Order = 1, Description = "判定")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class DiamondNodeData : WorkflowNodeBase
    {
        protected override Geometry GetGeometry()
        {
            return GeometryFactory.Diamond;
        }

        public override IFlowableResult Invoke(Part previors, Node current)
        {
            Thread.Sleep(1000);
            var r = this.Random.Next(0, 3);
            if (r == 1)
                return new FlowableResult<BoolResult>(BoolResult.True);

            return new FlowableResult<BoolResult>(BoolResult.False);
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
}
