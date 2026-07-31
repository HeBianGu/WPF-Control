// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;

namespace H.Components.VisionDiagram.NodeDataGroups;

public interface IConditionGroupableNodeData : INodeData, IOrderable
{

}

[Icon(FontIcons.Dial6)]
[Display(Name = "逻辑", Description = "对图像进行条件判断选择执行对应路径", Order = 10500)]
public class ConditionDataGroup : AssemblyVisionNodeDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.GetType().Assembly.GetInstances<IConditionGroupableNodeData>().OrderBy(x => x.Order);
    }
}

