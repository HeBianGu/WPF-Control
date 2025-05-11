// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.DataBase.Repository;

namespace H.Presenters.Repository;

public class RepositoryPresenter
{
    private readonly IRepositoryBindable _viewModel;
    public RepositoryPresenter(Type type)
    {
        Type gType = typeof(IRepositoryBindable<>).MakeGenericType(type);
        this._viewModel = Ioc.GetService<IRepositoryBindable>(gType, true);
    }
    public IRepositoryBindable ViewModel => _viewModel;
}
