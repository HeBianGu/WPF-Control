// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;
[Icon(FontIcons.Photo2)]
[Display(Name = "图片数据源", Description = "设置输入图像", Order = 0)]
public class ImageDataGroup : BasicDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IOpenCVImageNodeData).Assembly.GetInstances<IOpenCVImageNodeData>().OrderBy(x => x.Order); ;
    }
}
