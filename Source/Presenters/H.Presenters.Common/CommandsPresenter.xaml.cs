// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Presenters.Common;

public class CommandsPresenter : CommandsBindableBase
{
    public CommandsPresenter(object presenter)
    {
        this.Presenter = presenter;
    }
    public object Presenter { get; set; }
}

[Icon("\xEDE3")]
public class CommandsPresenterBase : CommandsBindableBase
{

}

public class CommandsPresenter<T> : CommandsPresenterBase
{
    public CommandsPresenter(T presenter)
    {
        this.Presenter = presenter;
    }
    public T Presenter { get; set; }
}
