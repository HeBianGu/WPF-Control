// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Other;
[Icon(FontIcons.EthernetError)]
[Display(Name = "NG", Description = "输出流程处理NG结果", Order = 10400)]
public class NGOutputNodeData : OutputNodeDataBase, IOutputGroupableNodeData
{
    public NGOutputNodeData()
    {
        this.UseInvokedPart = true;
    }
    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        return this.Error(fromImage, "NG");
    }
}

