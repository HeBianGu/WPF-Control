// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels;

namespace H.Components.VisionDiagram.NodeDataGroups;

public interface ITakeoffGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.Annotation)]
[Display(Name = "图像分割提取", Description = "对图像进行预处理操作", Order = 10300)]
public class TakeoffDataGroup : AssemblyVisionNodeDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.GetType().Assembly.GetInstances<ITakeoffGroupableNodeData>().OrderBy(x => x.Order);
    }
}

