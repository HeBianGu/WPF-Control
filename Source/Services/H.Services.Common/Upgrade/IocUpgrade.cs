// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Reflection;

namespace H.Services.Common.Upgrade;

public class IocUpgrade : Ioc<IUpgradeService>
{
    public static string UpgradeVersion { get; set; } = Instance?.UpgradeVersion;
    public static bool HasNewVersion { get; set; } = Instance?.CanUpgrade(out string message) != null;
    public static string CurrentVersion { get; set; } = Assembly.GetEntryAssembly().GetName().Version.ToString();
}