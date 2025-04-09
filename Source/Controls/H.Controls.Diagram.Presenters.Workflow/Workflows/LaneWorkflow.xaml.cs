// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
