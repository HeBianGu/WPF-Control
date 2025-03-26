namespace H.Controls.Diagram.Presenters.Workflow.Workflows;

[Display(Name = "审计流程图", GroupName = "流程图", Order = 3)]
public class AuditWorkflow : WorkflowBase
{
    public AuditWorkflow()
    {
        this.LinkDrawer = new BrokenLinkDrawer();
        //this.NodeGroups = NodeFactory.GetNodeGroups(DiagramType.AuditWorkflow)?.ToObservable();
    }
}
