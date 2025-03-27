namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "文档", GroupName = "基本流程图形状", Order = 8, Description = "文档")]


public class FileNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.File;
    }
}
