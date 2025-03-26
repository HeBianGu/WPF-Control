namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "纸带", GroupName = "基本流程图形状", Order = 5, Description = "纸带")]


public class WaveNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Wave;
    }
}
