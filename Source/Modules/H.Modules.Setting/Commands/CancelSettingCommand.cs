// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Setting.Commands;

[Icon("\xE77F")]
[Display(Name = "取消", Description = "取消保存并系统设置页面")]
public class CancelSettingCommand : DisplayMarkupCommandBase
{
    public override void Execute(object parameter)
    {
        IocSetting.Instance.Cancel();
    }
}
