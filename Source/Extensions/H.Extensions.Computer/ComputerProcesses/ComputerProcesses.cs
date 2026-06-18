// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Computer.ComputerProcesses;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ComputerProcessInfoAttribute : Attribute
{
    readonly string _fileName;
    public ComputerProcessInfoAttribute(string fileName)
    {
        this._fileName = fileName;
    }

    public string FileName
    {
        get { return _fileName; }
    }
}

public class ComputerProcessInfo
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string GroupName { get; set; }
    public int Order { get; set; }
    public string ShortName { get; set; }
    public string Prompt { get; set; }
    public string FileName { get; set; }
}

/// <summary>
/// 电脑系统进程
/// </summary>
public class ComputerProcesses
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

public interface IShowComputerProcessCommand : IStartComputerProcessCommand
{
    string FileName { get; set; }
}

public abstract class ShowComputerProcessCommandBase : StartComputerProcessCommandBase, IShowComputerProcessCommand
{
    public ShowComputerProcessCommandBase()
    {
        ComputerProcessInfoAttribute processInfoAttribute = this.GetType().GetCustomAttributes(typeof(ComputerProcessInfoAttribute), false)
            .OfType<ComputerProcessInfoAttribute>()
            .FirstOrDefault();
        if (processInfoAttribute != null)
            this.FileName = processInfoAttribute.FileName;
    }

    public string FileName { get; set; }

    protected override string GetFileName()
    {
        return this.FileName;
    }
}


[Display(Name = "我的电脑", Description = "显示我的电脑", GroupName = "系统", Order = 0, ShortName = "My Computer", Prompt = "显示我的电脑")]
[ComputerProcessInfo(ComputerProcesses.MyComputer)]
public class ShowMyComputerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "控制面板", Description = "打开控制面板", GroupName = "系统", Order = 1, ShortName = "Control Panel", Prompt = "打开控制面板")]
[ComputerProcessInfo(ComputerProcesses.Control)]
public class ShowControlProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "回收站", Description = "打开回收站", GroupName = "系统", Order = 2, ShortName = "Recycle Bin", Prompt = "打开回收站")]
[ComputerProcessInfo(ComputerProcesses.Recycle)]
public class ShowRecycleProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "网络邻居", Description = "打开网络邻居", GroupName = "系统", Order = 3, ShortName = "Network", Prompt = "打开网络邻居")]
[ComputerProcessInfo(ComputerProcesses.NetNeiborhood)]
public class ShowNetNeiborhoodProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "资源管理器", Description = "打开资源管理器", GroupName = "系统", Order = 4, ShortName = "Explorer", Prompt = "打开资源管理器")]
[ComputerProcessInfo(ComputerProcesses.Explorer)]
public class ShowExplorerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "设置", Description = "打开设置", GroupName = "系统", Order = 5, ShortName = "Settings", Prompt = "打开设置")]
[ComputerProcessInfo(ComputerProcesses.Settings)]
public class ShowSettingsProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "本机用户和组", Description = "打开本机用户和组", GroupName = "管理", Order = 10, ShortName = "Local Users and Groups", Prompt = "打开本机用户和组")]
[ComputerProcessInfo(ComputerProcesses.UserGroup)]
public class ShowUserGroupProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "计算机管理", Description = "打开计算机管理", GroupName = "管理", Order = 11, ShortName = "Computer Management", Prompt = "打开计算机管理")]
[ComputerProcessInfo(ComputerProcesses.ComputerManagement)]
public class ShowComputerManagementProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "设备管理器", Description = "打开设备管理器", GroupName = "管理", Order = 12, ShortName = "Device Manager", Prompt = "打开设备管理器")]
[ComputerProcessInfo(ComputerProcesses.DeviceManager)]
public class ShowDeviceManagerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "磁盘管理", Description = "打开磁盘管理", GroupName = "管理", Order = 13, ShortName = "Disk Management", Prompt = "打开磁盘管理")]
[ComputerProcessInfo(ComputerProcesses.DiskManagement)]
public class ShowDiskManagementProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "服务", Description = "打开服务", GroupName = "管理", Order = 14, ShortName = "Services", Prompt = "打开服务")]
[ComputerProcessInfo(ComputerProcesses.Services)]
public class ShowServicesProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "事件查看器", Description = "打开事件查看器", GroupName = "管理", Order = 15, ShortName = "Event Viewer", Prompt = "打开事件查看器")]
[ComputerProcessInfo(ComputerProcesses.EventViewer)]
public class ShowEventViewerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "任务计划程序", Description = "打开任务计划程序", GroupName = "管理", Order = 16, ShortName = "Task Scheduler", Prompt = "打开任务计划程序")]
[ComputerProcessInfo(ComputerProcesses.TaskScheduler)]
public class ShowTaskSchedulerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "性能监视器", Description = "打开性能监视器", GroupName = "管理", Order = 17, ShortName = "Performance Monitor", Prompt = "打开性能监视器")]
[ComputerProcessInfo(ComputerProcesses.PerformanceMonitor)]
public class ShowPerformanceMonitorProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "共享文件夹", Description = "打开共享文件夹", GroupName = "管理", Order = 18, ShortName = "Shared Folders", Prompt = "打开共享文件夹")]
[ComputerProcessInfo(ComputerProcesses.SharedFolders)]
public class ShowSharedFoldersProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "组策略编辑器", Description = "打开组策略编辑器", GroupName = "管理", Order = 19, ShortName = "Group Policy", Prompt = "打开组策略编辑器")]
[ComputerProcessInfo(ComputerProcesses.GroupPolicy)]
public class ShowGroupPolicyProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "证书管理器", Description = "打开证书管理器", GroupName = "管理", Order = 20, ShortName = "Certificates", Prompt = "打开证书管理器")]
[ComputerProcessInfo(ComputerProcesses.Certificates)]
public class ShowCertificatesProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "注册表编辑器", Description = "打开注册表编辑器", GroupName = "管理", Order = 21, ShortName = "Registry Editor", Prompt = "打开注册表编辑器")]
[ComputerProcessInfo(ComputerProcesses.RegistryEditor)]
public class ShowRegistryEditorProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "任务管理器", Description = "打开任务管理器", GroupName = "工具", Order = 30, ShortName = "Task Manager", Prompt = "打开任务管理器")]
[ComputerProcessInfo(ComputerProcesses.TaskManager)]
public class ShowTaskManagerProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "系统配置", Description = "打开系统配置", GroupName = "工具", Order = 31, ShortName = "System Configuration", Prompt = "打开系统配置")]
[ComputerProcessInfo(ComputerProcesses.SystemConfiguration)]
public class ShowSystemConfigurationProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "系统信息", Description = "打开系统信息", GroupName = "工具", Order = 32, ShortName = "System Information", Prompt = "打开系统信息")]
[ComputerProcessInfo(ComputerProcesses.SystemInformation)]
public class ShowSystemInformationProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "远程桌面连接", Description = "打开远程桌面连接", GroupName = "工具", Order = 33, ShortName = "Remote Desktop", Prompt = "打开远程桌面连接")]
[ComputerProcessInfo(ComputerProcesses.RemoteDesktop)]
public class ShowRemoteDesktopProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "命令提示符", Description = "打开命令提示符", GroupName = "工具", Order = 34, ShortName = "Command Prompt", Prompt = "打开命令提示符")]
[ComputerProcessInfo(ComputerProcesses.CommandPrompt)]
public class ShowCommandPromptProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "PowerShell", Description = "打开PowerShell", GroupName = "工具", Order = 35, ShortName = "PowerShell", Prompt = "打开PowerShell")]
[ComputerProcessInfo(ComputerProcesses.PowerShell)]
public class ShowPowerShellProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "记事本", Description = "打开记事本", GroupName = "应用", Order = 40, ShortName = "Notepad", Prompt = "打开记事本")]
[ComputerProcessInfo(ComputerProcesses.Notepad)]
public class ShowNotepadProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "计算器", Description = "打开计算器", GroupName = "应用", Order = 41, ShortName = "Calculator", Prompt = "打开计算器")]
[ComputerProcessInfo(ComputerProcesses.Calculator)]
public class ShowCalculatorProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "画图", Description = "打开画图", GroupName = "应用", Order = 42, ShortName = "Paint", Prompt = "打开画图")]
[ComputerProcessInfo(ComputerProcesses.Paint)]
public class ShowPaintProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "截图工具", Description = "打开截图工具", GroupName = "应用", Order = 43, ShortName = "Snipping Tool", Prompt = "打开截图工具")]
[ComputerProcessInfo(ComputerProcesses.SnippingTool)]
public class ShowSnippingToolProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "写字板", Description = "打开写字板", GroupName = "应用", Order = 44, ShortName = "WordPad", Prompt = "打开写字板")]
[ComputerProcessInfo(ComputerProcesses.WordPad)]
public class ShowWordPadProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "放大镜", Description = "打开放大镜", GroupName = "辅助功能", Order = 50, ShortName = "Magnifier", Prompt = "打开放大镜")]
[ComputerProcessInfo(ComputerProcesses.Magnifier)]
public class ShowMagnifierProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "屏幕键盘", Description = "打开屏幕键盘", GroupName = "辅助功能", Order = 51, ShortName = "On-Screen Keyboard", Prompt = "打开屏幕键盘")]
[ComputerProcessInfo(ComputerProcesses.OnScreenKeyboard)]
public class ShowOnScreenKeyboardProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "字符映射表", Description = "打开字符映射表", GroupName = "辅助功能", Order = 52, ShortName = "Character Map", Prompt = "打开字符映射表")]
[ComputerProcessInfo(ComputerProcesses.CharacterMap)]
public class ShowCharacterMapProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "清理磁盘", Description = "打开清理磁盘", GroupName = "工具", Order = 60, ShortName = "Disk Cleanup", Prompt = "打开清理磁盘")]
[ComputerProcessInfo(ComputerProcesses.DiskCleanup)]
public class ShowDiskCleanupProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "资源监视器", Description = "打开资源监视器", GroupName = "工具", Order = 61, ShortName = "Resource Monitor", Prompt = "打开资源监视器")]
[ComputerProcessInfo(ComputerProcesses.ResourceMonitor)]
public class ShowResourceMonitorProcessCommand : ShowComputerProcessCommandBase
{

}

[Display(Name = "诊断工具", Description = "打开DirectX诊断工具", GroupName = "工具", Order = 62, ShortName = "DirectX Diagnostic Tool", Prompt = "打开DirectX诊断工具")]
[ComputerProcessInfo(ComputerProcesses.DirectXDiagnosticTool)]
public class ShowDirectXDiagnosticToolProcessCommand : ShowComputerProcessCommandBase
{

}
