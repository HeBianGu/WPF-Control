

using System.ComponentModel.DataAnnotations;


namespace H.Controls.Diagram.Extensions.Workflow
{
    [Display(Name = "数据流程图", GroupName = "流程图", Order = 5)]
    public class DataWorkflow : WorkflowBase
    {
        public DataWorkflow()
        {
            this.LinkDrawer = new BrokenLinkDrawer();
            //this.NodeGroups = NodeFactory.GetNodeGroups(DiagramType.AuditWorkflow)?.ToObservable();
        }
    }
}
