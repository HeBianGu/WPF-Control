namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "手动操作", GroupName = "基本流程图形状", Order = 16, Description = "手动操作")]
public class OperationNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("F1M0,0 100,0 80,60 20,60Z") as Geometry;
    }
}
