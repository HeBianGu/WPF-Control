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

public interface ITemplateMatchingDataGroup : INodeDataGroup
{

}
[Icon(FontIcons.GotoToday)]
[Display(Name = "模板匹配", GroupName = "节点分组", Description = "提供灰度、形状、颜色和特征点模板匹配节点", Order = 10600)]
public class TemplateMatchingDataGroup : AssemblyNodeDataGroupBase, IImageDataGroup, ITemplateMatchingDataGroup
{
    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.GetType().Assembly.GetInstances<ITemplateMatchingGroupableNodeData>().OrderBy(x => x.Order);
    }
}

public interface ITemplateMatchingGroupableNodeData : INodeData, IDisplayBindable
{

}
