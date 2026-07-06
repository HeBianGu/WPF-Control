// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Image;
[Icon(FontIcons.Dial6)]
[Display(Name = "遍历分割结果", GroupName = "逻辑模块", Description = "设置像素阈值，根据阈值执行不同路径逻辑", Order = 20)]
public class ForeachSplitResultImageNodeData : ForeachNodeDataBase<IMatImage, IVisionResultImage<IMatImage>>, IConditionGroupableNodeData
{
    protected override FlowableResult<IMatImage> Invoke(IMatImage fromImage)
    {
        if (this.ItemSourceExpression == null)
            this.ItemSourceExpression = this.GetItemSourceExpressions().FirstOrDefault();
        var rv = this.GetExpressionValue<List<IVisionResultImage<IMatImage>>>(this.ItemSourceExpression);
        if (!rv.success)
            return this.Error(fromImage.ToMatImage(), "获取分割结果失败");
        foreach (var item in rv.value)
        {
            this.CurrentValue = item;
            if (rv.value.Last() == item)
                return this.OK(item.Image);
            this.InvokeFrameMatAsync(item.Image, true).Wait();
        }
        return this.OK(fromImage.ToMatImage());
    }
}
