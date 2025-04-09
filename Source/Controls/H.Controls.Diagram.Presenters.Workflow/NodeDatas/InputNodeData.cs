namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "手动输入", GroupName = "基本流程图形状", Order = 17, Description = "手动输入")]


public class InputNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("F1M0,20 100,0 100,60 0,60Z") as Geometry;
    }
}
