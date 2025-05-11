// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Common;
namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;
[Icon("\xE790")]
[Display(Name = "基础功能", Description = "图像处理的基础功能", Order = 1)]
public class BasicDataGroup : BasicDataGroupBase, IImageDataGroup, IVideoDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IBasicOpenCVNodeData).Assembly.GetInstances<IBasicOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
