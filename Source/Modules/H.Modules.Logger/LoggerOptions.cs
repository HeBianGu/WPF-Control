// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


using H.Extensions.Setting;
using H.Services.Logger;
using H.Services.Setting;

namespace H.Modules.Logger;
[Display(Name = "消息日志设置", GroupName = SettingGroupNames.GroupMessage, Description = "应用程序日志消息设置")]
public class LoggerOptions : IocOptionInstance<LoggerOptions>, ILoggerOptions
{
    private LogType _LogType = LogType.Info;
    [DefaultValue(LogType.Info)]
    [Display(Name = "消息日志记录级别")]
    public LogType LogType
    {
        get { return _LogType; }
        set
        {
            _LogType = value;
            RaisePropertyChanged();
        }
    }

    private int _Capacity = 100;
    [DefaultValue(100)]
    [Display(Name = "消息日志容量")]
    public int Capacity
    {
        get { return _Capacity; }
        set
        {
            _Capacity = value;
            RaisePropertyChanged();
        }
    }


}
