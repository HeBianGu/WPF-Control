// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Threading.Tasks;

namespace H.Controls.Diagram.Flowables;

public interface IFlowablePort : IFlowable
{
    IFlowableResult Invoke(Part previors, Port current);
    Task<IFlowableResult> InvokeAsync(Part previors, Port current);
    Task<IFlowableResult> TryInvokeAsync(Part previors, Port current);
}
