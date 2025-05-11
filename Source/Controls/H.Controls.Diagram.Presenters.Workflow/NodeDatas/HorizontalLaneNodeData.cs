// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
