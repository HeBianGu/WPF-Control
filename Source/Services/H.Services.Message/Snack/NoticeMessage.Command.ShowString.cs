// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Snack;

public class ShowStringSnackMessageCommand : ShowSnackMessageCommandBase
{
    public override void Execute(object parameter)
    {
        Func<ISnackItem, bool> action = x =>
        {
            for (int i = 0; i < 100; i++)
            {
                x.Message = $"{i}/100";
                Thread.Sleep(20);
            }
            x.Message = $"100/100";
            Thread.Sleep(1000);
            return true;
        };
        Ioc<ISnackMessageService>.Instance.ShowString(action);
    }
}
