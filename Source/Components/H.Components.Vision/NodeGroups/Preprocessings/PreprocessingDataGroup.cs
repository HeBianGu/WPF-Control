// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels;

namespace H.Components.Vision.NodeGroups.Preprocessings;

public interface IPreprocessingGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.Color)]
[Display(Name = "图像预处理模块", Description = "对图像进行预处理操作", Order = 10100)]
public class PreprocessingDataGroup : NodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return this.GetType().Assembly.GetInstances<IPreprocessingGroupableNodeData>().OrderBy(x => x.Order);
    }
}

