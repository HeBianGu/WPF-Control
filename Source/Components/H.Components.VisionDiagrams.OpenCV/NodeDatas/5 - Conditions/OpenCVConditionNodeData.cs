// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Image;
[Icon(FontIcons.Dial6)]
[Display(Name = "条件分支", GroupName = "逻辑模块", Description = "设置像素阈值，根据阈值执行不同路径逻辑", Order = 20)]
public class OpenCVConditionNodeData : ConditionNodeData<IMatImage>, IOnDiagramDeserialized, IConditionGroupableNodeData
{
    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        return this.OK(fromImage);
    }
}

