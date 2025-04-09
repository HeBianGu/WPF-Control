// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Extensions.FontIcon;

namespace H.Styles.Commands;

[Icon(FontIcons.ChromeMinimize)]
[Display(Name = "最小化", Description = "点击此按钮将当前窗口最小化")]
public class MinimizeWindowCommand : WindowCommandBase
{
    public override void Execute(object parameter)
    {
        if (parameter is Window window)
            SystemCommands.MinimizeWindow(window);
    }
}
