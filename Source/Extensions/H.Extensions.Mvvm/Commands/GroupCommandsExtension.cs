// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Mvvm.Commands;

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
