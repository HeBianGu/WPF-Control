// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using static System.Environment;

namespace H.Extensions.Computer.ComputerSpecialFolders;

[Display(Name = "我的文档", Description = "显示我的文档", GroupName = "电脑特殊文件夹", Order = 0, ShortName = "My Documents", Prompt = "显示我的文档")]
[SpecialFolderInfo(SpecialFolder.MyDocuments)]
public class ShowMyDocumentsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "桌面", Description = "显示桌面", GroupName = "电脑特殊文件夹", Order = 1, ShortName = "Desktop", Prompt = "显示桌面")]
[SpecialFolderInfo(SpecialFolder.Desktop)]
public class ShowDesktopCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "程序", Description = "显示程序", GroupName = "电脑特殊文件夹", Order = 2, ShortName = "Programs", Prompt = "显示程序")]
[SpecialFolderInfo(SpecialFolder.Programs)]
public class ShowProgramsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "个人", Description = "显示个人文件夹", GroupName = "电脑特殊文件夹", Order = 3, ShortName = "Personal", Prompt = "显示个人文件夹")]
[SpecialFolderInfo(SpecialFolder.Personal)]
public class ShowPersonalCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "收藏夹", Description = "显示收藏夹", GroupName = "电脑特殊文件夹", Order = 4, ShortName = "Favorites", Prompt = "显示收藏夹")]
[SpecialFolderInfo(SpecialFolder.Favorites)]
public class ShowFavoritesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "启动", Description = "显示启动文件夹", GroupName = "电脑特殊文件夹", Order = 5, ShortName = "Startup", Prompt = "显示启动文件夹")]
[SpecialFolderInfo(SpecialFolder.Startup)]
public class ShowStartupCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "最近使用", Description = "显示最近使用", GroupName = "电脑特殊文件夹", Order = 6, ShortName = "Recent", Prompt = "显示最近使用")]
[SpecialFolderInfo(SpecialFolder.Recent)]
public class ShowRecentCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "发送到", Description = "显示发送到文件夹", GroupName = "电脑特殊文件夹", Order = 7, ShortName = "Send To", Prompt = "显示发送到文件夹")]
[SpecialFolderInfo(SpecialFolder.SendTo)]
public class ShowSendToCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "开始菜单", Description = "显示开始菜单", GroupName = "电脑特殊文件夹", Order = 8, ShortName = "Start Menu", Prompt = "显示开始菜单")]
[SpecialFolderInfo(SpecialFolder.StartMenu)]
public class ShowStartMenuCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "我的音乐", Description = "显示我的音乐", GroupName = "电脑特殊文件夹", Order = 9, ShortName = "My Music", Prompt = "显示我的音乐")]
[SpecialFolderInfo(SpecialFolder.MyMusic)]
public class ShowMyMusicCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "我的视频", Description = "显示我的视频", GroupName = "电脑特殊文件夹", Order = 10, ShortName = "My Videos", Prompt = "显示我的视频")]
[SpecialFolderInfo(SpecialFolder.MyVideos)]
public class ShowMyVideosCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "桌面目录", Description = "显示桌面目录", GroupName = "电脑特殊文件夹", Order = 11, ShortName = "Desktop Directory", Prompt = "显示桌面目录")]
[SpecialFolderInfo(SpecialFolder.DesktopDirectory)]
public class ShowDesktopDirectoryCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "我的电脑", Description = "显示我的电脑", GroupName = "电脑特殊文件夹", Order = 12, ShortName = "My Computer", Prompt = "显示我的电脑")]
[SpecialFolderInfo(SpecialFolder.MyComputer)]
public class ShowMyComputerCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "网络快捷方式", Description = "显示网络快捷方式", GroupName = "电脑特殊文件夹", Order = 13, ShortName = "Network Shortcuts", Prompt = "显示网络快捷方式")]
[SpecialFolderInfo(SpecialFolder.NetworkShortcuts)]
public class ShowNetworkShortcutsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "字体", Description = "显示字体", GroupName = "电脑特殊文件夹", Order = 14, ShortName = "Fonts", Prompt = "显示字体")]
[SpecialFolderInfo(SpecialFolder.Fonts)]
public class ShowFontsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "模板", Description = "显示模板", GroupName = "电脑特殊文件夹", Order = 15, ShortName = "Templates", Prompt = "显示模板")]
[SpecialFolderInfo(SpecialFolder.Templates)]
public class ShowTemplatesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共开始菜单", Description = "显示公共开始菜单", GroupName = "电脑特殊文件夹", Order = 16, ShortName = "Common Start Menu", Prompt = "显示公共开始菜单")]
[SpecialFolderInfo(SpecialFolder.CommonStartMenu)]
public class ShowCommonStartMenuCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共程序", Description = "显示公共程序", GroupName = "电脑特殊文件夹", Order = 17, ShortName = "Common Programs", Prompt = "显示公共程序")]
[SpecialFolderInfo(SpecialFolder.CommonPrograms)]
public class ShowCommonProgramsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共启动", Description = "显示公共启动", GroupName = "电脑特殊文件夹", Order = 18, ShortName = "Common Startup", Prompt = "显示公共启动")]
[SpecialFolderInfo(SpecialFolder.CommonStartup)]
public class ShowCommonStartupCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共桌面", Description = "显示公共桌面", GroupName = "电脑特殊文件夹", Order = 19, ShortName = "Common Desktop", Prompt = "显示公共桌面")]
[SpecialFolderInfo(SpecialFolder.CommonDesktopDirectory)]
public class ShowCommonDesktopDirectoryCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "应用数据", Description = "显示应用数据", GroupName = "电脑特殊文件夹", Order = 20, ShortName = "Application Data", Prompt = "显示应用数据")]
[SpecialFolderInfo(SpecialFolder.ApplicationData)]
public class ShowApplicationDataCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "打印机快捷方式", Description = "显示打印机快捷方式", GroupName = "电脑特殊文件夹", Order = 21, ShortName = "Printer Shortcuts", Prompt = "显示打印机快捷方式")]
[SpecialFolderInfo(SpecialFolder.PrinterShortcuts)]
public class ShowPrinterShortcutsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "本地应用数据", Description = "显示本地应用数据", GroupName = "电脑特殊文件夹", Order = 22, ShortName = "Local Application Data", Prompt = "显示本地应用数据")]
[SpecialFolderInfo(SpecialFolder.LocalApplicationData)]
public class ShowLocalApplicationDataCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "Internet 缓存", Description = "显示 Internet 缓存", GroupName = "电脑特殊文件夹", Order = 23, ShortName = "Internet Cache", Prompt = "显示 Internet 缓存")]
[SpecialFolderInfo(SpecialFolder.InternetCache)]
public class ShowInternetCacheCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "Cookies", Description = "显示 Cookies", GroupName = "电脑特殊文件夹", Order = 24, ShortName = "Cookies", Prompt = "显示 Cookies")]
[SpecialFolderInfo(SpecialFolder.Cookies)]
public class ShowCookiesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "历史记录", Description = "显示历史记录", GroupName = "电脑特殊文件夹", Order = 25, ShortName = "History", Prompt = "显示历史记录")]
[SpecialFolderInfo(SpecialFolder.History)]
public class ShowHistoryCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共应用数据", Description = "显示公共应用数据", GroupName = "电脑特殊文件夹", Order = 26, ShortName = "Common Application Data", Prompt = "显示公共应用数据")]
[SpecialFolderInfo(SpecialFolder.CommonApplicationData)]
public class ShowCommonApplicationDataCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "Windows", Description = "显示 Windows 目录", GroupName = "电脑特殊文件夹", Order = 27, ShortName = "Windows", Prompt = "显示 Windows 目录")]
[SpecialFolderInfo(SpecialFolder.Windows)]
public class ShowWindowsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "系统目录", Description = "显示系统目录", GroupName = "电脑特殊文件夹", Order = 28, ShortName = "System", Prompt = "显示系统目录")]
[SpecialFolderInfo(SpecialFolder.System)]
public class ShowSystemCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "Program Files", Description = "显示 Program Files", GroupName = "电脑特殊文件夹", Order = 29, ShortName = "Program Files", Prompt = "显示 Program Files")]
[SpecialFolderInfo(SpecialFolder.ProgramFiles)]
public class ShowProgramFilesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "我的图片", Description = "显示我的图片", GroupName = "电脑特殊文件夹", Order = 30, ShortName = "My Pictures", Prompt = "显示我的图片")]
[SpecialFolderInfo(SpecialFolder.MyPictures)]
public class ShowMyPicturesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "用户配置文件", Description = "显示用户配置文件", GroupName = "电脑特殊文件夹", Order = 31, ShortName = "User Profile", Prompt = "显示用户配置文件")]
[SpecialFolderInfo(SpecialFolder.UserProfile)]
public class ShowUserProfileCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "系统目录 X86", Description = "显示系统目录 X86", GroupName = "电脑特殊文件夹", Order = 32, ShortName = "System X86", Prompt = "显示系统目录 X86")]
[SpecialFolderInfo(SpecialFolder.SystemX86)]
public class ShowSystemX86Command : ShowSpecialFolderCommandBase
{

}

[Display(Name = "Program Files X86", Description = "显示 Program Files X86", GroupName = "电脑特殊文件夹", Order = 33, ShortName = "Program Files X86", Prompt = "显示 Program Files X86")]
[SpecialFolderInfo(SpecialFolder.ProgramFilesX86)]
public class ShowProgramFilesX86Command : ShowSpecialFolderCommandBase
{

}

[Display(Name = "Common Program Files", Description = "显示 Common Program Files", GroupName = "电脑特殊文件夹", Order = 34, ShortName = "Common Program Files", Prompt = "显示 Common Program Files")]
[SpecialFolderInfo(SpecialFolder.CommonProgramFiles)]
public class ShowCommonProgramFilesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "Common Program Files X86", Description = "显示 Common Program Files X86", GroupName = "电脑特殊文件夹", Order = 35, ShortName = "Common Program Files X86", Prompt = "显示 Common Program Files X86")]
[SpecialFolderInfo(SpecialFolder.CommonProgramFilesX86)]
public class ShowCommonProgramFilesX86Command : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共模板", Description = "显示公共模板", GroupName = "电脑特殊文件夹", Order = 36, ShortName = "Common Templates", Prompt = "显示公共模板")]
[SpecialFolderInfo(SpecialFolder.CommonTemplates)]
public class ShowCommonTemplatesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共文档", Description = "显示公共文档", GroupName = "电脑特殊文件夹", Order = 37, ShortName = "Common Documents", Prompt = "显示公共文档")]
[SpecialFolderInfo(SpecialFolder.CommonDocuments)]
public class ShowCommonDocumentsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共管理工具", Description = "显示公共管理工具", GroupName = "电脑特殊文件夹", Order = 38, ShortName = "Common Admin Tools", Prompt = "显示公共管理工具")]
[SpecialFolderInfo(SpecialFolder.CommonAdminTools)]
public class ShowCommonAdminToolsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "管理工具", Description = "显示管理工具", GroupName = "电脑特殊文件夹", Order = 39, ShortName = "Admin Tools", Prompt = "显示管理工具")]
[SpecialFolderInfo(SpecialFolder.AdminTools)]
public class ShowAdminToolsCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共音乐", Description = "显示公共音乐", GroupName = "电脑特殊文件夹", Order = 40, ShortName = "Common Music", Prompt = "显示公共音乐")]
[SpecialFolderInfo(SpecialFolder.CommonMusic)]
public class ShowCommonMusicCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共图片", Description = "显示公共图片", GroupName = "电脑特殊文件夹", Order = 41, ShortName = "Common Pictures", Prompt = "显示公共图片")]
[SpecialFolderInfo(SpecialFolder.CommonPictures)]
public class ShowCommonPicturesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "公共视频", Description = "显示公共视频", GroupName = "电脑特殊文件夹", Order = 42, ShortName = "Common Videos", Prompt = "显示公共视频")]
[SpecialFolderInfo(SpecialFolder.CommonVideos)]
public class ShowCommonVideosCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "资源", Description = "显示资源目录", GroupName = "电脑特殊文件夹", Order = 43, ShortName = "Resources", Prompt = "显示资源目录")]
[SpecialFolderInfo(SpecialFolder.Resources)]
public class ShowResourcesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "本地化资源", Description = "显示本地化资源目录", GroupName = "电脑特殊文件夹", Order = 44, ShortName = "Localized Resources", Prompt = "显示本地化资源目录")]
[SpecialFolderInfo(SpecialFolder.LocalizedResources)]
public class ShowLocalizedResourcesCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "OEM 链接", Description = "显示 OEM 链接", GroupName = "电脑特殊文件夹", Order = 45, ShortName = "Common OEM Links", Prompt = "显示 OEM 链接")]
[SpecialFolderInfo(SpecialFolder.CommonOemLinks)]
public class ShowCommonOemLinksCommand : ShowSpecialFolderCommandBase
{

}

[Display(Name = "CD 刻录", Description = "显示 CD 刻录目录", GroupName = "电脑特殊文件夹", Order = 46, ShortName = "CD Burning", Prompt = "显示 CD 刻录目录")]
[SpecialFolderInfo(SpecialFolder.CDBurning)]
public class ShowCDBurningCommand : ShowSpecialFolderCommandBase
{

}
