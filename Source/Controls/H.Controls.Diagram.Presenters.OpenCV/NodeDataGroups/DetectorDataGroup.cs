// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;
[Icon(FontIcons.Photo)]
[Display(Name = "基础检测模块", Description = "图像处理的基础检测", Order = 3)]
public class DetectorDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IDetectorOpenCVNodeData).Assembly.GetInstances<IDetectorOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
