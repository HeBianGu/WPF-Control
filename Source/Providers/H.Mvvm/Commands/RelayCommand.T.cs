namespace H.Mvvm.Commands;

public class RelayCommand<T> : ICommand
{
    public Action<T> ExecuteCommand { get; private set; }
    public Predicate<T> CanExecuteCommand { get; private set; }

    public RelayCommand(Action<T> executeCommand, Predicate<T> canExecuteCommand)
    {
        this.ExecuteCommand = executeCommand;
        this.CanExecuteCommand = canExecuteCommand;
    }

    public RelayCommand(Action<T> executeCommand)
        : this(executeCommand, null) { }

    public void Execute(object parameter)
    {
        if (this.ExecuteCommand != null) this.ExecuteCommand((T)parameter);
    }

    public bool CanExecute(object parameter)
    {
        return this.CanExecuteCommand == null || this.CanExecuteCommand((T)parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add { if (this.CanExecuteCommand != null) CommandManager.RequerySuggested += value; }
        remove { if (this.CanExecuteCommand != null) CommandManager.RequerySuggested -= value; }
    }
}
