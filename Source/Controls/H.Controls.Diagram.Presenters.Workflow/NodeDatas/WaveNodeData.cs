// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "纸带", GroupName = "基本流程图形状", Order = 5, Description = "纸带")]

public class WaveNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Wave;
    }
}
