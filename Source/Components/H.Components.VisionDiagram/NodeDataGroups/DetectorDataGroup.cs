// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.NodeDataGroups;
using H.Extensions.Mvvm.ViewModels;

namespace H.Components.VisionDiagram.NodeDataGroups;

public interface IDetectorGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.Search)]
[Display(Name = "目标检测", GroupName = "节点分组", Description = "提供图像目标、边缘和几何特征检测节点", Order = 10700)]
public class DetectorDataGroup : AssemblyNodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.GetType().Assembly.GetInstances<IDetectorGroupableNodeData>().OrderBy(x => x.Order);
    }
}


