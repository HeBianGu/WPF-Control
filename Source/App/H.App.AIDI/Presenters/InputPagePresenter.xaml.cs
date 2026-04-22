// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.App.AIDI.DB;
using H.App.AIDI.Presenters.PageTabs;
using H.Common.Attributes;

namespace H.App.AIDI.Presenters;
[ID(InputID)]
public class InputPagePresenter : PagePresenterBase
{
    public const string InputID = "67090490-46BE-4C43-8329-10BAE7FE24CC";
    public InputPagePresenter()
    {

    }
    public InputPagePresenter(AIDIProjectItem projcetItem) : base(projcetItem)
    {
    }

    public override bool ResultWhere(fm_dd_image entity)
    {
        return true;
    }

    public override bool Where(fm_dd_image entity)
    {
        return entity.PageID == InputID || base.Where(entity);
    }

    protected override IEnumerable<IPageTabPresenter> CreatePageTabPresenters()
    {
        yield return new InputShowModePageTabPresenter(this);
    }
}




