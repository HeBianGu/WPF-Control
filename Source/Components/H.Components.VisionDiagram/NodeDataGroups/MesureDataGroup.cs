// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.NodeDataGroups;
using H.Extensions.Mvvm.ViewModels;

namespace H.Components.VisionDiagram.NodeDataGroups;

public interface IMesureGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.Design)]
[Display(Name = "几何测量", GroupName = "节点分组", Description = "提供点、线、圆之间距离和角度的测量节点", Order = 10700)]
public class MesureDataGroup : AssemblyNodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.GetType().Assembly.GetInstances<IMesureGroupableNodeData>().OrderBy(x => x.Order);
    }
}

