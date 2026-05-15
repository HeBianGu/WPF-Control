// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels;

namespace H.Components.Vision.NodeGroups.Mesure;

public interface IMesureGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.Design)]
[Display(Name = "测量模块", Description = "测量图像对象", Order = 10700)]
public class MesureDataGroup : NodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return this.GetType().Assembly.GetInstances<IMesureGroupableNodeData>().OrderBy(x => x.Order);
    }
}

