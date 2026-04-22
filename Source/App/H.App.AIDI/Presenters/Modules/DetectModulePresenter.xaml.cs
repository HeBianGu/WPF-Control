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
[Icon(FontIcons.DialShape1)]
[Display(Name = "检测", GroupName = "深度学习功能", Description = "模块介绍:定位且对目标进行分类，可以检测多种类型\r\n的缺陷\r\n应用场景:成块特征检测;边缘模糊缺陷的检测，散点\r\n状缺陷的检测")]
public class DetectModulePresenter : TabModulePresenterBase, ITabModulePresenter
{
    public DetectModulePresenter()
    {

    }
    public DetectModulePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {

    }

    protected override IEnumerable<IPageTabPresenter> CreatePageTabPresenters()
    {
        yield return new DetectShowModePageTabPresenter(this);
        yield return new DetectLabelingPageTabPresenter(this);
        yield return new ExceptAreaingPageTabPresenter(this);
    }
}




