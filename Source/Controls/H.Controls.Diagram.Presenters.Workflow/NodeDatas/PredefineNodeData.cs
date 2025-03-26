namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "预定义流程", GroupName = "基本流程图形状", Order = 12, Description = "预定义流程")]


public class PredefineNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("F1M0,0 100,0 100,60 0,60Z M90,0 L90,0 90,55Z M10,0 L10,0 10,55Z  M10,0 L10,0 0,55Z M0,0 0,0 10,55z") as Geometry;
    }
}
