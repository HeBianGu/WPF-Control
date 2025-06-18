// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Camera)]
[Display(Name = "视频捕获模块", Description = "设置输入视频捕获图片", Order = 0)]
public class VideoCaptureDataGroup : VideoDataGroupBase, IVideoDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IVideoNodeData).Assembly.GetInstances<IVideoNodeData>().OrderBy(x => x.Order); ;
    }
}
