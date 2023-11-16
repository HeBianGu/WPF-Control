
using H.Controls.Diagram.Extension;
using H.Extensions.Geometry;

using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Controls.Diagram.Extensions.Workflow
{
    [Display(Name = "开始", GroupName = "基本流程图形状", Order = 6, Description = "开始")]
    [NodeType(DiagramType = typeof(LaneWorkflow))]
    [NodeType(DiagramType = typeof(Workflow))]
    public class CircleNodeData : WorkflowNodeBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.Width = 70;
            this.Height = 70;
        }
        protected override Geometry GetGeometry()
        {
            return GeometryFactory.CreateCircle(35);
        }
    }
}
