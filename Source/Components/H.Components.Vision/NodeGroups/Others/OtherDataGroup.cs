// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels;

namespace H.Components.Vision.NodeGroups.Others;

public interface IOtherGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.More)]
[Display(Name = "其他模块", Description = "图像处理的其他算法", Order = 10900)]
public class OtherDataGroup : NodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return this.GetType().Assembly.GetInstances<IOtherGroupableNodeData>().OrderBy(x => x.Order);
    }
}

