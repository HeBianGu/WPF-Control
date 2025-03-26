namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "开始或结束", GroupName = "基本流程图形状", Order = 2, Description = "开始或结束")]


public class RunwayNodeData : WorkflowNodeBase
{
    public RunwayNodeData()
    {
        this.UseStart = true;
    }
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.CreateRunway(this.Width, this.Height);
    }
}
