﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using H.Mvvm;
using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Setting
{
    [Icon("\xE77F")]
    [Display(Name = "清空缓存", Description = "清空当前应用程序所保存的缓存数据")]
    public class ClearCacheDataCommand : DisplayMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            AppPaths.Instance.ClearCache();
        }
    }
}
