namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "循环界限", GroupName = "基本流程图形状", Order = 13, Description = "循环界限")]
public class WhileNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("F1M0,20 20,0 80,0  100,20 100,60 0,60Z") as Geometry;
    }
}
