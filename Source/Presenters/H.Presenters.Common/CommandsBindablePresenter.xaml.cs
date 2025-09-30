// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.ViewModels.Base;

namespace H.Presenters.Common;

/// <summary>
/// 显示并绑定CommandsBindableBase中的命令
/// </summary>
[Icon("\xEDE3")]
public class CommandsBindablePresenter : BindableBase
{
    public CommandsBindablePresenter(CommandsBindableBase presenter)
    {
        this.Presenter = presenter;
    }
    public CommandsBindableBase Presenter { get; set; }
}
