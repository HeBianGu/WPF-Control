// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.More)]
[Display(Name = "其他模块", Description = "图像处理的其他算法", Order = 100)]
public class OtherDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IOtherOpenCVNodeData).Assembly.GetInstances<IOtherOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
