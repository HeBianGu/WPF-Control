namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "流程", GroupName = "基本流程图形状", Order = 3, Description = "流程")]
public class CornerRadiusNodeData : WorkflowNodeBase
{
    private double _value = 5;
    public double Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }

    protected override Geometry GetGeometry()
    {
        return GeometryFactory.CreateCornerRadius(this.Width, this.Height, this.Value);
    }
}
