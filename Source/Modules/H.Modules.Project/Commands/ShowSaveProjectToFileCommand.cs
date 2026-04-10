// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FontIcon;

namespace H.Modules.Project.Commands;

[Icon(FontIcons.SaveAs)]
[Display(Name = "导出项目文件", Description = "保存当前选中向导到配置文件中")]
public class ShowSaveProjectToFileCommand : ShowProjectCommandBase
{
    public override void Execute(object parameter)
    {
        var current = IocProject.Instance?.Current;
        if (current == null)
            return;
        IocProject.Instance?.ShowSaveToFile(current);
    }
    public override bool CanExecute(object parameter)
    {
        return IocProject.Instance?.Current != null;
    }
}
