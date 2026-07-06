// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Image;
[Icon(FontIcons.Dial6)]
[Display(Name = "循环次数", GroupName = "逻辑模块", Description = "设置像素阈值，根据阈值执行不同路径逻辑", Order = 20)]
public class ForNodeData : ForNodeDataBase<IMatImage>, IConditionGroupableNodeData
{
    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        for (int i = this.From; i < this.To; i++)
        {
            this.CurrentIndex = i + this.From;
            this.InvokeFrameMatAsync(new MatImage(fromImage.Mat.Clone())).Wait();
        }
        return this.OK(fromImage);
    }
}
