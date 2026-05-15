// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels;

namespace H.Components.Vision.NodeGroups.Rectifications;

public interface IRectificationGroupableNodeData : INodeData, IDisplayBindable
{

}


[Icon(FontIcons.Rotate)]
[Display(Name = "图像校正模块", Description = "校正图像的对象", Order = 10300)]
public class RectificationDataGroup : NodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return this.GetType().Assembly.GetInstances<IRectificationGroupableNodeData>().OrderBy(x => x.Order);
    }
}


