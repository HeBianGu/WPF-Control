// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Threading.Tasks;

namespace H.Controls.Diagram
{
    public interface IFlowableNode : IFlowable
    {
        public bool UseStart { get; set; }
        IFlowableResult Invoke(Part previors, Node current);
        Task<IFlowableResult> InvokeAsync(Part previors, Node current);
        Task<IFlowableResult> TryInvokeAsync(Part previors, Node current);
    }
}
