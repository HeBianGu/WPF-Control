// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Threading.Tasks;
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
