namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "内部存储器", GroupName = "基本流程图形状", Order = 10, Description = "内部存储器")]


public class StoreInNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("F1M0,0 100,0 100,60 0,60Z M10,0 L10,0 10,55Z  M5,10 2,10 95,10Z") as Geometry;
    }
}
