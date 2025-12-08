// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ValueConverter;
using System.Collections.Generic;

namespace H.Extensions.Mvvm.Commands;

public class GetGroupCommandsValueConverter : MarkupValueConverterBase
{
    public string GroupName { get; set; } = CommandGroupNames.MenuBar;
    public string ExceptGroupName { get; set; }
    public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is ICommandsBindable bindable)
            return this.CreateCommands(bindable.Commands);
        if (value is IEnumerable<ICommand> commands)
            return this.CreateCommands(commands);
        return DependencyProperty.UnsetValue;
    }

    protected virtual IEnumerable<IDisplayCommand> CreateCommands(IEnumerable<ICommand> commands)
    {
        return commands.GetGroupCommands(this.GroupName).GetExceptGroupCommands(this.ExceptGroupName);
    }
}

public class CommandGroupNames
{
    public const string MenuBar = "菜单栏";
    public const string ToolBar = "工具栏";
    public const string ContextMenu = "右键菜单";
    public const string StatusBar = "状态栏";
    public const string QuickAccessToolbar = "快速访问工具栏";
    public const string Menu = $"{MenuBar},{ContextMenu}";
    public const string Tools = $"{MenuBar},{ContextMenu},{ToolBar}";
    //public const string Ribbon = "功能区";
    //public const string MainMenu = "主菜单";
    //public const string NavigationBar = "导航栏";
    //public const string SideBar = "侧边栏";
    //public const string FooterBar = "页脚栏";
    //public const string HeaderBar = "页眉栏";
    //public const string TitleBar = "标题栏";
    //public const string ActionBar = "操作栏";
    //public const string CommandPalette = "命令面板";
    //public const string NotificationArea = "通知区域";
    //public const string SearchBar = "搜索栏";
    //public const string FilterBar = "过滤栏";
    //public const string SortBar = "排序栏";
    //public const string NavigationMenu = "导航菜单";
    //public const string UserMenu = "用户菜单";
    //public const string SettingsMenu = "设置菜单";
    //public const string HelpMenu = "帮助菜单";
    //public const string FileMenu = "文件菜单";
    //public const string EditMenu = "编辑菜单";
    //public const string ViewMenu = "视图菜单";
    //public const string ToolsMenu = "工具菜单";
    //public const string WindowMenu = "窗口菜单";
}

public static class GroupCommandsExtension
{
    public static IEnumerable<IDisplayCommand> GetGroupCommands(this IEnumerable<ICommand> commands, string groupName = CommandGroupNames.MenuBar)
    {
        return commands.OfType<IDisplayCommand>().Where(x => x.GroupName?.Split(',').Contains(groupName) == true).OrderBy(x => x.Order);
    }
    public static IEnumerable<IDisplayCommand> GetExceptGroupCommands(this IEnumerable<ICommand> commands, string exceptGroupName)
    {
        if (exceptGroupName == null)
            return commands.OfType<IDisplayCommand>().Where(x => x.GroupName != null);
        return commands.OfType<IDisplayCommand>().Where(x => x.GroupName?.Split(',').Contains(exceptGroupName) == false);
    }
}
