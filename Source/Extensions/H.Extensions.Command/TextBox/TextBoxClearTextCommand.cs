// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Mvvm;
using System.Windows.Controls;

namespace H.Extensions.Command
{
    public class TextBoxClearTextCommand : MarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is TextBox tb && !string.IsNullOrEmpty(tb.Text);
        }
        public override void Execute(object parameter)
        {
            if (parameter is TextBox tb)
            {
                tb.Text = null;
            }
        }
    }
}
