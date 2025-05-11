// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Dial6)]
[Display(Name = "条件判断", Description = "对图像进行条件判断选择执行对应路径", Order = 2)]
public class IfConditionDataGroup : BasicDataGroupBase, IImageDataGroup, IVideoDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IIfConditionNodeData).Assembly.GetInstances<IIfConditionNodeData>().OrderBy(x => x.Order);
    }
}

