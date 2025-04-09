namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "卡", GroupName = "基本流程图形状", Order = 9, Description = "卡")]
public class CardNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("F1M0,20 20,0 100,0 100,60 0,60Z") as Geometry;
    }
}
