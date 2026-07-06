// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels;

namespace H.Components.VisionDiagram.NodeDataGroups;

public interface IOutputGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.Ethernet)]
[Display(Name = "结果输出", Description = "输出流程处理结果", Order = 10900)]
public class OutputDataGroup : NodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.GetType().Assembly.GetInstances<IOutputGroupableNodeData>().OrderBy(x => x.Order);
    }
}

