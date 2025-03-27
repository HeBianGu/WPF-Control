namespace H.Mvvm.Commands;

public interface IInvokeCommand : ICommand
{
    string Name { get; set; }
    bool IsBusy { get; set; }
    bool IsEnabled { get; set; }
    bool IsIndeterminate { get; set; }
    bool IsVisible { get; set; }
    //ILogService Logger { get; }
    string Message { get; set; }
    double Percent { get; set; }
    string GroupName { get; set; }
}
