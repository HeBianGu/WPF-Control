// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "跨页引用", GroupName = "基本流程图形状", Order = 11, Description = "跨页引用")]
public class ReferenceNodeData : WorkflowNodeBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Height = 100;
        this.Width = 100;
    }
    protected override Geometry GetGeometry()
    {
        this.Height = 100;
        this.Width = 100;
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("F1M0,0 100,0 100,50 50,100 0,50Z") as Geometry;
    }
}
