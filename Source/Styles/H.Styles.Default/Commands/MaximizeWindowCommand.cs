// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace H.Styles.Default
{
    public class MaximizeWindowCommand : WindowCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is Window window)
                SystemCommands.MaximizeWindow(window);
        }
    }
}
