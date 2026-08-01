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

public interface IBlurGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.InPrivate)]
[Display(Name = "滤波模块", Description = "对图像进行滤波，降噪，模糊处理", Order = 10200)]
public class BlurDataGroup : AssemblyNodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.GetType().Assembly.GetInstances<IBlurGroupableNodeData>().OrderBy(x => x.Order);
    }
}

