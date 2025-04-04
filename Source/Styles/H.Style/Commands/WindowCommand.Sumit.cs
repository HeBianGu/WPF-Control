// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Styles.Commands;

public class SumitWindowCommand : WindowCommandBase
{
    public override void Execute(object parameter)
    {
        if (parameter is Window window)
        {
            window.DialogResult = true;
            SystemCommands.CloseWindow(window);
        }
    }
}
