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

public interface IPreprocessingGroupableNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.Color)]
[Display(Name = "图像预处理", GroupName = "节点分组", Description = "提供颜色转换、尺寸调整和像素运算等预处理节点", Order = 10100)]
public class PreprocessingDataGroup : AssemblyNodeDataGroupBase, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateDatas()
    {
        return this.GetType().Assembly.GetInstances<IPreprocessingGroupableNodeData>().OrderBy(x => x.Order);
    }
}

