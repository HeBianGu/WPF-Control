// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.Commands.Base;

namespace H.Mvvm.Commands;

public class RelayCommand : CommandBase
{
    protected Action<object> _action;
    protected readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> action)
    {
        _action = action;
    }
    public RelayCommand(Action<object> execute, Predicate<object> canExecute) : this(execute)
    {
        _canExecute = canExecute;
    }

    public override void Execute(object parameter)
    {
        if (_action != null)
            _action(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return _canExecute == null ? true : _canExecute.Invoke(parameter);
    }
}
