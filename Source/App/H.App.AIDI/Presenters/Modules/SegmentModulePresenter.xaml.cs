// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.App.AIDI.Presenters.PageTabs;
using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.App.AIDI.Presenters.Modules;
[Icon(FontIcons.GenericScan)]
[Display(Name = "分割", GroupName = "深度学习功能", Description = "模块介绍:精确到像素级的特征检测工具，用于在图像\r\n中对复杂缺陷进行分割，可用于输出检测结果的图形属\r\n性(ex:面积、长边、短边等)\r\n应用场景:像素级ROI设定，不规则形状目标检测;小目\r\n标检测")]
public class SegmentModulePresenter : TabModulePresenterBase, ITabModulePresenter
{
    public SegmentModulePresenter()
    {

    }
    public SegmentModulePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {

    }

    protected override IEnumerable<IPageTabPresenter> CreatePageTabPresenters()
    {
        yield return new SegmentShowModePageTabPresenter(this);
        yield return new SegmentLabelingPageTabPresenter(this);
        yield return new SegmentKeyAreaingPageTabPresenter(this);
        yield return new ExceptAreaingPageTabPresenter(this);
    }
}




