// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Iocable;

namespace H.Services.Logger;

public interface IAppLogService
{
    void Log(string message, LogType level = LogType.Info);
}

public class AppLogger : Ioc<IAppLogService>
{
    public static void Log(string message, LogType level = LogType.Info)
    {
        Instance?.Log(message, level);
    }
    public static void Log(string[] messages, LogType level = LogType.Info)
    {
        foreach (var message in messages)
        {
            Instance?.Log(message, level);
        }
    }
    public static void Log(Exception[] messages, LogType level = LogType.Info)
    {
        foreach (var message in messages)
        {
            Instance?.Log(message.Message, level);
        }
    }
}