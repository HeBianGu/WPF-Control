// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Mvvm.Commands;
using H.Mvvm.Commands;
namespace H.Presenters.Design.Base;

public class CommandsDesignPresenterBase : CloneableDesignPresenterBase
{
    [Display(Name = "恢复默认")]
    public RelayCommand DefaultCommand => new RelayCommand(e =>
    {
        if (e is string project)
        {

        }
    });

    [Display(Name = "保存模板")]
    public RelayCommand SaveTempateCommand => new RelayCommand(e =>
    {
        if (e is string project)
        {

        }
    });
}
