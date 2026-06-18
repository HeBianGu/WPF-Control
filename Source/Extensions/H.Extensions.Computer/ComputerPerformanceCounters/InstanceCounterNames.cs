// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Diagnostics;

namespace H.Extensions.Computer.ComputerPerformanceCounters;

public class InstanceCounterNames
{
    /// <summary>
    /// 总计
    /// </summary>
    public const string Total = "_Total";

    /// <summary>
    /// 空闲进程
    /// </summary>
    public const string Idle = "Idle";

    /// <summary>
    /// 系统进程
    /// </summary>
    public const string System = "System";

    /// <summary>
    /// 当前进程
    /// </summary>
    public static string CurrentProcess => Process.GetCurrentProcess().ProcessName;

    /// <summary>
    /// 全部实例
    /// </summary>
    public const string AllInstances = "*";
}
