// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "手动操作", GroupName = "基本流程图形状", Order = 16, Description = "手动操作")]
public class OperationNodeData : WorkflowNodeBase
{
    protected override Geometry GetGeometry()
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("F1M0,0 100,0 80,60 20,60Z") as Geometry;
    }
}
