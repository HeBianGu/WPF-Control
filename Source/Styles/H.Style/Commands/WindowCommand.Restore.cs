// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.FontIcon;

namespace H.Styles.Commands;

[Icon(FontIcons.ChromeRestore)]
[Display(Name = "还原", Description = "点击此按钮将当前窗口还原")]
public class RestoreWindowCommand : WindowCommandBase
{
    public override void Execute(object parameter)
    {
        if (parameter is Window window)
            SystemCommands.RestoreWindow(window);
    }
}
