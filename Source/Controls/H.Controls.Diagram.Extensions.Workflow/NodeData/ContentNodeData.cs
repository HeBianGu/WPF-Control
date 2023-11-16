

using H.Controls.Diagram.Extension;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Controls.Diagram.Extensions.Workflow
{
    [Display(Name = "显示内容", GroupName = "基本流程图形状", Order = 5, Description = "显示内容")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class ContentNodeData : WorkflowNodeBase
    {
        protected override Geometry GetGeometry()
        {
            GeometryConverter converter = new GeometryConverter();
            return converter.ConvertFromString("M80,0 80,0 A 35,35 0 0 1 80,60 L80,60 30,60 Q-30,30 30,0Z") as Geometry;
        }
    }
}
