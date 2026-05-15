// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels;

namespace H.Components.Vision.NodeGroups.Detector;

public interface IDetectorGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.Search)]
[Display(Name = "对象识别模块", Description = "识别图像中的对象", Order = 10700)]
public class DetectorDataGroup : NodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return this.GetType().Assembly.GetInstances<IDetectorGroupableNodeData>().OrderBy(x => x.Order);
    }
}


