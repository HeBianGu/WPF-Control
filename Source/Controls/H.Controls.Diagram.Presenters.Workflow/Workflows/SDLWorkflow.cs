namespace H.Controls.Diagram.Presenters.Workflow.Workflows;

[Display(Name = "SDL图", GroupName = "流程图", Order = 8)]
public class SDLWorkflow : WorkflowBase
{
    public SDLWorkflow()
    {
        this.LinkDrawer = new BrokenLinkDrawer();
        //this.NodeGroups = NodeFactory.GetNodeGroups(DiagramType.AuditWorkflow)?.ToObservable();
    }
}
