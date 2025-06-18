// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Tablet)]
[Display(Name = "形态学模块", Description = "对图像进行腐蚀、膨胀、开运算和闭运算", Order = 4)]
public class MorphologyDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IMorphologyOpenCVNodeData).Assembly.GetInstances<IMorphologyOpenCVNodeData>().OrderBy(x => x.Order); ;
    }
}

