// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace H.Extensions.Computer;

/// <summary>
/// 电脑系统信息
/// </summary>
public class ComputerSystemInfo
{
    /// <summary>
    /// 操作系统说明
    /// </summary>
    [Display(Name = "操作系统说明", GroupName = "系统信息", Description = "操作系统的说明信息", Order = 0)]
    public string OSDescription { get; } = RuntimeInformation.OSDescription;

    /// <summary>
    /// 操作系统版本
    /// </summary>
    [Display(Name = "操作系统版本", GroupName = "系统信息", Description = "操作系统的版本信息", Order = 1)]
    public string OSVersion { get; } = Environment.OSVersion.VersionString;

    /// <summary>
    /// 操作系统架构（<see cref="Architecture">）
    /// </summary>
    [Display(Name = "操作系统架构", GroupName = "系统信息", Description = "操作系统的架构信息", Order = 2)]
    public string OSArchitecture { get; } = RuntimeInformation.OSArchitecture.ToString();

    /// <summary>
    /// 是否为Windows操作系统
    /// </summary>
    [Display(Name = "是否为Windows操作系统", GroupName = "系统信息", Description = "判断操作系统是否为Windows", Order = 3)]
    public bool IsOSPlatform { get; } = OperatingSystem.IsWindows();

    /// <summary>
    /// 是否为64位操作系统
    /// </summary>
    [Display(Name = "是否为64位操作系统", GroupName = "系统信息", Description = "判断操作系统是否为64位", Order = 4)]
    public bool Is64BitOperatingSystem { get; } = Environment.Is64BitOperatingSystem;

    /// <summary>
    /// 系统目录
    /// </summary>
    [Display(Name = "系统目录", GroupName = "系统信息", Description = "操作系统的系统目录", Order = 5)]
    public string SystemDirectory { get; } = Environment.SystemDirectory;

    /// <summary>
    /// 系统启动时长
    /// </summary>
    [Display(Name = "系统启动时长", GroupName = "系统信息", Description = "系统从启动到当前的运行时长", Order = 6)]
    public string SystemUptime { get; } = TimeSpan.FromMilliseconds(Environment.TickCount64).ToString("c");

    /// <summary>
    /// 当前时区
    /// </summary>
    [Display(Name = "当前时区", GroupName = "系统信息", Description = "当前系统时区信息", Order = 7)]
    public string TimeZone { get; } = TimeZoneInfo.Local.DisplayName;

    /// <summary>
    /// 框架说明
    /// </summary>
    [Display(Name = "框架说明", GroupName = "运行时信息", Description = "当前.NET运行时框架说明", Order = 8)]
    public string FrameworkDescription { get; } = RuntimeInformation.FrameworkDescription;

    /// <summary>
    /// 运行时标识
    /// </summary>
    [Display(Name = "运行时标识", GroupName = "运行时信息", Description = "当前.NET运行时标识", Order = 9)]
    public string RuntimeIdentifier { get; } = RuntimeInformation.RuntimeIdentifier;

    /// <summary>
    /// 进程架构（<see cref="Architecture">）
    /// </summary>
    [Display(Name = "进程架构", GroupName = "运行时信息", Description = "当前进程的架构信息", Order = 10)]
    public string ProcessArchitecture { get; } = RuntimeInformation.ProcessArchitecture.ToString();

    /// <summary>
    /// 是否为64位进程
    /// </summary>
    [Display(Name = "是否为64位进程", GroupName = "运行时信息", Description = "判断当前进程是否为64位", Order = 11)]
    public bool Is64BitProcess { get; } = Environment.Is64BitProcess;

    /// <summary>
    /// 处理器数量
    /// </summary>
    [Display(Name = "处理器数量", GroupName = "硬件信息", Description = "当前计算机的逻辑处理器数量", Order = 12)]
    public int ProcessorCount { get; } = Environment.ProcessorCount;

    /// <summary>
    /// 处理器架构
    /// </summary>
    [Display(Name = "处理器架构", GroupName = "硬件信息", Description = "处理器架构环境变量信息", Order = 13)]
    public string ProcessorArchitecture { get; } = GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");

    /// <summary>
    /// 处理器标识
    /// </summary>
    [Display(Name = "处理器标识", GroupName = "硬件信息", Description = "处理器标识环境变量信息", Order = 14)]
    public string ProcessorIdentifier { get; } = GetEnvironmentVariable("PROCESSOR_IDENTIFIER");

    /// <summary>
    /// 系统页大小
    /// </summary>
    [Display(Name = "系统页大小", GroupName = "硬件信息", Description = "操作系统内存页大小", Order = 15)]
    public string SystemPageSize { get; } = FormatBytes(Environment.SystemPageSize);

    /// <summary>
    /// 物理内存总量
    /// </summary>
    [Display(Name = "物理内存总量", GroupName = "硬件信息", Description = "当前计算机的物理内存总量", Order = 16)]
    public string TotalPhysicalMemory { get; } = FormatBytes(GetMemoryStatus()?.ullTotalPhys);

    /// <summary>
    /// 可用物理内存
    /// </summary>
    [Display(Name = "可用物理内存", GroupName = "硬件信息", Description = "当前计算机的可用物理内存", Order = 17)]
    public string AvailablePhysicalMemory { get; } = FormatBytes(GetMemoryStatus()?.ullAvailPhys);

    /// <summary>
    /// 计算机名称
    /// </summary>
    [Display(Name = "计算机名称", GroupName = "用户信息", Description = "计算机的名称", Order = 18)]
    public string ComputerName { get; } = Environment.MachineName;

    /// <summary>
    /// 计算机用户
    /// </summary>
    [Display(Name = "计算机用户", GroupName = "用户信息", Description = "当前登录的计算机用户", Order = 19)]
    public string UserName { get; set; } = Environment.UserName;

    /// <summary>
    /// 用户域名
    /// </summary>
    [Display(Name = "用户域名", GroupName = "用户信息", Description = "当前登录用户的域名", Order = 20)]
    public string UserDomainName { get; } = Environment.UserDomainName;

    /// <summary>
    /// 是否交互用户
    /// </summary>
    [Display(Name = "是否交互用户", GroupName = "用户信息", Description = "判断当前进程是否运行在用户交互模式", Order = 21)]
    public bool UserInteractive { get; } = Environment.UserInteractive;

    /// <summary>
    /// 是否管理员
    /// </summary>
    [Display(Name = "是否管理员", GroupName = "用户信息", Description = "判断当前用户是否拥有管理员权限", Order = 22)]
    public bool IsAdministrator { get; } = GetIsAdministrator();

    /// <summary>
    /// 用户目录
    /// </summary>
    [Display(Name = "用户目录", GroupName = "路径信息", Description = "当前用户目录", Order = 23)]
    public string UserProfile { get; } = GetEnvironmentVariable("USERPROFILE");

    /// <summary>
    /// 桌面目录
    /// </summary>
    [Display(Name = "桌面目录", GroupName = "路径信息", Description = "当前用户桌面目录", Order = 24)]
    public string DesktopDirectory { get; } = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

    /// <summary>
    /// 当前目录
    /// </summary>
    [Display(Name = "当前目录", GroupName = "路径信息", Description = "当前进程的工作目录", Order = 25)]
    public string CurrentDirectory { get; } = Environment.CurrentDirectory;

    /// <summary>
    /// 逻辑驱动器
    /// </summary>
    [Display(Name = "逻辑驱动器", GroupName = "路径信息", Description = "当前计算机的逻辑驱动器列表", Order = 26)]
    public string LogicalDrives { get; } = string.Join("; ", Environment.GetLogicalDrives());

    private static string GetEnvironmentVariable(string variable)
    {
        return Environment.GetEnvironmentVariable(variable) ?? string.Empty;
    }

    private static string FormatBytes(long bytes)
    {
        return FormatBytes((ulong)bytes);
    }

    private static string FormatBytes(ulong? bytes)
    {
        if (bytes is null)
            return string.Empty;

        string[] units = { "B", "KB", "MB", "GB", "TB" };
        double value = bytes.Value;
        int unit = 0;

        while (value >= 1024 && unit < units.Length - 1)
        {
            value /= 1024;
            unit++;
        }

        return $"{value:0.##} {units[unit]}";
    }

    private static bool GetIsAdministrator()
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    private static MemoryStatus? GetMemoryStatus()
    {
        if (!OperatingSystem.IsWindows())
            return null;

        MemoryStatus status = new();
        return GlobalMemoryStatusEx(status) ? status : null;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GlobalMemoryStatusEx([In, Out] MemoryStatus lpBuffer);

    [StructLayout(LayoutKind.Sequential)]
    private class MemoryStatus
    {
        public uint dwLength = (uint)Marshal.SizeOf<MemoryStatus>();
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }
}
