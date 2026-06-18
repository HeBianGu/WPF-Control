// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Computer.ComputerProcesses;


[Display(Name = "我的电脑", Description = "显示我的电脑", GroupName = "系统", Order = 0, ShortName = "My Computer", Prompt = "显示我的电脑")]
[ComputerProcessInfo(ComputerProcessKeys.MyComputer)]
public class ShowMyComputerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "控制面板", Description = "打开控制面板", GroupName = "系统", Order = 1, ShortName = "Control Panel", Prompt = "打开控制面板")]
[ComputerProcessInfo(ComputerProcessKeys.Control)]
public class ShowControlProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "回收站", Description = "打开回收站", GroupName = "系统", Order = 2, ShortName = "Recycle Bin", Prompt = "打开回收站")]
[ComputerProcessInfo(ComputerProcessKeys.Recycle)]
public class ShowRecycleProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "网络邻居", Description = "打开网络邻居", GroupName = "系统", Order = 3, ShortName = "Network", Prompt = "打开网络邻居")]
[ComputerProcessInfo(ComputerProcessKeys.NetNeiborhood)]
public class ShowNetNeiborhoodProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "资源管理器", Description = "打开资源管理器", GroupName = "系统", Order = 4, ShortName = "Explorer", Prompt = "打开资源管理器")]
[ComputerProcessInfo(ComputerProcessKeys.Explorer)]
public class ShowExplorerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "设置", Description = "打开设置", GroupName = "系统", Order = 5, ShortName = "Settings", Prompt = "打开设置")]
[ComputerProcessInfo(ComputerProcessKeys.Settings)]
public class ShowSettingsProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "本机用户和组", Description = "打开本机用户和组", GroupName = "管理", Order = 10, ShortName = "Local Users and Groups", Prompt = "打开本机用户和组")]
[ComputerProcessInfo(ComputerProcessKeys.UserGroup)]
public class ShowUserGroupProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "计算机管理", Description = "打开计算机管理", GroupName = "管理", Order = 11, ShortName = "Computer Management", Prompt = "打开计算机管理")]
[ComputerProcessInfo(ComputerProcessKeys.ComputerManagement)]
public class ShowComputerManagementProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "设备管理器", Description = "打开设备管理器", GroupName = "管理", Order = 12, ShortName = "Device Manager", Prompt = "打开设备管理器")]
[ComputerProcessInfo(ComputerProcessKeys.DeviceManager)]
public class ShowDeviceManagerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "磁盘管理", Description = "打开磁盘管理", GroupName = "管理", Order = 13, ShortName = "Disk Management", Prompt = "打开磁盘管理")]
[ComputerProcessInfo(ComputerProcessKeys.DiskManagement)]
public class ShowDiskManagementProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "服务", Description = "打开服务", GroupName = "管理", Order = 14, ShortName = "Services", Prompt = "打开服务")]
[ComputerProcessInfo(ComputerProcessKeys.Services)]
public class ShowServicesProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "事件查看器", Description = "打开事件查看器", GroupName = "管理", Order = 15, ShortName = "Event Viewer", Prompt = "打开事件查看器")]
[ComputerProcessInfo(ComputerProcessKeys.EventViewer)]
public class ShowEventViewerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "任务计划程序", Description = "打开任务计划程序", GroupName = "管理", Order = 16, ShortName = "Task Scheduler", Prompt = "打开任务计划程序")]
[ComputerProcessInfo(ComputerProcessKeys.TaskScheduler)]
public class ShowTaskSchedulerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "性能监视器", Description = "打开性能监视器", GroupName = "管理", Order = 17, ShortName = "Performance Monitor", Prompt = "打开性能监视器")]
[ComputerProcessInfo(ComputerProcessKeys.PerformanceMonitor)]
public class ShowPerformanceMonitorProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "共享文件夹", Description = "打开共享文件夹", GroupName = "管理", Order = 18, ShortName = "Shared Folders", Prompt = "打开共享文件夹")]
[ComputerProcessInfo(ComputerProcessKeys.SharedFolders)]
public class ShowSharedFoldersProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "组策略编辑器", Description = "打开组策略编辑器", GroupName = "管理", Order = 19, ShortName = "Group Policy", Prompt = "打开组策略编辑器")]
[ComputerProcessInfo(ComputerProcessKeys.GroupPolicy)]
public class ShowGroupPolicyProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "证书管理器", Description = "打开证书管理器", GroupName = "管理", Order = 20, ShortName = "Certificates", Prompt = "打开证书管理器")]
[ComputerProcessInfo(ComputerProcessKeys.Certificates)]
public class ShowCertificatesProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "注册表编辑器", Description = "打开注册表编辑器", GroupName = "管理", Order = 21, ShortName = "Registry Editor", Prompt = "打开注册表编辑器")]
[ComputerProcessInfo(ComputerProcessKeys.RegistryEditor)]
public class ShowRegistryEditorProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "任务管理器", Description = "打开任务管理器", GroupName = "工具", Order = 30, ShortName = "Task Manager", Prompt = "打开任务管理器")]
[ComputerProcessInfo(ComputerProcessKeys.TaskManager)]
public class ShowTaskManagerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "系统配置", Description = "打开系统配置", GroupName = "工具", Order = 31, ShortName = "System Configuration", Prompt = "打开系统配置")]
[ComputerProcessInfo(ComputerProcessKeys.SystemConfiguration)]
public class ShowSystemConfigurationProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "系统信息", Description = "打开系统信息", GroupName = "工具", Order = 32, ShortName = "System Information", Prompt = "打开系统信息")]
[ComputerProcessInfo(ComputerProcessKeys.SystemInformation)]
public class ShowSystemInformationProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "远程桌面连接", Description = "打开远程桌面连接", GroupName = "工具", Order = 33, ShortName = "Remote Desktop", Prompt = "打开远程桌面连接")]
[ComputerProcessInfo(ComputerProcessKeys.RemoteDesktop)]
public class ShowRemoteDesktopProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "命令提示符", Description = "打开命令提示符", GroupName = "工具", Order = 34, ShortName = "Command Prompt", Prompt = "打开命令提示符")]
[ComputerProcessInfo(ComputerProcessKeys.CommandPrompt)]
public class ShowCommandPromptProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "PowerShell", Description = "打开PowerShell", GroupName = "工具", Order = 35, ShortName = "PowerShell", Prompt = "打开PowerShell")]
[ComputerProcessInfo(ComputerProcessKeys.PowerShell)]
public class ShowPowerShellProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "记事本", Description = "打开记事本", GroupName = "应用", Order = 40, ShortName = "Notepad", Prompt = "打开记事本")]
[ComputerProcessInfo(ComputerProcessKeys.Notepad)]
public class ShowNotepadProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "计算器", Description = "打开计算器", GroupName = "应用", Order = 41, ShortName = "Calculator", Prompt = "打开计算器")]
[ComputerProcessInfo(ComputerProcessKeys.Calculator)]
public class ShowCalculatorProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "画图", Description = "打开画图", GroupName = "应用", Order = 42, ShortName = "Paint", Prompt = "打开画图")]
[ComputerProcessInfo(ComputerProcessKeys.Paint)]
public class ShowPaintProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "截图工具", Description = "打开截图工具", GroupName = "应用", Order = 43, ShortName = "Snipping Tool", Prompt = "打开截图工具")]
[ComputerProcessInfo(ComputerProcessKeys.SnippingTool)]
public class ShowSnippingToolProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "写字板", Description = "打开写字板", GroupName = "应用", Order = 44, ShortName = "WordPad", Prompt = "打开写字板")]
[ComputerProcessInfo(ComputerProcessKeys.WordPad)]
public class ShowWordPadProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "放大镜", Description = "打开放大镜", GroupName = "辅助功能", Order = 50, ShortName = "Magnifier", Prompt = "打开放大镜")]
[ComputerProcessInfo(ComputerProcessKeys.Magnifier)]
public class ShowMagnifierProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "屏幕键盘", Description = "打开屏幕键盘", GroupName = "辅助功能", Order = 51, ShortName = "On-Screen Keyboard", Prompt = "打开屏幕键盘")]
[ComputerProcessInfo(ComputerProcessKeys.OnScreenKeyboard)]
public class ShowOnScreenKeyboardProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "字符映射表", Description = "打开字符映射表", GroupName = "辅助功能", Order = 52, ShortName = "Character Map", Prompt = "打开字符映射表")]
[ComputerProcessInfo(ComputerProcessKeys.CharacterMap)]
public class ShowCharacterMapProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "清理磁盘", Description = "打开清理磁盘", GroupName = "工具", Order = 60, ShortName = "Disk Cleanup", Prompt = "打开清理磁盘")]
[ComputerProcessInfo(ComputerProcessKeys.DiskCleanup)]
public class ShowDiskCleanupProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "资源监视器", Description = "打开资源监视器", GroupName = "工具", Order = 61, ShortName = "Resource Monitor", Prompt = "打开资源监视器")]
[ComputerProcessInfo(ComputerProcessKeys.ResourceMonitor)]
public class ShowResourceMonitorProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "诊断工具", Description = "打开DirectX诊断工具", GroupName = "工具", Order = 62, ShortName = "DirectX Diagnostic Tool", Prompt = "打开DirectX诊断工具")]
[ComputerProcessInfo(ComputerProcessKeys.DirectXDiagnosticTool)]
public class ShowDirectXDiagnosticToolProcessCommand : ShowComputerProcessCommandBase
{

}
