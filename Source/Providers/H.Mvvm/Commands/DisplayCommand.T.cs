// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
namespace H.Mvvm.Commands;

public class DisplayCommand<T> : RelayCommand<T>, IDisplayCommand
{
    public DisplayCommand(Action<T> executeCommand) : base(executeCommand)
    {

    }

    public DisplayCommand(Action<T> executeCommand, Predicate<T> canExecuteCommand) : base(executeCommand, canExecuteCommand)
    {

    }
    public string Name { get; set; }
    public string Description { get; set; }
    public string GroupName { get; set; }
    public int Order { get; set; }
    public string Icon { get; set; }
}
