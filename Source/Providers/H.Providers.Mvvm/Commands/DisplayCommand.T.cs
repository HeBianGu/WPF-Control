// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Providers.Mvvm

{
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
    }
}
