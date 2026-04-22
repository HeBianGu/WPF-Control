// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.DB;
using H.App.AIDI.Presenters;

namespace H.App.AIDI.Base;
public interface IModuleViewPagePresenter : IPagePresenter
{
}

public abstract class ModuleViewPagePresenterBase : PagePresenterBase, IModuleViewPagePresenter
{
    public ModuleViewPagePresenterBase()
    {

    }
    public ModuleViewPagePresenterBase(AIDIProjectItem projcetItem) : base(projcetItem)
    {
    }

    public override bool Where(fm_dd_image entity)
    {
        return entity.PageID == InputPagePresenter.InputID || base.Where(entity);
    }
}






