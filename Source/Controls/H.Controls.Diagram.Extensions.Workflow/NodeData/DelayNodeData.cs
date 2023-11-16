

using H.Controls.Diagram.Extension;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Controls.Diagram.Extensions.Workflow
{
    [Display(Name = "推迟", GroupName = "基本流程图形状", Order = 18, Description = "推迟")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class DelayNodeData : WorkflowNodeBase
    {
        protected override Geometry GetGeometry()
        {
            GeometryConverter converter = new GeometryConverter();
            return converter.ConvertFromString("M0,0 70,0 A 30,30 0 0 1 70,60 L70,60 0,60Z") as Geometry;
        }
    }
}
