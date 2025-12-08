// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Mvvm.Commands;

public class GroupableCommands
{
    public GroupableCommands(ICommandsBindable commandsBindable)
    {
        this.MenuBarCommands = new ObservableCollection<IDisplayCommand>(this.CreateMenuBarCommands(commandsBindable.Commands));
        this.RightMenuCommands = new ObservableCollection<IDisplayCommand>(this.CreateRightMenuCommands(commandsBindable.Commands));
        this.ToolBarCommands = new ObservableCollection<IDisplayCommand>(this.CreateToolBarCommands(commandsBindable.Commands));
    }

    public ObservableCollection<IDisplayCommand> MenuBarCommands { get; private set; } = new ObservableCollection<IDisplayCommand>();

    public ObservableCollection<IDisplayCommand> RightMenuCommands { get; private set; } = new ObservableCollection<IDisplayCommand>();

    public ObservableCollection<IDisplayCommand> ToolBarCommands { get; } = new ObservableCollection<IDisplayCommand>();

    protected virtual IEnumerable<IDisplayCommand> CreateToolBarCommands(IEnumerable<ICommand> commands)
    {
        return commands.OfType<IDisplayCommand>().Where(x => x.GroupName.Split(',').Contains("工具栏")).OrderBy(x => x.Order);
    }

    protected virtual IEnumerable<IDisplayCommand> CreateMenuBarCommands(IEnumerable<ICommand> commands)
    {
        return commands.OfType<IDisplayCommand>().Where(x => x.GroupName.Split(',').Contains("菜单栏")).OrderBy(x => x.Order);
    }
    protected virtual IEnumerable<IDisplayCommand> CreateRightMenuCommands(IEnumerable<ICommand> commands)
    {
        return commands.OfType<IDisplayCommand>().Where(x => x.GroupName.Split(',').Contains("右键菜单")).OrderBy(x => x.Order);
    }
}
