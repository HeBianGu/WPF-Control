namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "准备", GroupName = "基本流程图形状", Order = 1, Description = "准备")]
public class HexagonNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Hexagon;
    }
}
