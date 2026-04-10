// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Iocable;
using System.Runtime.CompilerServices;

namespace H.Services.Logger;

public class IocLog : Ioc<ILogService>
{
    /// <summary>
    /// 记录调试级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    public static void Debug(params string[] messages)
    {
        Instance.Debug(messages);
    }

    /// <summary>
    /// 记录错误级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    public static void Error(params Exception[] messages)
    {
        Instance?.Error(messages);
    }

    /// <summary>
    /// 记录错误级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    public static void Error(params string[] messages)
    {
        Instance?.Error(messages);
    }

    /// <summary>
    /// 记录致命错误级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    public static void Fatal(params string[] messages)
    {
        Instance?.Fatal(messages);
    }

    /// <summary>
    /// 记录致命错误级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    public static void Fatal(params Exception[] messages)
    {
        Instance?.Fatal(messages);
    }

    /// <summary>
    /// 记录信息级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    public static void Info(params string[] messages)
    {
        Instance?.Info(messages);
    }

    /// <summary>
    /// 记录跟踪级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    public static void Trace(params string[] messages)
    {
        Instance?.Trace(messages);
    }

    /// <summary>
    /// 记录警告级别的日志消息。
    /// </summary>
    /// <param name="messages">要记录的日志消息。</param>
    public static void Warn(params string[] messages)
    {
        Instance?.Warn(messages);
    }
}
