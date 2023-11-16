using System.Threading.Tasks;

namespace H.Controls.Diagram.Extension
{
    public interface IFlowableDiagram : IDiagram
    {
        DiagramFlowableMode StartMode { get; set; }
        DiagramFlowableState State { get; set; }
        string Message { get; set; }
        Task<bool?> Start();
        Task<bool?> Start(Node startNode);
    }
}
