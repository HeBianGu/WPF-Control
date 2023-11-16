// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Threading.Tasks;

namespace H.Controls.Diagram
{
    public interface IFlowablePort : IFlowable
    {
        IFlowableResult Invoke(Part previors, Port current);
        Task<IFlowableResult> InvokeAsync(Part previors, Port current);
        Task<IFlowableResult> TryInvokeAsync(Part previors, Port current);
    }
}
