// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.DiagramDatas;
using H.Extensions.Mvvm.ViewModels;

namespace H.Components.Vision.NodeGroups.TemplateMatchings;

public interface ITemplateMatchingDataGroup : INodeDataGroup
{

}
[Icon(FontIcons.GotoToday)]
[Display(Name = "模板匹配模块", Description = "图像处理的基础检测", Order = 10700)]
public class TemplateMatchingDataGroup : NodeDataGroupBase, IImageDataGroup, ITemplateMatchingDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return this.GetType().Assembly.GetInstances<ITemplateMatchingGroupableNodeData>().OrderBy(x => x.Order);
    }
}

public interface ITemplateMatchingGroupableNodeData : INodeData, IDisplayBindable
{

}
