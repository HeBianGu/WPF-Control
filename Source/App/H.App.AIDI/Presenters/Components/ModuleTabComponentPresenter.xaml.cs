// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;

namespace H.App.AIDI.Presenters.Components;
public class ModuleTabComponentPresenter : RepositoryPresenterBase
{
    private readonly ITabModulePresenter _tabModulePresenter;
    public ModuleTabComponentPresenter(ITabModulePresenter tabModulePresenter)
    {
        this._tabModulePresenter = tabModulePresenter;
    }
    public ITabModulePresenter TabModulePresenter => this._tabModulePresenter;
}
