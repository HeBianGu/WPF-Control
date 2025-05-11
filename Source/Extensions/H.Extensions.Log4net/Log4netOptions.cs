// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.Setting;
using H.Services.AppPath;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Log4net;
[Icon("\xEC87")]
[Display(Name = "日志配置", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
public class Log4netOptions : IocOptionInstance<Log4netOptions>, ILog4netOptions
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.LogPath = AppPaths.Instance.Log;
        this.tempPath = AppPaths.Instance.Cache;
    }

    private string _logPath;
    [Display(Name = "日志路径")]
    public string LogPath
    {
        get { return _logPath; }
        set
        {
            _logPath = value;
            RaisePropertyChanged();
        }
    }

    private string _tempPath;
    [Display(Name = "缓存路径")]
    public string tempPath
    {
        get { return _tempPath; }
        set
        {
            _tempPath = value;
            RaisePropertyChanged();
        }
    }
}
