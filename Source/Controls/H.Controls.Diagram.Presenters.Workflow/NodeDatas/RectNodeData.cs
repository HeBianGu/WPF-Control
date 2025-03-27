namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "矩形", GroupName = "基本流程图形状", Order = 0, Description = "行动方案、普通工作环节用")]


public class RectNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("M0,0 100,0 100,60 0,60 Z") as Geometry;
    }
}
