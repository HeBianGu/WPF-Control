// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.Workflow.Workflows;

//[Display()]
[Display(Name = "泳道图", GroupName = "流程图", Order = 1)]
public class LaneWorkflow : WorkflowBase
{
    public LaneWorkflow()
    {
        this.Layout = new LaneLayout();
    }
}
