

using H.Controls.Diagram.Extension;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Controls.Diagram.Extensions.Workflow
{
    [Display(Name = "手动操作", GroupName = "基本流程图形状", Order = 16, Description = "手动操作")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class OperationNodeData : WorkflowNodeBase
    {
        protected override Geometry GetGeometry()
        {
            GeometryConverter converter = new GeometryConverter();
            return converter.ConvertFromString("F1M0,0 100,0 80,60 20,60Z") as Geometry;
        }
    }
}
