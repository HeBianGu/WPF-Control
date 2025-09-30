// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Attributes;
global using H.Common.Commands;
using H.Services.AppPath;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.AppPath;

[Icon("\xE77F")]
[Display(Name = "清空缓存", Description = "清空当前应用程序所保存的缓存数据")]
public class ClearCacheDataCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        var r = AppPaths.Instance.ClearCache(out string message);
        if (r == false)
        {
            await IocMessage.Dialog.Show(message, x => x.Title = "清空缓存失败");
            return;
        }
        await base.ExecuteAsync(parameter);
    }
}
