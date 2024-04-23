// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Mvvm;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Command
{

    public class ClipBoardPasteTextCommand : MarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return Clipboard.GetText() != null;
        }
        public override void Execute(object parameter)
        {
            if (parameter == null)
                return;
            var text = Clipboard.GetText();
            if (parameter is TextBox tb)
                tb.Text = text;
            if (parameter is ContentControl cc)
                cc.Content = text;
            if (parameter is TextBlock textBlock)
                textBlock.Text = text;
        }
    }
}
