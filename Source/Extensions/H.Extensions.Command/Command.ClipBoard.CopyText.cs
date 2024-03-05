// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Mvvm;
using System.Windows;

namespace H.Extensions.Command
{
    public class ClipBoardCopyTextCommand : MarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter != null;
        }
        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;
            try
            {
                Clipboard.SetText(parameter.ToString());
            }
            catch
            {

            }
        }
    }
}
