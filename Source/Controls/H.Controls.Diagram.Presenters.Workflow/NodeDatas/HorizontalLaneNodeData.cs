// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "水平泳道", GroupName = "泳道图形状", Order = 0, Description = "水平泳道")]

public class HorizontalLaneNodeData : LaneNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Width = 800;
        this.Height = 200;
    }

    protected override Geometry GetGeometry()
    {
        return Geometry.Empty;
    }
}
