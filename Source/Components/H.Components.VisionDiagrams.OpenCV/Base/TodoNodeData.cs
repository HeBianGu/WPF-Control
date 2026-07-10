// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.Base;

[Icon(FontIcons.SwitchUser)]
[Display(Name = "待完成节点", GroupName = "待完成", Order = 1, Description = "不包含实际逻辑，占位显示用于待完成的节点显示")]
public class TodoNodeData : OpenCVNodeDataBase
{
    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        return this.OK(fromImage.Clone().ToMatImage());
    }
}

