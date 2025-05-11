// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Presenter.DiagramDatas;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.EmojiTabPeople)]
[Display(Name = "人脸检测", Description = "图像处理的人脸检测", Order = 1)]
public class CascadeClassifierDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(ICascadeClassifierOpenCVNodeData).Assembly.GetInstances<ICascadeClassifierOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
