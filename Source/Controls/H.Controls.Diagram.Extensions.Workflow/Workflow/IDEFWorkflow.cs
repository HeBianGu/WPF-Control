

using System.ComponentModel.DataAnnotations;


namespace H.Controls.Diagram.Extensions.Workflow
{
    [Display(Name = "IDEF图", GroupName = "流程图", Order = 7)]
    public class IDEFWorkflow : WorkflowBase
    {
        public IDEFWorkflow()
        {
            this.LinkDrawer = new BrokenLinkDrawer();
            //this.NodeGroups = NodeFactory.GetNodeGroups(DiagramType.AuditWorkflow)?.ToObservable();
        }
    }
}
