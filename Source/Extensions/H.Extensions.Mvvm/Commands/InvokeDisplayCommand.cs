// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Mvvm.Commands;

public class InvokeDisplayCommand : DisplayCommand, IInvokeCommand
{
    protected Action<IInvokeCommand, object> _actionCommand;
    protected readonly Func<IInvokeCommand, object, bool> _canExecuteCommand;

    public InvokeDisplayCommand(Action<object> action) : base(action)
    {
    }
    public InvokeDisplayCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
    {
    }

    public InvokeDisplayCommand(Action<IInvokeCommand, object> action)
    {
        _actionCommand = action;
    }

    public InvokeDisplayCommand(Action<IInvokeCommand, object> execute, Func<IInvokeCommand, object, bool> canExecute) : this(execute)
    {
        _canExecuteCommand = canExecute ?? ((x, y) => true);
    }

    #region - INotifyPropertyChanged -


    private bool _isEnabled = true;
    public bool IsEnabled
    {
        get { return _isEnabled; }
        set
        {
            _isEnabled = value;
            RaisePropertyChanged();
        }
    }

    private bool _isVisible = true;
    public bool IsVisible
    {
        get { return _isVisible; }
        set
        {
            _isVisible = value;
            RaisePropertyChanged();
        }
    }

    private bool _isIndeterminate = true;
    public bool IsIndeterminate
    {
        get { return _isIndeterminate; }
        set
        {
            _isIndeterminate = value;
            RaisePropertyChanged();
        }
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get { return _isBusy; }
        set
        {
            _isBusy = value;
            RaisePropertyChanged();
        }
    }

    private double _percent;
    public double Percent
    {
        get { return _percent; }
        set
        {
            _percent = value;
            RaisePropertyChanged();
        }
    }

    private string _message = "正在运行";
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged();
        }
    }

    #endregion

    public override bool CanExecute(object parameter)
    {
        if (_canExecute != null)
            return _canExecute(parameter);
        return _canExecuteCommand != null ? _canExecuteCommand(this, parameter) : true;
    }

    public override void Execute(object parameter)
    {
        if (_action != null)
            _action(parameter);
        if (_actionCommand != null)
            _actionCommand(this, parameter);
    }
}
