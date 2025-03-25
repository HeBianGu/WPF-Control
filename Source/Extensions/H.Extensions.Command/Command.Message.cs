// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.Command;

/// <summary>
/// 带有绑定百分比的命令
/// </summary>
public class MessageCommand : BuzyCommand<MessageCommand>
{

    /// <summary> 执行命令 </summary>
    public MessageCommand(Action<MessageCommand, object> execute) : base(execute)
    {

    }

    /// <summary> 执行命令 </summary>
    public MessageCommand(Action<MessageCommand, object> execute, Predicate<object> canExecute) : base(execute, canExecute)
    {

    }


    public override async void Execute(object parameter)
    {
        this.IsBuzy = true;
        this.Message = "正在执行";
        this.Exception = null;
        await Task.Run(() =>
        {
            try
            {
                this.Action?.Invoke(this, parameter);
                this.Message = "执行成功";
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
                this.Exception = ex;
            }
            finally
            {
                this.IsBuzy = false;
            }
        });
    }
}
