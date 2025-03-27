namespace H.Mvvm.Commands;

public class InvokeCommand : ICommand, INotifyPropertyChanged, IInvokeCommand
{
    protected Action<object> _action;
    protected readonly Predicate<object> _canExecute;
    protected Action<IInvokeCommand, object> _actionCommand;
    protected readonly Func<IInvokeCommand, object, bool> _canExecuteCommand;
    public InvokeCommand(Action<object> action)
    {
        _action = action;
    }

    public InvokeCommand(Action<IInvokeCommand, object> action)
    {
        _actionCommand = action;
    }

    public InvokeCommand(Action<object> execute, Predicate<object> canExecute) : this(execute)
    {
        _canExecute = canExecute ?? (x => true);
    }

    public InvokeCommand(Action<IInvokeCommand, object> execute, Func<IInvokeCommand, object, bool> canExecute) : this(execute)
    {
        _canExecuteCommand = canExecute ?? ((x, y) => true);
    }

    public bool CanExecute(object parameter)
    {
        if (_canExecute != null)
            return _canExecute(parameter);

        return _canExecuteCommand != null ? _canExecuteCommand(this, parameter) : true;
    }

    public event EventHandler CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
        }
        remove
        {
            CommandManager.RequerySuggested -= value;
        }
    }

    /// <summary> 执行命令 </summary>
    public virtual void Execute(object parameter)
    {
        //#if DEBUG
        //            if (!string.IsNullOrEmpty(this.Name))
        //                this.Logger?.Debug(this.Name);
        //#endif
        if (_action != null)
            _action(parameter);

        if (_actionCommand != null)
            //  Do ：应用async方式try会直接 finally IsBusy不起作用了
            //try
            //{
            //this.IsBusy = true;
            _actionCommand(this, parameter);
    }

    public static implicit operator InvokeCommand(Action<object> action)
    {
        return new InvokeCommand(action);
    }

    public static implicit operator InvokeCommand(Action<IInvokeCommand, object> action)
    {
        return new InvokeCommand(action);
    }

    #region - INotifyPropertyChanged -

    public string Name { get; set; }

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

    private string _groupName;
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public string GroupName
    {
        get { return _groupName; }
        set
        {
            _groupName = value;
            RaisePropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    /// <summary> 刷新命令可执行状态 (会调用CanExecute方法) </summary>
    public void Refresh()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
