global using H.Controls.Diagram.Presenters.Workflow.Workflows;


namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "业务流程建模标注图", GroupName = "流程图", Order = 4)]
public class BusinessMarkWorkflow : WorkflowBase
{
    public BusinessMarkWorkflow()
    {
        this.LinkDrawer = new BrokenLinkDrawer();
        //this.NodeGroups = NodeFactory.GetNodeGroups(DiagramType.AuditWorkflow)?.ToObservable();
    }
}
