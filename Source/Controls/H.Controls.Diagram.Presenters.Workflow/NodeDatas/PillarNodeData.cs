namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "数据库", GroupName = "基本流程图形状", Order = 13, Description = "数据库")]
public class PillarNodeData : WorkflowNodeBase
{
    private double _radiusX = 50;
    /// <summary> 说明  </summary>
    public double RadiusX
    {
        get { return _radiusX; }
        set
        {
            _radiusX = value;
            RaisePropertyChanged();
        }
    }


    private double _radiusY = 10;
    /// <summary> 说明  </summary>
    public double RadiusY
    {
        get { return _radiusY; }
        set
        {
            _radiusY = value;
            RaisePropertyChanged();
        }
    }

    protected override Geometry GetGeometry()
    {
        return GeometryFactory.CreatePillar(this.Width, this.Height, this.RadiusX, this.RadiusY);
    }
}
