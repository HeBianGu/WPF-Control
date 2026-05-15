// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter;
using H.Extensions.Mvvm.Commands;

namespace H.Components.Vision.PropertyPresenters;
public class InvokeCommandsPropertyPresenter : CommandsPropertyPresenter
{
    public InvokeCommandsPropertyPresenter(IDiagramableNodeData nodeData) : base(nodeData)
    {
        string[] names = this.GetUseGroupNames().ToArray();
        this.Presenter.UseGroupNames = string.Join(',', names);
        this.Presenter.UpdateTabNames();
    }

    public IEnumerable<string> GetUseGroupNames()
    {
        return typeof(VisionPropertyGroupNames).GetFields(BindingFlags.Public | BindingFlags.Static)
               .Where(x => x.FieldType == typeof(string))
               .Select(x => x.GetValue(null)?.ToString())
               .Where(x => !string.IsNullOrEmpty(x));
    }

    [Icon(FontIcons.Delete)]
    [Display(Name = "执行", GroupName = "操作")]
    public DisplayCommand InvokeCommand => new DisplayCommand(async x =>
    {
        //var srcs = this.NodeData.GetSelectedFromNodeDatas(this.NodeData.DiagramData).OfType<ISrcImageNodeData>();
        if (this.NodeData.DiagramData is IFlowableDiagramData flowableDiagramData)
            await flowableDiagramData.Start();
        this.NodeData.DiagramData.Select(this.NodeData);
    }, x =>
    {
        if (this.NodeData.DiagramData is IFlowableDiagramData flowableDiagramData)
            return flowableDiagramData.State.CanStart();
        return false;
    });
}

