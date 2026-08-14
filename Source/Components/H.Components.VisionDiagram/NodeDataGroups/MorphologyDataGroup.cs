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

public interface IMorphologyGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.HomeGroup)]
[Display(Name = "形态学", GroupName = "节点分组", Description = "提供腐蚀、膨胀、开闭运算和形态学梯度节点", Order = 10400)]
public class MorphologyDataGroup : AssemblyNodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.GetType().Assembly.GetInstances<IMorphologyGroupableNodeData>().OrderBy(x => x.Order);
    }
}

