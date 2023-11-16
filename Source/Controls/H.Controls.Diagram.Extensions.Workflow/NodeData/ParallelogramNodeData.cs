


using H.Controls.Diagram.Extension;
using H.Extensions.Geometry;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Controls.Diagram.Extensions.Workflow
{
    [Display(Name = "数据", GroupName = "基本流程图形状", Order = 6, Description = "数据")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class ParallelogramNodeData : WorkflowNodeBase
    {
        protected override Geometry GetGeometry()
        {
            return GeometryFactory.Parallelogram;
        }
    }
}
