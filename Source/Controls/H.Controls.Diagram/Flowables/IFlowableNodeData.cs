// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Controls.Diagram.Parts.Base;
using H.Mvvm.ViewModels.Base;
using System.Threading.Tasks;

namespace H.Controls.Diagram.Flowables;

public interface IFlowableNodeData : IFlowable, IMessageable
{
    public bool UseStart { get; set; }
    IFlowableResult Invoke(Part previors, Node current);
    Task<IFlowableResult> InvokeAsync(Part previors, Node current);
    Task<IFlowableResult> TryInvokeAsync(Part previors, Node current);
}
