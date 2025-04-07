// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.FontIcon;

namespace H.Styles.Commands;

[Icon(FontIcons.ChromeMaximize)]
[Display(Name = "最大化", Description = "点击此按钮将当前窗口最大化")]
public class MaximizeWindowCommand : WindowCommandBase
{
    public override void Execute(object parameter)
    {
        if (parameter is Window window)
            SystemCommands.MaximizeWindow(window);
    }
}
