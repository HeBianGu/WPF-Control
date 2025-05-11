// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Filter;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Filter)]
[Display(Name = "滤波", Description = "对图像进行滤波，降噪，模糊处理", Order = 2)]
public class FilterDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IFilterOpenCVNodeData).Assembly.GetInstances<IFilterOpenCVNodeData>().OrderBy(x => x.Order);
    }
}

