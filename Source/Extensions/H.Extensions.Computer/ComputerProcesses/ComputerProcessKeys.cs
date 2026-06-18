// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Computer.ComputerProcesses;

/// <summary>
/// 电脑系统进程
/// </summary>
public class ComputerProcessKeys
{
    /// <summary>
    /// 我的电脑
    /// </summary>
    [Display(Name = "我的电脑", Description = "我的电脑", GroupName = "系统", Order = 0)]
    public const string MyComputer = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
    /// <summary>
    /// 控制面板
    /// </summary>
    public const string Control = "control";
    /// <summary>
    /// 回收站
    /// </summary>
    public const string Recycle = "::{645FF040-5081-101B-9F08-00AA002F954E}";

    /// <summary>
    /// 网络邻居
    /// </summary>
    public const string NetNeiborhood = "::{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}";

    /// <summary>
    /// 资源管理器
    /// </summary>
    public const string Explorer = "explorer";

    /// <summary>
    /// 设置
    /// </summary>
    public const string Settings = "ms-settings:";

    /// <summary>
    /// 本机用户和组
    /// </summary>
    public const string UserGroup = "lusrmgr.msc";

    /// <summary>
    /// 计算机管理
    /// </summary>
    public const string ComputerManagement = "compmgmt.msc";

    /// <summary>
    /// 设备管理器
    /// </summary>
    public const string DeviceManager = "devmgmt.msc";

    /// <summary>
    /// 磁盘管理
    /// </summary>
    public const string DiskManagement = "diskmgmt.msc";

    /// <summary>
    /// 服务
    /// </summary>
    public const string Services = "services.msc";

    /// <summary>
    /// 事件查看器
    /// </summary>
    public const string EventViewer = "eventvwr.msc";

    /// <summary>
    /// 任务计划程序
    /// </summary>
    public const string TaskScheduler = "taskschd.msc";

    /// <summary>
    /// 性能监视器
    /// </summary>
    public const string PerformanceMonitor = "perfmon.msc";

    /// <summary>
    /// 共享文件夹
    /// </summary>
    public const string SharedFolders = "fsmgmt.msc";

    /// <summary>
    /// 组策略编辑器
    /// </summary>
    public const string GroupPolicy = "gpedit.msc";

    /// <summary>
    /// 证书管理器
    /// </summary>
    public const string Certificates = "certmgr.msc";

    /// <summary>
    /// 注册表编辑器
    /// </summary>
    public const string RegistryEditor = "regedit";

    /// <summary>
    /// 任务管理器
    /// </summary>
    public const string TaskManager = "taskmgr";

    /// <summary>
    /// 系统配置
    /// </summary>
    public const string SystemConfiguration = "msconfig";

    /// <summary>
    /// 系统信息
    /// </summary>
    public const string SystemInformation = "msinfo32";

    /// <summary>
    /// 远程桌面连接
    /// </summary>
    public const string RemoteDesktop = "mstsc";

    /// <summary>
    /// 命令提示符
    /// </summary>
    public const string CommandPrompt = "cmd";

    /// <summary>
    /// PowerShell
    /// </summary>
    public const string PowerShell = "powershell";

    /// <summary>
    /// 记事本
    /// </summary>
    public const string Notepad = "notepad";

    /// <summary>
    /// 计算器
    /// </summary>
    public const string Calculator = "calc";

    /// <summary>
    /// 画图
    /// </summary>
    public const string Paint = "mspaint";

    /// <summary>
    /// 截图工具
    /// </summary>
    public const string SnippingTool = "snippingtool";

    /// <summary>
    /// 写字板
    /// </summary>
    public const string WordPad = "write";

    /// <summary>
    /// 放大镜
    /// </summary>
    public const string Magnifier = "magnify";

    /// <summary>
    /// 屏幕键盘
    /// </summary>
    public const string OnScreenKeyboard = "osk";

    /// <summary>
    /// 字符映射表
    /// </summary>
    public const string CharacterMap = "charmap";

    /// <summary>
    /// 清理磁盘
    /// </summary>
    public const string DiskCleanup = "cleanmgr";

    /// <summary>
    /// 资源监视器
    /// </summary>
    public const string ResourceMonitor = "resmon";

    /// <summary>
    /// 诊断工具
    /// </summary>
    public const string DirectXDiagnosticTool = "dxdiag";

}
