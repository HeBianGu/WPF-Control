// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using H.Iocable;
using H.Services.Common.Theme;
using H.Services.Logger;

namespace H.Modules.Logger;
public class AppLogService : IAppLogService
{
    public void Log(string message, LogType level = LogType.Info)
    {
        var presenter = Ioc.GetService<IAppLogPresenter>();
        if (presenter == null)
            return;
        presenter.Messages.Insert(0, new AppLogMessage(message, level));
        if (presenter.Messages.Count > LoggerOptions.Instance.Capacity)
        {
            if (presenter.Messages.Count == LoggerOptions.Instance.Capacity + 1)
                presenter.Messages.RemoveAt(LoggerOptions.Instance.Capacity);
            else
                presenter.Messages = presenter.Messages.Take(LoggerOptions.Instance.Capacity).ToObservable();

        }

    }
}
