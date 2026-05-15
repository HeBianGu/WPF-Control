// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels;

namespace H.Components.Vision.NodeGroups.Morphologys;

public interface IMorphologyGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.HomeGroup)]
[Display(Name = "形态学模块", Description = "对图像进行腐蚀、膨胀、开运算和闭运算", Order = 10400)]
public class MorphologyDataGroup : NodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return this.GetType().Assembly.GetInstances<IMorphologyGroupableNodeData>().OrderBy(x => x.Order);
    }
}

