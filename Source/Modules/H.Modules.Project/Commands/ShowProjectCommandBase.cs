// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Services.Message;
global using H.Services.Message.Dialog.Commands;

namespace H.Modules.Project.Commands;

public abstract class ShowProjectCommandBase : ShowIocPresenterCommandBase<IProjectService>
{
    public override bool CanExecute(object parameter)
    {
        return IocProject.Instance != null && base.CanExecute(parameter);
    }
}