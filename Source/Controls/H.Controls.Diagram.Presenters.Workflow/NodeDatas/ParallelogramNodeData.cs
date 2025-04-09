namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "数据", GroupName = "基本流程图形状", Order = 6, Description = "数据")]


public class ParallelogramNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Parallelogram;
    }
}
