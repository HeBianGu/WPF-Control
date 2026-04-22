// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.App.AIDI.Presenters.Modules;
using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.App.AIDI.Presenters.PageTabs;
[Icon(FontIcons.InPrivate)]
[Display(Name = "重点学习区域")]
public class SegmentKeyAreaingPageTabPresenter : PageTabPresenterBase
{
    public SegmentKeyAreaingPageTabPresenter(SegmentModulePresenter pagePresenter) : base(pagePresenter)
    {
    }
}







